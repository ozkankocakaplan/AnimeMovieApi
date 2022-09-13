﻿using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Models;
using AnimeMovie.Entites;
namespace AnimeMovie.Business.Abstract
{
    public interface IUsersService : IService<Users>
    {
        ServiceResponse<UserModel> login(string userName, string password);
        ServiceResponse<Users> updateUserBanned(int userID);
        ServiceResponse<Users> updateImage(string imgUrl, int userID);
        ServiceResponse<Users> updatePassword(string currentPassword, string newPassword, int userID);
        ServiceResponse<Users> updateUserName(string userName, int userID);
    }
}
