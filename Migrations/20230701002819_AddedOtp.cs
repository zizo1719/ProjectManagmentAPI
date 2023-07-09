using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project_Managment_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OtpId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OtpModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    Otp = table.Column<string>(type: "longtext", nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpModels", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OtpId",
                table: "AspNetUsers",
                column: "OtpId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_OtpModels_OtpId",
                table: "AspNetUsers",
                column: "OtpId",
                principalTable: "OtpModels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_OtpModels_OtpId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OtpModels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OtpId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtpId",
                table: "AspNetUsers");
        }
    }
}
