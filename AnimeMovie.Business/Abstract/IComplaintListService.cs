using System;
using AnimeMovie.Business.Models;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Abstract
{
    public interface IComplaintListService : IService<ComplaintList>
    {
        ServiceResponse<ComplaintListModels> getComplaintListModels();
    }
}

