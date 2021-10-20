﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoWebApplication.DbContexts;

namespace ToDoWebApplication.Migrations
{
    [DbContext(typeof(ToDoEventItemContext))]
    [Migration("20210517033443_ToDoWebApplication.Models.ToDoEventItem")]
    partial class ToDoWebApplicationModelsToDoEventItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ToDoWebApplication.Models.Property", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PropertyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ToDoEventItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ToDoEventItemId");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("ToDoWebApplication.Models.ToDoEventItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventActionEnum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ToDoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ToDoEventItems");
                });

            modelBuilder.Entity("ToDoWebApplication.Models.Property", b =>
                {
                    b.HasOne("ToDoWebApplication.Models.ToDoEventItem", null)
                        .WithMany("Properties")
                        .HasForeignKey("ToDoEventItemId");
                });

            modelBuilder.Entity("ToDoWebApplication.Models.ToDoEventItem", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
