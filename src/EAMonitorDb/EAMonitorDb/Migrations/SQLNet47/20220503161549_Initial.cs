using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EAMonitor.Migrations.SQLNet47
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EAMonitorJobStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAMonitorJobStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EAMonitorSettingKey",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAMonitorSettingKey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EAMonitorState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAMonitorState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EAMonitor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    MonitorStateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAMonitor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EAMonitor_EAMonitorState_MonitorStateId",
                        column: x => x.MonitorStateId,
                        principalTable: "EAMonitorState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EAMonitorJob",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MonitorId = table.Column<Guid>(nullable: false),
                    JobStatusId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Completed = table.Column<DateTime>(nullable: true),
                    Notified = table.Column<bool>(nullable: false),
                    MonitorStateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAMonitorJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EAMonitorJob_EAMonitorJobStatus_JobStatusId",
                        column: x => x.JobStatusId,
                        principalTable: "EAMonitorJobStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EAMonitorJob_EAMonitor_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "EAMonitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EAMonitorJob_EAMonitorState_MonitorStateId",
                        column: x => x.MonitorStateId,
                        principalTable: "EAMonitorState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EAMonitorSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SettingKeyId = table.Column<int>(nullable: false),
                    MonitorId = table.Column<Guid>(nullable: true),
                    SettingValue = table.Column<string>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAMonitorSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EAMonitorSetting_EAMonitor_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "EAMonitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EAMonitorSetting_EAMonitorSettingKey_SettingKeyId",
                        column: x => x.SettingKeyId,
                        principalTable: "EAMonitorSettingKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EAMonitorJobTest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    JobId = table.Column<Guid>(nullable: false),
                    TestPath = table.Column<string>(nullable: true),
                    TestExpandedPath = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Passed = table.Column<bool>(nullable: false),
                    ExecutedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAMonitorJobTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EAMonitorJobTest_EAMonitorJob_JobId",
                        column: x => x.JobId,
                        principalTable: "EAMonitorJob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EAMonitorJobStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Created" },
                    { 2, "InProgress" },
                    { 3, "Completed" },
                    { 4, "Failed" },
                    { 5, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "EAMonitorState",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Unknown" },
                    { 2, "Up" },
                    { 3, "Down" },
                    { 4, "Warning" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitor_MonitorStateId",
                table: "EAMonitor",
                column: "MonitorStateId");

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitor_Name",
                table: "EAMonitor",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitorJob_JobStatusId",
                table: "EAMonitorJob",
                column: "JobStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitorJob_MonitorStateId",
                table: "EAMonitorJob",
                column: "MonitorStateId");

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitorJob_MonitorId_Created",
                table: "EAMonitorJob",
                columns: new[] { "MonitorId", "Created" });

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitorJobTest_JobId",
                table: "EAMonitorJobTest",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitorSetting_MonitorId",
                table: "EAMonitorSetting",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitorSetting_SettingKeyId",
                table: "EAMonitorSetting",
                column: "SettingKeyId");
            migrationBuilder.Sql(@"
                CREATE OR ALTER VIEW v_EAMonitor AS
                SELECT
                    eaM.Id,
                    eaM.Name,
                    eaM.Description,
                    eaM.LastModified,
                    eaM.Created,
                    eaMs.Name as ""MonitorState"",
                    eaJobOuter.Created as ""LastJobCreatedAt"",
                    eaJobOuter.LastModified as ""LastJobModifiedAt"",
                    eaJobOuter.Completed as ""LastJobCompletedAt"",
                    eaMJS.Name as ""LastJobStatus""
                FROM EAMonitor eaM
                OUTER APPLY (
                    SELECT TOP 1 *
                    FROM EAMonitorJob eaJob
                    WHERE eaJob.MonitorId = eaM.Id
                    ORDER BY eaJob.Created DESC
                ) as eaJobOuter
                JOIN EAMonitorState eaMs
                    ON eaMs.Id = eaM.MonitorStateId
                JOIN EAMonitorJobStatus eaMJS
                    ON eaMJS.Id = eaJobOuter.JobStatusId
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EAMonitorJobTest");

            migrationBuilder.DropTable(
                name: "EAMonitorSetting");

            migrationBuilder.DropTable(
                name: "EAMonitorJob");

            migrationBuilder.DropTable(
                name: "EAMonitorSettingKey");

            migrationBuilder.DropTable(
                name: "EAMonitorJobStatus");

            migrationBuilder.DropTable(
                name: "EAMonitor");

            migrationBuilder.DropTable(
                name: "EAMonitorState");
        }
    }
}
