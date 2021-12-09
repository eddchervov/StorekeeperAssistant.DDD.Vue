﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StorekeeperAssistant.DataAccess;

namespace StorekeeperAssistant.Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StorekeeperAssistant.Domain.InventoryItemAggregate.InventoryItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.MovingAggregate.Moving", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ArrivalWarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DepartureWarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime>("TransferDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("TransferDate");

                    b.HasKey("Id");

                    b.ToTable("Movings");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.MovingAggregate.MovingDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InventoryItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MovingId");

                    b.ToTable("MovingDetails");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.WarehouseAggregate.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.WarehouseInventoryItemAggregate.WarehouseInventoryItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("Count");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("Date");

                    b.Property<Guid>("InventoryItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("WarehouseInventoryItems");
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.InventoryItemAggregate.InventoryItem", b =>
                {
                    b.OwnsOne("StorekeeperAssistant.Domain.InventoryItemAggregate.InventoryItemName", "Name", b1 =>
                        {
                            b1.Property<Guid>("InventoryItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("InventoryItemId");

                            b1.ToTable("InventoryItems");

                            b1.WithOwner()
                                .HasForeignKey("InventoryItemId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.MovingAggregate.MovingDetail", b =>
                {
                    b.HasOne("StorekeeperAssistant.Domain.MovingAggregate.Moving", null)
                        .WithMany("MovingDetails")
                        .HasForeignKey("MovingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("StorekeeperAssistant.Domain.MovingAggregate.MovingDetailCount", "Count", b1 =>
                        {
                            b1.Property<Guid>("MovingDetailId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Count")
                                .HasColumnType("int")
                                .HasColumnName("Count");

                            b1.HasKey("MovingDetailId");

                            b1.ToTable("MovingDetails");

                            b1.WithOwner()
                                .HasForeignKey("MovingDetailId");
                        });

                    b.Navigation("Count")
                        .IsRequired();
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.WarehouseAggregate.Warehouse", b =>
                {
                    b.OwnsOne("StorekeeperAssistant.Domain.WarehouseAggregate.WarehouseName", "Name", b1 =>
                        {
                            b1.Property<Guid>("WarehouseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("WarehouseId");

                            b1.ToTable("Warehouses");

                            b1.WithOwner()
                                .HasForeignKey("WarehouseId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("StorekeeperAssistant.Domain.MovingAggregate.Moving", b =>
                {
                    b.Navigation("MovingDetails");
                });
#pragma warning restore 612, 618
        }
    }
}