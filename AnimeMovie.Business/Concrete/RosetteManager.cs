using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class RosetteManager : IRosetteService
    {
        private readonly IRosetteRepository rosetteRepository;
        public RosetteManager(IRosetteRepository rosette)
        {
            rosetteRepository = rosette;
        }

        public ServiceResponse<Rosette> add(Rosette entity)
        {
            var response = new ServiceResponse<Rosette>();
            try
            {
                response.Entity = rosetteRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Rosette> delete(Expression<Func<Rosette, bool>> expression)
        {
            var response = new ServiceResponse<Rosette>();
            try
            {
                response.IsSuccessful = rosetteRepository.Delete(expression);
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Rosette> get(Expression<Func<Rosette, bool>> expression)
        {
            var response = new ServiceResponse<Rosette>();
            try
            {
                response.Entity = rosetteRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Rosette> getList()
        {
            var response = new ServiceResponse<Rosette>();
            try
            {
                response.List = rosetteRepository.GetAll().ToList();
                response.Count = rosetteRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Rosette> getList(Expression<Func<Rosette, bool>> expression)
        {
            var response = new ServiceResponse<Rosette>();
            try
            {
                var list = rosetteRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<Rosette> update(Rosette entity)
        {
            var response = new ServiceResponse<Rosette>();
            try
            {
                response.Entity = rosetteRepository.Update(entity);
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

