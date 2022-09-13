using System;
using System.Linq.Expressions;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Entites;

namespace AnimeMovie.Business.Concrete
{
    public class MangaEpisodeContentManager : IMangaEpisodeContentService
    {
        public MangaEpisodeContentManager()
        {

        }

        public ServiceResponse<MangaEpisodeContent> add(MangaEpisodeContent entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<MangaEpisodeContent> delete(Expression<Func<MangaEpisodeContent, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<MangaEpisodeContent> get(Expression<Func<MangaEpisodeContent, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<MangaEpisodeContent> getList()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<MangaEpisodeContent> getList(Expression<Func<MangaEpisodeContent, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<MangaEpisodeContent> update(MangaEpisodeContent entity)
        {
            throw new NotImplementedException();
        }
    }
}

