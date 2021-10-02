﻿// <auto-generated />
using System;
using Core.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Migrations.Migrations
{
    [DbContext(typeof(CodeFirstDbContext))]
    partial class CodeFirstDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Data.DataModels.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsMembershipWithdrawn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("JoinedUtcDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(130)
                        .HasColumnType("nvarchar(130)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("varchar(320)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.HasIndex("UserEmail")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Core.Data.DataModels.UserRole", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("ModifiedUtcDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte>("RolePriority")
                        .HasColumnType("tinyint");

                    b.HasKey("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Core.Data.DataModels.UserRolesByUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("RoleId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("OwnedUtcDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRolesByUser");
                });

            modelBuilder.Entity("Core.Data.DataModels.UserRolesByUser", b =>
                {
                    b.HasOne("Core.Data.DataModels.UserRole", "UserRole")
                        .WithMany("UserRolesByUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Data.DataModels.User", "User")
                        .WithMany("UserRolesByUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Core.Data.DataModels.User", b =>
                {
                    b.Navigation("UserRolesByUsers");
                });

            modelBuilder.Entity("Core.Data.DataModels.UserRole", b =>
                {
                    b.Navigation("UserRolesByUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
