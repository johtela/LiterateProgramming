/*
# Parsing a Syntax Tree into Blocks

A bit of parsing is required to extract a block list out of a source file. We
could use fairly simple regular expressions, for instance, to recognize the 
comments containing documentation. Instead, we utilize a more sophisticated tool:
the [Roslyn](https://github.com/dotnet/roslyn) compiler platform. Roslyn comprises
a set of libraries and APIs that expose all facets of the C# compiler: lexer, parser, 
type checker, code generator, and so on. This is the same compiler that is used by 
Visual Studio and other official Microsoft tools, so it should be always complete and 
up-to-date.

We need only the parser to separate documentation and code, but we can take 
advantage of other parts of Roslyn too. For example, we can use the semantic analysis
to distinguish which identifiers represent types and highlight them in the output 
as Visual Studio does. We can also get the type information of any expression
and show it in a tooltip window.

The semantic information is useful only in conjunction with HTML output, though. 
In markdown we cannot make use of it, mostly because the code blocks cannot be 
augmented with styling information. Even if there was a way to add metadata to 
code blocks, any of the markdown-to-HTML converters would not probably use it. 
Consequently, we defer utilizing the semantic information to the 
[HtmlBlockBuilder](HtmlBlockBuild.html) class which is used in context of HTML 
output.

So, let's tackle the parsing problem first, and see how it is performed by 
the BlockBuilder class.
*/
namespace LiterateProgramming
{
	using Microsoft.CodeAnalysis;
	using Microsoft.CodeAnalysis.CSharp;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;

	/*
	## Syntax Walker

	BlockBuilder inherits from [CSharpSyntaxWalker](http://www.coderesx.com/roslyn/html/B6833F66.htm)
	which implements the [visitor pattern](https://en.wikipedia.org/wiki/Visitor_pattern).
	It "walks" through an abstract syntax tree and calls a particular
	virtual method in the class based on the type of the node visited.
	Inheriting from the walker allows us to visit only specific types of 
	syntactic elements and ignore the ones we are not interested in.
	*/
	public class BlockBuilder : CSharpSyntaxWalker
	{
		/*
		We are extracting multi-line comment blocks, that is, comments
		starting with `/*`. To get the contents of a comment block, we need
		to strip the comment delimiters using a regular expression. The 
		following regex matches comment body.
		*/
		private Regex _commentBody = new Regex (@"/\*(.*?)\*/",
			RegexOptions.Singleline | RegexOptions.Compiled);
		/*
		While building the linked list of block, we need to maintain the latest 
		(current) block as well as the first block (head of the list).
		*/
		private BlockList _currentBlock;
		private BlockList _blocks;
		/*
		## Constructor

		We need the command line options to access the parameters controlling
		the parsing. The Options object is given as an argument to the constructor, 
		as usual.
		*/
		protected Options _options;

		public BlockBuilder (Options options)
			: base (SyntaxWalkerDepth.Token)
		{
			_options = options;
		}
		/*
		## Executing Block Builder

		The only public method in the class is Execute. It has too variants: 
		one that takes a file, and another that takes a MSBuild
		[document](http://www.coderesx.com/roslyn/html/5373B517.htm). The
		implementations are basically the same in both cases. They differ 
		only in how the AST is obtained.
		*/
		public virtual BlockList Execute (string codeFile)
		{
			var tree = CSharpSyntaxTree.ParseText (File.ReadAllText (codeFile));
			return CreateForTree (tree);
		}

