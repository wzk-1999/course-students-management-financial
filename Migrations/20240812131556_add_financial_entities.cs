using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Migrations
{
    /// <inheritdoc />
    public partial class add_financial_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
  

            migrationBuilder.CreateTable(
                name: "FeePolicy",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TuitionFee = table.Column<double>(type: "float", nullable: false),
                    RegistrationFee = table.Column<double>(type: "float", nullable: false),
                    FacilitiesFee = table.Column<double>(type: "float", nullable: false),
                    UnionFee = table.Column<double>(type: "float", nullable: false),
                    AlumniFee = table.Column<double>(type: "float", nullable: false),
                    TaxRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeePolicy", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FinancialStatement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    FeePolicyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FinancialStatement_FeePolicy_FeePolicyID",
                        column: x => x.FeePolicyID,
                        principalTable: "FeePolicy",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialStatement_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatementEntry",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    FinancialStatementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementEntry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StatementEntry_FinancialStatement_FinancialStatementID",
                        column: x => x.FinancialStatementID,
                        principalTable: "FinancialStatement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatement_FeePolicyID",
                table: "FinancialStatement",
                column: "FeePolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatement_StudentID",
                table: "FinancialStatement",
                column: "StudentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatementEntry_FinancialStatementID",
                table: "StatementEntry",
                column: "FinancialStatementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatementEntry");

            migrationBuilder.DropTable(
                name: "FinancialStatement");

            migrationBuilder.DropTable(
                name: "FeePolicy");


        }
    }
}
