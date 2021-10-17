using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zebra.AuthService.API.Migrations
{
    public partial class AddColumnsToUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomerCreated",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsCustomerCreated",
                table: "AspNetUsers");
        }
    }
}
