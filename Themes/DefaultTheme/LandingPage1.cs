﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace DefaultTheme
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\LandingPage.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class LandingPage : LandingPageBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n<!DOCTYPE html>\n<html lang=\"en\">\n\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n<head>\n    <meta charset=\"utf-8\">\n    <meta http-equiv=\"X-UA-Compatible\" content" +
                    "=\"IE=edge\">\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale" +
                    "=1\">\n\n    <title>");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/LandingHead.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params["projectname"]));
            
            #line default
            #line hidden
            this.Write("</title>\n\n\t<link rel=\"icon\" type=\"image/x-icon\" href=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/LandingHead.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.Root));
            
            #line default
            #line hidden
            this.Write("images/favicon.ico\">\n    ");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n<link rel=\"stylesheet\" href=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include\LandingStyles.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.Root));
            
            #line default
            #line hidden
            this.Write("bootstrap/css/bootstrap.min.css\" />\n<link rel=\"stylesheet\" href=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include\LandingStyles.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.Root));
            
            #line default
            #line hidden
            this.Write("font-awesome/css/font-awesome.min.css\">\n<link rel=\"stylesheet\" href=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include\LandingStyles.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.Root));
            
            #line default
            #line hidden
            this.Write("css/landingpage.css\" />\n");
            this.Write("\n</head>");
            this.Write("\n\n<body>\n    ");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write(@"

    <nav class=""navbar navbar-inverse"">
        <div class=""container-fluid"">
            <div class=""navbar-header"">
                <button type=""button"" class=""navbar-toggle collapsed"" data-toggle=""collapse"" data-target=""#navbar"" aria-expanded=""false"" aria-controls=""navbar"">
                    <span class=""sr-only"">Toggle navigation</span>
                    <span class=""icon-bar""></span>
                    <span class=""icon-bar""></span>
                    <span class=""icon-bar""></span>
                </button>
                <a class=""navbar-brand"" href=""");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/navbar.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.Root));
            
            #line default
            #line hidden
            this.Write("Index.html\">");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/navbar.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params["projectname"]));
            
            #line default
            #line hidden
            this.Write("</a>\n            </div>\n            <div id=\"navbar\" class=\"navbar-collapse colla" +
                    "pse\">\n                <ul class=\"nav navbar-nav\">\n                    <li><a hre" +
                    "f=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/navbar.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.Root));
            
            #line default
            #line hidden
            this.Write("Index.html\"><i class=\"fa fa-home\" aria-hidden=\"true\"></i> Home</a></li>\n         " +
                    "           <li><a href=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/navbar.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params["github"]));
            
            #line default
            #line hidden
            this.Write("\"><i class=\"fa fa-github\" aria-hidden=\"true\"></i> GitHub Repository</a></li>\n    " +
                    "                <li><a href=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/navbar.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params["download"]));
            
            #line default
            #line hidden
            this.Write("\"><i class=\"fa fa-download\" aria-hidden=\"true\"></i> Download</a></li>\n           " +
                    "         <li><a href=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/navbar.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.Root));
            
            #line default
            #line hidden
            this.Write("License.html\">License</a></li>\n                </ul>\n            </div>\n        <" +
                    "/div>\n    </nav>");
            this.Write("\n\n    <!-- Main jumbotron for a primary marketing message or call to action -->\n " +
                    "   <div class=\"jumbotron\">\n      <div class=\"container\">\n        ");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\LandingPage.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params["_jumbotron"]));
            
            #line default
            #line hidden
            this.Write("\n        <p><a class=\"btn btn-primary btn-lg\" href=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\LandingPage.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.SectionPath (Params.Toc.NextSection (Params.CurrentSection))));
            
            #line default
            #line hidden
            this.Write("\" role=\"button\">Learn more &raquo;</a></p>\n      </div>\n    </div>\n\n    <div clas" +
                    "s=\"container\">\n\t\t");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\LandingPage.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.Contents));
            
            #line default
            #line hidden
            this.Write("\n\t</div>\n\n    ");
            this.Write("\r\n    <footer class=\"panel-footer text-center\">\r\n        <p>");
            
            #line 7 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/Footer.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params["footer"]));
            
            #line default
            #line hidden
            this.Write("</p>\r\n    </footer>\r\n");
            this.Write("\n\n    ");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n");
            this.Write("\n\n    <script src=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/LandingScripts.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.Root));
            
            #line default
            #line hidden
            this.Write("bootstrap/js/jquery.min.js\"></script>\n    <script src=\"");
            
            #line 1 "C:\Users\johteto\Source\Repos\LiterateProgramming\Themes\DefaultTheme\_include/LandingScripts.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Params.Root));
            
            #line default
            #line hidden
            this.Write("bootstrap/js/bootstrap.min.js\"></script>\n");
            this.Write("\n</body>\n</html>");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public class LandingPageBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}