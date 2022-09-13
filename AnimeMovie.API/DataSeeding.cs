using System;
using AnimeMovie.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AnimeMovie.API
{
    public static class DataSeeding
    {
        public static void Seed(IApplicationBuilder application)
        {
            var scope = application.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<MovieDbContext>();
            context.Database.Migrate();
            if (context.SiteDescription.Count() == 0)
            {
                context.SiteDescription.Add(new Entites.SiteDescription()
                {
                    InstagramUrl = "#",
                    DiscordUrl = "#",
                    YoutubeUrl = "#",
                    Keywords = "Anime,Manga",
                    Description = "Anime ve Manga Film Sitesi",
                });
                context.SaveChanges();
            }
            if (context.Announcements.Count() == 0)
            {
                context.Announcements.Add(new Entites.Announcement()
                {
                    AddDate = DateTime.Now,
                    AddToInformation = "Eklenecekler Bilgisi",
                    ComingSoonDate = DateTime.Now,
                    ComingSoonInfo = "Yakında Bilgisi",
                    ComplaintsDate = DateTime.Now,
                    ComplaintsInformation = "Şikayetler Hakkında",
                    InnovationDate = DateTime.Now,
                    InnovationInformation = "Yenilikler",
                    UpdateDate = DateTime.Now,
                    UpdateInformation = "Güncellemeler",
                    WarningDate = DateTime.Now,
                    WarningInformation = "Uyarı"
                });
                context.SaveChanges();
            }

        }
    }
}

