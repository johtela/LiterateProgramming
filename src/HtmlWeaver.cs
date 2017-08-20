/*
# HTML Weaver

On the file level, weaving source files into web pages resembles the markdown 
generation process. The biggest difference is that HTML weaver has to manage 
the table of contents when working with input files. Also, a few helper classes 
are used to produce the actual HTML output: 

* [HtmlBlockBuilder](HtmlBlockBuilder.html) which decorates the code blocks
  with styling information.
* [HtmlGenerator](HtmlGenerator.html) which converts the markdown to HTML, and loads 
  the theme assembly, which in turn renders the HTML into a fully functional website.
  
But first, let's focus on how the source files are processed when HTML output is
selected.
*/
namespace LiterateProgramming
{
	using Microsoft.CodeAnalysis;
	/*
	## Initialization

	Constructor loads the TOC manager and creates the HTML generator.
	*/
	public class HtmlWeaver : Weaver
	{
		private HtmlGenerator _generateHtml;

		public HtmlWeaver (Options options)
			: base (options) 
		{
			LoadToc ();
			_generateHtml = new HtmlGenerator (_options, _tocManager.Toc);
		}
		/*
		A customized version of BlockBuilder is employed by overriding the virtual
		method that creates it.
		*/
		protected override BlockBuilder CreateBlockBuilder ()
		{
			return new HtmlBlockBuilder (_options);
		}
		/*
		Generation methods process the input files and update the TOC while 
		looping through them. Finally they copy the auxiliary files (CSS, 
		Javascript, fonts, etc.) that are required by the generated HTML 
		files. Auxiliary files reside in a theme assembly, and themes are
		responsible for copying them to the output folder. The weaver just 
		invokes the copy method through the HTML generator object.
		*/
		protected override void GenerateFromFiles ()
		{
			foreach (var inputFile in InputFiles ())
			{
				if (IsSourceFile (inputFile))
					GenerateHtmlFromCodeFile (inputFile);
				else
					GenerateHtmlFromMarkdown (inputFile);
				AddToToc (inputFile);
			}
			ConsoleOut ("Copying auxiliary HTML files...");
			_generateHtml.CopyAuxiliaryFiles ();
			_tocManager.Save ();
		}

		protected override void GenerateFromSolution ()
		{
			var solution = MSBuildHelpers.OpenSolution (_options.Solution);
			foreach (var inputFile in MarkdownFilesInSolution (solution))
			{
				GenerateHtmlFromMarkdown (inputFile);
				AddToToc (inputFile);
			}
			foreach (var doc in CSharpDocumentsInSolution (solution))
			{
				GenerateHtmlFromCSharpDocument (doc.Item1, doc.Item2);
				AddToToc (doc.Item1);
			}
			ConsoleOut ("Copying auxiliary HTML files...");
			_generateHtml.CopyAuxiliaryFiles ();
			_tocManager.Save ();
		}
		/*
		The rest of the methods are helper functions that process a single file 
		at a time; first they split it to blocks, and then call HTMLGenerator 
		to convert it to a HTML page.
		*/
		private void GenerateHtmlFromCodeFile (SplitPath codeFile)
		{
			_generateHtml.FromCode (CreateBlockList (!codeFile), codeFile, 
				CreateOutputPath (codeFile, ".html"));
		}

		private void GenerateHtmlFromCSharpDocument (SplitPath codeFile, Document document)
		{
			_generateHtml.FromCode (CreateBlockList (document), codeFile, 
				CreateOutputPath (codeFile, ".html"));
		}

		private void GenerateHtmlFromMarkdown (SplitPath mdFile)
		{
			_generateHtml.FromMarkdown (mdFile, CreateOutputPath (mdFile, ".html"));
		}
	}
}