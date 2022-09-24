using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class UserRosetteManager : IUserRosetteService
    {
        IUserRosetteRepository userRosetteRepository;
        public UserRosetteManager(IUserRosetteRepository userRosette)
        {
            userRosetteRepository = userRosette;
        }

        public ServiceResponse<UserRosette> add(UserRosette entity)
        {
            var response = new ServiceResponse<UserRosette>();
            try
            {
                response.Entity = userRosetteRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserRosette> delete(Expression<Func<UserRosette, bool>> expression)
        {
            var response = new ServiceResponse<UserRosette>();
            try
            {
                userRosetteRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserRosette> get(Expression<Func<UserRosette, bool>> expression)
        {
            var response = new ServiceResponse<UserRosette>();
            try
            {
                response.Entity = userRosetteRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserRosette> getList()
        {
            var response = new ServiceResponse<UserRosette>();
            try
            {
                response.List = userRosetteRepository.GetAll().ToList();
                response.Count = userRosetteRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<UserRosette> getList(Expression<Func<UserRosette, bool>> expression)
        {
            var response = new ServiceResponse<UserRosette>();
            try
            {
                var list = userRosetteRepository.TableNoTracking.Where(expression).ToList();
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

        public ServiceResponse<UserRosette> update(UserRosette entity)
        {
            var response = new ServiceResponse<UserRosette>();
            try
            {
                response.Entity = userRosetteRepository.Update(entity);
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

