namespace DefaultTheme
{
	using CSWeave.Theme;

	interface IPageTemplate
	{
		PageParams Params { get; set; }
		string Render ();
	}
}
