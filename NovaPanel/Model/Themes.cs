namespace NovaPanel.Model
{
    public class Themes
    {
        public enum Theme
        {
            Dark,
            Light,
            Default
        }

        public static Theme Parse(string theme)
        {
            switch (theme.ToLower())
            {
                case "dark":
                    return Theme.Dark;
                case "light":
                    return Theme.Light;
                default:
                    return Theme.Default;
            }
        }

        public static void Toggle()
        {
            switch (ApplicationRuntimes.Theme)
            {
                case Theme.Dark:
                    ApplicationRuntimes.Theme = Theme.Light;
                    break;
                case Theme.Light:
                    ApplicationRuntimes.Theme = Theme.Default;
                    break;
                default:
                    ApplicationRuntimes.Theme = Theme.Dark;
                    break;
            }

        }
    }
}
