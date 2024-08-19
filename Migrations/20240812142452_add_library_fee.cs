using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Migrations
{
    /// <inheritdoc />
    public partial class add_library_fee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<double>(
                name: "LibraryFee",
                table: "FeePolicy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "LibraryFee",
                table: "FeePolicy");

  
        }
    }
}
