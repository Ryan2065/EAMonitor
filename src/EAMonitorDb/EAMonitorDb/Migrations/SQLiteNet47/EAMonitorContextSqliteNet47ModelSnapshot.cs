﻿// <auto-generated />
using System;
using EAMonitorDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EAMonitorDb.Migrations.SQLiteNet47
{
    [DbContext(typeof(EAMonitorContextSqliteNet47))]
    partial class EAMonitorContextSqliteNet47ModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("EAMonitorDb.EAMonitor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("MonitorStateId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<DateTime?>("NextRun");

                    b.HasKey("Id");

                    b.HasIndex("MonitorStateId");

                    b.ToTable("EAMonitor");
                });

            modelBuilder.Entity("EAMonitorDb.EAMonitorEnvironment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EAMonitorEnvironment");
                });

            modelBuilder.Entity("EAMonitorDb.EAMonitorJob", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Completed");

                    b.Property<DateTime>("Created");

                    b.Property<int>("JobStatusId");

                    b.Property<DateTime>("LastModified");

                    b.Property<Guid>("MonitorId");

                    b.Property<int>("MonitorStateId");

                    b.Property<bool>("Notified");

                    b.HasKey("Id");

                    b.HasIndex("JobStatusId");

                    b.HasIndex("MonitorId");

                    b.HasIndex("MonitorStateId");

                    b.ToTable("EAMonitorJob");
                });

            modelBuilder.Entity("EAMonitorDb.EAMonitorJobStatus", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EAMonitorJobStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Created"
                        },
                        new
                        {
                            Id = 2,
                            Name = "InProgress"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Completed"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Failed"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("EAMonitorDb.EAMonitorSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("MonitorId");

                    b.Property<int?>("SettingEnvironmentId");

                    b.Property<int>("SettingKeyId");

                    b.Property<string>("SettingValue")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MonitorId");

                    b.HasIndex("SettingEnvironmentId");

                    b.HasIndex("SettingKeyId");

                    b.ToTable("EAMonitorSetting");
                });

            modelBuilder.Entity("EAMonitorDb.EAMonitorSettingKey", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EAMonitorSettingKey");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Schedule-RunTimespan"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Notify-Emails"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Notify-Repeat"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Enabled"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Schedule-CRONExpression"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Tags"
                        },
                        new
                        {
                            Id = 7,
                            Name = "StateChange-UpToDown"
                        },
                        new
                        {
                            Id = 8,
                            Name = "StateChange-UpToWarning"
                        },
                        new
                        {
                            Id = 9,
                            Name = "StateChange-DownToWarning"
                        },
                        new
                        {
                            Id = 10,
                            Name = "StateChange-DownToUp"
                        });
                });

            modelBuilder.Entity("EAMonitorDb.EAMonitorState", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EAMonitorState");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Unknown"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Up"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Down"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Warning"
                        });
                });

            modelBuilder.Entity("EAMonitorDb.EAMonitor", b =>
                {
                    b.HasOne("EAMonitorDb.EAMonitorState", "MonitorState")
                        .WithMany("Monitors")
                        .HasForeignKey("MonitorStateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EAMonitorDb.EAMonitorJob", b =>
                {
                    b.HasOne("EAMonitorDb.EAMonitorJobStatus", "JobStatus")
                        .WithMany("Jobs")
                        .HasForeignKey("JobStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EAMonitorDb.EAMonitor", "Monitor")
                        .WithMany("Jobs")
                        .HasForeignKey("MonitorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EAMonitorDb.EAMonitorState", "State")
                        .WithMany("Jobs")
                        .HasForeignKey("MonitorStateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EAMonitorDb.EAMonitorSetting", b =>
                {
                    b.HasOne("EAMonitorDb.EAMonitor", "Monitor")
                        .WithMany("Settings")
                        .HasForeignKey("MonitorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EAMonitorDb.EAMonitorEnvironment", "SettingEnvironment")
                        .WithMany("Settings")
                        .HasForeignKey("SettingEnvironmentId");

                    b.HasOne("EAMonitorDb.EAMonitorSettingKey", "SettingKey")
                        .WithMany("Settings")
                        .HasForeignKey("SettingKeyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}