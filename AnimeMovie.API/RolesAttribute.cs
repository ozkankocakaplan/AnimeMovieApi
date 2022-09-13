using System;
using Microsoft.AspNetCore.Authorization;

namespace AnimeMovie.API
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class RolesAttribute : AuthorizeAttribute
    {
        public const string Admin = "1";
        public const string User = "2";
        public const string Moderator = "3";
        public const string AdminOrModerator = Admin + "," + Moderator;
        public const string All = Admin + "," + User + "," + Moderator;
        public RolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles.Select(x => x.ToString()));
        }
    }
}

