﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MülakatProje.Context;

#nullable disable

namespace MülakatProje.Migrations
{
    [DbContext(typeof(VeritabaniContext))]
    [Migration("20240918122117_UpdateConstraints")]
    partial class UpdateConstraints
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MülakatProje.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CikisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("SanatciId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SanatciId");

                    b.ToTable("Albumler");
                });

            modelBuilder.Entity("MülakatProje.Models.CalmaListesi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CalmaListeleri");
                });

            modelBuilder.Entity("MülakatProje.Models.CalmaListesiSarkilari", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CalmaListesiId")
                        .HasColumnType("int");

                    b.Property<int>("SarkiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CalmaListesiId");

                    b.HasIndex("SarkiId");

                    b.ToTable("CalmaListesiSarkilari");
                });

            modelBuilder.Entity("MülakatProje.Models.Sanatci", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("KurulusTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Sanatcilar");
                });

            modelBuilder.Entity("MülakatProje.Models.Sarki", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("SanatciId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("SanatciId");

                    b.ToTable("Sarkilar");
                });

            modelBuilder.Entity("MülakatProje.Models.Album", b =>
                {
                    b.HasOne("MülakatProje.Models.Sanatci", "Sanatci")
                        .WithMany("Albumler")
                        .HasForeignKey("SanatciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sanatci");
                });

            modelBuilder.Entity("MülakatProje.Models.CalmaListesiSarkilari", b =>
                {
                    b.HasOne("MülakatProje.Models.CalmaListesi", "CalmaListesi")
                        .WithMany("CalmaListesiSarkilari")
                        .HasForeignKey("CalmaListesiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MülakatProje.Models.Sarki", "Sarki")
                        .WithMany()
                        .HasForeignKey("SarkiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CalmaListesi");

                    b.Navigation("Sarki");
                });

            modelBuilder.Entity("MülakatProje.Models.Sarki", b =>
                {
                    b.HasOne("MülakatProje.Models.Album", "Album")
                        .WithMany("Sarkilar")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MülakatProje.Models.Sanatci", "Sanatci")
                        .WithMany()
                        .HasForeignKey("SanatciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Sanatci");
                });

            modelBuilder.Entity("MülakatProje.Models.Album", b =>
                {
                    b.Navigation("Sarkilar");
                });

            modelBuilder.Entity("MülakatProje.Models.CalmaListesi", b =>
                {
                    b.Navigation("CalmaListesiSarkilari");
                });

            modelBuilder.Entity("MülakatProje.Models.Sanatci", b =>
                {
                    b.Navigation("Albumler");
                });
#pragma warning restore 612, 618
        }
    }
}
