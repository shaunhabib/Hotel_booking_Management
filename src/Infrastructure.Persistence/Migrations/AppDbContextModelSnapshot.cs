﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("Archived")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Predefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PredefinedValue")
                        .HasColumnType("longtext");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("Archived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<bool>("Predefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PredefinedValue")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Archived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<bool>("Predefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PredefinedValue")
                        .HasColumnType("longtext");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.HotelFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("Archived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<bool>("Predefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PredefinedValue")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelFeatures");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("Archived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Predefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PredefinedValue")
                        .HasColumnType("longtext");

                    b.Property<int>("ReferenceId")
                        .HasColumnType("int");

                    b.Property<string>("ReferenceName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("Archived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Predefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PredefinedValue")
                        .HasColumnType("longtext");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId")
                        .IsUnique();

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("Archived")
                        .HasColumnType("tinyint(1)");

                    b.Property<float>("Capacity")
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Predefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PredefinedValue")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.RoomFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("Archived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<bool>("Predefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PredefinedValue")
                        .HasColumnType("longtext");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomFeatures");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("Archived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("EffectiveFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EffectiveTo")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<bool>("Predefined")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PredefinedValue")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Booking", b =>
                {
                    b.HasOne("Core.Domain.Persistence.Entities.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Persistence.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Comment", b =>
                {
                    b.HasOne("Core.Domain.Persistence.Entities.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.HotelFeature", b =>
                {
                    b.HasOne("Core.Domain.Persistence.Entities.Hotel", "Hotel")
                        .WithMany("Features")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Review", b =>
                {
                    b.HasOne("Core.Domain.Persistence.Entities.Hotel", "Hotel")
                        .WithOne("Review")
                        .HasForeignKey("Core.Domain.Persistence.Entities.Review", "HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Room", b =>
                {
                    b.HasOne("Core.Domain.Persistence.Entities.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Persistence.Entities.RoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.RoomFeature", b =>
                {
                    b.HasOne("Core.Domain.Persistence.Entities.Room", "Hotel")
                        .WithMany("Features")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Hotel", b =>
                {
                    b.Navigation("Features");

                    b.Navigation("Review");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Core.Domain.Persistence.Entities.Room", b =>
                {
                    b.Navigation("Features");
                });
#pragma warning restore 612, 618
        }
    }
}
