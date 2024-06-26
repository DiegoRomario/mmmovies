﻿// <auto-generated />
using System;
using MMMovies.Movies.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MMMovies.Movies.Data.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20240505175710_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Movies")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MMMovies.Movies.Domain.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Movies", "Movies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a89f6cd7-4693-457b-9009-02205dbbfe45"),
                            Director = "Quentin Tarantino",
                            Price = 10.99m,
                            Title = "Pulp Fiction"
                        },
                        new
                        {
                            Id = new Guid("e4fa19bf-6981-4e50-a542-7c9b26e9ec31"),
                            Director = "Quentin Tarantino",
                            Price = 11.99m,
                            Title = "Reservoir Dogs"
                        },
                        new
                        {
                            Id = new Guid("17c61e41-3953-42cd-8f88-d3f698869b35"),
                            Director = "Quentin Tarantino",
                            Price = 12.99m,
                            Title = "Kill Bill"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
