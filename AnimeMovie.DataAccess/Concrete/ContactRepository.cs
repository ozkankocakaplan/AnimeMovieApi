using System;
using System.Diagnostics.Contracts;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.DataAccess.Concrete
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(MovieDbContext movieDb) : base(movieDb)
        {
        }
    }
}

