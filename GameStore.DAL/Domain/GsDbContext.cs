using GameStore.DAL.Entities.Dictionaries;
using GameStore.DAL.Entities.Games;
using GameStore.DAL.Entities.History;
using GameStore.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.DAL.Domain
{
    public class GsDbContext : IdentityDbContext<AppUser>
    {
        public GsDbContext(DbContextOptions<GsDbContext> options) : base(options) { }

        #region DB SETS
       
        //-- Dictionaries
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<GameDeveloper> GameDevelopers { get; set; }
        public DbSet<GameLabel> GameLabels { get; set; }
        public DbSet<GameKeyStatus> GameKeyStatuses { get; set; }

        //-- Games
        public DbSet<Game> Games { get; set; }
        public DbSet<GameKey> GameKeys { get; set; }
        public DbSet<GameScreenshot> GameScreenshots { get; set; }
        public DbSet<GameReview> GameReviews { get; set; }

        //-- Identity
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        //-- History
        public DbSet<HistoryGameReviews> HistoryGameReviews { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region DB CREATION

            //-- Dictionaries
            builder.Entity<Genre>().ToTable("Dictionaries_Genres");
            builder.Entity<GamePlatform>().ToTable("Dictionaries_GamePlatforms");
            builder.Entity<GameDeveloper>().ToTable("Dictionaries_GameDevelopers");
            builder.Entity<GameLabel>().ToTable("Dictionaries_GameLabels");
            builder.Entity<GameKeyStatus>().ToTable("Dictionaries_GameKeyStatuses");

            //-- Games
            builder.Entity<Game>().ToTable("Games");
            builder.Entity<GameKey>().ToTable("Games_Keys");
            builder.Entity<GameScreenshot>().ToTable("Games_Screenshots");
            builder.Entity<GameReview>().ToTable("Games_Reviews");

            //-- Identity
            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<ShoppingCart>().ToTable("Users_ShoppingCarts");

            //-- History
            builder.Entity<HistoryGameReviews>().ToTable("History_GameReviews");
            #endregion

            #region SEED DATA

            //-- Dictionaries
            builder.Entity<Genre>().HasData(new Predefined.Dictionaries.PdGenres().ListGanres);
            builder.Entity<GamePlatform>().HasData(new Predefined.Dictionaries.PdGamePlatforms().ListGamePlatforms);
            builder.Entity<GameDeveloper>().HasData(new Predefined.Dictionaries.PdGameDevelopers().ListGameDevelopers);
            builder.Entity<GameLabel>().HasData(new Predefined.Dictionaries.PdGameLabels().Listlabels);
            builder.Entity<GameKeyStatus>().HasData(new Predefined.Dictionaries.PdGameKeyStatuses().ListGameKeyStatuses);

            // -- Identity
            builder.Entity<IdentityRole>().HasData(new Predefined.Identity.PdRoles().RoleList);
            #endregion
        }

        #region CREATION DEFAULT ACCOUNT
        public static async Task CreationDefaultAccount(IServiceProvider serviceProvider)
        {
            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            string role_admin = "Администратор";
            string role_user = "Пользователь";
            string defPassword = "Admin1234!";

            AppUser user_role_admin = new()
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
                EmailConfirmed = true,
                UserRoleName = role_admin,
                CustomUserId = 1,
                CustomUserName = "Администратор"
            };

            AppUser user_role_user = new()
            {
                UserName = "user@test.com",
                Email = "user@test.com",
                EmailConfirmed = true,
                UserRoleName = role_user,
                CustomUserId = 2,
                CustomUserName = "Пользователь"
            };

            IdentityResult resultAdd_user_admin = await userManager.CreateAsync(user_role_admin, defPassword);
            if (resultAdd_user_admin.Succeeded) { await userManager.AddToRoleAsync(user_role_admin, role_admin); }

            IdentityResult resultAdd_user = await userManager.CreateAsync(user_role_user, defPassword);
            if (resultAdd_user_admin.Succeeded) { await userManager.AddToRoleAsync(user_role_user, role_user); }
        }
        #endregion
    }
}
