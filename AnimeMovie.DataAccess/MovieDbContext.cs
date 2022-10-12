using System;
using Microsoft.EntityFrameworkCore;
using AnimeMovie.Entites;
namespace AnimeMovie.DataAccess
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("server=127.0.0.1,1433;database=AnimeMovieDB;User=Sa;password=3461663Oo.");
        //}

        public DbSet<Anime> Animes { get; set; }
        public DbSet<AnimeEpisodes> AnimeEpisodes { get; set; }
        public DbSet<AnimeList> AnimeLists { get; set; }
        public DbSet<AnimeOfTheWeek> AnimeOfTheWeeks { get; set; }
        public DbSet<AnimeRating> AnimeRatings { get; set; }
        public DbSet<AnimeSeason> AnimeSeasons { get; set; }
        public DbSet<AnimeSeasonMusic> AnimeSeasonMusics { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<ComplaintList> ComplaintLists { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactSubject> ContactSubjects { get; set; }
        public DbSet<ContentComplaint> ContentComplaints { get; set; }
        public DbSet<Episodes> Episodes { get; set; }
        public DbSet<FanArt> FanArts { get; set; }
        public DbSet<HomeSlider> HomeSliders { get; set; }
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<MangaEpisodeContent> MangaEpisodeContents { get; set; }
        public DbSet<MangaEpisodes> MangaEpisodes { get; set; }
        public DbSet<MangaList> MangaLists { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rosette> Rosettes { get; set; }
        public DbSet<SiteDescription> SiteDescription { get; set; }
        public DbSet<SocialMediaAccount> socialMediaAccounts { get; set; }
        public DbSet<UserBlockList> UserBlockLists { get; set; }
        public DbSet<UserEmailVertification> UserEmailVertifications { get; set; }
        public DbSet<UserForgotPassword> UserForgotPasswords { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistories { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<UserRosette> UserRosettes { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<AnimeImages> AnimeImages { get; set; }
        public DbSet<MangaImages> MangaImages { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<RosetteContent> RosetteContents { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<UserList> userLists { get; set; }
        public DbSet<UserListContents> UserListContents { get; set; }
    }
}

