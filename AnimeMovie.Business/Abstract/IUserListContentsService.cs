using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Models;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Abstract
{
    public interface IUserListContentsService : IService<UserListContents>
    {
        ServiceResponse<UserListModels> getUserListModels(Expression<Func<UserListContents, bool>> expression);
        ServiceResponse<UserListContentsModels> getUserContentListModels();
    }
}

