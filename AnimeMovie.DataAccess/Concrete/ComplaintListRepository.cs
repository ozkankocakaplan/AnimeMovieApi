using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class ComplaintListRepository : GenericRepository<ComplaintList>, IComplaintListRepository
    {
        public ComplaintListRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

