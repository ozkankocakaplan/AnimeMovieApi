using System;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;
using Hangfire;

namespace AnimeMovie.API.Jobs.Recurring
{
    public class UserRosetteCheck
    {
        private readonly IUserListContentsService userListContentsService;
        private readonly IAnimeListService animeListService;
        private readonly IMangaListService mangaListService;
        private readonly IUserRosetteService userRosetteService;
        private readonly IRosetteContentService rosetteContentService;
        public UserRosetteCheck(IUserListContentsService userListContents, IAnimeListService animeList
            , IMangaListService mangaList, IUserRosetteService userRosette, IRosetteContentService rosetteContent)
        {
            userListContentsService = userListContents;
            animeListService = animeList;
            mangaListService = mangaList;
            userRosetteService = userRosette;
            rosetteContentService = rosetteContent;
        }
        public void Run(IJobCancellationToken jobCancallationToken)
        {
            jobCancallationToken.ThrowIfCancellationRequested();
            UserListContentCheck();
            AnimeContentCheck();
            MangaContentCheck();
        }
        public void UserListContentCheck()
        {
            var userListContents = userListContentsService.getList().List.ToList();
            foreach (var userContent in userListContents)
            {
                var rosetteContent = rosetteContentService.get(x => x.ContentID == userContent.ContentID && x.Type == userContent.Type && x.EpisodesID == userContent.EpisodeID).Entity;
                if (rosetteContent != null)
                {
                    var userRosetteCheck = userRosetteService.get(x => x.RosetteID == rosetteContent.RosetteID && x.UserID == userContent.UserID).Entity;
                    if (userRosetteCheck == null)
                    {
                        userRosetteService.add(new Entites.UserRosette()
                        {
                            RosetteID = userRosetteCheck.RosetteID,
                            UserID = userContent.UserID,
                            Status = Entites.Status.Approved
                        });
                    }
                }
                else
                {
                    var userRosetteCheck = userRosetteService.get(x => x.RosetteID == rosetteContent.RosetteID && x.UserID == userContent.UserID).Entity;
                    if (userRosetteCheck != null)
                    {
                        userRosetteService.delete(x => x.ID == userRosetteCheck.ID);
                    }
                }
            }

        }
        public void AnimeContentCheck()
        {
            var animeList = mangaListService.getList(x => x.Status == Entites.MangaStatus.IRead).List.ToList();
            foreach (var anime in animeList)
            {
                var rosetteContent = rosetteContentService.get(x => x.ContentID == anime.ID && x.EpisodesID == anime.EpisodeID).Entity;
                if (rosetteContent != null)
                {
                    var userRosette = userRosetteService.get(x => x.UserID == anime.UserID && x.RosetteID == rosetteContent.RosetteID);
                    if (userRosette == null)
                    {
                        userRosetteService.add(new Entites.UserRosette()
                        {
                            RosetteID = rosetteContent.RosetteID,
                            UserID = anime.UserID,
                            Status = Entites.Status.Approved
                        });
                    }
                }
                else
                {
                    var rosetteContentCheck = rosetteContentService.get(x => x.ContentID == anime.ID && x.EpisodesID == anime.EpisodeID).Entity;
                    userRosetteService.delete(x => x.RosetteID == rosetteContentCheck.RosetteID && x.UserID == anime.UserID);
                }
            }
        }
        public void MangaContentCheck()
        {
            var mangaList = mangaListService.getList(x => x.Status == Entites.MangaStatus.IRead).List.ToList();
            foreach (var manga in mangaList)
            {
                var rosetteContent = rosetteContentService.get(x => x.ContentID == manga.ID && x.EpisodesID == manga.EpisodeID).Entity;
                if (rosetteContent != null)
                {
                    var userRosette = userRosetteService.get(x => x.UserID == manga.UserID && x.RosetteID == rosetteContent.RosetteID);
                    if(userRosette == null)
                    {
                        userRosetteService.add(new Entites.UserRosette()
                        {
                            RosetteID = rosetteContent.RosetteID,
                            UserID = manga.UserID,
                            Status = Entites.Status.Approved
                        });
                    }
                }
                else
                {
                    var rosetteContentCheck = rosetteContentService.get(x => x.ContentID == manga.ID && x.EpisodesID == manga.EpisodeID).Entity;
                    userRosetteService.delete(x => x.RosetteID == rosetteContentCheck.RosetteID && x.UserID == manga.UserID);
                }
            }


        }
    }
}

