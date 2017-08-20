/*
# Document Weaver

The main work horse of the application is the weaver. It performs multiple
tasks:

* Selecting the list of source files as specified by command line options.
* Generating documentation for each source file.
* Updating the table of contents file, if necessary.

The Weaver class is abstract, and the actual documentation generation is
delegated to its subclasses. There are two subclasses for the two output
formats: [MdWeaver](MdWeaver.html) for markdown and [HtmlWeaver](HtmlWeaver.html) 
for HTML.

Let's first look at the base class which contains code common to both weavers.

## Dependencies

We utilize a couple of libraries provided by Microsoft to access MSBuild 
solution and project files. The first one resides in the `Microsoft.Build.*`
assemblies. They contain classes and methods for manipulating MS Build solution
files; to open a solution, enumerate the files in it, get the build 
configurations, and so on. 

The other important dependency is the [Roslyn](https://github.com/dotnet/roslyn)
compiler platform. The assemblies related to that lay under the `Microsoft.CodeAnalysis.*`
namespace. Weaver needs to reference the namespaces to access their types, but 
doesn't really use them. More involved code related to Roslyn and solution compilation 
is separated to the helper class [MSBuildHelpers](MSBuildHelpers.html). Weaver tries 
to keep things simple by working on the higher abstraction level.
*/
namespace LiterateProgramming
{
	using CSWeave.Theme;
	using Microsoft.Build.Construction;
	using Microsoft.CodeAnalysis;
	using Microsoft.CodeAnalysis.MSBuild;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;

	public abstract class Weaver
	{
		/*
		## Index and Table of Contents Files
		
		There are two special input files which will be processed separately. The
		first one is the index file which can be either a C# file or  a markdown file. 
		Its	file name without extension has to be "Index". This file will be processed
		before any of the other files, so all the global (project-level) properties 
		should be defined in its front matter. See [Front Matter](../FrontMatter.html) 
		to learn how to add a front matter to a file.
		*/
		private const string _indexFile = "index";
		/*
		The other special file is the table of contents or _TOC_ for short. It is 
		defined in [YAML](http://yaml.org/) format and it always has the same file 
		name `TOC.yml`. The structure of the TOC file is described on 
		[another page](../TableOfContents.html). You can add entries to TOC manually, 
		or use the `-u` option to automatically update it when files are missing from it.
		*/
		private const string _tocFile = "TOC.yml";
		/*
		## Creating a Weaver

		When creating a weaver the selected command line options must be passed to it. 
		An optional reference to [TocManager](TocManager.html) object is also stored. 
		It is initialized by the subclasses when they need it.
		*/
		protected Options _options;
		protected TocManager _tocManager;

		public Weaver (Options options)
		{
			_options = options;
		}
		/*
		## Generating Documentation

		Weaver can generate documentation from two sources: from a directory or from a 
		solution. One of the abstract methods below is invoked when the tool is run. Which
		one is selected depends on the command line options. 
		*/
		protected abstract void GenerateFromFiles ();
		protected abstract void GenerateFromSolution ();
		/*
		Another method which the subclasses can override is the CreateBlockBuilder that is
		defined	below. This	method will create the [BlockBuilder](BlockBuilder.html) class 
		that is responsible for splitting the input files into blocks. The standard 
		implementation is sufficient for generating markdown output, but 
		[HtmlWeaver](HtmlWeaver.html) requires more sophisticated functionality provided by 
		the [HtmlBlockBuilder](HtmlBlockBuilder.html) class.
		*/
		protected virtual BlockBuilder CreateBlockBuilder ()
		{
			return new BlockBuilder (_options);
		}
		/*
		### Main Execution Method
		
		Main program will call the method below to run the document generation. The method
		determines whether the files are read from a solution or from an input folder. Based
		on the options it will call the appropriate virtual method. 
		*/
		public void GenerateDocumentation ()
		{
			if (_options.Solution != null)
			{
				ConsoleOut ("Creating documentation for solution: {0}", 
					_options.Solution);
				GenerateFromSolution ();
			}
			else
			{
				ConsoleOut ("Creating documentation for files in directory: {0}", 
					_options.InputFolder);
				GenerateFromFiles ();
			}
			ConsoleOut ("Done.");
		}
		/*
		### Verbose Output

		If verbose mode is active (`-v` option), information about the progress of
		the tool is outputted to the console.
		*/
		protected void ConsoleOut (string text, params object[] args)
		{
			if (_options.Verbose)
				Console.WriteLine (text, args);
		}
		/*
		### Output Path for a File

		For each input file we need to construct the output path. We take the base 
		path from command line options and join it with the relative file path of 
		the input file changing the extension simultaneously. We also check that the 
		output directory exists, and create it when needed.
		*/
		protected SplitPath CreateOutputPath (SplitPath codeFile, string extension)
		{
			var outputFile = _options.OutputPath + codeFile.ChangeExtension (extension);
			ConsoleOut ("Generating {0} from file '{1}' into file '{2}'",
				extension, codeFile.FilePath, outputFile.FilePath);
			DirHelpers.EnsureExists (outputFile.DirectoryName);
			return outputFile;
		}
		/*
		## Selecting Input Files

		The subclasses can use the following methods to retrieve paths of he input 
		files. The methods use LINQ to enumerate the SplitPath structures that refer
		to files in a directory or in a solution.

		### Enumerating Files in Input Folder

		First variant returns all the files in the input directory that match the 
		specified filters. The filters are regular expressions that are constructed 
		from the glob patterns by the methods shown below.
		*/
		protected Regex[] FilterRegexes ()
		{
			return (from filt in _options.Filters
					select new Regex (WildcardToRegex (filt)))
				   .ToArray ();
		}

