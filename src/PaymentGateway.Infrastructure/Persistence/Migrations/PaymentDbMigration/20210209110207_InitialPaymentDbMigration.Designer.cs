﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaymentGateway.Infrastructure.Persistence;

namespace PaymentGateway.Infrastructure.Persistence.Migrations.PaymentDbMigration
{
    [DbContext(typeof(PaymentDbContext))]
    [Migration("20210209110207_InitialPaymentDbMigration")]
    partial class InitialPaymentDbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("PaymentGateway.Domain.Entities.Card", b =>
                {
                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ExpiryMonth")
                        .HasColumnType("integer");

                    b.Property<int>("ExpiryYear")
                        .HasColumnType("integer");

                    b.HasKey("Number");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("PaymentGateway.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CardNumber")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CardNumber");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PaymentGateway.Domain.Entities.Payment", b =>
                {
                    b.HasOne("PaymentGateway.Domain.Entities.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardNumber");

                    b.Navigation("Card");
                });
#pragma warning restore 612, 618
        }
    }
}
