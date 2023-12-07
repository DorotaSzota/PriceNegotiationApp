﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PriceNegotiationApp.Data;

#nullable disable

namespace PriceNegotiationApp.Migrations
{
    [DbContext(typeof(PriceNegotiationDbContext))]
    partial class PriceNegotiationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("PriceNegotiationApp.Models.PriceProposal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Accepted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AttemptsLeft")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ProposedPrice")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.ToTable("PriceProposals");
                });

            modelBuilder.Entity("PriceNegotiationApp.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductCategory")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PriceNegotiationApp.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PriceNegotiationApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PriceNegotiationApp.Models.User", b =>
                {
                    b.HasOne("PriceNegotiationApp.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
