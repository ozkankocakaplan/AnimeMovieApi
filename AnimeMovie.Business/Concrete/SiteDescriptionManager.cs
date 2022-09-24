using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class SiteDescriptionManager : ISiteDescriptionService
    {
        private readonly ISiteDescriptionRepository siteDescriptionRepository;
        public SiteDescriptionManager(ISiteDescriptionRepository siteDescription)
        {
            siteDescriptionRepository = siteDescription;
        }

        public ServiceResponse<SiteDescription> add(SiteDescription entity)
        {
            var response = new ServiceResponse<SiteDescription>();
            try
            {
                response.Entity = siteDescriptionRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<SiteDescription> delete(Expression<Func<SiteDescription, bool>> expression)
        {
            var response = new ServiceResponse<SiteDescription>();
            try
            {
                response.IsSuccessful = siteDescriptionRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<SiteDescription> get(Expression<Func<SiteDescription, bool>> expression)
        {
            var response = new ServiceResponse<SiteDescription>();
            try
            {
                response.Entity = siteDescriptionRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<SiteDescription> getList()
        {
            var response = new ServiceResponse<SiteDescription>();
            try
            {
                response.List = siteDescriptionRepository.GetAll().ToList();
                response.Count = siteDescriptionRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<SiteDescription> getList(Expression<Func<SiteDescription, bool>> expression)
        {
            var response = new ServiceResponse<SiteDescription>();
            try
            {
                var list = siteDescriptionRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<SiteDescription> update(SiteDescription entity)
        {
            var response = new ServiceResponse<SiteDescription>();
            try
            {
                response.Entity = siteDescriptionRepository.Update(entity);
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

