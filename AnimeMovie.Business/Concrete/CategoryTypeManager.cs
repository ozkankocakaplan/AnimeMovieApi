using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class CategoryTypeManager : ICategoryTypeService
    {
        public ICategoryTypeRepository categoryTypeRepository { get; set; }
        public CategoryTypeManager(ICategoryTypeRepository categoryType)
        {
            categoryTypeRepository = categoryType;
        }

        public ServiceResponse<CategoryType> add(CategoryType entity)
        {
            var response = new ServiceResponse<CategoryType>();
            try
            {
                response.Entity = categoryTypeRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<CategoryType> delete(Expression<Func<CategoryType, bool>> expression)
        {
            var response = new ServiceResponse<CategoryType>();
            try
            {
                response.IsSuccessful = categoryTypeRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<CategoryType> get(Expression<Func<CategoryType, bool>> expression)
        {
            var response = new ServiceResponse<CategoryType>();
            try
            {
                response.Entity = categoryTypeRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<CategoryType> getList()
        {
            var response = new ServiceResponse<CategoryType>();
            try
            {
                response.List = categoryTypeRepository.GetAll().ToList();
                response.Count = categoryTypeRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<CategoryType> getList(Expression<Func<CategoryType, bool>> expression)
        {
            var response = new ServiceResponse<CategoryType>();
            try
            {
                var list = categoryTypeRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<CategoryType> update(CategoryType entity)
        {
            var response = new ServiceResponse<CategoryType>();
            try
            {
                response.Entity = categoryTypeRepository.Update(entity);
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

