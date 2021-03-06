// <auto-generated />
using System;
using CFU.UniversityManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CFU.UniversityManagement.WebAPI.Migrations
{
    [DbContext(typeof(UniversityManagementDBContext))]
    [Migration("20220315102053_Initial9")]
    partial class Initial9
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CFU.Domain.StructureContext.AcademyAggregate.Academy", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDisbanded")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("academies", (string)null);
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.AcademyAggregate.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AcademyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AcademyId");

                    b.ToTable("faculties", (string)null);
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.FacultyAggregate.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("faculty-departments", (string)null);
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.FacultyAggregate.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("faculties", (string)null);
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.InstituteAggregate.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InstituteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("InstituteId");

                    b.ToTable("institute-departments", (string)null);
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.InstituteAggregate.Institute", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDisbanded")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("institutes", (string)null);
                });

            modelBuilder.Entity("CFU.Domain.SupplyContext.BuildingAggregate.Auditorium", b =>
                {
                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.HasKey("BuildingId", "Number");

                    b.ToTable("auditoriums", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Auditorium");
                });

            modelBuilder.Entity("CFU.Domain.SupplyContext.BuildingAggregate.Building", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("buildings", (string)null);
                });

            modelBuilder.Entity("CFU.Domain.SupplyContext.StructureUnitAggregate.StructureUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AuditoriumNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("structure-units", (string)null);
                });

            modelBuilder.Entity("CFU.Domain.SupplyContext.BuildingAggregate.AdministrativeAuditorium", b =>
                {
                    b.HasBaseType("CFU.Domain.SupplyContext.BuildingAggregate.Auditorium");

                    b.Property<Guid>("StructureUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("StructureUnitId")
                        .IsUnique()
                        .HasFilter("[StructureUnitId] IS NOT NULL");

                    b.ToTable("auditoriums", (string)null);

                    b.HasDiscriminator().HasValue("AdministrativeAuditorium");
                });

            modelBuilder.Entity("CFU.Domain.SupplyContext.BuildingAggregate.ClassRoom", b =>
                {
                    b.HasBaseType("CFU.Domain.SupplyContext.BuildingAggregate.Auditorium");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.ToTable("auditoriums", (string)null);

                    b.HasDiscriminator().HasValue("ClassRoom");
                });

            modelBuilder.Entity("CFU.Domain.SupplyContext.BuildingAggregate.UnassignedAuditorium", b =>
                {
                    b.HasBaseType("CFU.Domain.SupplyContext.BuildingAggregate.Auditorium");

                    b.ToTable("auditoriums", (string)null);

                    b.HasDiscriminator().HasValue("UnassignedAuditorium");
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.AcademyAggregate.Faculty", b =>
                {
                    b.HasOne("CFU.Domain.StructureContext.AcademyAggregate.Academy", null)
                        .WithMany("Faculties")
                        .HasForeignKey("AcademyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.FacultyAggregate.Department", b =>
                {
                    b.HasOne("CFU.Domain.StructureContext.FacultyAggregate.Faculty", null)
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.FacultyAggregate.Faculty", b =>
                {
                    b.HasOne("CFU.Domain.StructureContext.AcademyAggregate.Faculty", null)
                        .WithOne()
                        .HasForeignKey("CFU.Domain.StructureContext.FacultyAggregate.Faculty", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.InstituteAggregate.Department", b =>
                {
                    b.HasOne("CFU.Domain.StructureContext.InstituteAggregate.Institute", null)
                        .WithMany("Departments")
                        .HasForeignKey("InstituteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CFU.Domain.SupplyContext.BuildingAggregate.Auditorium", b =>
                {
                    b.HasOne("CFU.Domain.SupplyContext.BuildingAggregate.Building", null)
                        .WithMany("Auditoriums")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CFU.Domain.SupplyContext.BuildingAggregate.Building", b =>
                {
                    b.OwnsOne("CFU.Domain.SupplyContext.BuildingAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("BuildingId1")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Block")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Block");

                            b1.Property<Guid>("BuildingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("BuildingNumber")
                                .HasColumnType("int")
                                .HasColumnName("BuildingNumber");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.HasKey("BuildingId1");

                            b1.ToTable("buildings");

                            b1.WithOwner()
                                .HasForeignKey("BuildingId1");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("CFU.Domain.SupplyContext.BuildingAggregate.AdministrativeAuditorium", b =>
                {
                    b.HasOne("CFU.Domain.SupplyContext.StructureUnitAggregate.StructureUnit", null)
                        .WithOne()
                        .HasForeignKey("CFU.Domain.SupplyContext.BuildingAggregate.AdministrativeAuditorium", "StructureUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.AcademyAggregate.Academy", b =>
                {
                    b.Navigation("Faculties");
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.FacultyAggregate.Faculty", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("CFU.Domain.StructureContext.InstituteAggregate.Institute", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("CFU.Domain.SupplyContext.BuildingAggregate.Building", b =>
                {
                    b.Navigation("Auditoriums");
                });
#pragma warning restore 612, 618
        }
    }
}
