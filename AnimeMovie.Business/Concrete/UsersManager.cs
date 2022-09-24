using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Business.Helper;
using AnimeMovie.Business.Models;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AnimeMovie.Business.Concrete
{
    public class UsersManager : IUsersService
    {
        ISeoUrl seoUrl;
        IUsersRepository usersRepository;
        IConfiguration configuration;
        public UsersManager(ISeoUrl seo, IConfiguration conf, IUsersRepository users)
        {
            configuration = conf;
            seoUrl = seo;
            usersRepository = users;
        }

        public ServiceResponse<Users> add(Users users)
        {
            var response = new ServiceResponse<Users>();
            try
            {
                users.SeoUrl = seoUrl.createUserLink(users);
                var user = usersRepository.addUser(users);
                if (user != null)
                {
                    response.Entity = user;
                    response.IsSuccessful = true;
                }
                else
                {
                    response.HasExceptionError = true;
                    response.ExceptionMessage = "Bu kullanıcı adı kullanılıyor";
                }

            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Users> delete(Expression<Func<Users, bool>> expression)
        {
            var response = new ServiceResponse<Users>();
            return response;
        }

        public ServiceResponse<Users> get(Expression<Func<Users, bool>> expression)
        {
            var response = new ServiceResponse<Users>();
            try
            {
                response.Entity = usersRepository.get(expression);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Users> getList()
        {
            var response = new ServiceResponse<Users>();
            try
            {
                response.List = usersRepository.GetAll().ToList();
                response.Count = usersRepository.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Users> getList(Expression<Func<Users, bool>> expression)
        {
            var response = new ServiceResponse<Users>();
            try
            {
                var list = usersRepository.Table.Where(expression).ToList();
                response.List = list;
                response.Count = list.Count();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Users> getPaginatedUsers(int pageNo, int ShowCount)
        {
            var response = new ServiceResponse<Users>();
            try
            {
                var list = usersRepository.TableNoTracking.ToList();
                response.List = list.Skip((pageNo - 1) * ShowCount).Take(ShowCount).ToList();
                int page = 0;
                var totalUsers = list.Count();
                if (totalUsers % ShowCount > 0)
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

        public ServiceResponse<UserModel> login(string userName, string password)
        {
            var response = new ServiceResponse<UserModel>();
            try
            {
                var user = usersRepository.get(x => x.Email == userName && x.Password == password);
                if (user != null)
                {
                    if (!user.isBanned)
                    {
                        UserModel tokenWithUser = new UserModel(user);
                        tokenWithUser.Token = GenerateJsonWebToken(user);
                        response.Entity = tokenWithUser;
                        response.IsSuccessful = true;
                    }
                    else
                    {
                        response.HasExceptionError = true;
                        response.ExceptionMessage = "Bu Kullanıcı Banlı";
                    }
                }
                else
                {
                    response.ExceptionMessage = "Kullanıcı adı veya şifreniz yanlış";
                    response.HasExceptionError = true;
                }


            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Users> update(Users entity)
        {
            throw new NullReferenceException();
        }

        public ServiceResponse<Users> updateImage(string imgUrl, int userID)
        {
            var response = new ServiceResponse<Users>();
            try
            {
                var user = usersRepository.updateImage(imgUrl, userID);
                if (user != null)
                {
                    response.Entity = user;
                    response.IsSuccessful = true;
                }
                else
                {
                    response.HasExceptionError = true;
                    response.ExceptionMessage = "Kullanıcı Bulunamadı";
                }
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Users> updatePassword(string currentPassword, string newPassword, int userID)
        {
            var response = new ServiceResponse<Users>();
            try
            {
                var user = usersRepository.updatePassword(currentPassword, newPassword, userID);
                if (user != null)
                {
                    response.Entity = user;
                    response.IsSuccessful = true;
                }
                else
                {
                    response.ExceptionMessage = "Mevcut şifreniz yanlış";
                    response.HasExceptionError = true;
                }

            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Users> updateUserBanned(int userID)
        {
            var response = new ServiceResponse<Users>();
            try
            {
                var user = usersRepository.updateUserBanned(userID);
                if (user != null)
                {
                    response.Entity = user;
                    response.IsSuccessful = true;
                }
                else
                {
                    response.HasExceptionError = true;
                    response.ExceptionMessage = "Kullanıcı Bulunamadı";
                }
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        public ServiceResponse<Users> updateUserName(string userName, int userID)
        {
            var response = new ServiceResponse<Users>();
            try
            {
                var user = usersRepository.updateUserName(userName, userID);
                if (user != null)
                {
                    response.Entity = user;
                    response.IsSuccessful = true;
                }
                else
                {
                    response.HasExceptionError = true;
                    response.ExceptionMessage = "Bu kullanıcı adı kullanılıyor";
                }
            }
            catch (Exception ex)
            {
                response.HasExceptionError = true;
                response.ExceptionMessage = ex.ToString();
            }
            return response;
        }

        private string GenerateJsonWebToken(Users users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var claims = new[] {
                 new Claim(ClaimTypes.Role,users.RoleType == RoleType.Admin ? "Admin" : users.RoleType == RoleType.User ? "User" : "Moderator"),
                 new Claim("ID",users.ID.ToString()),
                 new Claim("Role",users.RoleType.ToString())
            };
            var creadentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddYears(2),
                signingCredentials: creadentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

