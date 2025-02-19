﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameStore.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dictionaries_GameDevelopers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries_GameDevelopers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dictionaries_GameKeyStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries_GameKeyStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dictionaries_GameLabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries_GameLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dictionaries_GamePlatforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries_GamePlatforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dictionaries_Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    AvatarImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    AvatarName = table.Column<string>(type: "text", nullable: true),
                    LastVisit = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserRoleName = table.Column<string>(type: "text", nullable: true),
                    CustomUserId = table.Column<long>(type: "bigint", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    CustomUserName = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DeveloperId = table.Column<int>(type: "integer", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    IsShare = table.Column<bool>(type: "boolean", nullable: false),
                    SharePrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Poster = table.Column<byte[]>(type: "bytea", nullable: true),
                    PosterName = table.Column<string>(type: "text", nullable: true),
                    YtLinkGameTrailer = table.Column<string>(type: "text", nullable: true),
                    isShowInSlider = table.Column<bool>(type: "boolean", nullable: false),
                    SliderImg = table.Column<byte[]>(type: "bytea", nullable: true),
                    SliderName = table.Column<string>(type: "text", nullable: true),
                    Os = table.Column<string>(type: "text", nullable: true),
                    Cpu = table.Column<string>(type: "text", nullable: true),
                    Gpu = table.Column<string>(type: "text", nullable: true),
                    Ram = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: true),
                    DateAddedSite = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CountSold = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Dictionaries_GameDevelopers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Dictionaries_GameDevelopers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_ShoppingCarts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameGameLabel",
                columns: table => new
                {
                    GameLabelsId = table.Column<int>(type: "integer", nullable: false),
                    GamesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGameLabel", x => new { x.GameLabelsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GameGameLabel_Dictionaries_GameLabels_GameLabelsId",
                        column: x => x.GameLabelsId,
                        principalTable: "Dictionaries_GameLabels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGameLabel_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    GameGanresId = table.Column<int>(type: "integer", nullable: false),
                    GamesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => new { x.GameGanresId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GameGenre_Dictionaries_Genres_GameGanresId",
                        column: x => x.GameGanresId,
                        principalTable: "Dictionaries_Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games_Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    Review = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isPositive = table.Column<bool>(type: "boolean", nullable: false),
                    isDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Reviews_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games_Screenshots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Screenshot = table.Column<byte[]>(type: "bytea", nullable: true),
                    ScreenshotName = table.Column<string>(type: "text", nullable: true),
                    GameId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games_Screenshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Screenshots_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games_Keys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    PlatformId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    ShoppingCartId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games_Keys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Keys_Dictionaries_GameKeyStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Dictionaries_GameKeyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Keys_Dictionaries_GamePlatforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Dictionaries_GamePlatforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Keys_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Keys_Users_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "Users_ShoppingCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "History_GameReviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReviewId = table.Column<long>(type: "bigint", nullable: false),
                    OldValue = table.Column<string>(type: "text", nullable: true),
                    NewValue = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History_GameReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_History_GameReviews_Games_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Games_Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "admin", null, "Администратор", "АДМИНИСТРАТОР" },
                    { "user", null, "Пользователь", "ПОЛЬЗОВАТЕЛЬ" }
                });

            migrationBuilder.InsertData(
                table: "Dictionaries_GameDevelopers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Epic Games" },
                    { 2, "Electronic Arts" },
                    { 3, "Rockstar Games" },
                    { 4, "Ubisoft" },
                    { 5, "Activision" },
                    { 6, "Nintendo" },
                    { 7, "Microsoft Studios" },
                    { 8, "Sony Interactive Entertainment" },
                    { 9, "Capcom" },
                    { 10, "Square Enix" },
                    { 11, "Sega" },
                    { 12, "Bethesda Softworks" },
                    { 13, "Warner Bros. Interactive Entertainment" },
                    { 14, "Take-Two Interactive" },
                    { 15, "Konami" },
                    { 16, "11 Bit Studios" },
                    { 17, "0verflow" },
                    { 18, "1-Up Studio" },
                    { 19, "2K Games" },
                    { 20, "Elemental Games" },
                    { 21, "Elite Systems" },
                    { 22, "Engine Software" },
                    { 23, "Ensemble Studios" },
                    { 24, "Epicenter Studios" },
                    { 25, "Eric Barone" },
                    { 26, "Epyx" },
                    { 27, "Ready At Dawn" },
                    { 28, "Red Entertainment" },
                    { 29, "Raven Software" },
                    { 30, "Techland" },
                    { 31, "Telltale Games" },
                    { 32, "Nintendo Software Technology" },
                    { 33, "Nippon Ichi Software" },
                    { 34, "Demiurge Studios" },
                    { 35, "DeNA" },
                    { 36, "DevCAT Studios" },
                    { 37, "Dhruva Interactive" },
                    { 38, "Die Gute Fabrik" },
                    { 39, "Digital Extremes" },
                    { 40, "CD Projekt Red" }
                });

            migrationBuilder.InsertData(
                table: "Dictionaries_GameKeyStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Активный" },
                    { 2, "Забронирован" },
                    { 3, "Неактивный" }
                });

            migrationBuilder.InsertData(
                table: "Dictionaries_GameLabels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "PVE" },
                    { 2, "PVP" },
                    { 3, "Одиночная" },
                    { 4, "Co-op" }
                });

            migrationBuilder.InsertData(
                table: "Dictionaries_GamePlatforms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Steam" },
                    { 2, "Epic Launcher" },
                    { 3, "Battle.net" },
                    { 4, "Origin" },
                    { 5, "Uplay" },
                    { 6, "Rockstar Games Launcher" }
                });

            migrationBuilder.InsertData(
                table: "Dictionaries_Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Экшн" },
                    { 2, "Приключения" },
                    { 3, "Ролевая игра (RPG)" },
                    { 4, "Стратегия" },
                    { 5, "Спорт" },
                    { 6, "Симулятор" },
                    { 7, "Головоломка" },
                    { 8, "Драки" },
                    { 9, "Гонки" },
                    { 10, "Шутер" },
                    { 11, "Выживание" },
                    { 12, "Открытый мир" },
                    { 13, "Платформер" },
                    { 14, "Ужасы" },
                    { 15, "Образовательная игра" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGameLabel_GamesId",
                table: "GameGameLabel",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GamesId",
                table: "GameGenre",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeveloperId",
                table: "Games",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Keys_GameId",
                table: "Games_Keys",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Keys_PlatformId",
                table: "Games_Keys",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Keys_ShoppingCartId",
                table: "Games_Keys",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Keys_StatusId",
                table: "Games_Keys",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Reviews_GameId",
                table: "Games_Reviews",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Reviews_UserId",
                table: "Games_Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Screenshots_GameId",
                table: "Games_Screenshots",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_History_GameReviews_ReviewId",
                table: "History_GameReviews",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShoppingCarts_UserId",
                table: "Users_ShoppingCarts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GameGameLabel");

            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "Games_Keys");

            migrationBuilder.DropTable(
                name: "Games_Screenshots");

            migrationBuilder.DropTable(
                name: "History_GameReviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Dictionaries_GameLabels");

            migrationBuilder.DropTable(
                name: "Dictionaries_Genres");

            migrationBuilder.DropTable(
                name: "Dictionaries_GameKeyStatuses");

            migrationBuilder.DropTable(
                name: "Dictionaries_GamePlatforms");

            migrationBuilder.DropTable(
                name: "Users_ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Games_Reviews");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Dictionaries_GameDevelopers");
        }
    }
}
