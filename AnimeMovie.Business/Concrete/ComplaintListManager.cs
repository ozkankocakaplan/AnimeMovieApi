using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Business.Models;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AnimeMovie.Business.Concrete
{
    public class ComplaintListManager : IComplaintListService
    {
        private readonly IComplaintListRepository complaintListRepository;
        private readonly IUsersRepository usersRepository;
        public ComplaintListManager(IComplaintListRepository complaintList, IUsersRepository users)
        {
            usersRepository = users;
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

        public ServiceResponse<ComplaintListModels> getComplaintListModels()
        {
            var response = new ServiceResponse<ComplaintListModels>();
            try
            {
                List<ComplaintListModels> modelList = new List<ComplaintListModels>();

                var list = complaintListRepository.GetAll().ToList();
                foreach (var item in list)
                {
                    ComplaintListModels model = new ComplaintListModels(item);
                    model.ComplainantUser = usersRepository.get(x => x.ID == item.ComplainantID);
                    model.Users = usersRepository.get(x => x.ID == item.UserID);
                    modelList.Add(model);
                }
                response.List = modelList;
                response.Count = modelList.Count();
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
                response.Count = complaintListRepository.Count();
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
                var list = complaintListRepository.TableNoTracking.Where(expression).ToList();
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

