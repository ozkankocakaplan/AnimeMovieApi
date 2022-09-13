using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class RosetteManager : IRosetteService
    {
        public RosetteManager()
        {
        }

        public ServiceResponse<Rosette> add(Rosette entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<Rosette> delete(Expression<Func<Rosette, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<Rosette> get(Expression<Func<Rosette, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<Rosette> getList()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<Rosette> getList(Expression<Func<Rosette, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<Rosette> update(Rosette entity)
        {
            throw new NotImplementedException();
        }
    }
}

