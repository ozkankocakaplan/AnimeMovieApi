using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactRepository contactRepository;
        public ContactManager(IContactRepository contact)
        {
            contactRepository = contact;
        }

        public ServiceResponse<Contact> add(Contact entity)
        {
            var response = new ServiceResponse<Contact>();
            try
            {
                response.Entity = contactRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Contact> delete(Expression<Func<Contact, bool>> expression)
        {
            var response = new ServiceResponse<Contact>();
            try
            {
                contactRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Contact> get(Expression<Func<Contact, bool>> expression)
        {
            var response = new ServiceResponse<Contact>();
            try
            {
                response.Entity = contactRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Contact> getList()
        {
            var response = new ServiceResponse<Contact>();
            try
            {
                response.List = contactRepository.GetAll().ToList();
                response.Count = contactRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Contact> getList(Expression<Func<Contact, bool>> expression)
        {
            var response = new ServiceResponse<Contact>();
            try
            {
                var list = contactRepository.Table.Where(expression).ToList();
                response.List = list;
                response.Count = list.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Contact> getListPagined(int pageNo, int showCount)
        {
            var response = new ServiceResponse<Contact>();
            try
            {
                var list = contactRepository.GetAll();
                response.List = list.Skip((pageNo - 1) * showCount).Take(showCount).ToList();
                int page = 0;
                var totalContact = list.Count();
                if (totalContact % showCount > 0)
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

        public ServiceResponse<Contact> update(Contact entity)
        {
            var response = new ServiceResponse<Contact>();
            try
            {
                response.Entity = contactRepository.Update(entity);
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

