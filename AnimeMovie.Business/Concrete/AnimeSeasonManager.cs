using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class AnimeSeasonManager : IAnimeSeasonService
    {
        private readonly IAnimeSeasonRepository animeSeasonRepository;
        public AnimeSeasonManager(IAnimeSeasonRepository animeSeason)
        {
            animeSeasonRepository = animeSeason;
        }

        public ServiceResponse<AnimeSeason> add(AnimeSeason entity)
        {
            var response = new ServiceResponse<AnimeSeason>();
            try
            {
                response.Entity = animeSeasonRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeSeason> delete(Expression<Func<AnimeSeason, bool>> expression)
        {
            var response = new ServiceResponse<AnimeSeason>();
            try
            {
                animeSeasonRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeSeason> get(Expression<Func<AnimeSeason, bool>> expression)
        {
            var response = new ServiceResponse<AnimeSeason>();
            try
            {
                response.Entity = animeSeasonRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeSeason> getList()
        {
            var response = new ServiceResponse<AnimeSeason>();
            try
            {
                response.List = animeSeasonRepository.GetAll().ToList();
                response.Count = animeSeasonRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeSeason> getList(Expression<Func<AnimeSeason, bool>> expression)
        {
            var response = new ServiceResponse<AnimeSeason>();
            try
            {
                var list = animeSeasonRepository.TableNoTracking.Where(expression).ToList();
                response.Count = list.Count();
                response.List = list;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<AnimeSeason> update(AnimeSeason entity)
        {
            var response = new ServiceResponse<AnimeSeason>();
            try
            {
                response.Entity = animeSeasonRepository.Update(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }
    }
}

