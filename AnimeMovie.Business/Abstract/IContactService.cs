using System;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Abstract
{
    public interface IContactService : IService<Contact>
    {
        ServiceResponse<Contact> getListPagined(int pageNo, int showCount);
    }
}

