﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Minimal_Web_API.DataContext;

#nullable disable

namespace MinimalWebAPI.Migrations
{
    [DbContext(typeof(PTTContext))]
    [Migration("20221226173434_PTTDB_init")]
    partial class PTTDBinit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Minimal_Web_API.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Login");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Minimal_Web_API.TransportStop", b =>
                {
                    b.Property<string>("StopId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZoneName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StopId");

                    b.ToTable("TransportStops");
                });

            modelBuilder.Entity("TransportStopUser", b =>
                {
                    b.Property<string>("TransportStopsStopId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UsersLogin")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TransportStopsStopId", "UsersLogin");

                    b.HasIndex("UsersLogin");

                    b.ToTable("TransportStopUser");
                });

            modelBuilder.Entity("TransportStopUser", b =>
                {
                    b.HasOne("Minimal_Web_API.TransportStop", null)
                        .WithMany()
                        .HasForeignKey("TransportStopsStopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Minimal_Web_API.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}