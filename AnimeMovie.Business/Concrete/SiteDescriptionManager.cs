using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class SiteDescriptionManager : ISiteDescriptionService
    {
        public SiteDescriptionManager()
        {
        }

        public ServiceResponse<SiteDescription> add(SiteDescription entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<SiteDescription> delete(Expression<Func<SiteDescription, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<SiteDescription> get(Expression<Func<SiteDescription, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<SiteDescription> getList()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<SiteDescription> getList(Expression<Func<SiteDescription, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<SiteDescription> update(SiteDescription entity)
        {
            throw new NotImplementedException();
        }
    }
}

