using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class UserForgotPasswordRepository : GenericRepository<UserForgotPassword>, IUserForgotPasswordRepository
    {
        public UserForgotPasswordRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

