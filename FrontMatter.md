# Front Matter

All of the input files may contain a special block called _front matter_ 
in the very beginning of them. The front matter is a [YAML](http://yaml.org/)
block that contains metadata related to the input file or to the whole project.
It is a collection of string properties that is passed to the theme assembly 
for controlling the HTML generation.

Typically the front matter looks something like this:

    ---
    ProjectName: Literate Programming
    GitHub: https://github.com/johtela/LiterateProgramming
    Footer: Copyright (c) 2017 Tommi Johtela
    ShowDescriptionsInToc: true
    SyntaxHighlight: son-of-obsidian
    ---

It is delimited by three dashes and it must be first lines of text of a 
markdown or C# file. No other text may appear before it, excluding the comment 
start token `/*` when it appears in a C# file.

## Available Properties

The list of available properties depends on the theme used. The properties 
available in the default theme are listed below. Property names and values 
are case-insensitive, so they can be typed using any convention: lowercase, 
uppercase, Camel case, Pascal case, etc.

### `Template` (string)

The name of the template used for producing the HTML page. This setting
is theme-dependent. In the default theme, the possible choices are:
* **Default** - Default template for documentation pages.
* **Landing** - Template for the landing page.

Depending on the template used, a property might be relevant or not. The list 
of supported parameters available in the default theme is shown in the table 
below. The columns indicate if a parameter is supported by a specific template.

| Parameter             | Default   | Landing  |
| --------------------- |:---------:|:--------:|
| ProjectName           |     *     |     *    |
| GitHub                |     *     |     *    |
| Footer                |     *     |     *    |
| MarkdownStyle         |     *     |          |
| ShowDescriptionInToc  |     *     |          |
| SyntaxHighlight       |     *     |          |
| UseDiagrams           |     *     |          |
| DiagramStyle          |     *     |          |
| UseMath               |     *     |          |
| Jumbotron             |           |     *    |

**Note:** You don't need to set a parameter separately in each input file. All
the parameters except for `Template` will retain their values, once they are set.
If you want a parameter to be defined for all the input files, define it in the 
Index file (typically on the landing page), which is always processed first. You 
can define parameters that are not used on the page in question. For example, 
you can set the `SyntaxHighlight` parameter on the landing page. The setting is 
used with the subsequently processed files.

### `ProjectName` (string)

The name of the project that is shown in the toolbar of the HTML pages.

### `GitHub` (URL)

The URL of the project in GitHub. The link is shown as a button in the toolbar.

### `Footer` (string)

The text shown in the footer of the HTML pages.

### `MarkdownStyle` (string)

The name of the CSS style sheet that is used for markdown formatting. 
The available styles in the default theme are:
* **book** - The default style with serif fonts.
* **modern** - Uses the Lucida font family.
* **plain** - Simplistic style with sans serif fonts.

### `ShowDescriptionInToc` (true/false)

Control whether the description of a page is shown along with the page 
title in the table of contents shown in the side bar. Off by default.

### `SyntaxHighlight` (string)

The name of the CSS style sheet that is used for syntax highlighting. 
The available syntax highlighting schemes in the default theme are:

* **monokai** (default) - Dark theme used in many text editors. 
* **coding-horror** - Light theme inspired by the 
  [Coding Horror](https://blog.codinghorror.com/) blog.
* **solarized-light** - Solarized theme used in many text editors.
* **son-of-obsidian** - The most popular color scheme in 
  [studiostyles](https://studiostyl.es/).

### `UseDiagrams` (true/false)

Diagram support is off by default. This is because enabling it increases page 
loading times. If you want to include diagrams in your documentation, set this 
property to `true`. For more information refer to the
[Tips & Tricks](TipsAndTricks.html) page.

### `DiagramStyle` (string)

The name of CSS style file used by the [mermaid](http://knsv.github.io/mermaid/)
diagramming library. The default theme includes all style files that
come out-of-the-box with the library:

* **mermaid** (default) - Default light blue theme.
* **mermaid.dark** - A slightly darker blue theme.
* **mermaid.forest** - Green-colored theme.

### `UseMath` (true/false)

As with diagrams, support for mathematical formulas is disabled by default. The 
default theme uses the [MathJax](https://www.mathjax.org/) library to render
the formulas, and since it is a big library, loading it takes some time. To 
enable the math support, set this property to `true`. For more information refer 
to the [Tips & Tricks](TipsAndTricks.html) page.

### `_Jumbotron` (markdown)
The contents of the jumbotron pane of the landing page. All the parameters 
that start with underscore `_` will be translated to HTML automatically.
