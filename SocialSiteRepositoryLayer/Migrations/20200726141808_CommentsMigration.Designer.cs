﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialSiteRepositoryLayer.ApplicationContext;

namespace SocialSiteRepositoryLayer.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20200726141808_CommentsMigration")]
    partial class CommentsMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SocialSiteCommonLayer.DBModels.Comments", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<int>("CommentByUserID");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DateTime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("DateTime2");

                    b.Property<int>("PostID");

                    b.HasKey("ID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SocialSiteCommonLayer.DBModels.Friends", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DateTime2");

                    b.Property<int>("FriendID");

                    b.Property<bool>("IsAccepted");

                    b.Property<bool>("IsRejected");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("DateTime2");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("SocialSiteCommonLayer.DBModels.Likes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DateTime2");

                    b.Property<bool>("IsLiked");

                    b.Property<int>("LikeByUserID");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("DateTime2");

                    b.Property<int>("PostID");

                    b.HasKey("ID");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("SocialSiteCommonLayer.DBModels.Posts", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DateTime2");

                    b.Property<bool>("IsRemoved");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("DateTime2");

                    b.Property<int>("PostLikes");

                    b.Property<string>("PostPath")
                        .IsRequired();

                    b.Property<int>("UserID");

                    b.HasKey("PostID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("SocialSiteCommonLayer.DBModels.Users", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DateTime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("DateTime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
