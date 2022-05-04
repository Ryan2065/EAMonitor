using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EAMonitor.Migrations.SQLite
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
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
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
                    Notified = table.Column<bool>(nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "EAMonitorSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 129, "Reserved setting key for future features", "__Reserved129__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 130, "Reserved setting key for future features", "__Reserved130__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 131, "Reserved setting key for future features", "__Reserved131__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 132, "Reserved setting key for future features", "__Reserved132__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 133, "Reserved setting key for future features", "__Reserved133__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 134, "Reserved setting key for future features", "__Reserved134__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 135, "Reserved setting key for future features", "__Reserved135__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 136, "Reserved setting key for future features", "__Reserved136__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 137, "Reserved setting key for future features", "__Reserved137__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 138, "Reserved setting key for future features", "__Reserved138__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 141, "Reserved setting key for future features", "__Reserved141__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 140, "Reserved setting key for future features", "__Reserved140__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 128, "Reserved setting key for future features", "__Reserved128__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 142, "Reserved setting key for future features", "__Reserved142__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 143, "Reserved setting key for future features", "__Reserved143__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 144, "Reserved setting key for future features", "__Reserved144__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 145, "Reserved setting key for future features", "__Reserved145__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 146, "Reserved setting key for future features", "__Reserved146__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 147, "Reserved setting key for future features", "__Reserved147__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 148, "Reserved setting key for future features", "__Reserved148__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 149, "Reserved setting key for future features", "__Reserved149__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 139, "Reserved setting key for future features", "__Reserved139__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 127, "Reserved setting key for future features", "__Reserved127__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 124, "Reserved setting key for future features", "__Reserved124__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 125, "Reserved setting key for future features", "__Reserved125__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 103, "Reserved setting key for future features", "__Reserved103__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 104, "Reserved setting key for future features", "__Reserved104__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 105, "Reserved setting key for future features", "__Reserved105__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 106, "Reserved setting key for future features", "__Reserved106__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 107, "Reserved setting key for future features", "__Reserved107__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 108, "Reserved setting key for future features", "__Reserved108__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 109, "Reserved setting key for future features", "__Reserved109__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 110, "Reserved setting key for future features", "__Reserved110__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 111, "Reserved setting key for future features", "__Reserved111__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 112, "Reserved setting key for future features", "__Reserved112__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 126, "Reserved setting key for future features", "__Reserved126__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 113, "Reserved setting key for future features", "__Reserved113__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 115, "Reserved setting key for future features", "__Reserved115__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 116, "Reserved setting key for future features", "__Reserved116__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 117, "Reserved setting key for future features", "__Reserved117__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 118, "Reserved setting key for future features", "__Reserved118__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 119, "Reserved setting key for future features", "__Reserved119__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 120, "Reserved setting key for future features", "__Reserved120__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 121, "Reserved setting key for future features", "__Reserved121__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 122, "Reserved setting key for future features", "__Reserved122__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 123, "Reserved setting key for future features", "__Reserved123__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 150, "Reserved setting key for future features", "__Reserved150__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 114, "Reserved setting key for future features", "__Reserved114__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 151, "Reserved setting key for future features", "__Reserved151__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 153, "Reserved setting key for future features", "__Reserved153__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 102, "Reserved setting key for future features", "__Reserved102__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 180, "Reserved setting key for future features", "__Reserved180__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 181, "Reserved setting key for future features", "__Reserved181__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 182, "Reserved setting key for future features", "__Reserved182__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 183, "Reserved setting key for future features", "__Reserved183__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 184, "Reserved setting key for future features", "__Reserved184__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 185, "Reserved setting key for future features", "__Reserved185__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 186, "Reserved setting key for future features", "__Reserved186__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 187, "Reserved setting key for future features", "__Reserved187__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 188, "Reserved setting key for future features", "__Reserved188__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 189, "Reserved setting key for future features", "__Reserved189__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 190, "Reserved setting key for future features", "__Reserved190__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 191, "Reserved setting key for future features", "__Reserved191__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 192, "Reserved setting key for future features", "__Reserved192__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 193, "Reserved setting key for future features", "__Reserved193__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 194, "Reserved setting key for future features", "__Reserved194__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 195, "Reserved setting key for future features", "__Reserved195__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 196, "Reserved setting key for future features", "__Reserved196__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 197, "Reserved setting key for future features", "__Reserved197__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 198, "Reserved setting key for future features", "__Reserved198__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 199, "Reserved setting key for future features", "__Reserved199__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 200, "Reserved setting key for future features", "__Reserved200__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 179, "Reserved setting key for future features", "__Reserved179__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 178, "Reserved setting key for future features", "__Reserved178__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 177, "Reserved setting key for future features", "__Reserved177__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 176, "Reserved setting key for future features", "__Reserved176__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 154, "Reserved setting key for future features", "__Reserved154__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 155, "Reserved setting key for future features", "__Reserved155__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 156, "Reserved setting key for future features", "__Reserved156__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 157, "Reserved setting key for future features", "__Reserved157__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 158, "Reserved setting key for future features", "__Reserved158__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 159, "Reserved setting key for future features", "__Reserved159__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 160, "Reserved setting key for future features", "__Reserved160__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 161, "Reserved setting key for future features", "__Reserved161__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 162, "Reserved setting key for future features", "__Reserved162__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 163, "Reserved setting key for future features", "__Reserved163__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 152, "Reserved setting key for future features", "__Reserved152__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 164, "Reserved setting key for future features", "__Reserved164__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 166, "Reserved setting key for future features", "__Reserved166__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 167, "Reserved setting key for future features", "__Reserved167__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 168, "Reserved setting key for future features", "__Reserved168__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 169, "Reserved setting key for future features", "__Reserved169__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 170, "Reserved setting key for future features", "__Reserved170__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 171, "Reserved setting key for future features", "__Reserved171__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 172, "Reserved setting key for future features", "__Reserved172__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 173, "Reserved setting key for future features", "__Reserved173__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 174, "Reserved setting key for future features", "__Reserved174__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 175, "Reserved setting key for future features", "__Reserved175__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 165, "Reserved setting key for future features", "__Reserved165__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 101, "Reserved setting key for future features", "__Reserved101__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 100, "Reserved setting key for future features", "__Reserved100__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 99, "Reserved setting key for future features", "__Reserved99__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 26, "Reserved setting key for future features", "__Reserved26__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 27, "Reserved setting key for future features", "__Reserved27__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 28, "Reserved setting key for future features", "__Reserved28__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 29, "Reserved setting key for future features", "__Reserved29__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 30, "Reserved setting key for future features", "__Reserved30__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 31, "Reserved setting key for future features", "__Reserved31__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 32, "Reserved setting key for future features", "__Reserved32__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 33, "Reserved setting key for future features", "__Reserved33__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 34, "Reserved setting key for future features", "__Reserved34__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 35, "Reserved setting key for future features", "__Reserved35__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 36, "Reserved setting key for future features", "__Reserved36__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 37, "Reserved setting key for future features", "__Reserved37__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 38, "Reserved setting key for future features", "__Reserved38__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 39, "Reserved setting key for future features", "__Reserved39__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 40, "Reserved setting key for future features", "__Reserved40__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 41, "Reserved setting key for future features", "__Reserved41__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 42, "Reserved setting key for future features", "__Reserved42__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 43, "Reserved setting key for future features", "__Reserved43__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 44, "Reserved setting key for future features", "__Reserved44__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 45, "Reserved setting key for future features", "__Reserved45__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 46, "Reserved setting key for future features", "__Reserved46__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 25, "Reserved setting key for future features", "__Reserved25__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 47, "Reserved setting key for future features", "__Reserved47__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 24, "Reserved setting key for future features", "__Reserved24__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 22, "Reserved setting key for future features", "__Reserved22__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 4, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 5, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 6, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 7, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 8, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 9, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 10, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 11, "Monitor description", "Description" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 12, "Reserved setting key for future features", "__Reserved12__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 13, "Reserved setting key for future features", "__Reserved13__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 14, "Reserved setting key for future features", "__Reserved14__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 15, "Reserved setting key for future features", "__Reserved15__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 16, "Reserved setting key for future features", "__Reserved16__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 17, "Reserved setting key for future features", "__Reserved17__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 18, "Reserved setting key for future features", "__Reserved18__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 19, "Reserved setting key for future features", "__Reserved19__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 20, "Reserved setting key for future features", "__Reserved20__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 21, "Reserved setting key for future features", "__Reserved21__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 23, "Reserved setting key for future features", "__Reserved23__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 49, "Reserved setting key for future features", "__Reserved49__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 48, "Reserved setting key for future features", "__Reserved48__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 51, "Reserved setting key for future features", "__Reserved51__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 77, "Reserved setting key for future features", "__Reserved77__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 78, "Reserved setting key for future features", "__Reserved78__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 79, "Reserved setting key for future features", "__Reserved79__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 80, "Reserved setting key for future features", "__Reserved80__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 81, "Reserved setting key for future features", "__Reserved81__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 82, "Reserved setting key for future features", "__Reserved82__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 83, "Reserved setting key for future features", "__Reserved83__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 84, "Reserved setting key for future features", "__Reserved84__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 85, "Reserved setting key for future features", "__Reserved85__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 86, "Reserved setting key for future features", "__Reserved86__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 50, "Reserved setting key for future features", "__Reserved50__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 87, "Reserved setting key for future features", "__Reserved87__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 89, "Reserved setting key for future features", "__Reserved89__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 90, "Reserved setting key for future features", "__Reserved90__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 91, "Reserved setting key for future features", "__Reserved91__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 92, "Reserved setting key for future features", "__Reserved92__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 93, "Reserved setting key for future features", "__Reserved93__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 94, "Reserved setting key for future features", "__Reserved94__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 95, "Reserved setting key for future features", "__Reserved95__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 96, "Reserved setting key for future features", "__Reserved96__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 97, "Reserved setting key for future features", "__Reserved97__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 98, "Reserved setting key for future features", "__Reserved98__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 88, "Reserved setting key for future features", "__Reserved88__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 75, "Reserved setting key for future features", "__Reserved75__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 76, "Reserved setting key for future features", "__Reserved76__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 73, "Reserved setting key for future features", "__Reserved73__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 52, "Reserved setting key for future features", "__Reserved52__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 53, "Reserved setting key for future features", "__Reserved53__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 54, "Reserved setting key for future features", "__Reserved54__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 55, "Reserved setting key for future features", "__Reserved55__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 56, "Reserved setting key for future features", "__Reserved56__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 57, "Reserved setting key for future features", "__Reserved57__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 58, "Reserved setting key for future features", "__Reserved58__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 59, "Reserved setting key for future features", "__Reserved59__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 74, "Reserved setting key for future features", "__Reserved74__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 61, "Reserved setting key for future features", "__Reserved61__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 60, "Reserved setting key for future features", "__Reserved60__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 63, "Reserved setting key for future features", "__Reserved63__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 64, "Reserved setting key for future features", "__Reserved64__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 65, "Reserved setting key for future features", "__Reserved65__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 66, "Reserved setting key for future features", "__Reserved66__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 67, "Reserved setting key for future features", "__Reserved67__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 68, "Reserved setting key for future features", "__Reserved68__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 69, "Reserved setting key for future features", "__Reserved69__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 70, "Reserved setting key for future features", "__Reserved70__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 71, "Reserved setting key for future features", "__Reserved71__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 72, "Reserved setting key for future features", "__Reserved72__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 62, "Reserved setting key for future features", "__Reserved62__" });

            migrationBuilder.InsertData(
                table: "EAMonitorState",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Down" });

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
                values: new object[] { 4, "Warning" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 1, new DateTime(2022, 5, 4, 14, 0, 47, 884, DateTimeKind.Utc).AddTicks(479), null, 8, "False" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 122, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 131, "__Reserved131__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 123, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 132, "__Reserved132__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 124, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 133, "__Reserved133__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 125, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 134, "__Reserved134__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 126, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 135, "__Reserved135__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 127, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 136, "__Reserved136__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 128, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 137, "__Reserved137__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 129, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 138, "__Reserved138__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 130, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 139, "__Reserved139__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 121, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 130, "__Reserved130__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 131, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 140, "__Reserved140__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 133, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 142, "__Reserved142__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 134, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 143, "__Reserved143__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 135, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 144, "__Reserved144__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 136, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 145, "__Reserved145__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 137, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 146, "__Reserved146__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 138, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 147, "__Reserved147__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 139, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 148, "__Reserved148__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 140, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 149, "__Reserved149__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 141, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 150, "__Reserved150__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 132, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 141, "__Reserved141__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 142, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 151, "__Reserved151__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 120, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 129, "__Reserved129__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 118, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 127, "__Reserved127__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 98, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 107, "__Reserved107__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 99, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 108, "__Reserved108__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 100, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 109, "__Reserved109__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 101, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 110, "__Reserved110__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 102, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 111, "__Reserved111__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 103, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 112, "__Reserved112__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 104, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 113, "__Reserved113__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 105, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 114, "__Reserved114__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 106, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 115, "__Reserved115__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 119, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 128, "__Reserved128__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 107, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 116, "__Reserved116__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 109, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 118, "__Reserved118__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 110, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 119, "__Reserved119__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 111, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 120, "__Reserved120__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 112, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 121, "__Reserved121__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 113, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 122, "__Reserved122__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 114, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 123, "__Reserved123__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 115, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 124, "__Reserved124__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 116, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 125, "__Reserved125__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 117, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 126, "__Reserved126__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 108, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 117, "__Reserved117__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 97, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 106, "__Reserved106__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 143, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 152, "__Reserved152__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 145, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 154, "__Reserved154__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 170, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 179, "__Reserved179__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 171, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 180, "__Reserved180__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 172, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 181, "__Reserved181__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 173, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 182, "__Reserved182__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 174, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 183, "__Reserved183__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 175, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 184, "__Reserved184__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 176, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 185, "__Reserved185__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 177, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 186, "__Reserved186__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 178, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 187, "__Reserved187__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 169, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 178, "__Reserved178__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 179, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 188, "__Reserved188__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 181, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 190, "__Reserved190__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 182, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 191, "__Reserved191__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 183, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 192, "__Reserved192__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 184, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 193, "__Reserved193__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 185, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 194, "__Reserved194__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 186, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 195, "__Reserved195__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 187, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 196, "__Reserved196__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 188, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 197, "__Reserved197__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 189, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 198, "__Reserved198__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 180, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 189, "__Reserved189__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 144, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 153, "__Reserved153__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 168, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 177, "__Reserved177__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 166, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 175, "__Reserved175__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 146, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 155, "__Reserved155__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 147, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 156, "__Reserved156__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 148, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 157, "__Reserved157__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 149, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 158, "__Reserved158__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 150, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 159, "__Reserved159__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 151, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 160, "__Reserved160__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 152, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 161, "__Reserved161__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 153, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 162, "__Reserved162__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 154, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 163, "__Reserved163__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 167, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 176, "__Reserved176__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 155, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 164, "__Reserved164__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 157, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 166, "__Reserved166__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 158, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 167, "__Reserved167__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 159, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 168, "__Reserved168__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 160, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 169, "__Reserved169__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 161, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 170, "__Reserved170__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 162, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 171, "__Reserved171__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 163, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 172, "__Reserved172__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 164, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 173, "__Reserved173__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 165, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 174, "__Reserved174__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 156, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 165, "__Reserved165__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 190, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 199, "__Reserved199__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 96, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 105, "__Reserved105__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 94, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 103, "__Reserved103__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 26, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 35, "__Reserved35__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 27, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 36, "__Reserved36__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 28, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 37, "__Reserved37__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 29, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 38, "__Reserved38__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 30, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 39, "__Reserved39__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 31, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 40, "__Reserved40__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 32, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 41, "__Reserved41__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 33, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 42, "__Reserved42__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 34, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 43, "__Reserved43__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 25, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 34, "__Reserved34__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 35, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 44, "__Reserved44__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 37, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 46, "__Reserved46__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 38, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 47, "__Reserved47__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 39, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 48, "__Reserved48__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 40, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 49, "__Reserved49__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 41, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 50, "__Reserved50__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 42, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 51, "__Reserved51__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 43, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 52, "__Reserved52__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 44, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 53, "__Reserved53__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 45, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 54, "__Reserved54__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 36, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 45, "__Reserved45__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 46, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 55, "__Reserved55__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 24, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 33, "__Reserved33__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 22, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 31, "__Reserved31__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 2, new DateTime(2022, 5, 4, 14, 0, 47, 885, DateTimeKind.Utc).AddTicks(453), null, 9, "15" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 3, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 12, "__Reserved12__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 4, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 13, "__Reserved13__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 5, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 14, "__Reserved14__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 6, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 15, "__Reserved15__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 7, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 16, "__Reserved16__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 8, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 17, "__Reserved17__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 9, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 18, "__Reserved18__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 10, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 19, "__Reserved19__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 23, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 32, "__Reserved32__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 11, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 20, "__Reserved20__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 13, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 22, "__Reserved22__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 14, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 23, "__Reserved23__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 15, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 24, "__Reserved24__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 16, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 25, "__Reserved25__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 17, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 26, "__Reserved26__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 18, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 27, "__Reserved27__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 19, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 28, "__Reserved28__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 20, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 29, "__Reserved29__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 21, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 30, "__Reserved30__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 12, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 21, "__Reserved21__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 95, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 104, "__Reserved104__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 47, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 56, "__Reserved56__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 49, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 58, "__Reserved58__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 74, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 83, "__Reserved83__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 75, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 84, "__Reserved84__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 76, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 85, "__Reserved85__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 77, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 86, "__Reserved86__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 78, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 87, "__Reserved87__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 79, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 88, "__Reserved88__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 80, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 89, "__Reserved89__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 81, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 90, "__Reserved90__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 82, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 91, "__Reserved91__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 73, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 82, "__Reserved82__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 83, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 92, "__Reserved92__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 85, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 94, "__Reserved94__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 86, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 95, "__Reserved95__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 87, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 96, "__Reserved96__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 88, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 97, "__Reserved97__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 89, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 98, "__Reserved98__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 90, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 99, "__Reserved99__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 91, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 100, "__Reserved100__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 92, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 101, "__Reserved101__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 93, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 102, "__Reserved102__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 84, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 93, "__Reserved93__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 48, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 57, "__Reserved57__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 72, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 81, "__Reserved81__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 70, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 79, "__Reserved79__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 50, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 59, "__Reserved59__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 51, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 60, "__Reserved60__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 52, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 61, "__Reserved61__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 53, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 62, "__Reserved62__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 54, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 63, "__Reserved63__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 55, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 64, "__Reserved64__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 56, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 65, "__Reserved65__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 57, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 66, "__Reserved66__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 58, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 67, "__Reserved67__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 71, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 80, "__Reserved80__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 59, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 68, "__Reserved68__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 61, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 70, "__Reserved70__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 62, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 71, "__Reserved71__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 63, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 72, "__Reserved72__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 64, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 73, "__Reserved73__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 65, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 74, "__Reserved74__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 66, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 75, "__Reserved75__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 67, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 76, "__Reserved76__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 68, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 77, "__Reserved77__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 69, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 78, "__Reserved78__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 60, new DateTime(2022, 5, 4, 14, 0, 47, 886, DateTimeKind.Utc).AddTicks(481), null, 69, "__Reserved69__" });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[] { 191, new DateTime(2022, 5, 4, 14, 0, 47, 887, DateTimeKind.Utc).AddTicks(455), null, 200, "__Reserved200__" });

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
