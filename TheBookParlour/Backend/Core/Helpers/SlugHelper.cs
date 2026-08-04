using System.Text.RegularExpressions;

namespace TheBookParlour.Core.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string title)
        {
            string titleSlug = title.ToLower();
            titleSlug = Regex.Replace(titleSlug, @"\s+", " "); // ← ersätt flera mellanslag med ett.
            titleSlug = Regex.Replace(titleSlug, @"[^a-z0-9\s-]", ""); // ← ta bort specialtecken. 
            titleSlug = titleSlug.Replace(" ", "-");
            return titleSlug;

        }
    }
}
