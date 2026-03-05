using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Helpdesk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ReporterId = table.Column<int>(type: "int", nullable: false),
                    SolverId = table.Column<int>(type: "int", nullable: false),
                    ReportedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItems_Users_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItems_Users_SolverId",
                        column: x => x.SolverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskItemId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    DoneAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItems_TaskItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskItemId = table.Column<int>(type: "int", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    StoredFileName = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAttachments_TaskItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskItemId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskMessages_TaskItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskMessages_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { 1, "Alice Admin" },
                    { 2, "Bob Solver" },
                    { 3, "Charlie Tester" }
                });

            migrationBuilder.InsertData(
                table: "TaskItems",
                columns: new[] { "Id", "CompanyName", "Description", "DueAt", "Priority", "ReportedAt", "ReporterId", "ResolvedAt", "SolverId", "Status" },
                values: new object[,]
                {
                    { 1, "Contoso", "Login page does not work", new DateTime(2026, 3, 4, 12, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2026, 2, 27, 12, 0, 0, 0, DateTimeKind.Utc), 1, null, 2, 2 },
                    { 2, "Fabrikam", "Email notifications not sent", new DateTime(2026, 2, 27, 12, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2026, 2, 19, 12, 0, 0, 0, DateTimeKind.Utc), 3, null, 2, 1 },
                    { 3, "Northwind", "Update address on invoice template", new DateTime(2026, 2, 24, 12, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2026, 2, 22, 12, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2026, 2, 25, 12, 0, 0, 0, DateTimeKind.Utc), 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "ChecklistItems",
                columns: new[] { "Id", "DoneAt", "DueAt", "IsDone", "TaskItemId", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 1, 7, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 2, 12, 0, 0, 0, DateTimeKind.Utc), true, 1, "Check application logs" },
                    { 2, null, new DateTime(2026, 3, 3, 12, 0, 0, 0, DateTimeKind.Utc), false, 1, "Reproduce the issue" },
                    { 3, null, new DateTime(2026, 2, 28, 12, 0, 0, 0, DateTimeKind.Utc), false, 2, "Verify SMTP settings" }
                });

            migrationBuilder.InsertData(
                table: "TaskMessages",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Message", "TaskItemId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 3, 1, 2, 0, 0, 0, DateTimeKind.Utc), "Users report that login fails with error 500", 1 },
                    { 2, 2, new DateTime(2026, 3, 1, 4, 0, 0, 0, DateTimeKind.Utc), "Investigating the issue", 1 },
                    { 3, 3, new DateTime(2026, 2, 26, 12, 0, 0, 0, DateTimeKind.Utc), "It stopped sending emails after last deployment.", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_DueAt",
                table: "ChecklistItems",
                column: "DueAt");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_IsDone",
                table: "ChecklistItems",
                column: "IsDone");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_TaskItemId",
                table: "ChecklistItems",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_TaskItemId",
                table: "TaskAttachments",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_DueAt",
                table: "TaskItems",
                column: "DueAt");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_ReporterId",
                table: "TaskItems",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_SolverId",
                table: "TaskItems",
                column: "SolverId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_Status",
                table: "TaskItems",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMessages_AuthorId",
                table: "TaskMessages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMessages_CreatedAt",
                table: "TaskMessages",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMessages_TaskItemId",
                table: "TaskMessages",
                column: "TaskItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistItems");

            migrationBuilder.DropTable(
                name: "TaskAttachments");

            migrationBuilder.DropTable(
                name: "TaskMessages");

            migrationBuilder.DropTable(
                name: "TaskItems");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
