/*
# Source Blocks
In order to extract the documentation from the code, `csweave` needs to split a source file
into blocks which represent either text or code. Blocks capture parts of a source file and
make it easier to process them differently depending on their kind.

## Dependencies
The usual suspects here. No references to external libraries.
*/
namespace LiterateProgramming
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Text;
	/*
	## Block Kinds

	A block can capture either documentation written in markdown or C# code. The enumeration 
	below indicates which one we are dealing with.
	*/
	public enum BlockKind { Markdown, Code }
	/*
	## Linked List of Blocks

	The block data structure itself is a simple singly-linked list. It implements 
	`IEnumerable<T>` to make traversing the structure easier.
	*/
	public class BlockList : IEnumerable<BlockList>
	{
		/*
		The properties below describe the data associated with a block. `Kind` tells the 
		whether it is a code or text block. `Contents` is the part of the source file
		that was extracted. Comment delimiters are excluded from the text, so that 
		contents is valid markdown. `Next` points to the following block. Obviously
		code blocks are in the same order as they appear in the source file.
		*/
		public BlockKind Kind { get; private set; }
		public string Contents { get; private set; }
		public BlockList Next { get; internal set; }
		/*
		The following constants describe how the code blocks are decorated in the outputted
		markdown or HTML. In the case of markdown output, the code blocks are surrounded
		by standard triple backticks followed by the language designator. In HTML output the 
		header and footer contain tags which have the required CSS classes in order for the 
		syntax highlighting to work.
		*/
		private const string _mdCodeHeader = @"``` csharp";
		private const string _mdCodeFooter = @"```";
		private const string _htmlCodeHeader = @"<pre class=""csharp""><code class=""csharp"">";
		private const string _htmlCodeFooter = @"</code></pre>";
		/*
		The string builder is used to construct contents of a block. It is discarded after
		its content is extracted.
		*/
		internal StringBuilder _builder;
		/*
		### Creating a Block
		Blocks are constructed dynamically as the source file is parsed. When creating a new
		block only the kind of the block and the output format needs to be specified. Based
		on those the constructor initializes a new block and inserts a correct header to it.
		
		In markdown output whitespace is a bit more important than in HTML. That is why 
		we sometimes need to add additional line breaks to the output.
		*/
		public BlockList (BlockKind kind, OutputFormat format)
		{
			Kind = kind;
			_builder = new StringBuilder ();
			if (Kind == BlockKind.Code)
			{
				_builder.Append (format == OutputFormat.html ?
					_htmlCodeHeader : 
					_mdCodeHeader + Environment.NewLine);
			}
		}
		/*
		### Closing a Block
		After a block is filled with its content it can be closed. Closing involves
		adding some whitespace and a correct footer to the output. After the `StringBuilder` 
		object has converted the contents to string, it can be disposed by assigning `null`
		to the field.
		*/
		public void Close (OutputFormat format)
		{
			if (Kind == BlockKind.Code)
			{
				TrimRight ();
				_builder.Append (Environment.NewLine);
				_builder.AppendLine (format == OutputFormat.html ?
					 _htmlCodeFooter :
					 _mdCodeFooter);
			}
			Contents = _builder.ToString ();
			_builder = null;
		}
		/*
		To get rid of extra white space at the end of code blocks they are right-trimmed. 
		This makes the verbatim output denser and more readable.
		*/
		private void TrimRight ()
		{
			var last = _builder.Length - 1;
			var i = last;
			while (i >= 0 && char.IsWhiteSpace (_builder[i]))
				i--;
			if (i < last)
				_builder.Remove (i + 1, last - i);
		}

		/* 
		### IEnumerable Implementation
		Iterator is used to implement `IEnumerable<BlockList>` interface.
		*/
		public IEnumerator<BlockList> GetEnumerator ()
		{
			for (var block = this; block != null; block = block.Next)
				yield return block;
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}
	}
}
