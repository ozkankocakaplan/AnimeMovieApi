using System;
using System.Linq;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Business.Models;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore;

namespace AnimeMovie.Business.Concrete
{
    public class UserListContentsManager : IUserListContentsService
    {
        private readonly IUserListContentsRepository userListContentsRepository;
        private readonly IAnimeRepository animeRepository;
        private readonly IMangaRepository mangaRepository;
        private readonly IAnimeEpisodesRepository animeEpisodesRepository;
        private readonly IMangaEpisodesRepository mangaEpisodesRepository;
        private readonly ICategoryTypeRepository categoryTypeRepository;
        private readonly IAnimeSeasonRepository animeSeasonRepository;
        public UserListContentsManager(IUserListContentsRepository userListContents
            , IAnimeRepository anime, IMangaRepository manga, IAnimeEpisodesRepository animeEpisodes, IMangaEpisodesRepository mangaEpisodes,
            IAnimeSeasonRepository animeSeason, ICategoryTypeRepository categoryType)
        {
            mangaEpisodesRepository = mangaEpisodes;
            animeRepository = anime;
            mangaRepository = manga;
            animeEpisodesRepository = animeEpisodes;
            userListContentsRepository = userListContents;
            categoryTypeRepository = categoryType;
            animeSeasonRepository = animeSeason;
        }

        public ServiceResponse<UserListContents> add(UserListContents entity)
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                response.Entity = userListContentsRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContents> delete(Expression<Func<UserListContents, bool>> expression)
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                response.IsSuccessful = userListContentsRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContents> get(Expression<Func<UserListContents, bool>> expression)
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                response.Entity = userListContentsRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContents> getList()
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                response.List = userListContentsRepository.GetAll().ToList();
                response.Count = userListContentsRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContents> getList(Expression<Func<UserListContents, bool>> expression)
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                var list = userListContentsRepository.TableNoTracking.Where(expression).ToList();
                response.Count = list.Count;
                response.List = list;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContentsModels> getUserContentListModels()
        {
            var response = new ServiceResponse<UserListContentsModels>();
            try
            {
                List<UserListContentsModels> listModels = new List<UserListContentsModels>();
                var list = userListContentsRepository.Table.Include(x => x.UserList).Include(x => x.Users).ToList();
                foreach (var item in list)
                {
                    UserListModels user = new UserListModels(item);
                    if (item.Type == Entites.Type.Anime)
                    {
                        user.Anime = animeRepository.get(x => x.ID == item.ContentID);
                        user.AnimeEpisodes = animeEpisodesRepository.GetAll().Where(x => x.ID == item.EpisodeID).ToList();
                    }
                    else
                    {
                        user.Manga = mangaRepository.get(x => x.ID == item.ContentID);
                        user.MangaEpisodes = mangaEpisodesRepository.GetAll().Where(x => x.ID == item.EpisodeID).ToList();
                    }
                    list.Add(item);

                }
                response.Count = list.Count;
                response.List = listModels;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListModels> getUserListModels(Expression<Func<UserListContents, bool>> expression)
        {
            var response = new ServiceResponse<UserListModels>();
            try
            {
                List<UserListModels> listModels = new List<UserListModels>();
                var list = userListContentsRepository.Table.Include(x => x.UserList).Include(x => x.Users).Where(expression).ToList();
                foreach (var item in list.DistinctBy(x => x.ContentID))
                {
                    UserListModels user = new UserListModels(item);
                    if (item.Type == Entites.Type.Anime)
                    {
                        user.Anime = animeRepository.get(x => x.ID == item.ContentID);
                        user.AnimeEpisodes = animeEpisodesRepository.GetAll().Where(x => x.ID == item.EpisodeID && x.AnimeID == item.ContentID).ToList();
                        user.Categories = categoryTypeRepository.TableNoTracking.Include(x => x.Categories).Where(x => x.ContentID == item.ContentID && x.Type == Entites.Type.Anime).ToList();
                        user.AnimeSeasons = animeSeasonRepository.TableNoTracking.Where(x => x.AnimeID == item.ContentID).ToList();
                    }
                    else
                    {
                        user.Manga = mangaRepository.get(x => x.ID == item.ContentID);
                        user.MangaEpisodes = mangaEpisodesRepository.GetAll().Where(x => x.ID == item.EpisodeID && x.MangaID == item.ContentID).ToList();
                        user.Categories = categoryTypeRepository.TableNoTracking.Include(x => x.Categories).Where(x => x.ContentID == item.ContentID && x.Type == Entites.Type.Manga).ToList();
                    }
                    listModels.Add(user);

                }
                response.Count = list.Count;
                response.List = listModels;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserListContents> update(UserListContents entity)
        {
            var response = new ServiceResponse<UserListContents>();
            try
            {
                response.Entity = userListContentsRepository.Update(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

    }
}

