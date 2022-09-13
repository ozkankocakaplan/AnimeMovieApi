using System;
using System.Linq.Expressions;
using AnimeMovie.Entites;

namespace AnimeMovie.Business
{
	public interface IService<T>
	{
		ServiceResponse<T> add(T entity);
		ServiceResponse<T> update(T entity);
		ServiceResponse<T> get(Expression<Func<T, bool>> expression);
        ServiceResponse<T> getList();
        ServiceResponse<T> getList(Expression<Func<T, bool>> expression);
        ServiceResponse<T> delete(Expression<Func<T, bool>> expression);
    }
}

