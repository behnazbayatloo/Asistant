using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Asistant_Infra_Db_Sql.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ExpertId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeServices_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Experts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Experts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentReadyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    VerifyExpertDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpertHomeServices",
                columns: table => new
                {
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertHomeServices", x => new { x.ExpertId, x.HomeServiceId });
                    table.ForeignKey(
                        name: "FK_ExpertHomeServices_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpertHomeServices_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suggestions_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suggestions_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suggestions_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageCategory = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ExpertId = table.Column<int>(type: "int", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    SuggestionId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    HomeServiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "Customer", "CUSTOMER" },
                    { 3, null, "Expert", "EXPERT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Balance", "ConcurrencyStamp", "CreatedUserId", "CustomerId", "Email", "EmailConfirmed", "ExpertId", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UpdatedUserId", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, "b9ede448-60b1-4291-9d37-8aea803d523e", 0, null, "admin@admin.com", false, null, "Admin", "Admin", false, null, "ADMIN@ADMIN.COM", "09351650512", "AQAAAAIAAYagAAAAEOJ1i4H5k22QaBkR+f97qHp8W0La09ReuO0C0xaByE0bXwtdLlnERGs4b9bY+UaiEA==", null, false, "1b5e57fc-c0bd-4afb-90ce-f414421271b0", false, null, 0, "09351650512" },
                    { 2, 0, 6000000m, "56db7a36-3bd7-4fc3-8271-e23513491617", 0, 1, "behnaz@gmail.com", false, null, "بهناز", "بیاتلو", false, null, "BEHNAZ@GMAIL.COM", "BEHNAZ", "AQAAAAIAAYagAAAAEJd4pA3edA7vB8LqlKrHzK6T40JhhI3hzo+Cdyi7WJ0orvnKoR0nAgg1DzN1SkWJ2g==", null, false, "191382ef-a618-40e4-b7e7-70558c9dc70e", false, null, 0, "behnaz" },
                    { 3, 0, 4000000m, "2e5d6979-9803-45cb-bf21-b7777bab9bfa", 0, 2, "hasan@gmail.com", false, null, "حسن", "اسدی", false, null, "HASAN@GMAIL.COM", "HASAN", "AQAAAAIAAYagAAAAEJwLz25oIpmYtcQRb28X5KJvq5lWVswhrvfYsFWgfzUOEXSIDIjAwrvcIFMWFbWwnA==", null, false, "3a24a440-c2c5-4245-9d1d-b2d21f2fdaa1", false, null, 0, "hasan" },
                    { 4, 0, null, "2968360d-8b3c-4416-9d8c-0f8dd79321c1", 0, null, "mohammad@gmail.com", false, 1, "محمد", "اکبری", false, null, "MOHAMMAD@GMAIL.COM", "MOHAMMAD", "AQAAAAIAAYagAAAAENhRdlp9eCvM6Lcc/xm+ieZ/RCfGJivZToMrbyb4bMSmSwUJawvmpkCZjF7/3qPpIg==", null, false, "3cc7447b-8843-477e-a5dd-af69c9f34b3d", false, null, 0, "mohammad" },
                    { 5, 0, null, "d1e597b3-3761-4e43-afc3-ea666ba4dbdf", 0, null, "majid@gmail.com", false, 2, "مجید", "بیگی", false, null, "MAJID@GMAIL.COM", "MAJID", "AQAAAAIAAYagAAAAELYx236vCdi/aKgDZdGe1BA7Yk2gFk6iq9gEpUqXsYEX1lq1roeWfbtJjlHjK7LhMA==", null, false, "565034a7-7d45-4d3d-9323-28e76fa76702", false, null, 0, "majid" },
                    { 6, 0, null, "25cb5acc-d239-43d9-9bb1-45214c8f35a2", 0, null, "meysam@gmail.com", false, 3, "میثم", "محسنی", false, null, "MEYSAM@GMAIL.COM", "MEYSAM", "AQAAAAIAAYagAAAAELbzX4Qc/tZYgyWIJspYZz8fO+VvBY7OO/i9lvi10bnw7jHI4Rr7Lpl2SX9m1J0MxA==", null, false, "4ff5b95a-9b61-4f4f-a704-40c61e6bcac6", false, null, 0, "meysam" },
                    { 7, 0, null, "96ef1e9b-6dca-421e-91a9-014b10254dbc", 0, null, "saman@gmail.com", false, 4, "سامان", "جلیلی", false, null, "SAMAN@GMAIL.COM", "SAMAN", "AQAAAAIAAYagAAAAEImJX7x4yMhrIkzUSK3RBDt21GjjfWv/LEiwCLbODMi/C4xrZHW/oM99brLwWZU8Sw==", null, false, "06fd5c8c-1c20-4a2c-bd77-d4e5bd46bacd", false, null, 0, "saman" },
                    { 8, 0, null, "2a0d648d-0d41-4a26-9426-71061f7d2004", 0, null, "sara@gmail.com", false, 5, "سارا", "دلشاد", false, null, "SARA@GMAIL.COM", "SARA", "AQAAAAIAAYagAAAAENGlCdWGd+kzPdwW+L/wHF1XJfjdA19ZW3PSGA50J+4driKjTr1Ryh1I6xDfy3mjdg==", null, false, "a70e16a9-37c5-49db-b148-a476ac2818c3", false, null, 0, "sara" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ImageId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "نظافت منزل" },
                    { 2, 2, "تعمیرات لوازم خانگی" },
                    { 3, 3, "خدمات برقکاری" },
                    { 4, 4, "خدمات لوله‌کشی" },
                    { 5, 5, "خدمات نقاشی و دکوراسیون" },
                    { 6, 6, "خدمات باغبانی" },
                    { 7, 7, "خدمات کامپیوتر و شبکه" },
                    { 8, 8, "خدمات خودرو" },
                    { 9, 9, "خدمات آموزشی" },
                    { 10, 10, "خدمات پزشکی و پرستاری" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "تهران" },
                    { 2, "مشهد" },
                    { 3, "اصفهان" },
                    { 4, "شیراز" },
                    { 5, "تبریز" },
                    { 6, "کرج" },
                    { 7, "قم" },
                    { 8, "اهواز" },
                    { 9, "کرمانشاه" },
                    { 10, "رشت" },
                    { 11, "یزد" },
                    { 12, "کرمان" },
                    { 13, "ارومیه" },
                    { 14, "زاهدان" },
                    { 15, "ساری" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CityId", "ImageId", "UserId" },
                values: new object[,]
                {
                    { 1, "تهران خیابان ایت الله کاشانی کوچه بهنام پلاک 4 واحد1", 1, null, 2 },
                    { 2, "شیراز بلوار سعدی کوچه پرستو پلاک 12 واحد 2 ", 4, null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Experts",
                columns: new[] { "Id", "CityId", "ImageId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, null, 4 },
                    { 2, 4, null, 5 },
                    { 3, 1, null, 6 },
                    { 4, 4, null, 7 },
                    { 5, 1, null, 8 }
                });

            migrationBuilder.InsertData(
                table: "HomeServices",
                columns: new[] { "Id", "BasePrice", "CategoryId", "Description", "ImageId", "Name" },
                values: new object[,]
                {
                    { 1, 300000m, 1, "تمیزکاری کامل کابینت‌ها، کف و دیوارها", 11, "نظافت آشپزخانه" },
                    { 2, 200000m, 1, "گردگیری و جاروکشی اتاق‌ها", 12, "نظافت اتاق‌ها" },
                    { 3, 250000m, 1, "شستشو و ضدعفونی سرویس‌ها", 13, "نظافت سرویس بهداشتی" },
                    { 4, 500000m, 2, "عیب‌یابی و تعمیر انواع ماشین لباسشویی", 14, "تعمیر ماشین لباسشویی" },
                    { 5, 600000m, 2, "سرویس و تعمیر یخچال و فریزر", 15, "تعمیر یخچال" },
                    { 6, 150000m, 3, "نصب و تعویض کلید و پریز برق", 16, "نصب کلید و پریز" },
                    { 7, 800000m, 3, "اجرای سیم‌کشی برق داخلی", 17, "سیم‌کشی ساختمان" },
                    { 8, 250000m, 4, "باز کردن لوله‌های فاضلاب و آب", 18, "رفع گرفتگی لوله" },
                    { 9, 200000m, 4, "نصب و تعویض شیرآلات آشپزخانه و حمام", 19, "نصب شیرآلات" },
                    { 10, 400000m, 5, "رنگ‌آمیزی دیوارهای داخلی", 20, "نقاشی دیوار" },
                    { 11, 700000m, 5, "نصب انواع کاغذ دیواری", 21, "کاغذ دیواری" },
                    { 12, 350000m, 6, "هرس و مرتب‌سازی درختان باغ و حیاط", 22, "هرس درختان" },
                    { 13, 300000m, 6, "کاشت و نگهداری گل‌ها و گیاهان", 23, "کاشت گل و گیاه" },
                    { 14, 200000m, 7, "نصب و راه‌اندازی سیستم عامل ویندوز", 24, "نصب ویندوز" },
                    { 15, 400000m, 7, "نصب مودم و تنظیم شبکه داخلی", 25, "راه‌اندازی شبکه خانگی" },
                    { 16, 150000m, 8, "تعویض روغن موتور خودرو", 26, "تعویض روغن" },
                    { 17, 250000m, 8, "نصب و تعمیر باتری خودرو", 27, "باتری‌سازی" },
                    { 18, 500000m, 9, "کلاس خصوصی زبان انگلیسی", 28, "آموزش زبان انگلیسی" },
                    { 19, 400000m, 9, "کلاس تقویتی ریاضی", 29, "آموزش ریاضی" },
                    { 20, 700000m, 10, "مراقبت از بیمار در منزل", 30, "پرستاری در منزل" },
                    { 21, 800000m, 10, "ویزیت پزشک در منزل", 31, "ویزیت پزشک عمومی" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CategoryId", "CustomerId", "ExpertId", "HomeServiceId", "ImageCategory", "ImagePath", "RequestId", "SuggestionId" },
                values: new object[,]
                {
                    { 1, 1, null, null, null, 2, "\\Images\\Category\\1.jpg", null, null },
                    { 2, 2, null, null, null, 2, "\\Images\\Category\\2.jpg", null, null },
                    { 3, 3, null, null, null, 2, "\\Images\\Category\\3.jpg", null, null },
                    { 4, 4, null, null, null, 2, "\\Images\\Category\\4.jpg", null, null },
                    { 5, 5, null, null, null, 2, "\\Images\\Category\\5.jpg", null, null },
                    { 6, 6, null, null, null, 2, "\\Images\\Category\\6.jpg", null, null },
                    { 7, 7, null, null, null, 2, "\\Images\\Category\\7.jpg", null, null },
                    { 8, 8, null, null, null, 2, "\\Images\\Category\\8.jpg", null, null },
                    { 9, 9, null, null, null, 2, "\\Images\\Category\\9.jpg", null, null },
                    { 10, 10, null, null, null, 2, "\\Images\\Category\\10.jpg", null, null },
                    { 11, null, null, null, 1, 3, "\\Images\\HomeService\\4-3-3.jpg", null, null },
                    { 12, null, null, null, 2, 3, "\\Images\\HomeService\\download.jpg", null, null },
                    { 13, null, null, null, 3, 3, "\\Images\\HomeService\\cleantoilet.jpg", null, null },
                    { 14, null, null, null, 4, 3, "\\Images\\HomeService\\fixlaundry.jpg", null, null },
                    { 15, null, null, null, 5, 3, "\\Images\\HomeService\\fixrefrig.jpg", null, null },
                    { 16, null, null, null, 6, 3, "\\Images\\HomeService\\download (1).jpg", null, null },
                    { 17, null, null, null, 7, 3, "\\Images\\HomeService\\download (2).jpg", null, null },
                    { 18, null, null, null, 8, 3, "\\Images\\HomeService\\download (3).jpg", null, null },
                    { 19, null, null, null, 9, 3, "\\Images\\HomeService\\download (4).jpg", null, null },
                    { 20, null, null, null, 10, 3, "\\Images\\HomeService\\download (5).jpg", null, null },
                    { 21, null, null, null, 11, 3, "\\Images\\HomeService\\download (6).jpg", null, null },
                    { 22, null, null, null, 12, 3, "\\Images\\HomeService\\download (7).jpg", null, null },
                    { 23, null, null, null, 13, 3, "\\Images\\HomeService\\download (8).jpg", null, null },
                    { 24, null, null, null, 14, 3, "\\Images\\HomeService\\download (9).jpg", null, null },
                    { 25, null, null, null, 15, 3, "\\Images\\HomeService\\download (10).jpg", null, null },
                    { 26, null, null, null, 16, 3, "\\Images\\HomeService\\download (11).jpg", null, null },
                    { 27, null, null, null, 17, 3, "\\Images\\HomeService\\download (12).jpg", null, null },
                    { 28, null, null, null, 18, 3, "\\Images\\HomeService\\download (13).jpg", null, null },
                    { 29, null, null, null, 19, 3, "\\Images\\HomeService\\download (14).jpg", null, null },
                    { 30, null, null, null, 20, 3, "\\Images\\HomeService\\download (15).jpg", null, null },
                    { 31, null, null, null, 21, 3, "\\Images\\HomeService\\download (16).jpg", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CustomerId",
                table: "Comments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ExpertId",
                table: "Comments",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_HomeServiceId",
                table: "Comments",
                column: "HomeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CityId",
                table: "Customers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertHomeServices_HomeServiceId",
                table: "ExpertHomeServices",
                column: "HomeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_CityId",
                table: "Experts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_UserId",
                table: "Experts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeServices_CategoryId",
                table: "HomeServices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CategoryId",
                table: "Images",
                column: "CategoryId",
                unique: true,
                filter: "[CategoryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CustomerId",
                table: "Images",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ExpertId",
                table: "Images",
                column: "ExpertId",
                unique: true,
                filter: "[ExpertId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_HomeServiceId",
                table: "Images",
                column: "HomeServiceId",
                unique: true,
                filter: "[HomeServiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RequestId",
                table: "Images",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SuggestionId",
                table: "Images",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CustomerId",
                table: "Requests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_HomeServiceId",
                table: "Requests",
                column: "HomeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_ExpertId",
                table: "Suggestions",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_HomeServiceId",
                table: "Suggestions",
                column: "HomeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_RequestId",
                table: "Suggestions",
                column: "RequestId");
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
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ExpertHomeServices");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "HomeServices");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
