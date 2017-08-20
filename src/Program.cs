/*
# Main Program

The main program is quite simple. It just checks the command line arguments and calls the 
[Weaver](Weaver.html) class that does the actual work.

## Dependencies

There are no special dependencies in the main program. Only reference is to the System 
namespace.
*/
namespace LiterateProgramming
{
	using System;
	/* 
	The main class imaginatively named as `Program`. 
	*/
	class Program
	{
		static int Main (string[] args)
		{
			/* 
			## Command Line Parsing

			First we create a command line parser that we got from the 
			[NuGet](https://commandline.codeplex.com/) library. We configure the command line options 
			to be case insensitive.
			*/
			var cmdLineParser = new CommandLine.Parser (settings =>
			{
				settings.CaseSensitive = false;
				settings.HelpWriter = Console.Out;
			});
			/*
			Then we parse the command line options into an object that contains the settings. If the
			parsing fails, the parser will output usage information automatically after which we terminate
			the program.
			*/
			var options = new Options ();
			if (!cmdLineParser.ParseArguments (args, options))
				return 0;
			/*
			## Generating Documentation

			Finally we create a [Weaver](Weaver.html) object and call its `Generate` method to generate 
			the documentation according to the options. If an exception is thrown during the process,
			its error message is outputted and the program is terminated with an error code.
			*/
			try
			{
				var weaver = options.Format == OutputFormat.html ?
					new HtmlWeaver (options) :
					(Weaver)new MdWeaver (options);
				weaver.GenerateDocumentation ();
			}
			catch (Exception e)
			{
				Console.WriteLine ("Fatal error in document generation:");
				Console.WriteLine (e.Message);
				Console.WriteLine (e.StackTrace);
				if (e.InnerException != null)
				{
					Console.WriteLine ("Inner exception:");
					Console.WriteLine (e.InnerException.Message);
					Console.WriteLine (e.InnerException.StackTrace);
				}
				return 1;
			}
			return 0;
		}
	}
}
/*
And that's it! If you want to know how the document generation actually works, jump to the 
documentation of the [Weaver](Weaver.html) class.
*/
