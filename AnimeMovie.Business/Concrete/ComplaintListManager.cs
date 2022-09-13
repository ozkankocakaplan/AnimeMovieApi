using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class ComplaintListManager : IComplaintListService
    {
        private readonly IComplaintListRepository complaintListRepository;
        public ComplaintListManager(IComplaintListRepository complaintList)
        {
            complaintListRepository = complaintList;
        }

        public ServiceResponse<ComplaintList> add(ComplaintList entity)
        {
            var response = new ServiceResponse<ComplaintList>();
            try
            {
                response.Entity = complaintListRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ComplaintList> delete(Expression<Func<ComplaintList, bool>> expression)
        {
            var response = new ServiceResponse<ComplaintList>();
            try
            {
                complaintListRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ComplaintList> get(Expression<Func<ComplaintList, bool>> expression)
        {
            var response = new ServiceResponse<ComplaintList>();
            try
            {
                response.Entity = complaintListRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ComplaintList> getList()
        {
            var response = new ServiceResponse<ComplaintList>();
            try
            {
                response.List = complaintListRepository.GetAll().ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<ComplaintList> getList(Expression<Func<ComplaintList, bool>> expression)
        {
            var response = new ServiceResponse<ComplaintList>();
            try
            {
                var list = complaintListRepository.Table.Where(expression).ToList();
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

        public ServiceResponse<ComplaintList> update(ComplaintList entity)
        {
            var response = new ServiceResponse<ComplaintList>();
            try
            {
                var list = complaintListRepository.Update(entity);
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

