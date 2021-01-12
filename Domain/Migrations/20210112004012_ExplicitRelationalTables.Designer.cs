﻿// <auto-generated />
using System;
using Domain.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(TeachContext))]
    [Migration("20210112004012_ExplicitRelationalTables")]
    partial class ExplicitRelationalTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Domain.ClassDays.ClassDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassroomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.ToTable("ClassDays");
                });

            modelBuilder.Entity("Domain.ClassDays.StudentPresence", b =>
                {
                    b.Property<Guid>("ClassDayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Present")
                        .HasColumnType("bit");

                    b.Property<string>("Reason")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ClassDayId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentPresences");
                });

            modelBuilder.Entity("Domain.Classrooms.Classroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Subjects")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Classrooms");
                });

            modelBuilder.Entity("Domain.Classrooms.ClassroomStudent", b =>
                {
                    b.Property<Guid>("ClassroomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClassroomId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("ClassroomStudents");
                });

            modelBuilder.Entity("Domain.Classrooms.ClassroomTeacher", b =>
                {
                    b.Property<Guid>("ClassroomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClassroomId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("ClassroomTeachers");
                });

            modelBuilder.Entity("Domain.Grades.Grade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassroomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateClosed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Domain.Grades.StudentGrade", b =>
                {
                    b.Property<Guid>("BaseGradeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Grade")
                        .HasColumnType("float");

                    b.HasKey("BaseGradeId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentGrades");
                });

            modelBuilder.Entity("Domain.Students.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.HasIndex("Registration")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domain.Teachers.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Profile")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.ClassDays.ClassDay", b =>
                {
                    b.HasOne("Domain.Classrooms.Classroom", "Classroom")
                        .WithMany("ClassDays")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");
                });

            modelBuilder.Entity("Domain.ClassDays.StudentPresence", b =>
                {
                    b.HasOne("Domain.ClassDays.ClassDay", "ClassDay")
                        .WithMany("StudentPresences")
                        .HasForeignKey("ClassDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Students.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassDay");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Domain.Classrooms.ClassroomStudent", b =>
                {
                    b.HasOne("Domain.Classrooms.Classroom", "Classroom")
                        .WithMany("Students")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Students.Student", "Student")
                        .WithMany("Classrooms")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Domain.Classrooms.ClassroomTeacher", b =>
                {
                    b.HasOne("Domain.Classrooms.Classroom", "Classroom")
                        .WithMany("Teachers")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Teachers.Teacher", "Teacher")
                        .WithMany("Classrooms")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Domain.Grades.Grade", b =>
                {
                    b.HasOne("Domain.Classrooms.Classroom", "Classroom")
                        .WithMany("Grades")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");
                });

            modelBuilder.Entity("Domain.Grades.StudentGrade", b =>
                {
                    b.HasOne("Domain.Grades.Grade", "BaseGrade")
                        .WithMany("StudentGrades")
                        .HasForeignKey("BaseGradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Students.Student", "Student")
                        .WithMany("StudentGrades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseGrade");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Domain.ClassDays.ClassDay", b =>
                {
                    b.Navigation("StudentPresences");
                });

            modelBuilder.Entity("Domain.Classrooms.Classroom", b =>
                {
                    b.Navigation("ClassDays");

                    b.Navigation("Grades");

                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("Domain.Grades.Grade", b =>
                {
                    b.Navigation("StudentGrades");
                });

            modelBuilder.Entity("Domain.Students.Student", b =>
                {
                    b.Navigation("Classrooms");

                    b.Navigation("StudentGrades");
                });

            modelBuilder.Entity("Domain.Teachers.Teacher", b =>
                {
                    b.Navigation("Classrooms");
                });
#pragma warning restore 612, 618
        }
    }
}
