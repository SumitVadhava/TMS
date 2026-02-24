using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<int>(type: "int", nullable: false),
                    Assigned_To = table.Column<int>(type: "int", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_Assigned_To",
                        column: x => x.Assigned_To,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_Created_By",
                        column: x => x.Created_By,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketComments_Tickets_Ticket_id",
                        column: x => x.Ticket_id,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketComments_Users_User_id",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketStatusLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_id = table.Column<int>(type: "int", nullable: false),
                    Old_Status = table.Column<int>(type: "int", nullable: false),
                    New_Status = table.Column<int>(type: "int", nullable: false),
                    Changed_By = table.Column<int>(type: "int", nullable: false),
                    Changed_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatusLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketStatusLogs_Tickets_Ticket_id",
                        column: x => x.Ticket_id,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketStatusLogs_Users_Changed_By",
                        column: x => x.Changed_By,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_Ticket_id",
                table: "TicketComments",
                column: "Ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_User_id",
                table: "TicketComments",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Assigned_To",
                table: "Tickets",
                column: "Assigned_To");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Created_By",
                table: "Tickets",
                column: "Created_By");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatusLogs_Changed_By",
                table: "TicketStatusLogs",
                column: "Changed_By");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatusLogs_Ticket_id",
                table: "TicketStatusLogs",
                column: "Ticket_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role_Id",
                table: "Users",
                column: "Role_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketComments");

            migrationBuilder.DropTable(
                name: "TicketStatusLogs");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
