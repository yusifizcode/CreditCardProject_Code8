using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnestHackhaton.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialize_07052023120728 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<float>(type: "real", nullable: false),
                    HomeOwnership = table.Column<int>(type: "int", nullable: false),
                    EmploymentTime = table.Column<float>(type: "real", nullable: false),
                    LoanPurposes = table.Column<int>(type: "int", nullable: false),
                    CreditAmount = table.Column<float>(type: "real", nullable: false),
                    LoanRate = table.Column<float>(type: "real", nullable: false),
                    CreditStatus = table.Column<bool>(type: "bit", nullable: false),
                    LoanPercentage = table.Column<int>(type: "int", nullable: false),
                    PaymentHistory = table.Column<int>(type: "int", nullable: false),
                    CreditHistoryLength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
