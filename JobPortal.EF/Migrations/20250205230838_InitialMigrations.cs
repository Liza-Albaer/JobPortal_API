using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobPortal.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    Company = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    Location = table.Column<string>(type: "Nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "Nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "Nvarchar(max)", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "Nvarchar(max)", nullable: false),
                    ResumeUrl = table.Column<string>(type: "Nvarchar(max)", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_JobApplications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "Company", "Description", "Location", "PostedDate", "Requirements", "Title" },
                values: new object[,]
                {
                    { 1, "TechCorp", "Develop and maintain web applications.", "New York", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3+ years experience in C# and ASP.NET.", "Software Engineer" },
                    { 2, "DataCorp", "Analyze large datasets and provide insights.", "San Francisco", new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Experience with SQL and Power BI.", "Data Analyst" }
                });

            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "ApplicationId", "AppliedDate", "Email", "JobId", "Name", "ResumeUrl" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", 1, "John Doe", "resumes/johndoe.pdf" },
                    { 2, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "janesmith@example.com", 2, "Jane Smith", "resumes/janesmith.pdf" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
