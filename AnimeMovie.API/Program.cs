using System.Text;
using AnimeMovie.API;
using AnimeMovie.API.Hubs;
using AnimeMovie.Business;
using AnimeMovie.Business.Abstract;
using AnimeMovie.Business.Concrete;
using AnimeMovie.Business.Helper;
using AnimeMovie.DataAccess;
using AnimeMovie.DataAccess.Abstract;
using AnimeMovie.DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://192.168.2.175:37323");


builder.Services.AddSignalR(ayar =>
{
    ayar.EnableDetailedErrors = true;
    ayar.KeepAliveInterval = TimeSpan.FromSeconds(10);
});

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer((options) =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Path.ToString().StartsWith("/userHub"))
                {
                    context.Token = context.Request.Query["access_token"];
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();
builder.Services.AddDbContext<MovieDbContext>(x => x.UseSqlServer(builder.Configuration["ConnectionString"]));
builder.Services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ISeoUrl, SeoUrl>();



builder.Services.AddScoped<IAnimeEpisodesRepository, AnimeEpisodesRepository>();
builder.Services.AddScoped<IAnimeEpisodesService, AnimeEpisodesManager>();

builder.Services.AddScoped<IAnimeListRepository, AnimeListRepository>();
builder.Services.AddScoped<IAnimeListService, AnimeListManager>();

builder.Services.AddScoped<IMovieTheWeekRepository, MovieTheWeekRepository>();
builder.Services.AddScoped<IMovieTheWeekService, MovieOfTheWeekManager>();

builder.Services.AddScoped<IRatingsRepository, RatingsRepository>();
builder.Services.AddScoped<IRatingsService, RatingsManager>();

builder.Services.AddScoped<IAnimeSeasonMusicRepository, AnimeSeasonMusicRepository>();
builder.Services.AddScoped<IAnimeSeasonMusicService, AnimeSeasonMusicManager>();

builder.Services.AddScoped<IAnimeSeasonRepository, AnimeSeasonRepository>();
builder.Services.AddScoped<IAnimeSeasonService, AnimeSeasonManager>();

builder.Services.AddScoped<IAnimeSeasonRepository, AnimeSeasonRepository>();
builder.Services.AddScoped<IAnimeSeasonService, AnimeSeasonManager>();

builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<IAnimeService, AnimeManager>();

builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementManager>();

builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICategoriesService, CategoriesManager>();

builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<ICommentsService, CommentsManager>();

builder.Services.AddScoped<IComplaintListRepository, ComplaintListRepository>();
builder.Services.AddScoped<IComplaintListService, ComplaintListManager>();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactManager>();

builder.Services.AddScoped<IContactSubjectRepository, ContactSubjectRepository>();
builder.Services.AddScoped<IContactSubjectService, ContactSubjectManager>();

builder.Services.AddScoped<IContentComplaintRepository, ContentComplaintRepository>();
builder.Services.AddScoped<IContentComplaintService, ContentComplaintManager>();

builder.Services.AddScoped<IEpisodesRepository, EpisodesRepository>();
builder.Services.AddScoped<IEpisodesService, EpisodesManager>();

builder.Services.AddScoped<IFanArtRepository, FanArtRepository>();
builder.Services.AddScoped<IFanArtService, FanArtManager>();

builder.Services.AddScoped<IHomeSliderRepository, HomeSliderRepository>();
builder.Services.AddScoped<IHomeSliderService, HomeSliderManager>();

builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ILikeService, LikeManager>();

builder.Services.AddScoped<IMangaEpisodeContentRepository, MangaEpisodeContentRepository>();
builder.Services.AddScoped<IMangaEpisodeContentService, MangaEpisodeContentManager>();

builder.Services.AddScoped<IMangaEpisodesRepository, MangaEpisodesRepository>();
builder.Services.AddScoped<IMangaEpisodesService, MangaEpisodesManager>();

builder.Services.AddScoped<IMangaListRepository, MangaListRepository>();
builder.Services.AddScoped<IMangaListService, MangaListManager>();

builder.Services.AddScoped<IMangaRepository, MangaRepository>();
builder.Services.AddScoped<IMangaService, MangaManager>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationManager>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewManager>();

builder.Services.AddScoped<IRosetteRepository, RosetteRepository>();
builder.Services.AddScoped<IRosetteService, RosetteManager>();

builder.Services.AddScoped<ISiteDescriptionRepository, SiteDescriptionRepository>();
builder.Services.AddScoped<ISiteDescriptionService, SiteDescriptionManager>();

builder.Services.AddScoped<ISocialMediaAccountRepository, SocialMediaAccountRepository>();
builder.Services.AddScoped<ISocialMediaAccountService, SocialMediaAccountManager>();

builder.Services.AddScoped<IUserBlockListRepository, UserBlockListRepository>();
builder.Services.AddScoped<IUserBlockListService, UserBlockListManager>();

builder.Services.AddScoped<IUserEmailVertificationRepository, UserEmailVertificationRepository>();
builder.Services.AddScoped<IUserEmailVertificationService, UserEmailVertificationManager>();

builder.Services.AddScoped<IUserForgotPasswordRepository, UserForgotPasswordRepository>();
builder.Services.AddScoped<IUserForgotPasswordService, UserForgotPasswordManager>();

builder.Services.AddScoped<IUserLoginHistoryRepository, UserLoginHistoryRepository>();
builder.Services.AddScoped<IUserLoginHistoryService, UserLoginHistoryManager>();

builder.Services.AddScoped<IUserMessageRepository, UserMessageRepository>();
builder.Services.AddScoped<IUserMessageService, UserMessageManager>();

builder.Services.AddScoped<IUserRosetteRepository, UserRosetteRepository>();
builder.Services.AddScoped<IUserRosetteService, UserRosetteManager>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersManager>();

builder.Services.AddScoped<ISocialMediaAccountRepository, SocialMediaAccountRepository>();
builder.Services.AddScoped<ISocialMediaAccountService, SocialMediaAccountManager>();

builder.Services.AddScoped<ICategoryTypeRepository, CategoryTypeRepository>();
builder.Services.AddScoped<ICategoryTypeService, CategoryTypeManager>();

builder.Services.AddScoped<IAnimeImageRepository, AnimeImageRepository>();
builder.Services.AddScoped<IAnimeImageService, AnimeImageManager>();

builder.Services.AddScoped<IMangaImageRepository, MangaImageRepository>();
builder.Services.AddScoped<IMangaImageService, MangaImageManager>();

builder.Services.AddScoped<IRosetteContentRepository, RosetteContentRepository>();
builder.Services.AddScoped<IRosetteContentService, RosetteContentManager>();

builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IFavoriteService, FavoriteManager>();

builder.Services.AddScoped<IUserListRepository, UserListRepository>();
builder.Services.AddScoped<IUserListService, UserListManager>();

builder.Services.AddScoped<IUserListContentsRepository, UserListContentsRepository>();
builder.Services.AddScoped<IUserListContentsService, UserListContentsManager>();

builder.Services.AddScoped<IContentNotificationRepository, ContentNotificatonRepository>();
builder.Services.AddScoped<IContentNotificationService, ContentNotificationManager>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
DataSeeding.Seed(app);
app.UseHttpsRedirection();
app.UseCors(builder =>
{
    builder.WithOrigins("https://admin.lycorisa.com")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
    builder.WithOrigins("http://localhost:3000")
   .AllowAnyHeader()
   .AllowAnyMethod()
   .AllowCredentials();
    builder.WithOrigins("http://localhost:3001")
  .AllowAnyHeader()
  .AllowAnyMethod()
  .AllowCredentials();
});
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<UserHub>("/userHub");
app.MapControllers();
app.UseStaticFiles();
app.Run();

