using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingDiary.Data.Migrations
{
    public partial class AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "55e19652-5084-43d6-8e4f-da868683ad21", "83e37953-aa53-442e-8852-207bee0052a7", "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b12f2adb-973e-4044-bac2-5d9fa592c751", "1d4886a3-88e4-435e-9c3a-4cdaae4afa76", "moderator", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd4bc608-446f-4c82-a590-b1ac08c1c990", "457f48ab-34a2-425d-8249-2aaf259075f4", "user", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55e19652-5084-43d6-8e4f-da868683ad21");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b12f2adb-973e-4044-bac2-5d9fa592c751");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd4bc608-446f-4c82-a590-b1ac08c1c990");
        }
    }
}
