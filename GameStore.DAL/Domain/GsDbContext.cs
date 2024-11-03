using GameStore.DAL.Entities.Dictionaries;
using GameStore.DAL.Entities.Games;
using GameStore.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;

namespace GameStore.DAL.Domain
{
    public class GsDbContext : IdentityDbContext<AppUser>
    {
        public GsDbContext(DbContextOptions<GsDbContext> options) : base(options) { }

        #region DB SETS
        //-- Identity
        public DbSet<AppUser> AppUsers { get; set; }

        //-- Dictionaries
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<GameDeveloper> GameDevelopers { get; set; }
        public DbSet<GameLabel> GameLabels { get; set; }

        //-- Games
        public DbSet<Game> Games { get; set; }
        public DbSet<GameKey> GameKeys { get; set; }
        public DbSet<GameScreenshot> GameScreenshots { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region DB CREATION
            
            //--Identity
            builder.Entity<AppUser>().ToTable("Users");
          
            //-- Dictionaries
            builder.Entity<Genre>().ToTable("Dictionaries_Genres");
            builder.Entity<GamePlatform>().ToTable("Dictionaries_GamePlatforms");
            builder.Entity<GameDeveloper>().ToTable("Dictionaries_GameDevelopers");
            builder.Entity<GameLabel>().ToTable("Dictionaries_GameLabels");

            //-- Games
            builder.Entity<Game>().ToTable("Games_Games");
            builder.Entity<GameKey>().ToTable("Games_Keys");
            builder.Entity<GameScreenshot>().ToTable("Games_Screenshots");
            #endregion

            #region SEED DATA

            // -- Identity
            builder.Entity<IdentityRole>().HasData(new Predefined.Identity.PdRoles().RoleList);

            //-- Dictionaries
            builder.Entity<Genre>().HasData(new Predefined.Dictionaries.PdGenres().ListGanres);
            builder.Entity<GamePlatform>().HasData(new Predefined.Dictionaries.PdGamePlatforms().ListGamePlatforms);
            builder.Entity<GameDeveloper>().HasData(new Predefined.Dictionaries.PdGameDevelopers().ListGameDevelopers);
            builder.Entity<GameLabel>().HasData(new Predefined.Dictionaries.PdGameLabels().Listlabels);

            //-- Games
            builder.Entity<Game>().HasData(new Predefined.Games.PdGames().testGames);
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
            };

            AppUser user_role_user = new()
            {
                UserName = "user@test.com",
                Email = "user@test.com",
                EmailConfirmed = true,
                UserRoleName = role_user,
                CustomUserId = 2,
            };

            IdentityResult resultAdd_user_admin = await userManager.CreateAsync(user_role_admin, defPassword);
            if (resultAdd_user_admin.Succeeded) { await userManager.AddToRoleAsync(user_role_admin, role_admin); }

            IdentityResult resultAdd_user = await userManager.CreateAsync(user_role_user, defPassword);
            if (resultAdd_user_admin.Succeeded) { await userManager.AddToRoleAsync(user_role_user, role_user); }
        }
        #endregion
    }
}
