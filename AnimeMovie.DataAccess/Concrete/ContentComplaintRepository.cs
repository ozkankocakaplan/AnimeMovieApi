using System;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class ContentComplaintRepository : GenericRepository<ContentComplaint>, IContentComplaintRepository
    {
        public ContentComplaintRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

