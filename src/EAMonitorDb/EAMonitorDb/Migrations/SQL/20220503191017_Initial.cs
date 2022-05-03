using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EAMonitor.Migrations.SQL
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
                table: "EAMonitorSettingKey",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 129, "Reserved setting key for future features", "__Reserved129__" },
                    { 130, "Reserved setting key for future features", "__Reserved130__" },
                    { 131, "Reserved setting key for future features", "__Reserved131__" },
                    { 132, "Reserved setting key for future features", "__Reserved132__" },
                    { 133, "Reserved setting key for future features", "__Reserved133__" },
                    { 134, "Reserved setting key for future features", "__Reserved134__" },
                    { 135, "Reserved setting key for future features", "__Reserved135__" },
                    { 136, "Reserved setting key for future features", "__Reserved136__" },
                    { 137, "Reserved setting key for future features", "__Reserved137__" },
                    { 138, "Reserved setting key for future features", "__Reserved138__" },
                    { 141, "Reserved setting key for future features", "__Reserved141__" },
                    { 140, "Reserved setting key for future features", "__Reserved140__" },
                    { 128, "Reserved setting key for future features", "__Reserved128__" },
                    { 142, "Reserved setting key for future features", "__Reserved142__" },
                    { 143, "Reserved setting key for future features", "__Reserved143__" },
                    { 144, "Reserved setting key for future features", "__Reserved144__" },
                    { 145, "Reserved setting key for future features", "__Reserved145__" },
                    { 146, "Reserved setting key for future features", "__Reserved146__" },
                    { 147, "Reserved setting key for future features", "__Reserved147__" },
                    { 148, "Reserved setting key for future features", "__Reserved148__" },
                    { 149, "Reserved setting key for future features", "__Reserved149__" },
                    { 139, "Reserved setting key for future features", "__Reserved139__" },
                    { 127, "Reserved setting key for future features", "__Reserved127__" },
                    { 124, "Reserved setting key for future features", "__Reserved124__" },
                    { 125, "Reserved setting key for future features", "__Reserved125__" },
                    { 103, "Reserved setting key for future features", "__Reserved103__" },
                    { 104, "Reserved setting key for future features", "__Reserved104__" },
                    { 105, "Reserved setting key for future features", "__Reserved105__" },
                    { 106, "Reserved setting key for future features", "__Reserved106__" },
                    { 107, "Reserved setting key for future features", "__Reserved107__" },
                    { 108, "Reserved setting key for future features", "__Reserved108__" },
                    { 109, "Reserved setting key for future features", "__Reserved109__" },
                    { 110, "Reserved setting key for future features", "__Reserved110__" },
                    { 111, "Reserved setting key for future features", "__Reserved111__" },
                    { 112, "Reserved setting key for future features", "__Reserved112__" },
                    { 126, "Reserved setting key for future features", "__Reserved126__" },
                    { 113, "Reserved setting key for future features", "__Reserved113__" },
                    { 115, "Reserved setting key for future features", "__Reserved115__" },
                    { 116, "Reserved setting key for future features", "__Reserved116__" },
                    { 117, "Reserved setting key for future features", "__Reserved117__" },
                    { 118, "Reserved setting key for future features", "__Reserved118__" },
                    { 119, "Reserved setting key for future features", "__Reserved119__" },
                    { 120, "Reserved setting key for future features", "__Reserved120__" },
                    { 121, "Reserved setting key for future features", "__Reserved121__" },
                    { 122, "Reserved setting key for future features", "__Reserved122__" },
                    { 123, "Reserved setting key for future features", "__Reserved123__" },
                    { 150, "Reserved setting key for future features", "__Reserved150__" },
                    { 114, "Reserved setting key for future features", "__Reserved114__" },
                    { 151, "Reserved setting key for future features", "__Reserved151__" },
                    { 153, "Reserved setting key for future features", "__Reserved153__" },
                    { 102, "Reserved setting key for future features", "__Reserved102__" },
                    { 180, "Reserved setting key for future features", "__Reserved180__" },
                    { 181, "Reserved setting key for future features", "__Reserved181__" },
                    { 182, "Reserved setting key for future features", "__Reserved182__" },
                    { 183, "Reserved setting key for future features", "__Reserved183__" },
                    { 184, "Reserved setting key for future features", "__Reserved184__" },
                    { 185, "Reserved setting key for future features", "__Reserved185__" },
                    { 186, "Reserved setting key for future features", "__Reserved186__" },
                    { 187, "Reserved setting key for future features", "__Reserved187__" },
                    { 188, "Reserved setting key for future features", "__Reserved188__" },
                    { 189, "Reserved setting key for future features", "__Reserved189__" },
                    { 190, "Reserved setting key for future features", "__Reserved190__" },
                    { 191, "Reserved setting key for future features", "__Reserved191__" },
                    { 192, "Reserved setting key for future features", "__Reserved192__" },
                    { 193, "Reserved setting key for future features", "__Reserved193__" },
                    { 194, "Reserved setting key for future features", "__Reserved194__" },
                    { 195, "Reserved setting key for future features", "__Reserved195__" },
                    { 196, "Reserved setting key for future features", "__Reserved196__" },
                    { 197, "Reserved setting key for future features", "__Reserved197__" },
                    { 198, "Reserved setting key for future features", "__Reserved198__" },
                    { 199, "Reserved setting key for future features", "__Reserved199__" },
                    { 200, "Reserved setting key for future features", "__Reserved200__" },
                    { 179, "Reserved setting key for future features", "__Reserved179__" },
                    { 178, "Reserved setting key for future features", "__Reserved178__" },
                    { 177, "Reserved setting key for future features", "__Reserved177__" },
                    { 176, "Reserved setting key for future features", "__Reserved176__" },
                    { 154, "Reserved setting key for future features", "__Reserved154__" },
                    { 155, "Reserved setting key for future features", "__Reserved155__" },
                    { 156, "Reserved setting key for future features", "__Reserved156__" },
                    { 157, "Reserved setting key for future features", "__Reserved157__" },
                    { 158, "Reserved setting key for future features", "__Reserved158__" },
                    { 159, "Reserved setting key for future features", "__Reserved159__" },
                    { 160, "Reserved setting key for future features", "__Reserved160__" },
                    { 161, "Reserved setting key for future features", "__Reserved161__" },
                    { 162, "Reserved setting key for future features", "__Reserved162__" },
                    { 163, "Reserved setting key for future features", "__Reserved163__" },
                    { 152, "Reserved setting key for future features", "__Reserved152__" },
                    { 164, "Reserved setting key for future features", "__Reserved164__" },
                    { 166, "Reserved setting key for future features", "__Reserved166__" },
                    { 167, "Reserved setting key for future features", "__Reserved167__" },
                    { 168, "Reserved setting key for future features", "__Reserved168__" },
                    { 169, "Reserved setting key for future features", "__Reserved169__" },
                    { 170, "Reserved setting key for future features", "__Reserved170__" },
                    { 171, "Reserved setting key for future features", "__Reserved171__" },
                    { 172, "Reserved setting key for future features", "__Reserved172__" },
                    { 173, "Reserved setting key for future features", "__Reserved173__" },
                    { 174, "Reserved setting key for future features", "__Reserved174__" },
                    { 175, "Reserved setting key for future features", "__Reserved175__" },
                    { 165, "Reserved setting key for future features", "__Reserved165__" },
                    { 101, "Reserved setting key for future features", "__Reserved101__" },
                    { 100, "Reserved setting key for future features", "__Reserved100__" },
                    { 99, "Reserved setting key for future features", "__Reserved99__" },
                    { 26, "Reserved setting key for future features", "__Reserved26__" },
                    { 27, "Reserved setting key for future features", "__Reserved27__" },
                    { 28, "Reserved setting key for future features", "__Reserved28__" },
                    { 29, "Reserved setting key for future features", "__Reserved29__" },
                    { 30, "Reserved setting key for future features", "__Reserved30__" },
                    { 31, "Reserved setting key for future features", "__Reserved31__" },
                    { 32, "Reserved setting key for future features", "__Reserved32__" },
                    { 33, "Reserved setting key for future features", "__Reserved33__" },
                    { 34, "Reserved setting key for future features", "__Reserved34__" },
                    { 35, "Reserved setting key for future features", "__Reserved35__" },
                    { 36, "Reserved setting key for future features", "__Reserved36__" },
                    { 37, "Reserved setting key for future features", "__Reserved37__" },
                    { 38, "Reserved setting key for future features", "__Reserved38__" },
                    { 39, "Reserved setting key for future features", "__Reserved39__" },
                    { 40, "Reserved setting key for future features", "__Reserved40__" },
                    { 41, "Reserved setting key for future features", "__Reserved41__" },
                    { 42, "Reserved setting key for future features", "__Reserved42__" },
                    { 43, "Reserved setting key for future features", "__Reserved43__" },
                    { 44, "Reserved setting key for future features", "__Reserved44__" },
                    { 45, "Reserved setting key for future features", "__Reserved45__" },
                    { 46, "Reserved setting key for future features", "__Reserved46__" },
                    { 25, "Reserved setting key for future features", "__Reserved25__" },
                    { 47, "Reserved setting key for future features", "__Reserved47__" },
                    { 24, "Reserved setting key for future features", "__Reserved24__" },
                    { 22, "Reserved setting key for future features", "__Reserved22__" },
                    { 1, "Monitor description", "Description" },
                    { 2, "Monitor description", "Description" },
                    { 3, "Monitor description", "Description" },
                    { 4, "Monitor description", "Description" },
                    { 5, "Monitor description", "Description" },
                    { 6, "Monitor description", "Description" },
                    { 7, "Monitor description", "Description" },
                    { 8, "Monitor description", "Description" },
                    { 9, "Monitor description", "Description" },
                    { 10, "Monitor description", "Description" },
                    { 11, "Monitor description", "Description" },
                    { 12, "Reserved setting key for future features", "__Reserved12__" },
                    { 13, "Reserved setting key for future features", "__Reserved13__" },
                    { 14, "Reserved setting key for future features", "__Reserved14__" },
                    { 15, "Reserved setting key for future features", "__Reserved15__" },
                    { 16, "Reserved setting key for future features", "__Reserved16__" },
                    { 17, "Reserved setting key for future features", "__Reserved17__" },
                    { 18, "Reserved setting key for future features", "__Reserved18__" },
                    { 19, "Reserved setting key for future features", "__Reserved19__" },
                    { 20, "Reserved setting key for future features", "__Reserved20__" },
                    { 21, "Reserved setting key for future features", "__Reserved21__" },
                    { 23, "Reserved setting key for future features", "__Reserved23__" },
                    { 49, "Reserved setting key for future features", "__Reserved49__" },
                    { 48, "Reserved setting key for future features", "__Reserved48__" },
                    { 51, "Reserved setting key for future features", "__Reserved51__" },
                    { 77, "Reserved setting key for future features", "__Reserved77__" },
                    { 78, "Reserved setting key for future features", "__Reserved78__" },
                    { 79, "Reserved setting key for future features", "__Reserved79__" },
                    { 80, "Reserved setting key for future features", "__Reserved80__" },
                    { 81, "Reserved setting key for future features", "__Reserved81__" },
                    { 82, "Reserved setting key for future features", "__Reserved82__" },
                    { 83, "Reserved setting key for future features", "__Reserved83__" },
                    { 84, "Reserved setting key for future features", "__Reserved84__" },
                    { 85, "Reserved setting key for future features", "__Reserved85__" },
                    { 86, "Reserved setting key for future features", "__Reserved86__" },
                    { 50, "Reserved setting key for future features", "__Reserved50__" },
                    { 87, "Reserved setting key for future features", "__Reserved87__" },
                    { 89, "Reserved setting key for future features", "__Reserved89__" },
                    { 90, "Reserved setting key for future features", "__Reserved90__" },
                    { 91, "Reserved setting key for future features", "__Reserved91__" },
                    { 92, "Reserved setting key for future features", "__Reserved92__" },
                    { 93, "Reserved setting key for future features", "__Reserved93__" },
                    { 94, "Reserved setting key for future features", "__Reserved94__" },
                    { 95, "Reserved setting key for future features", "__Reserved95__" },
                    { 96, "Reserved setting key for future features", "__Reserved96__" },
                    { 97, "Reserved setting key for future features", "__Reserved97__" },
                    { 98, "Reserved setting key for future features", "__Reserved98__" },
                    { 88, "Reserved setting key for future features", "__Reserved88__" },
                    { 75, "Reserved setting key for future features", "__Reserved75__" },
                    { 76, "Reserved setting key for future features", "__Reserved76__" },
                    { 73, "Reserved setting key for future features", "__Reserved73__" },
                    { 52, "Reserved setting key for future features", "__Reserved52__" },
                    { 53, "Reserved setting key for future features", "__Reserved53__" },
                    { 54, "Reserved setting key for future features", "__Reserved54__" },
                    { 55, "Reserved setting key for future features", "__Reserved55__" },
                    { 56, "Reserved setting key for future features", "__Reserved56__" },
                    { 57, "Reserved setting key for future features", "__Reserved57__" },
                    { 58, "Reserved setting key for future features", "__Reserved58__" },
                    { 59, "Reserved setting key for future features", "__Reserved59__" },
                    { 74, "Reserved setting key for future features", "__Reserved74__" },
                    { 61, "Reserved setting key for future features", "__Reserved61__" },
                    { 60, "Reserved setting key for future features", "__Reserved60__" },
                    { 63, "Reserved setting key for future features", "__Reserved63__" },
                    { 64, "Reserved setting key for future features", "__Reserved64__" },
                    { 65, "Reserved setting key for future features", "__Reserved65__" },
                    { 66, "Reserved setting key for future features", "__Reserved66__" },
                    { 67, "Reserved setting key for future features", "__Reserved67__" },
                    { 68, "Reserved setting key for future features", "__Reserved68__" },
                    { 69, "Reserved setting key for future features", "__Reserved69__" },
                    { 70, "Reserved setting key for future features", "__Reserved70__" },
                    { 71, "Reserved setting key for future features", "__Reserved71__" },
                    { 72, "Reserved setting key for future features", "__Reserved72__" },
                    { 62, "Reserved setting key for future features", "__Reserved62__" }
                });

            migrationBuilder.InsertData(
                table: "EAMonitorState",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Down" },
                    { 1, "Unknown" },
                    { 2, "Up" },
                    { 4, "Warning" }
                });

            migrationBuilder.InsertData(
                table: "EAMonitorSetting",
                columns: new[] { "Id", "LastModified", "MonitorId", "SettingKeyId", "SettingValue" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 5, 3, 19, 10, 15, 365, DateTimeKind.Utc).AddTicks(3131), null, 8, "False" },
                    { 122, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 131, "__Reserved131__" },
                    { 123, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 132, "__Reserved132__" },
                    { 124, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 133, "__Reserved133__" },
                    { 125, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 134, "__Reserved134__" },
                    { 126, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 135, "__Reserved135__" },
                    { 127, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 136, "__Reserved136__" },
                    { 128, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 137, "__Reserved137__" },
                    { 129, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 138, "__Reserved138__" },
                    { 130, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 139, "__Reserved139__" },
                    { 121, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 130, "__Reserved130__" },
                    { 131, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 140, "__Reserved140__" },
                    { 133, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 142, "__Reserved142__" },
                    { 134, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 143, "__Reserved143__" },
                    { 135, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 144, "__Reserved144__" },
                    { 136, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 145, "__Reserved145__" },
                    { 137, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 146, "__Reserved146__" },
                    { 138, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 147, "__Reserved147__" },
                    { 139, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 148, "__Reserved148__" },
                    { 140, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 149, "__Reserved149__" },
                    { 141, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 150, "__Reserved150__" },
                    { 132, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 141, "__Reserved141__" },
                    { 142, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 151, "__Reserved151__" },
                    { 120, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 129, "__Reserved129__" },
                    { 118, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 127, "__Reserved127__" },
                    { 98, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 107, "__Reserved107__" },
                    { 99, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 108, "__Reserved108__" },
                    { 100, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 109, "__Reserved109__" },
                    { 101, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 110, "__Reserved110__" },
                    { 102, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 111, "__Reserved111__" },
                    { 103, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 112, "__Reserved112__" },
                    { 104, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 113, "__Reserved113__" },
                    { 105, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 114, "__Reserved114__" },
                    { 106, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 115, "__Reserved115__" },
                    { 119, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(541), null, 128, "__Reserved128__" },
                    { 107, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 116, "__Reserved116__" },
                    { 109, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 118, "__Reserved118__" },
                    { 110, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 119, "__Reserved119__" },
                    { 111, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 120, "__Reserved120__" },
                    { 112, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 121, "__Reserved121__" },
                    { 113, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 122, "__Reserved122__" },
                    { 114, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 123, "__Reserved123__" },
                    { 115, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 124, "__Reserved124__" },
                    { 116, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 125, "__Reserved125__" },
                    { 117, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 126, "__Reserved126__" },
                    { 108, new DateTime(2022, 5, 3, 19, 10, 15, 370, DateTimeKind.Utc).AddTicks(3103), null, 117, "__Reserved117__" },
                    { 97, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 106, "__Reserved106__" },
                    { 143, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 152, "__Reserved152__" },
                    { 145, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 154, "__Reserved154__" },
                    { 170, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 179, "__Reserved179__" },
                    { 171, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 180, "__Reserved180__" },
                    { 172, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 181, "__Reserved181__" },
                    { 173, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 182, "__Reserved182__" },
                    { 174, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 183, "__Reserved183__" },
                    { 175, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 184, "__Reserved184__" },
                    { 176, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 185, "__Reserved185__" },
                    { 177, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 186, "__Reserved186__" },
                    { 178, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 187, "__Reserved187__" },
                    { 169, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 178, "__Reserved178__" },
                    { 179, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 188, "__Reserved188__" },
                    { 181, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 190, "__Reserved190__" },
                    { 182, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 191, "__Reserved191__" },
                    { 183, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 192, "__Reserved192__" },
                    { 184, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 193, "__Reserved193__" },
                    { 185, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 194, "__Reserved194__" },
                    { 186, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 195, "__Reserved195__" },
                    { 187, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 196, "__Reserved196__" },
                    { 188, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 197, "__Reserved197__" },
                    { 189, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 198, "__Reserved198__" },
                    { 180, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 189, "__Reserved189__" },
                    { 144, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 153, "__Reserved153__" },
                    { 168, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 177, "__Reserved177__" },
                    { 166, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 175, "__Reserved175__" },
                    { 146, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 155, "__Reserved155__" },
                    { 147, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 156, "__Reserved156__" },
                    { 148, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 157, "__Reserved157__" },
                    { 149, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 158, "__Reserved158__" },
                    { 150, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 159, "__Reserved159__" },
                    { 151, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 160, "__Reserved160__" },
                    { 152, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 161, "__Reserved161__" },
                    { 153, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 162, "__Reserved162__" },
                    { 154, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 163, "__Reserved163__" },
                    { 167, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 176, "__Reserved176__" },
                    { 155, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 164, "__Reserved164__" },
                    { 157, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 166, "__Reserved166__" },
                    { 158, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 167, "__Reserved167__" },
                    { 159, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 168, "__Reserved168__" },
                    { 160, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 169, "__Reserved169__" },
                    { 161, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 170, "__Reserved170__" },
                    { 162, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 171, "__Reserved171__" },
                    { 163, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 172, "__Reserved172__" },
                    { 164, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 173, "__Reserved173__" },
                    { 165, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 174, "__Reserved174__" },
                    { 156, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 165, "__Reserved165__" },
                    { 190, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 199, "__Reserved199__" },
                    { 96, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 105, "__Reserved105__" },
                    { 94, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 103, "__Reserved103__" },
                    { 26, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 35, "__Reserved35__" },
                    { 27, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 36, "__Reserved36__" },
                    { 28, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 37, "__Reserved37__" },
                    { 29, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 38, "__Reserved38__" },
                    { 30, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 39, "__Reserved39__" },
                    { 31, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 40, "__Reserved40__" },
                    { 32, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 41, "__Reserved41__" },
                    { 33, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 42, "__Reserved42__" },
                    { 34, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 43, "__Reserved43__" },
                    { 25, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 34, "__Reserved34__" },
                    { 35, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 44, "__Reserved44__" },
                    { 37, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 46, "__Reserved46__" },
                    { 38, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 47, "__Reserved47__" },
                    { 39, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 48, "__Reserved48__" },
                    { 40, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 49, "__Reserved49__" },
                    { 41, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 50, "__Reserved50__" },
                    { 42, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 51, "__Reserved51__" },
                    { 43, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 52, "__Reserved52__" },
                    { 44, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 53, "__Reserved53__" },
                    { 45, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 54, "__Reserved54__" },
                    { 36, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 45, "__Reserved45__" },
                    { 46, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 55, "__Reserved55__" },
                    { 24, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 33, "__Reserved33__" },
                    { 22, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 31, "__Reserved31__" },
                    { 2, new DateTime(2022, 5, 3, 19, 10, 15, 366, DateTimeKind.Utc).AddTicks(3127), null, 9, "15" },
                    { 3, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 12, "__Reserved12__" },
                    { 4, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 13, "__Reserved13__" },
                    { 5, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 14, "__Reserved14__" },
                    { 6, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 15, "__Reserved15__" },
                    { 7, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 16, "__Reserved16__" },
                    { 8, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 17, "__Reserved17__" },
                    { 9, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 18, "__Reserved18__" },
                    { 10, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 19, "__Reserved19__" },
                    { 23, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 32, "__Reserved32__" },
                    { 11, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 20, "__Reserved20__" },
                    { 13, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 22, "__Reserved22__" },
                    { 14, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 23, "__Reserved23__" },
                    { 15, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 24, "__Reserved24__" },
                    { 16, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 25, "__Reserved25__" },
                    { 17, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 26, "__Reserved26__" },
                    { 18, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 27, "__Reserved27__" },
                    { 19, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 28, "__Reserved28__" },
                    { 20, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 29, "__Reserved29__" },
                    { 21, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 30, "__Reserved30__" },
                    { 12, new DateTime(2022, 5, 3, 19, 10, 15, 368, DateTimeKind.Utc).AddTicks(3145), null, 21, "__Reserved21__" },
                    { 95, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 104, "__Reserved104__" },
                    { 47, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 56, "__Reserved56__" },
                    { 49, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 58, "__Reserved58__" },
                    { 74, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 83, "__Reserved83__" },
                    { 75, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 84, "__Reserved84__" },
                    { 76, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 85, "__Reserved85__" },
                    { 77, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 86, "__Reserved86__" },
                    { 78, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 87, "__Reserved87__" },
                    { 79, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 88, "__Reserved88__" },
                    { 80, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 89, "__Reserved89__" },
                    { 81, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 90, "__Reserved90__" },
                    { 82, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 91, "__Reserved91__" },
                    { 73, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 82, "__Reserved82__" },
                    { 83, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 92, "__Reserved92__" },
                    { 85, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 94, "__Reserved94__" },
                    { 86, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 95, "__Reserved95__" },
                    { 87, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 96, "__Reserved96__" },
                    { 88, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 97, "__Reserved97__" },
                    { 89, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 98, "__Reserved98__" },
                    { 90, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 99, "__Reserved99__" },
                    { 91, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 100, "__Reserved100__" },
                    { 92, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 101, "__Reserved101__" },
                    { 93, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 102, "__Reserved102__" },
                    { 84, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 93, "__Reserved93__" },
                    { 48, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 57, "__Reserved57__" },
                    { 72, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 81, "__Reserved81__" },
                    { 70, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 79, "__Reserved79__" },
                    { 50, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 59, "__Reserved59__" },
                    { 51, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 60, "__Reserved60__" },
                    { 52, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 61, "__Reserved61__" },
                    { 53, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 62, "__Reserved62__" },
                    { 54, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 63, "__Reserved63__" },
                    { 55, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 64, "__Reserved64__" },
                    { 56, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 65, "__Reserved65__" },
                    { 57, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 66, "__Reserved66__" },
                    { 58, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 67, "__Reserved67__" },
                    { 71, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 80, "__Reserved80__" },
                    { 59, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 68, "__Reserved68__" },
                    { 61, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 70, "__Reserved70__" },
                    { 62, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 71, "__Reserved71__" },
                    { 63, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 72, "__Reserved72__" },
                    { 64, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 73, "__Reserved73__" },
                    { 65, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 74, "__Reserved74__" },
                    { 66, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 75, "__Reserved75__" },
                    { 67, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 76, "__Reserved76__" },
                    { 68, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 77, "__Reserved77__" },
                    { 69, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 78, "__Reserved78__" },
                    { 60, new DateTime(2022, 5, 3, 19, 10, 15, 369, DateTimeKind.Utc).AddTicks(3146), null, 69, "__Reserved69__" },
                    { 191, new DateTime(2022, 5, 3, 19, 10, 15, 371, DateTimeKind.Utc).AddTicks(3143), null, 200, "__Reserved200__" }
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
