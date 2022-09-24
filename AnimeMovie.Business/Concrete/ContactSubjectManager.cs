using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class ContactSubjectManager : IContactSubjectService
    {
        private readonly IContactSubjectRepository contactSubjectRepository;
        public ContactSubjectManager(IContactSubjectRepository contactSubject)
        {
            contactSubjectRepository = contactSubject;
        }

        public ServiceResponse<ContactSubject> add(ContactSubject entity)
        {
            var response = new ServiceResponse<ContactSubject>();
            try
            {
                response.Entity = contactSubjectRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ContactSubject> delete(Expression<Func<ContactSubject, bool>> expression)
        {
            var response = new ServiceResponse<ContactSubject>();
            try
            {
                contactSubjectRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ContactSubject> get(Expression<Func<ContactSubject, bool>> expression)
        {
            var response = new ServiceResponse<ContactSubject>();
            try
            {
                response.Entity = contactSubjectRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ContactSubject> getList()
        {
            var response = new ServiceResponse<ContactSubject>();
            try
            {
                response.List = contactSubjectRepository.TableNoTracking.ToList();
                response.Count = contactSubjectRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ContactSubject> getList(Expression<Func<ContactSubject, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<ContactSubject> update(ContactSubject entity)
        {
            var response = new ServiceResponse<ContactSubject>();
            try
            {
                response.Entity = contactSubjectRepository.Update(entity);
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

