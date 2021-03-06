// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieAPI.DAL;

#nullable disable

namespace MovieAPI.MovieMigrations.Development.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20220422030351_MovieDatabase")]
    partial class MovieDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MovieAPI.DAL.Actor", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("PictureDiskURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UpdateUserId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("MovieAPI.DAL.ApkVersion", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<int>("Downloads")
                        .HasColumnType("int");

                    b.Property<string>("FileDiskURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.Property<int>("VersionCode")
                        .HasColumnType("int");

                    b.Property<string>("VersionName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("UpdateUserId");

                    b.HasIndex("VersionCode")
                        .IsUnique();

                    b.HasIndex("VersionName")
                        .IsUnique();

                    b.ToTable("ApkVersions");
                });

            modelBuilder.Entity("MovieAPI.DAL.HostConfig", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("HostConfigs");

                    b.HasData(
                        new
                        {
                            Id = 101L,
                            Host = "http://localhost:8100",
                            Name = "http://localhost:8100",
                            Remark = "http://localhost:8100"
                        },
                        new
                        {
                            Id = 102L,
                            Host = "http://192.168.0.101:8100",
                            Name = "http://192.168.0.101:8100",
                            Remark = "http://192.168.0.101:8100"
                        },
                        new
                        {
                            Id = 103L,
                            Host = "http://192.168.43.189:8100",
                            Name = "http://192.168.43.189:8100",
                            Remark = "http://192.168.43.189:8100"
                        });
                });

            modelBuilder.Entity("MovieAPI.DAL.Movie", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("ActorId")
                        .HasColumnType("bigint");

                    b.Property<string>("ActorName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("DetailRelativeURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("HasMovieFile")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("HasPicture")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("MovieFileId")
                        .HasColumnType("bigint");

                    b.Property<string>("MovieFileName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<long?>("MovieTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("MovieTypeName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("PictureDiskURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("PushTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("ResourceLink")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("DetailRelativeURL")
                        .IsUnique();

                    b.HasIndex("MovieFileId");

                    b.HasIndex("MovieTypeId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieAPI.DAL.MovieFile", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("DiskURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("FileFullName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("MovieFiles");
                });

            modelBuilder.Entity("MovieAPI.DAL.MovieType", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("MovieTypes");
                });

            modelBuilder.Entity("MovieAPI.DAL.Role", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 101L,
                            DisplayName = "系统",
                            IsAdmin = true,
                            IsDisabled = false,
                            Name = "system",
                            Remark = ""
                        },
                        new
                        {
                            Id = 102L,
                            DisplayName = "管理员",
                            IsAdmin = true,
                            IsDisabled = false,
                            Name = "admin",
                            Remark = ""
                        },
                        new
                        {
                            Id = 105L,
                            DisplayName = "用户",
                            IsAdmin = false,
                            IsDisabled = false,
                            Name = "user",
                            Remark = ""
                        },
                        new
                        {
                            Id = 50L,
                            DisplayName = "匿名",
                            IsAdmin = false,
                            IsDisabled = false,
                            Name = "anonymous",
                            Remark = ""
                        });
                });

            modelBuilder.Entity("MovieAPI.DAL.RoleUser", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RoleUser");

                    b.HasData(
                        new
                        {
                            RoleId = 101L,
                            UserId = 101L
                        },
                        new
                        {
                            RoleId = 102L,
                            UserId = 102L
                        },
                        new
                        {
                            RoleId = 50L,
                            UserId = 50L
                        },
                        new
                        {
                            RoleId = 105L,
                            UserId = 105L
                        });
                });

            modelBuilder.Entity("MovieAPI.DAL.UploadFile", b =>
                {
                    b.Property<string>("SaveName")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Ext")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("UploadName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("SaveName");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("UpdateUserId");

                    b.ToTable("UploadFiles");
                });

            modelBuilder.Entity("MovieAPI.DAL.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<byte>("Gender")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UpdateUserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 50L,
                            Age = 0,
                            Email = "",
                            FullName = "anonymous",
                            Gender = (byte)0,
                            Identity = "",
                            Name = "anonymous",
                            NickName = "匿名",
                            Phone = "",
                            Picture = ""
                        },
                        new
                        {
                            Id = 101L,
                            Age = 0,
                            Email = "",
                            FullName = "system",
                            Gender = (byte)0,
                            Identity = "",
                            Name = "system",
                            NickName = "系统",
                            Phone = "",
                            Picture = ""
                        },
                        new
                        {
                            Id = 102L,
                            Age = 0,
                            Email = "",
                            FullName = "admin",
                            Gender = (byte)0,
                            Identity = "",
                            Name = "admin",
                            NickName = "管理员",
                            Phone = "",
                            Picture = ""
                        },
                        new
                        {
                            Id = 105L,
                            Age = 0,
                            Email = "",
                            FullName = "user",
                            Gender = (byte)0,
                            Identity = "",
                            Name = "user",
                            NickName = "用户",
                            Phone = "",
                            Picture = ""
                        });
                });

            modelBuilder.Entity("MovieAPI.DAL.UserMovie", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDislike")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsStar")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("MovieId");

                    b.HasIndex("UpdateUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMovies");
                });

            modelBuilder.Entity("MovieAPI.DAL.UserSearchHistory", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("UpdateUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSearchHistories");
                });

            modelBuilder.Entity("MovieAPI.DAL.UserSecret", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreateUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("UpdateUserId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("UpdateUserId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("UserSecrets");

                    b.HasData(
                        new
                        {
                            Id = 101L,
                            Password = "017B509333DCF2C50E26D6214BDE0DBF9DD227B81216FBAFC61CAC4451AE0ECF",
                            Salt = "wangsir",
                            UserId = 102L,
                            Username = "admin"
                        },
                        new
                        {
                            Id = 102L,
                            Password = "017B509333DCF2C50E26D6214BDE0DBF9DD227B81216FBAFC61CAC4451AE0ECF",
                            Salt = "wangsir",
                            UserId = 105L,
                            Username = "user"
                        });
                });

            modelBuilder.Entity("MovieAPI.DAL.Actor", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreateUser");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("MovieAPI.DAL.ApkVersion", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreateUser");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("MovieAPI.DAL.HostConfig", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreateUser");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("MovieAPI.DAL.Movie", b =>
                {
                    b.HasOne("MovieAPI.DAL.Actor", "Actor")
                        .WithMany("Movies")
                        .HasForeignKey("ActorId");

                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.MovieFile", "MovieFile")
                        .WithMany("Movies")
                        .HasForeignKey("MovieFileId");

                    b.HasOne("MovieAPI.DAL.MovieType", "MovieType")
                        .WithMany("Movies")
                        .HasForeignKey("MovieTypeId");

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Actor");

                    b.Navigation("CreateUser");

                    b.Navigation("MovieFile");

                    b.Navigation("MovieType");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("MovieAPI.DAL.MovieFile", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreateUser");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("MovieAPI.DAL.MovieType", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreateUser");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("MovieAPI.DAL.Role", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreateUser");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("MovieAPI.DAL.RoleUser", b =>
                {
                    b.HasOne("MovieAPI.DAL.Role", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieAPI.DAL.User", "User")
                        .WithMany("RoleUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieAPI.DAL.UploadFile", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreateUser");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("MovieAPI.DAL.User", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreateUser");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("MovieAPI.DAL.UserMovie", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.Movie", "Movie")
                        .WithMany("UserMovies")
                        .HasForeignKey("MovieId");

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "User")
                        .WithMany("UserMovies")
                        .HasForeignKey("UserId");

                    b.Navigation("CreateUser");

                    b.Navigation("Movie");

                    b.Navigation("UpdateUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieAPI.DAL.UserSearchHistory", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "User")
                        .WithMany("UserSearchHistories")
                        .HasForeignKey("UserId");

                    b.Navigation("CreateUser");

                    b.Navigation("UpdateUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieAPI.DAL.UserSecret", b =>
                {
                    b.HasOne("MovieAPI.DAL.User", "CreateUser")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieAPI.DAL.User", "User")
                        .WithOne("UserSecret")
                        .HasForeignKey("MovieAPI.DAL.UserSecret", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreateUser");

                    b.Navigation("UpdateUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieAPI.DAL.Actor", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("MovieAPI.DAL.Movie", b =>
                {
                    b.Navigation("UserMovies");
                });

            modelBuilder.Entity("MovieAPI.DAL.MovieFile", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("MovieAPI.DAL.MovieType", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("MovieAPI.DAL.Role", b =>
                {
                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("MovieAPI.DAL.User", b =>
                {
                    b.Navigation("RoleUsers");

                    b.Navigation("UserMovies");

                    b.Navigation("UserSearchHistories");

                    b.Navigation("UserSecret");
                });
#pragma warning restore 612, 618
        }
    }
}
