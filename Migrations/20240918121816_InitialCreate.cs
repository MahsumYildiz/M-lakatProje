using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MülakatProje.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalmaListeleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalmaListeleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sanatcilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KurulusTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanatcilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albumler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CikisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SanatciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albumler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albumler_Sanatcilar_SanatciId",
                        column: x => x.SanatciId,
                        principalTable: "Sanatcilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sarkilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    SanatciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sarkilar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sarkilar_Albumler_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albumler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Sarkilar_Sanatcilar_SanatciId",
                        column: x => x.SanatciId,
                        principalTable: "Sanatcilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CalmaListesiSarkilari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalmaListesiId = table.Column<int>(type: "int", nullable: false),
                    SarkiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalmaListesiSarkilari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalmaListesiSarkilari_CalmaListeleri_CalmaListesiId",
                        column: x => x.CalmaListesiId,
                        principalTable: "CalmaListeleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalmaListesiSarkilari_Sarkilar_SarkiId",
                        column: x => x.SarkiId,
                        principalTable: "Sarkilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albumler_SanatciId",
                table: "Albumler",
                column: "SanatciId");

            migrationBuilder.CreateIndex(
                name: "IX_CalmaListesiSarkilari_CalmaListesiId",
                table: "CalmaListesiSarkilari",
                column: "CalmaListesiId");

            migrationBuilder.CreateIndex(
                name: "IX_CalmaListesiSarkilari_SarkiId",
                table: "CalmaListesiSarkilari",
                column: "SarkiId");

            migrationBuilder.CreateIndex(
                name: "IX_Sarkilar_AlbumId",
                table: "Sarkilar",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Sarkilar_SanatciId",
                table: "Sarkilar",
                column: "SanatciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalmaListesiSarkilari");

            migrationBuilder.DropTable(
                name: "CalmaListeleri");

            migrationBuilder.DropTable(
                name: "Sarkilar");

            migrationBuilder.DropTable(
                name: "Albumler");

            migrationBuilder.DropTable(
                name: "Sanatcilar");
        }
    }
}
