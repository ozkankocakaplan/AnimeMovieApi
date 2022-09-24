using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class RosetteContentManager : IRosetteContentService
    {
        private readonly IRosetteContentRepository rosetteContentRepository;
        public RosetteContentManager(IRosetteContentRepository rosetteContent)
        {
            rosetteContentRepository = rosetteContent;
        }

        public ServiceResponse<RosetteContent> add(RosetteContent entity)
        {
            var response = new ServiceResponse<RosetteContent>();
            try
            {
                response.Entity = rosetteContentRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<RosetteContent> delete(Expression<Func<RosetteContent, bool>> expression)
        {
            var response = new ServiceResponse<RosetteContent>();
            try
            {
                response.IsSuccessful = rosetteContentRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<RosetteContent> get(Expression<Func<RosetteContent, bool>> expression)
        {
            var response = new ServiceResponse<RosetteContent>();
            try
            {
                response.Entity = rosetteContentRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<RosetteContent> getList()
        {
            var response = new ServiceResponse<RosetteContent>();
            try
            {
                response.List = rosetteContentRepository.GetAll().ToList();
                response.Count = rosetteContentRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<RosetteContent> getList(Expression<Func<RosetteContent, bool>> expression)
        {
            var response = new ServiceResponse<RosetteContent>();
            try
            {
                var list = rosetteContentRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<RosetteContent> update(RosetteContent entity)
        {
            var response = new ServiceResponse<RosetteContent>();
            try
            {
                response.Entity = rosetteContentRepository.Update(entity);
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

