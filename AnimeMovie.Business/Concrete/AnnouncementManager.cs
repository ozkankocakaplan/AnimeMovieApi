using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class AnnouncementManager : IAnnouncementService
    {
        private readonly IAnnouncementRepository announcementRepository;
        public AnnouncementManager(IAnnouncementRepository announcement)
        {
            announcementRepository = announcement;
        }

        public ServiceResponse<Announcement> add(Announcement entity)
        {
            var response = new ServiceResponse<Announcement>();
            try
            {
                response.Entity = announcementRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Announcement> delete(Expression<Func<Announcement, bool>> expression)
        {
            var response = new ServiceResponse<Announcement>();
            try
            {
                announcementRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Announcement> get(Expression<Func<Announcement, bool>> expression)
        {
            var response = new ServiceResponse<Announcement>();
            try
            {
                response.Entity = announcementRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Announcement> getList()
        {
            var response = new ServiceResponse<Announcement>();
            try
            {
                response.List = announcementRepository.GetAll().ToList();
                response.Count = announcementRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.ExceptionMessage = ex.ToString();
                response.HasExceptionError = true;
            }
            return response;
        }

        public ServiceResponse<Announcement> getList(Expression<Func<Announcement, bool>> expression)
        {
            var response = new ServiceResponse<Announcement>();
            try
            {
                var list = announcementRepository.Table.Where(expression).ToList();
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

        public ServiceResponse<Announcement> update(Announcement entity)
        {
            var response = new ServiceResponse<Announcement>();
            try
            {
                response.Entity = announcementRepository.Update(entity);
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

