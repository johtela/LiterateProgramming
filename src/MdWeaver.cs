/*
# Markdown Weaver

Now we can look at how markdown output is generated. This is the easier of the 
two use cases. We only need to split a source file into comments and code and 
write them verbatim to an output file. This is done mostly in the 
[BlockBuilder](BlockBuilder.html) class. The markdown files can be copied directly 
to the output directory.
*/
namespace LiterateProgramming
{
	using CSWeave.Theme;
	using Microsoft.CodeAnalysis;
	using System.IO;

	public class MdWeaver : Weaver
	{
		public MdWeaver (Options options)
			: base (options)
		{ }
		/*
		## Generating Output
		
		The first method to be implemented is to generate documentation	from 
		an input directory. We use the inherited function to enumerate input
		files and based on their types we either transform them or just copy 
		them to the output folder.
		*/
		protected override void GenerateFromFiles ()
		{
			foreach (var inputFile in InputFiles ())
				if (IsSourceFile (inputFile))
					GenerateMarkdownFromCode (inputFile);
				else
					CopyFile (inputFile);
		}
		/*
		The second options is to use a solution as an input. In this case we 
		iterate markdown and C# source files separately, copying the MD files
		and transforming the C# files.
		*/
		protected override void GenerateFromSolution ()
		{ 
			var solutionFile = MSBuildHelpers.OpenSolution (_options.Solution);
			foreach (var inputFile in MarkdownFilesInSolution (solutionFile))
				CopyFile (inputFile);
			foreach (var doc in CSharpDocumentsInSolution (solutionFile))
				GenerateMarkdownFromCSharpDocument (doc.Item1, doc.Item2);
		}
		/*
		### Helper Functions

		The rest of the code in this class consist of helper functions which are 
		called by the main generation methods. The function below copies an input 
		file to the output directory.
		*/
		private void CopyFile (SplitPath inputFile)
		{
			var outputFile = _options.OutputPath + inputFile;
			ConsoleOut ("Copying from file '{0}' into folder '{1}'",
				inputFile.FilePath, outputFile.FilePath);
			DirHelpers.Copy (!inputFile, !outputFile, true);
		}
		/*
		The methods below construct the block list for a source file and write them 
		into an output file. Since the blocks already contain valid markdown, no 
		further processing is needed.
		*/
		private void GenerateMarkdownFromCode (SplitPath codeFile)
		{
			SplitPath outputFile = CreateOutputPath (codeFile, _options.MarkdownExt);
			OutputBlocks (CreateBlockList (!codeFile), !outputFile);
		}

		private void GenerateMarkdownFromCSharpDocument (SplitPath codeFile, Document document)
		{
			SplitPath outputFile = CreateOutputPath (codeFile, _options.MarkdownExt);
			OutputBlocks (CreateBlockList (document), !outputFile);
		}

		private void OutputBlocks (BlockList blocks, string outputFile)
		{
			using (var writer = File.CreateText (outputFile))
				foreach (var block in blocks)
					writer.Write (block.Contents);
		}
	}
}