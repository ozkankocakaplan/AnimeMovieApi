using System;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Abstract
{
    public interface IUserListService : IService<UserList>
    {
        ServiceResponse<UserList> addUserList(UserList userList, List<UserListContents> userListContents);
    }
}

