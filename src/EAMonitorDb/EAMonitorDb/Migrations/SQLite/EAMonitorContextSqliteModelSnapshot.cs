﻿// <auto-generated />
using System;
using EAMonitor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EAMonitor.Migrations.SQLite
{
    [DbContext(typeof(EAMonitorContextSqlite))]
    partial class EAMonitorContextSqliteModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("EAMonitor.EAMonitor", b =>
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

                    b.HasKey("Id");

                    b.HasIndex("MonitorStateId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("EAMonitor");
                });

            modelBuilder.Entity("EAMonitor.EAMonitorJob", b =>
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

                    b.HasIndex("MonitorStateId");

                    b.HasIndex("MonitorId", "Created");

                    b.ToTable("EAMonitorJob");
                });

            modelBuilder.Entity("EAMonitor.EAMonitorJobStatus", b =>
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

            modelBuilder.Entity("EAMonitor.EAMonitorJobTest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<DateTime?>("ExecutedAt");

                    b.Property<Guid>("JobId");

                    b.Property<bool>("Passed");

                    b.Property<string>("TestExpandedPath");

                    b.Property<string>("TestPath");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("EAMonitorJobTest");
                });

            modelBuilder.Entity("EAMonitor.EAMonitorSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastModified");

                    b.Property<Guid?>("MonitorId");

                    b.Property<int>("SettingKeyId");

                    b.Property<string>("SettingValue")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MonitorId");

                    b.HasIndex("SettingKeyId");

                    b.ToTable("EAMonitorSetting");
                });

            modelBuilder.Entity("EAMonitor.EAMonitorSettingKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EAMonitorSettingKey");
                });

            modelBuilder.Entity("EAMonitor.EAMonitorState", b =>
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

            modelBuilder.Entity("EAMonitor.EAMonitor", b =>
                {
                    b.HasOne("EAMonitor.EAMonitorState", "MonitorState")
                        .WithMany("Monitors")
                        .HasForeignKey("MonitorStateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EAMonitor.EAMonitorJob", b =>
                {
                    b.HasOne("EAMonitor.EAMonitorJobStatus", "JobStatus")
                        .WithMany("Jobs")
                        .HasForeignKey("JobStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EAMonitor.EAMonitor", "Monitor")
                        .WithMany("Jobs")
                        .HasForeignKey("MonitorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EAMonitor.EAMonitorState", "State")
                        .WithMany("Jobs")
                        .HasForeignKey("MonitorStateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EAMonitor.EAMonitorJobTest", b =>
                {
                    b.HasOne("EAMonitor.EAMonitorJob", "Job")
                        .WithMany("Tests")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EAMonitor.EAMonitorSetting", b =>
                {
                    b.HasOne("EAMonitor.EAMonitor", "Monitor")
                        .WithMany("Settings")
                        .HasForeignKey("MonitorId");

                    b.HasOne("EAMonitor.EAMonitorSettingKey", "SettingKey")
                        .WithMany("Settings")
                        .HasForeignKey("SettingKeyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
