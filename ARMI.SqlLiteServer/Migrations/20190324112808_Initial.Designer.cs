﻿// <auto-generated />
using System;
using ARMI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ARMI.SqlLiteServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190324112808_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("ARMI.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AttractModeFolder");

                    b.Property<int>("ClientHostType");

                    b.Property<string>("Host");

                    b.Property<bool>("IsMaster");

                    b.Property<string>("Password");

                    b.Property<int>("Port");

                    b.Property<string>("RomFolder");

                    b.Property<string>("Title");

                    b.Property<string>("UserName");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("ARMI.Models.Emulator", b =>
                {
                    b.Property<int>("EmulatorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Args");

                    b.Property<string>("Boxart");

                    b.Property<string>("Cartart");

                    b.Property<int>("ClientId");

                    b.Property<string>("EmulatorName");

                    b.Property<string>("Executable");

                    b.Property<string>("Fanart");

                    b.Property<string>("Flyer");

                    b.Property<string>("InfoSource");

                    b.Property<string>("Marquee");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("RomFolderName");

                    b.Property<string>("Romext");

                    b.Property<string>("Rompath");

                    b.Property<string>("Snap");

                    b.Property<int?>("SystemId");

                    b.Property<string>("Wheel");

                    b.HasKey("EmulatorId");

                    b.HasIndex("ClientId");

                    b.HasIndex("SystemId");

                    b.ToTable("Emulators");
                });

            modelBuilder.Entity("ARMI.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("End");

                    b.Property<string>("JobIdGuid");

                    b.Property<int>("JobStatus");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Title");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("ARMI.Models.Rom", b =>
                {
                    b.Property<int>("RomId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AltRomname");

                    b.Property<string>("AltTitle");

                    b.Property<string>("Buttons");

                    b.Property<string>("Category");

                    b.Property<string>("CloneOf");

                    b.Property<string>("Control");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayCount");

                    b.Property<string>("DisplayType");

                    b.Property<int?>("EmulatorId");

                    b.Property<string>("EmulatorNameOrg");

                    b.Property<string>("Extra");

                    b.Property<string>("Manufacturer");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("Players");

                    b.Property<int?>("RomListId");

                    b.Property<string>("Rotation");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.Property<string>("Year");

                    b.HasKey("RomId");

                    b.HasIndex("EmulatorId");

                    b.ToTable("Roms");
                });

            modelBuilder.Entity("ARMI.Models.RomList", b =>
                {
                    b.Property<int>("RomListId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<int?>("ParentRomListId");

                    b.Property<string>("Title");

                    b.HasKey("RomListId");

                    b.HasIndex("ClientId");

                    b.HasIndex("ParentRomListId");

                    b.ToTable("RomLists");
                });

            modelBuilder.Entity("ARMI.Models.RomListRoms", b =>
                {
                    b.Property<int>("RomListId");

                    b.Property<int>("RomId");

                    b.HasKey("RomListId", "RomId");

                    b.HasIndex("RomId");

                    b.ToTable("RomListRoms");
                });

            modelBuilder.Entity("ARMI.Models.System", b =>
                {
                    b.Property<int>("SystemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Controllers");

                    b.Property<string>("Cpu");

                    b.Property<string>("Description");

                    b.Property<string>("Developer");

                    b.Property<string>("Display");

                    b.Property<string>("FileExtensions");

                    b.Property<string>("Graphics");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("Media");

                    b.Property<string>("Memory");

                    b.Property<string>("Name");

                    b.Property<string>("ReleaseDate");

                    b.Property<string>("RomFolder");

                    b.Property<string>("Sound");

                    b.Property<string>("Wiki");

                    b.HasKey("SystemId");

                    b.ToTable("Systems");
                });

            modelBuilder.Entity("ARMI.Models.Emulator", b =>
                {
                    b.HasOne("ARMI.Models.Client", "Client")
                        .WithMany("Emulators")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ARMI.Models.System", "System")
                        .WithMany("Emulators")
                        .HasForeignKey("SystemId");
                });

            modelBuilder.Entity("ARMI.Models.Rom", b =>
                {
                    b.HasOne("ARMI.Models.Emulator", "Emulator")
                        .WithMany("Roms")
                        .HasForeignKey("EmulatorId");
                });

            modelBuilder.Entity("ARMI.Models.RomList", b =>
                {
                    b.HasOne("ARMI.Models.Client", "Client")
                        .WithMany("RomLists")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ARMI.Models.RomList", "ParentRomList")
                        .WithMany("SubRomLists")
                        .HasForeignKey("ParentRomListId");
                });

            modelBuilder.Entity("ARMI.Models.RomListRoms", b =>
                {
                    b.HasOne("ARMI.Models.Rom", "Rom")
                        .WithMany("RomListRoms")
                        .HasForeignKey("RomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ARMI.Models.RomList", "RomList")
                        .WithMany("RomListRoms")
                        .HasForeignKey("RomListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
