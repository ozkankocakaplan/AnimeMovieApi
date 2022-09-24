using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class HomeSliderManager : IHomeSliderService
    {
        private readonly IHomeSliderRepository homeSliderRepository;
        public HomeSliderManager(IHomeSliderRepository homeSlider)
        {
            homeSliderRepository = homeSlider;
        }

        public ServiceResponse<HomeSlider> add(HomeSlider entity)
        {
            var response = new ServiceResponse<HomeSlider>();
            try
            {
                response.Entity = homeSliderRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<HomeSlider> delete(Expression<Func<HomeSlider, bool>> expression)
        {
            var response = new ServiceResponse<HomeSlider>();
            try
            {
                homeSliderRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<HomeSlider> get(Expression<Func<HomeSlider, bool>> expression)
        {
            var response = new ServiceResponse<HomeSlider>();
            try
            {
                response.Entity = homeSliderRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<HomeSlider> getList()
        {
            var response = new ServiceResponse<HomeSlider>();
            try
            {
                response.List = homeSliderRepository.GetAll().ToList();
                response.Count = homeSliderRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<HomeSlider> getList(Expression<Func<HomeSlider, bool>> expression)
        {
            var response = new ServiceResponse<HomeSlider>();
            try
            {
                var list = homeSliderRepository.TableNoTracking.Where(expression).ToList();
                response.List = list;
                response.Count = list.Count;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<HomeSlider> update(HomeSlider entity)
        {
            var response = new ServiceResponse<HomeSlider>();
            try
            {
                response.Entity = homeSliderRepository.Update(entity);
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