		public virtual BlockList Execute (Document document)
		{
			var tree = document.GetSyntaxTreeAsync ().Result;
			return CreateForTree (tree);
		}
		/*
		Execute methods use the method below to initialize a new block list, 
		and to run the syntax walker by visiting the root of the tree. After
		all the blocks are created, the last block is closed, and the head of
		the list is returned.
		*/
		private BlockList CreateForTree (SyntaxTree tree)
		{
			_blocks = null;
			Visit (tree.GetRoot ());
			_currentBlock.Close (_options.Format);
			return _blocks;
		}
		/*
		Starting a new block involves closing the previous one, if it exists.
		*/
		private void StartNewBlock (BlockKind kind)
		{
			var result = new BlockList (kind, _options.Format);
			if (_blocks == null)
				_blocks = result;
			else
			{
				_currentBlock.Next = result;
				_currentBlock.Close (_options.Format);
			}
			_currentBlock = result;
		}
		/*
		The helper function below checks if the kind of the current block
		matches the kind of the block requested. If it does, the current
		block is returned; if not, a new block is started.
		*/
		private BlockList CurrentBlock (BlockKind kind)
		{
			if (_blocks == null || _currentBlock.Kind != kind)
				StartNewBlock (kind);
			return _currentBlock;
		}
		/*
		### Adding Documentation

		Appending a piece of markdown into a block would be very straightforward 
		if we wouldn't need to deal with whitespace. Usually the `--trim` option
		is used to remove the leading whitespace from the comments. This complicates
		the code a bit.
		*/
		protected void AppendMarkdown (string text)
		{
			if (_options.Trim)
			{
				/*
				Trimming starts with splitting the comment block into lines.
				We maintain the variable `firstNonWS` which contains the index
				of the first non-whitespace character in the comment block.
				Remember that we already removed the comment delimiters from
				the text block before calling this function.
				*/
				var firstNonWS = -1;
				foreach (var line in Regex.Split (text, "\r\n|\r|\n"))
				{
					if (firstNonWS < 0)
					{
						var i = line.TakeWhile (char.IsWhiteSpace).Count ();
						if (i < line.Length)
							firstNonWS = i;
					}
					/*
					If the index of the first non-whitespace character is found,
					we extract a substring of the line starting from the index. 
					If not, we add the whole line into the block.
					*/
					CurrentBlock (BlockKind.Markdown)._builder.AppendLine (
						firstNonWS < 0 || line.Length < firstNonWS ?
							line :
							line.Substring (firstNonWS));
				}
			}
			/*
			If the trimming option is not used then we add the whole comment 
			text as-is into the markdown block.
			*/
			else
				CurrentBlock (BlockKind.Markdown)._builder.Append (text);
		}
		/*
		### Adding Code

		Whitespace poses an additional challenge also when adding text to a code 
		block. We want to remove the empty lines before and after the code blocks 
		because otherwise they would appear also in the outputted HTML. Unlike 
		comment text, code is always added one token at a time. So, no multi-line 
		strings are passed to this function.
		*/
		protected void AppendCode (string text)
		{
			if (_currentBlock != null && (string.IsNullOrEmpty (text) ||
				(_currentBlock.Kind == BlockKind.Markdown && IsEmptyLine (text))))
				return;
			CurrentBlock (BlockKind.Code)._builder.Append (text);
		}
		/*
		Recognizing an empty line is done be removing all the spaces and tabs
		from its beginning, and then checking if the remaining string contains
		just a linefeed. We handle both Windows and Unix style line delimiters. 
		*/
		private bool IsEmptyLine (string text)
		{
			switch (text.TrimStart (' ', '\t'))
			{
				case "\r\n":
				case "\r":
				case "\n": return true;
				default: return false;
			}
		}
		/*
		Removing comment delimiters is easy. The regex does the work for
		us automatically.
		*/
		private string StripCommentDelimiters (string comment) =>
			_commentBody.Match (comment).Groups[1].Value;
		/*
		## Parsing Tokens

		Now that we have all the helper functions defined, we can implement the
		visitor for tokens. The visitor overrides the inherited method and outputs 
		text to the current block based on the tokens.

		Roslyn parser does not add comments to AST as syntactic nodes. Instead 
		they are included as trivia which is additional information attached to 
		tokens. Trivia can be present before and/or after a token, so we have both
		leading and trailing trivia. In addition to comments, trivia can represent 
		also whitespace or preprocessor directives.
		*/
		public override void VisitToken (SyntaxToken token)
		{
			/*
			First we extract the leading trivia. If the trivia contains
			multi-line comment, we will output the comment as documentation.
			Otherwise we revert to the generic method which by default 
			outputs trivia verbatim as code.
			*/
			foreach (var trivia in token.LeadingTrivia)
				if (trivia.Kind () == SyntaxKind.MultiLineCommentTrivia)
					AppendMarkdown (StripCommentDelimiters (trivia.ToString ()));
				else
					OutputTrivia (trivia);
			/*
			Then we output the token itself as code.
			*/
			OutputToken (token);
			/*
			And finally we output the trailing trivia. Comments are always attached
			to the leading trivia, so there is no need to handle them here.
			*/
			foreach (var trivia in token.TrailingTrivia)
				OutputTrivia (trivia);
			/*
			Visitors need to call the inherited method too. If they fail to	do so, 
			the tree walker will not traverse the whole AST.
			*/
			base.VisitToken (token);
		}
		/*
		The following two methods are virtual so that subclasses can alter their 
		behavior. The first one outputs a token, and the second one a piece of 
		trivia. Both methods just send the text verbatim to the current code block.
		*/
		protected virtual void OutputToken (SyntaxToken token)
		{
			AppendCode (token.ToString ());
		}

		protected virtual void OutputTrivia (SyntaxTrivia trivia)
		{
			AppendCode (trivia.ToString ());
		}
	}
}