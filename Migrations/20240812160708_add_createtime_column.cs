using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Migrations
{
    /// <inheritdoc />
    public partial class add_createtime_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "StatementEntry",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "StatementEntry");

        
        }
    }
}