		protected static string WildcardToRegex (string pattern) =>
			"^" + Regex.Escape (pattern)
					.Replace (@"\*\*", ".*")
					.Replace (@"\*", "[^\\\\/]*")
					.Replace (@"\?", ".")
				  + "$";
		/*
		Now we can enumerate the input files.
		*/
		protected IEnumerable<SplitPath> InputFiles ()
		{
			var filtRegexes = FilterRegexes ();
			var files = from file in DirHelpers.Dir (_options.InputPath.BasePath, "*",
							_options.Recursive)
						let relPath = SplitPath.Split (_options.InputPath.BasePath, file)
						where filtRegexes.Any (re => re.IsMatch (relPath.FilePath))
						select relPath;
			return IndicesFirst (files);
		}
		/*
		The index file needs to be processed first. The following method takes an 
		enumeration of SplitPath structures and picks the index file from it, and 
		moves it in front of the enumeration.
		*/
		protected IEnumerable<SplitPath> IndicesFirst (IEnumerable<SplitPath> files)
		{
			var inds = files.Where (IsIndexFile);
			return inds.Concat (files.Except (inds));
		}
		/*
		### Enumerating Files in Solution

		When a solution file is used as an input, markdown and C# files are retrieved
		separately. This is because the markdown files are not visible in MSBuild 
		Workspace provided by Roslyn. Apparently Roslyn is only interested in the C# 
		source files. We need the classes provided by the `Microsoft.Build.Construction` 
		library to access all files in a solution.

		First let's collect the markdown files from a solution file. Note that the LINQ
		query assumes the "Build Action" property of the retrieved files to be either 
		"Content" or "None". Files having other build types are skipped.
		*/
		protected IEnumerable<SplitPath> MarkdownFilesInSolution (SolutionFile solution)
		{
			var filtRegexes = FilterRegexes ();
			var files = from proj in solution.ProjectsInOrder
						where proj.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat
						let root = ProjectRootElement.Open (proj.AbsolutePath)
						from item in root.Items
						where item.ItemType.In ("Content", "None")
						let relPath = _options.InputPath.WithFile (item.Include)
						where IsMarkdownFile (relPath) &&
							filtRegexes.Any (re => re.IsMatch (relPath.FilePath))
						select relPath;
			return IndicesFirst (files);
		}
		/*
		And then we can gather the C# source files. Most of the heavy lifting is
		delegated to the MSBuildHelper class. In addition to the file path we
		return also the Document object that contains syntactic and semantic
		information that the Roslyn compiler attaches to the source file.
		*/
		protected IEnumerable<Tuple<SplitPath, Document>> CSharpDocumentsInSolution (
			SolutionFile solutionFile)
		{
			var workspace = MSBuildWorkspace.Create ();
			var solution = workspace.OpenSolutionAsync (_options.Solution).Result;
			var filtRegexes = FilterRegexes ();
			return from proj in MSBuildHelpers.LoadProjectsInSolution (solution, solutionFile)
				   from doc in proj.Documents
				   let relPath = SplitPath.Split (_options.InputPath.BasePath, doc.FilePath)
				   where filtRegexes.Any (re => re.IsMatch (relPath.FilePath))
				   select Tuple.Create (relPath, doc);
		}
		/*
		We need some helper functions to determine the type of a file. The type of a file
		is deduced from its extension or from its name. 
		*/
		protected bool IsSourceFile (SplitPath file) =>
			file.Extension == _options.SourceExt;

		protected bool IsMarkdownFile (SplitPath file) =>
			file.Extension == _options.MarkdownExt;

		protected bool IsIndexFile (SplitPath file) =>
			file.FileNameWithoutExtension.ToLower () == _indexFile;
		/*
		The following two helper function construct a block list from a source file or
		Roslyn document. They are used by the subclasses.
		*/
		protected BlockList CreateBlockList (string codeFile) =>
			CreateBlockBuilder ().Execute (codeFile);

		protected BlockList CreateBlockList (Document document) =>
			CreateBlockBuilder ().Execute (document);
		/*
		## TOC Management

		Loading and updating table of contents is handled by the 
		[TocManager](TocManager.html) class. The subclasses can initialize the
		TOC manager and add entries to TOC using the methods below. Updating
		is performed only, if `-u` switch is specified in the options.
		*/
		protected void LoadToc ()
		{
			var tocFile = _options.InputPath.WithFile (_tocFile);
			if (File.Exists (!tocFile))
			{
				ConsoleOut ("Reading TOC from file '{0}'", tocFile);
				_tocManager = TocManager.Load (!tocFile);
			}
			else
				_tocManager = new TocManager ();
		}

		protected void AddToToc (SplitPath path)
		{
			if (_options.UpdateToc)
				_tocManager.AddToToc (path);
		}
	}
}