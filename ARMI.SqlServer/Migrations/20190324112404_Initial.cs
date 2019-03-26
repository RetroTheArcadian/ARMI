using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ARMI.SqlServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    ClientHostType = table.Column<int>(nullable: false),
                    Host = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsMaster = table.Column<bool>(nullable: false),
                    RomFolder = table.Column<string>(nullable: true),
                    AttractModeFolder = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobIdGuid = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    JobStatus = table.Column<int>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    SystemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    RomFolder = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<string>(nullable: true),
                    Developer = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Controllers = table.Column<string>(nullable: true),
                    Cpu = table.Column<string>(nullable: true),
                    Memory = table.Column<string>(nullable: true),
                    Graphics = table.Column<string>(nullable: true),
                    Sound = table.Column<string>(nullable: true),
                    Display = table.Column<string>(nullable: true),
                    Media = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FileExtensions = table.Column<string>(nullable: true),
                    Wiki = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.SystemId);
                });

            migrationBuilder.CreateTable(
                name: "RomLists",
                columns: table => new
                {
                    RomListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentRomListId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RomLists", x => x.RomListId);
                    table.ForeignKey(
                        name: "FK_RomLists_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RomLists_RomLists_ParentRomListId",
                        column: x => x.ParentRomListId,
                        principalTable: "RomLists",
                        principalColumn: "RomListId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Emulators",
                columns: table => new
                {
                    EmulatorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmulatorName = table.Column<string>(nullable: true),
                    Executable = table.Column<string>(nullable: true),
                    Args = table.Column<string>(nullable: true),
                    Rompath = table.Column<string>(nullable: true),
                    Romext = table.Column<string>(nullable: true),
                    InfoSource = table.Column<string>(nullable: true),
                    Boxart = table.Column<string>(nullable: true),
                    Cartart = table.Column<string>(nullable: true),
                    Fanart = table.Column<string>(nullable: true),
                    Flyer = table.Column<string>(nullable: true),
                    Marquee = table.Column<string>(nullable: true),
                    Snap = table.Column<string>(nullable: true),
                    Wheel = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    SystemId = table.Column<int>(nullable: true),
                    RomFolderName = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emulators", x => x.EmulatorId);
                    table.ForeignKey(
                        name: "FK_Emulators_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emulators_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "SystemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roms",
                columns: table => new
                {
                    RomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    CloneOf = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Players = table.Column<string>(nullable: true),
                    Rotation = table.Column<string>(nullable: true),
                    Control = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    DisplayCount = table.Column<string>(nullable: true),
                    DisplayType = table.Column<string>(nullable: true),
                    AltRomname = table.Column<string>(nullable: true),
                    AltTitle = table.Column<string>(nullable: true),
                    Extra = table.Column<string>(nullable: true),
                    Buttons = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    EmulatorId = table.Column<int>(nullable: true),
                    RomListId = table.Column<int>(nullable: true),
                    EmulatorNameOrg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roms", x => x.RomId);
                    table.ForeignKey(
                        name: "FK_Roms_Emulators_EmulatorId",
                        column: x => x.EmulatorId,
                        principalTable: "Emulators",
                        principalColumn: "EmulatorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RomListRoms",
                columns: table => new
                {
                    RomListId = table.Column<int>(nullable: false),
                    RomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RomListRoms", x => new { x.RomListId, x.RomId });
                    table.ForeignKey(
                        name: "FK_RomListRoms_Roms_RomId",
                        column: x => x.RomId,
                        principalTable: "Roms",
                        principalColumn: "RomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RomListRoms_RomLists_RomListId",
                        column: x => x.RomListId,
                        principalTable: "RomLists",
                        principalColumn: "RomListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emulators_ClientId",
                table: "Emulators",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Emulators_SystemId",
                table: "Emulators",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RomListRoms_RomId",
                table: "RomListRoms",
                column: "RomId");

            migrationBuilder.CreateIndex(
                name: "IX_RomLists_ClientId",
                table: "RomLists",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RomLists_ParentRomListId",
                table: "RomLists",
                column: "ParentRomListId");

            migrationBuilder.CreateIndex(
                name: "IX_Roms_EmulatorId",
                table: "Roms",
                column: "EmulatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "RomListRoms");

            migrationBuilder.DropTable(
                name: "Roms");

            migrationBuilder.DropTable(
                name: "RomLists");

            migrationBuilder.DropTable(
                name: "Emulators");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Systems");
        }
    }
}
