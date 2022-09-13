﻿using System;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Abstract
{
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Users addUser(Users user);
        Users updateUserBanned(int userID);
        Users updateImage(string imgUrl, int userID);
        Users updatePassword(string currentPassword, string newPassword, int userID);
        Users updateUserName(string userName, int userID);
    }
}

