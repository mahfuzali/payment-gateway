using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGateway.Infrastructure.Persistence.Migrations.PaymentDbMigration
{
    public partial class InitialPaymentDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Number = table.Column<string>(type: "text", nullable: false),
                    ExpiryMonth = table.Column<int>(type: "integer", nullable: false),
                    ExpiryYear = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    CVV = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CardNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Cards_CardNumber",
                        column: x => x.CardNumber,
                        principalTable: "Cards",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CardNumber",
                table: "Payments",
                column: "CardNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
