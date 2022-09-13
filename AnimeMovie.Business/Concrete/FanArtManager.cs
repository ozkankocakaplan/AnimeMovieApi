using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class FanArtManager : IFanArtService
    {
        private readonly IFanArtRepository fanArtRepository;
        public FanArtManager(IFanArtRepository fanArt)
        {
            fanArtRepository = fanArt;
        }

        public ServiceResponse<FanArt> add(FanArt entity)
        {
            var response = new ServiceResponse<FanArt>();
            try
            {
                response.Entity = fanArtRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<FanArt> delete(Expression<Func<FanArt, bool>> expression)
        {
            var response = new ServiceResponse<FanArt>();
            try
            {
                fanArtRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<FanArt> get(Expression<Func<FanArt, bool>> expression)
        {
            var response = new ServiceResponse<FanArt>();
            try
            {
                response.Entity = fanArtRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<FanArt> getList()
        {
            var response = new ServiceResponse<FanArt>();
            try
            {
                response.List = fanArtRepository.GetAll().ToList();
                response.Count = fanArtRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<FanArt> getList(Expression<Func<FanArt, bool>> expression)
        {
            var response = new ServiceResponse<FanArt>();
            try
            {
                var list = fanArtRepository.Table.Where(expression).ToList();
                response.List = list;
                response.Count = list.Count;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<FanArt> getPaginatedFanArt(Expression<Func<FanArt, bool>> expression, int pageNo, int ShowCount)
        {
            var response = new ServiceResponse<FanArt>();
            try
            {
                var list = fanArtRepository.Table.Where(expression).ToList();
                response.List = list.Skip((pageNo - 1) * ShowCount).Take(ShowCount).ToList();
                int page = 0;
                var totalFanArt = list.Count();
                if (totalFanArt % ShowCount > 0)
                {
                    page++;
                }
                response.Count = page;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<FanArt> update(FanArt entity)
        {
            var response = new ServiceResponse<FanArt>();
            try
            {
                response.Entity = fanArtRepository.Update(entity);
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

