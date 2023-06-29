using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballClubApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FanClubs_FootballFans_FootballFanId",
                table: "FanClubs");

            migrationBuilder.DropIndex(
                name: "IX_FanClubs_FootballFanId",
                table: "FanClubs");

            migrationBuilder.DropColumn(
                name: "FootballFanId",
                table: "FanClubs");

            migrationBuilder.AlterColumn<int>(
                name: "FanId",
                table: "FanClubs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "FanClubs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_FanClubs_ClubId",
                table: "FanClubs",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_FanClubs_FanId",
                table: "FanClubs",
                column: "FanId");

            migrationBuilder.AddForeignKey(
                name: "FK_FanClubs_FootballClubs_ClubId",
                table: "FanClubs",
                column: "ClubId",
                principalTable: "FootballClubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FanClubs_FootballFans_FanId",
                table: "FanClubs",
                column: "FanId",
                principalTable: "FootballFans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FanClubs_FootballClubs_ClubId",
                table: "FanClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_FanClubs_FootballFans_FanId",
                table: "FanClubs");

            migrationBuilder.DropIndex(
                name: "IX_FanClubs_ClubId",
                table: "FanClubs");

            migrationBuilder.DropIndex(
                name: "IX_FanClubs_FanId",
                table: "FanClubs");

            migrationBuilder.AlterColumn<int>(
                name: "FanId",
                table: "FanClubs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "FanClubs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FootballFanId",
                table: "FanClubs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FanClubs_FootballFanId",
                table: "FanClubs",
                column: "FootballFanId");

            migrationBuilder.AddForeignKey(
                name: "FK_FanClubs_FootballFans_FootballFanId",
                table: "FanClubs",
                column: "FootballFanId",
                principalTable: "FootballFans",
                principalColumn: "Id");
        }
    }
}
