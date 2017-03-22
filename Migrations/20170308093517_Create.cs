﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GraphQL.Benchmarks.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Droids",
                columns: table => new
                {
                    DroidId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    PrimaryFunction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Droids", x => x.DroidId);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    EpisodeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.EpisodeId);
                });

            migrationBuilder.CreateTable(
                name: "Humans",
                columns: table => new
                {
                    HumanId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HomePlanet = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humans", x => x.HumanId);
                });

            migrationBuilder.CreateTable(
                name: "DroidAppearances",
                columns: table => new
                {
                    DroidAppearanceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DroidId = table.Column<int>(nullable: false),
                    EpisodeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroidAppearances", x => x.DroidAppearanceId);
                    table.ForeignKey(
                        name: "FK_DroidAppearances_Droids_DroidId",
                        column: x => x.DroidId,
                        principalTable: "Droids",
                        principalColumn: "DroidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DroidAppearances_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "EpisodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    FriendshipId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DroidId = table.Column<int>(nullable: false),
                    HumanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.FriendshipId);
                    table.ForeignKey(
                        name: "FK_Friendships_Droids_DroidId",
                        column: x => x.DroidId,
                        principalTable: "Droids",
                        principalColumn: "DroidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendships_Humans_HumanId",
                        column: x => x.HumanId,
                        principalTable: "Humans",
                        principalColumn: "HumanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HumanAppearances",
                columns: table => new
                {
                    HumanAppearanceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EpisodeId = table.Column<int>(nullable: false),
                    HumanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumanAppearances", x => x.HumanAppearanceId);
                    table.ForeignKey(
                        name: "FK_HumanAppearances_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "EpisodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HumanAppearances_Humans_HumanId",
                        column: x => x.HumanId,
                        principalTable: "Humans",
                        principalColumn: "HumanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DroidAppearances_DroidId",
                table: "DroidAppearances",
                column: "DroidId");

            migrationBuilder.CreateIndex(
                name: "IX_DroidAppearances_EpisodeId",
                table: "DroidAppearances",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_DroidId",
                table: "Friendships",
                column: "DroidId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_HumanId",
                table: "Friendships",
                column: "HumanId");

            migrationBuilder.CreateIndex(
                name: "IX_HumanAppearances_EpisodeId",
                table: "HumanAppearances",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_HumanAppearances_HumanId",
                table: "HumanAppearances",
                column: "HumanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DroidAppearances");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "HumanAppearances");

            migrationBuilder.DropTable(
                name: "Droids");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Humans");
        }
    }
}
