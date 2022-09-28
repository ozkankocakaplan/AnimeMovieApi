using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        private readonly MovieDbContext movieDbContext;
        public UsersRepository(MovieDbContext movieDb) : base(movieDb)
        {
            movieDbContext = movieDb;
        }

        public Users addUser(Users user)
        {
            var userNameCheck = get(x => x.UserName == user.UserName);
            if (userNameCheck == null)
            {
                Create(user);
                return user;
            }
            return null;
        }

        public Users updateEmail(string email, int userID)
        {
            var _user = get(x => x.ID == userID);
            if (_user != null)
            {
                _user.Email = email;
                movieDbContext.Users.Attach(_user);
                movieDbContext.Entry(_user).Property(x => x.Email).IsModified = true;
                movieDbContext.SaveChanges();
                return _user;
            }
            return null;
        }

        public Users updateImage(string imgUrl, int userID)
        {
            var user = get(x => x.ID == userID);
            if (user != null)
            {
                user.Image = imgUrl;
                movieDbContext.Users.Attach(user);
                movieDbContext.Entry(user).Property(x => x.Image).IsModified = true;
                movieDbContext.SaveChanges();
            }
            return user;
        }

        public Users updatePassword(string currentPassword, string newPassword, int userID)
        {
            var user = get(x => x.ID == userID);
            if (user != null)
            {
                if (user.Password == currentPassword)
                {
                    user.Password = newPassword;
                    movieDbContext.Users.Attach(user);
                    movieDbContext.Entry(user).Property(x => x.Password).IsModified = true;
                    movieDbContext.SaveChanges();
                }
                else
                {
                    return null;
                }
            }
            return user;
        }

        public Users updateUserBanned(int userID)
        {
            var user = get(x => x.ID == userID);
            if (user != null)
            {
                user.isBanned = !user.isBanned;
                movieDbContext.Users.Attach(user);
                movieDbContext.Entry(user).Property(x => x.isBanned).IsModified = true;
                movieDbContext.SaveChanges();
            }
            return user;
        }

        public Users updateUserInfo(string nameSurname, string userName, string seoUrl, int userID)
        {
            var user = get(x => x.ID == userID);
            if (user != null)
            {
                user.NameSurname = nameSurname;
                user.UserName = userName;
                user.SeoUrl = seoUrl;
                movieDbContext.Users.Attach(user);
                movieDbContext.Entry(user).Property(x => x.UserName).IsModified = true;
                movieDbContext.Entry(user).Property(x => x.NameSurname).IsModified = true;
                movieDbContext.Entry(user).Property(x => x.SeoUrl).IsModified = true;
                movieDbContext.SaveChanges();
            }
            return user;
        }

        public Users updateUserName(string userName, int userID)
        {
            var userNameCheck = get(x => x.UserName == userName);
            if (userNameCheck == null)
            {
                var user = get(x => x.ID == userID);
                if (user != null)
                {
                    user.UserName = userName;
                    movieDbContext.Users.Attach(user);
                    movieDbContext.Entry(user).Property(x => x.UserName).IsModified = true;
                    movieDbContext.SaveChanges();
                    return user;
                }
            }
            return null;
        }
    }
}

