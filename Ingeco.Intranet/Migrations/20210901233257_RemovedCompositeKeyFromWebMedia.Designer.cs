﻿// <auto-generated />
using System;
using Ingeco.Intranet.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ingeco.Intranet.Migrations
{
    [DbContext(typeof(WebDataContext))]
    [Migration("20210901233257_RemovedCompositeKeyFromWebMedia")]
    partial class RemovedCompositeKeyFromWebMedia
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsReply")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RepliedToId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("RepliedToId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("PostedById")
                        .HasColumnType("uuid");

                    b.Property<bool>("Public")
                        .HasColumnType("boolean");

                    b.Property<string>("TagsLine")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PostedById");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.VisitRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("HostClient")
                        .HasColumnType("text");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Visited")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("VisitRecords");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.WebMedia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Filename")
                        .HasColumnType("text");

                    b.Property<bool>("IsCover")
                        .HasColumnType("boolean");

                    b.Property<int>("MediaType")
                        .HasColumnType("integer");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<int>("ViewId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("SmartB1t.Security.WebSecurity.Local.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SmartB1t.Security.WebSecurity.Local.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Department")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .HasColumnType("text");

                    b.Property<bool>("PermanentDeactivation")
                        .HasColumnType("boolean");

                    b.Property<string>("Position")
                        .HasColumnType("text");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("bytea");

                    b.Property<Guid>("UserSecretsId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartB1t.Security.WebSecurity.Local.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("SmartB1t.Security.WebSecurity.Local.UserSecrets", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Secrets");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.Comment", b =>
                {
                    b.HasOne("Ingeco.Intranet.Data.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ingeco.Intranet.Data.Models.Comment", "RepliedTo")
                        .WithMany("Replies")
                        .HasForeignKey("RepliedToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartB1t.Security.WebSecurity.Local.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("RepliedTo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.Post", b =>
                {
                    b.HasOne("Ingeco.Intranet.Data.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartB1t.Security.WebSecurity.Local.User", "PostedBy")
                        .WithMany()
                        .HasForeignKey("PostedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("PostedBy");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.VisitRecord", b =>
                {
                    b.HasOne("Ingeco.Intranet.Data.Models.Post", "Post")
                        .WithMany("Visits")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.WebMedia", b =>
                {
                    b.HasOne("Ingeco.Intranet.Data.Models.Post", "Post")
                        .WithMany("Media")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("SmartB1t.Security.WebSecurity.Local.UserRole", b =>
                {
                    b.HasOne("SmartB1t.Security.WebSecurity.Local.Role", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartB1t.Security.WebSecurity.Local.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartB1t.Security.WebSecurity.Local.UserSecrets", b =>
                {
                    b.HasOne("SmartB1t.Security.WebSecurity.Local.User", "User")
                        .WithOne("Secrets")
                        .HasForeignKey("SmartB1t.Security.WebSecurity.Local.UserSecrets", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.Category", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("Ingeco.Intranet.Data.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Media");

                    b.Navigation("Visits");
                });

            modelBuilder.Entity("SmartB1t.Security.WebSecurity.Local.Role", b =>
                {
                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("SmartB1t.Security.WebSecurity.Local.User", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("Secrets");
                });
#pragma warning restore 612, 618
        }
    }
}
