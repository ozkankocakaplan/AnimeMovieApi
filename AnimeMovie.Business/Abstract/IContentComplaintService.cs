using System;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Abstract
{
    public interface IContentComplaintService : IService<ContentComplaint>
    {
        ServiceResponse<ContentComplaint> getListPagined(int pageNo, int showCount);
    }
}

