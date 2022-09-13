using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class CategoriesManager : ICategoriesService
    {
        private readonly ICategoriesRepository categoriesRepository;
        public CategoriesManager(ICategoriesRepository categories)
        {
            categoriesRepository = categories;
        }

        public ServiceResponse<Categories> add(Categories entity)
        {
            var response = new ServiceResponse<Categories>();
            try
            {
                response.Entity = categoriesRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Categories> delete(Expression<Func<Categories, bool>> expression)
        {
            var response = new ServiceResponse<Categories>();
            try
            {
                categoriesRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Categories> get(Expression<Func<Categories, bool>> expression)
        {
            var response = new ServiceResponse<Categories>();
            try
            {
                response.Entity = categoriesRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Categories> getList()
        {
            var response = new ServiceResponse<Categories>();
            try
            {
                response.List = categoriesRepository.GetAll().ToList();
                response.Count = categoriesRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Categories> getList(Expression<Func<Categories, bool>> expression)
        {
            var response = new ServiceResponse<Categories>();
            try
            {
                var list = categoriesRepository.Table.Where(expression).ToList();
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

        public ServiceResponse<Categories> update(Categories entity)
        {
            var response = new ServiceResponse<Categories>();
            try
            {
                response.Entity = categoriesRepository.Update(entity);
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

