using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class MangaEpisodesManager : IMangaEpisodesService
    {
        public MangaEpisodesManager()
        {
        }

        public ServiceResponse<MangaEpisodes> add(MangaEpisodes entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<MangaEpisodes> delete(Expression<Func<MangaEpisodes, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<MangaEpisodes> get(Expression<Func<MangaEpisodes, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<MangaEpisodes> getList()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<MangaEpisodes> getList(Expression<Func<MangaEpisodes, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<MangaEpisodes> update(MangaEpisodes entity)
        {
            throw new NotImplementedException();
        }
    }
}

