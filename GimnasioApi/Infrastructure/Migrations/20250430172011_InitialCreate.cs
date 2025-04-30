using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    UserType = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: true),
                    Height = table.Column<double>(type: "REAL", nullable: true),
                    TrainerSpeciality = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routines_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Sets = table.Column<int>(type: "INTEGER", nullable: false),
                    Reps = table.Column<int>(type: "INTEGER", nullable: false),
                    RestTime = table.Column<int>(type: "INTEGER", nullable: false),
                    RoutineId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GymSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SessionType = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RoutineId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymSessions_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GymSessions_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientGymSession",
                columns: table => new
                {
                    ClientsId = table.Column<int>(type: "INTEGER", nullable: false),
                    GymSessionsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGymSession", x => new { x.ClientsId, x.GymSessionsId });
                    table.ForeignKey(
                        name: "FK_ClientGymSession_GymSessions_GymSessionsId",
                        column: x => x.GymSessionsId,
                        principalTable: "GymSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientGymSession_Users_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAvailable", "Name", "Password", "Phone", "Surname", "UserType" },
                values: new object[,]
                {
                    { 1, "superadmin@gmail.com", true, "SuperAdmin", "superadmin", 123456789, "Gym", 3 },
                    { 2, "admin@gmail.com", true, "Admin", "admin", 123456789, "Gym", 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Height", "IsAvailable", "Name", "Password", "Phone", "Surname", "UserType", "Weight" },
                values: new object[] { 3, "client@gmail.com", 0.0, true, "Client", "client", 123456789, "Gym", 0, 0.0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAvailable", "Name", "Password", "Phone", "Surname", "TrainerSpeciality", "UserType" },
                values: new object[] { 4, "trainer@gmail.com", true, "Trainer", "trainer", 123456789, "Gym", 3, 1 });

            migrationBuilder.InsertData(
                table: "Routines",
                columns: new[] { "Id", "IsAvailable", "Name", "TrainerId" },
                values: new object[] { 2, true, "Full Body Beginner", 4 });

            migrationBuilder.InsertData(
                table: "GymSessions",
                columns: new[] { "Id", "IsAvailable", "RoutineId", "SessionDate", "SessionType", "TrainerId" },
                values: new object[] { 2, true, 2, new DateTime(2025, 8, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, 4 });

            migrationBuilder.CreateIndex(
                name: "IX_ClientGymSession_GymSessionsId",
                table: "ClientGymSession",
                column: "GymSessionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_RoutineId",
                table: "Exercises",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_GymSessions_RoutineId",
                table: "GymSessions",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_GymSessions_TrainerId",
                table: "GymSessions",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Routines_TrainerId",
                table: "Routines",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientGymSession");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "GymSessions");

            migrationBuilder.DropTable(
                name: "Routines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
