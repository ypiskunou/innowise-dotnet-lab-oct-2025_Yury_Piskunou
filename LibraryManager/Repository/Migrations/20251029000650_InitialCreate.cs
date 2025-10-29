using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    PublishedYear = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "Name" },
                values: new object[,]
                {
                    { new Guid("3310f8f9-f4bd-4a88-81fc-d50fdc2bc7dd"), new DateTime(1920, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ray Bradbury" },
                    { new Guid("d6b0b215-9108-493c-985c-1b541a2c92ef"), new DateTime(1920, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Isaac Asimov" },
                    { new Guid("f16c5009-1386-4be4-a4cc-8944fe7e92c1"), new DateTime(1948, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Terry Pratchett" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "PublishedYear", "Title" },
                values: new object[,]
                {
                    { new Guid("09820e43-229b-42c6-9f38-39f58bbefb58"), new Guid("d6b0b215-9108-493c-985c-1b541a2c92ef"), 1951, "Foundation" },
                    { new Guid("236187cc-6c1c-48e5-8fc4-6df2976f31ca"), new Guid("f16c5009-1386-4be4-a4cc-8944fe7e92c1"), 1987, "Mort" },
                    { new Guid("4145bbe3-5e1e-4034-9d54-da63f56c02c3"), new Guid("3310f8f9-f4bd-4a88-81fc-d50fdc2bc7dd"), 1953, "Fahrenheit 451" },
                    { new Guid("4e4a2cf1-6bb3-462c-b8cd-208e9161cb9a"), new Guid("f16c5009-1386-4be4-a4cc-8944fe7e92c1"), 1983, "The Colour of Magic" },
                    { new Guid("560a5138-59b7-43be-aac4-03056c4a27d8"), new Guid("3310f8f9-f4bd-4a88-81fc-d50fdc2bc7dd"), 1950, "The Martian Chronicles" },
                    { new Guid("d6808a67-50c8-493f-b224-3ecc4cabab1b"), new Guid("3310f8f9-f4bd-4a88-81fc-d50fdc2bc7dd"), 1957, "Dandelion Wine" },
                    { new Guid("dfc4017b-733e-49b3-b915-0572a729ffa0"), new Guid("d6b0b215-9108-493c-985c-1b541a2c92ef"), 1950, "I, Robot" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
