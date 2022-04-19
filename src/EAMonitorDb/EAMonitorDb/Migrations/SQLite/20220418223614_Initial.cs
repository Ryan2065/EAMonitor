using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EAMonitorDb.Migrations.SQLite
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EAMonitorEnvironment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAMonitorEnvironment", x => x.Id);
                });

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
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
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
                        .Annotation("Sqlite:Autoincrement", true),
                    SettingKeyId = table.Column<int>(nullable: false),
                    MonitorId = table.Column<Guid>(nullable: false),
                    SettingValue = table.Column<string>(nullable: false),
                    SettingEnvironmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAMonitorSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EAMonitorSetting_EAMonitor_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "EAMonitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EAMonitorSetting_EAMonitorEnvironment_SettingEnvironmentId",
                        column: x => x.SettingEnvironmentId,
                        principalTable: "EAMonitorEnvironment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EAMonitorSetting_EAMonitorSettingKey_SettingKeyId",
                        column: x => x.SettingKeyId,
                        principalTable: "EAMonitorSettingKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EAMonitorJobStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Created" });

            migrationBuilder.InsertData(
                table: "EAMonitorJobStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "InProgress" });

            migrationBuilder.InsertData(
                table: "EAMonitorJobStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Completed" });

            migrationBuilder.InsertData(
                table: "EAMonitorJobStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Failed" });

            migrationBuilder.InsertData(
                table: "EAMonitorJobStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Cancelled" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Name" },
                values: new object[] { 10, "StateChange-DownToUp" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Name" },
                values: new object[] { 9, "StateChange-DownToWarning" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Name" },
                values: new object[] { 8, "StateChange-UpToWarning" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "StateChange-UpToDown" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Tags" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Schedule-CRONExpression" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Enabled" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Notify-Repeat" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Notify-Emails" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Schedule-RunTimespan" });

            migrationBuilder.InsertData(
                table: "EAMonitorState",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Unknown" });

            migrationBuilder.InsertData(
                table: "EAMonitorState",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Up" });

            migrationBuilder.InsertData(
                table: "EAMonitorState",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Down" });

            migrationBuilder.InsertData(
                table: "EAMonitorState",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Warning" });

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
                name: "IX_EAMonitorJob_MonitorId",
                table: "EAMonitorJob",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitorJob_MonitorStateId",
                table: "EAMonitorJob",
                column: "MonitorStateId");

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitorSetting_MonitorId",
                table: "EAMonitorSetting",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitorSetting_SettingEnvironmentId",
                table: "EAMonitorSetting",
                column: "SettingEnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EAMonitorSetting_SettingKeyId",
                table: "EAMonitorSetting",
                column: "SettingKeyId");
            migrationBuilder.Sql(@"
                CREATE VIEW v_EAMonitor AS
                SELECT
                    eaM.Id,
                    eaM.Name,
                    eaM.Description,
                    eaM.LastModified,
                    eaM.Created,
                    eaMs.Name as ""MonitorState"",
                    eaJob.Created as ""LastJobCreatedAt"",
                    eaJob.LastModified as ""LastJobModifiedAt"",
                    eaJob.Completed as ""LastJobCompletedAt"",
                    eaMJS.Name as ""LastJobStatus""
                FROM EAMonitor eaM
                LEFT JOIN EAMonitorJob eaJob
                    ON eaJob.Id = (
                        SELECT Id
                        FROM EAMonitorJob
                        WHERE MonitorId = eam.Id
                        ORDER BY Created DESC
                        LIMIT 1
                    )
                JOIN EAMonitorState eaMs
                    ON eaMs.Id = eaM.MonitorStateId
                JOIN EAMonitorJobStatus eaMJS
                    ON eaMJS.Id = eaJob.JobStatusId
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EAMonitorJob");

            migrationBuilder.DropTable(
                name: "EAMonitorSetting");

            migrationBuilder.DropTable(
                name: "EAMonitorJobStatus");

            migrationBuilder.DropTable(
                name: "EAMonitor");

            migrationBuilder.DropTable(
                name: "EAMonitorEnvironment");

            migrationBuilder.DropTable(
                name: "EAMonitorSettingKey");

            migrationBuilder.DropTable(
                name: "EAMonitorState");
        }
    }
}
