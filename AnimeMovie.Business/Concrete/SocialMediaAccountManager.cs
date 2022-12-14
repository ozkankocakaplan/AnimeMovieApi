using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore;

namespace AnimeMovie.Business.Concrete
{
    public class SocialMediaAccountManager : ISocialMediaAccountService
    {
        private readonly ISocialMediaAccountRepository socialMediaAccountRepository;
        public SocialMediaAccountManager(ISocialMediaAccountRepository socialMediaAccount)
        {
            socialMediaAccountRepository = socialMediaAccount;
        }

        public ServiceResponse<SocialMediaAccount> add(SocialMediaAccount entity)
        {
            var response = new ServiceResponse<SocialMediaAccount>();
            try
            {
                response.Entity = socialMediaAccountRepository.Create(entity);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<SocialMediaAccount> delete(Expression<Func<SocialMediaAccount, bool>> expression)
        {
            var response = new ServiceResponse<SocialMediaAccount>();
            try
            {
                socialMediaAccountRepository.Delete(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<SocialMediaAccount> get(Expression<Func<SocialMediaAccount, bool>> expression)
        {
            var response = new ServiceResponse<SocialMediaAccount>();
            try
            {
                response.Entity = socialMediaAccountRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<SocialMediaAccount> getList()
        {
            var response = new ServiceResponse<SocialMediaAccount>();
            try
            {
                response.List = socialMediaAccountRepository.Table.Include(x => x.Users).ToList();
                response.Count = socialMediaAccountRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<SocialMediaAccount> getList(Expression<Func<SocialMediaAccount, bool>> expression)
        {
            var response = new ServiceResponse<SocialMediaAccount>();
            try
            {
                var list = socialMediaAccountRepository.TableNoTracking.Where(expression).ToList();
                response.Count = list.Count;
                response.List = list;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<SocialMediaAccount> update(SocialMediaAccount entity)
        {
            var response = new ServiceResponse<SocialMediaAccount>();
            try
            {
                response.Entity = socialMediaAccountRepository.Update(entity);
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

