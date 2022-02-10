using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingDiary.Data.Migrations
{
    public partial class UpdateRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "17d42049-fa82-4d66-b803-aa0885494bb3", "897a40bc-c768-4a40-8e4a-afcc3b3987b7", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "33c0a84c-9cb6-4615-b0b0-c67c4c4785c2", "a6144f81-0757-4852-9efa-adad8ba9c9be", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b7f700d-be27-47a7-979c-49ace3c22ac0", "22a9c8f0-5983-4997-9f33-412a830ac0fd", "moderator", "MODERATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17d42049-fa82-4d66-b803-aa0885494bb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33c0a84c-9cb6-4615-b0b0-c67c4c4785c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b7f700d-be27-47a7-979c-49ace3c22ac0");

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
    }
}
