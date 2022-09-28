using System;
using System.Security.Claims;
using System.Text;

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
        public static string RandomData()
        {
            Random rnd = new Random();
            string data = "";
            int number, min = 65;
            char _char;
            for (int i = 0; i < 2; i++)
            {
                number = rnd.Next(min, min + 25);
                _char = Convert.ToChar(number);
                data += _char;
            }
            return data;
        }
        public static string createData()
        {

            Random rnd = new Random();
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomData());
            builder.Append(rnd.Next(100, 999));
            builder.Append(RandomData());
            return builder.ToString();
        }
    }
}

