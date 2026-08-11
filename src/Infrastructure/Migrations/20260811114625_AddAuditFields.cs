using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                table: "Categories",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Categories");
        }
    }
}
