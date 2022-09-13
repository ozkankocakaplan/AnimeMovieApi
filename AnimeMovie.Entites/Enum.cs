using System;
namespace AnimeMovie.Entites
{
    public enum Type
    {
        Anime = 1,
        Manga = 2,
        FanArt = 3,
        Comment = 4
    }
    public enum RoleType
    {
        Admin = 1,
        User = 2,
        Moderator = 3
    }
    public enum Status
    {
        Approved = 1,
        NotApproved = 2,
        Continues = 3,
        Completed = 4
    }
    public enum VideoType
    {
        AnimeSeries = 1,
        AnimeMovie = 2
    }
    public enum ComplaintType
    {
        Video = 1,
        Image = 2
    }
    public enum AnimeStatus
    {
        IWatched = 1,
        IWillWatch = 2,
        Watching = 3
    }
    public enum MangaStatus
    {
        IRead = 1,
        IWillRead = 2,
        Reading = 3
    }
    public enum NotificationType
    {
        Comments = 1,
        Message = 2,
        Rosette = 3,
        Anime = 4,
        Manga = 5,
        UserWarning = 6
    }
}

