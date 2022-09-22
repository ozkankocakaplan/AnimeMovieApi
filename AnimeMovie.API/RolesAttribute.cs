using System;
using Microsoft.AspNetCore.Authorization;

namespace AnimeMovie.API
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class RolesAttribute : AuthorizeAttribute
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Moderator = "Moderator";
        public const string AdminOrModerator = Admin + "," + Moderator;
        public const string All = Admin + "," + User + "," + Moderator;
        public RolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles.Select(x => x.ToString()));
        }
    }
}

