using System;
using System.Globalization;
using System.Text;
using System.Web;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Helper
{
    public interface ISeoUrl
    {
        string createAnimeLink(Anime anime);
        string createMangaLink(Manga manga);
        string createUserLink(Users user);
    }
    public class SeoUrl : ISeoUrl
    {
        public string createAnimeLink(Anime anime)
        {
            string url = createUrl(anime.AnimeName + "-" + anime.ID);
            return url.ToLower();
        }

        public string createMangaLink(Manga manga)
        {
            string url = createUrl(manga.Name + "-" + manga.ID);
            return url.ToLower();
        }

        public string createUserLink(Users user)
        {
            string url = createUrl(user.UserName + "-" + user.ID);
            return url.ToLower();
        }

        private string createUrl(string pTitle)
        {
            pTitle = pTitle.Replace(" ", "-");
            pTitle = pTitle.Replace(".", "-");
            pTitle = pTitle.Replace("ı", "i");
            pTitle = pTitle.Replace("İ", "I");
            pTitle = pTitle.Replace("I", "i");

            pTitle = String.Join("", pTitle.Normalize(NormalizationForm.FormD)
                    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            pTitle = HttpUtility.UrlEncode(pTitle);
            return System.Text.RegularExpressions.Regex.Replace(pTitle, @"\%[0-9A-Fa-f]{2}", "");
        }
    }
}

