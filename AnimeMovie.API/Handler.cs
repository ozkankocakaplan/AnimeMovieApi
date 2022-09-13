using System;
using System.Security.Claims;

namespace AnimeMovie.API
{
	public static class Handler
	{
		public static int UserID(HttpContext httpContext)
		{
            var identity = (ClaimsIdentity)httpContext.User.Identity;
            int? id = Convert.ToInt32(identity.Claims.Where(x => x.Type == "ID").Select(x => x.Value).SingleOrDefault());
            if (id != null)
                return (int)id;
            return 0;
        }
        public static int RolType(HttpContext httpContext)
        {
            var identity = (ClaimsIdentity)httpContext.User.Identity;
            int? id = Convert.ToInt32(identity.Claims.Where(x => x.Type == "Role").Select(x => x.Value).SingleOrDefault());
            if (id != null)
                return (int)id;
            return 0;
        }
    }
}

