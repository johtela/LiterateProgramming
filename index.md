---
Template: landing
ProjectName: Literate Programming
GitHub: https://github.com/johtela/LiterateProgramming
Download: https://github.com/johtela/LiterateProgramming/releases
Footer: "Copyright © 2017 Tommi Johtela"
ShowDescriptionsInToc: true
MarkdownStyle: book
SyntaxHighlight: solarized-light
UseDiagrams: true
DiagramStyle: mermaid
UseMath: true
_Jumbotron: >
    # Literate Programming in C#

    Produce good-looking, interactive documentation for your C# projects using
    [literate programming](https://en.wikipedia.org/wiki/Literate_programming). 
    Write comments in [markdown](https://en.wikipedia.org/wiki/Markdown) and 
    create fully functional web sites that can be published on 
    [GitHub](https://github.com) or other web hosting services.
---

::::::row
:::col-md-6
## Fully Customizable Output

Generate either raw markdown files or static web sites with everything included. 
Use themes and parameters to customize the sites. You can include a 
[YAML](http://yaml.org/) front matter in your source files to pass parameters to 
themes.

[Markdig](https://github.com/lunet-io/markdig)
library is used to convert the markdown to HTML. It enables a lot of useful
extensions from [MathJax](https://www.mathjax.org/) formulas to 
[mermaid](https://knsv.github.io/mermaid/) diagrams. 
:::
:::col-md-6
![Diagram](images/Diagram.png){.img-responsive .center-block}
:::
::::::

::::::row
:::col-md-6
![Diagram](images/Code.png){.img-responsive .center-block}
:::
:::col-md-6
## Interactive Code Blocks

The tool uses [Roslyn](https://github.com/dotnet/roslyn) to parse and analyze
source code. Syntactic and semantic information provided by Roslyn is used for 
syntax-highlighting and for adding cross-references and type information in
code snippets. You can jump to the definition of a symbol by clicking it, or 
inspect its type by hovering over it.
:::
::::::

::::::row
:::col-md-6
## Easy Navigation

Navigation panes are added automatically to help find a specific topic. Table 
of Contents file outlines the structure of the documentation. TOC file can be 
automatically generated and updated. The outputted web pages will show the TOC
in a sidebar as well as navigation buttons that jump to the previous and next
section.
:::
:::col-md-6
![Diagram](images/Navigation.png){.img-responsive .center-block}
:::
::::::

::::::row
:::col-md-3
<i class="fa fa-cloud-download fa-5x pull-right"></i>
:::
:::col-md-6
## Give It a Try!

Go to the releases page to download the latest version. You can also clone the
[repository](https://github.com/johtela/LiterateProgramming) and build the tool
from the sources.

<a class="btn btn-default" href="https://github.com/johtela/LiterateProgramming/releases" role="button">Download &raquo;</a>
:::
::::::
