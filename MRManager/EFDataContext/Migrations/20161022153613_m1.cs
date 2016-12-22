using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EF.DBContext.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "Diagnostics");

            migrationBuilder.EnsureSchema(
                name: "Vitals");

            migrationBuilder.EnsureSchema(
                name: "Interview");

            migrationBuilder.CreateTable(
                name: "AddressCities",
                schema: "dbo",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressCities", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "AddressCountries",
                schema: "dbo",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressCountries", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressLines",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    AddressLine = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressParishes",
                schema: "dbo",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Id = table.Column<int>(nullable: false),
                    ParishId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressParishes", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "AddressStates",
                schema: "dbo",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Id = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressStates", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "AddressTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressZipCodes",
                schema: "dbo",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Id = table.Column<int>(nullable: false),
                    ZipCodeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressZipCodes", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Allergies",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSettings",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    SoftwareName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignedDating",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExamResultsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedDating", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodPressure",
                schema: "Vitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Diastolic = table.Column<int>(nullable: false),
                    Systolic = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPressure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoatInfo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoatName = table.Column<string>(maxLength: 50, nullable: false),
                    Comments = table.Column<string>(maxLength: -1, nullable: false),
                    MarinaList = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarePlan",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Diagnosis = table.Column<string>(maxLength: -1, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarePlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarePlanDetails",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarePlanId = table.Column<int>(nullable: false),
                    Instructions = table.Column<string>(maxLength: -1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarePlanDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarePlanDetailsSuggestedMedication",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarePlanDetailId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarePlanDetailsSuggestedMedication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultImages",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MediaId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamDetails",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExamId = table.Column<int>(nullable: false),
                    Section = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExamDetailsId = table.Column<int>(nullable: false),
                    PatientResultsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults_AnioticFluid",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AFI = table.Column<double>(nullable: false),
                    MVP = table.Column<double>(nullable: false),
                    Q1 = table.Column<double>(nullable: false),
                    Q2 = table.Column<double>(nullable: false),
                    Q3 = table.Column<double>(nullable: false),
                    Q4 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults_AnioticFluid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults_FetalDates",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Details = table.Column<string>(maxLength: -1, nullable: false),
                    EstimatedDays = table.Column<int>(nullable: false),
                    ExamResultsId = table.Column<int>(nullable: false),
                    Method = table.Column<string>(maxLength: 50, nullable: false),
                    PatientResultsId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults_FetalDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults_SimpleValues",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExamResultsId = table.Column<int>(nullable: false),
                    Field = table.Column<int>(nullable: false),
                    PatientResultsId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: -1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults_SimpleValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults_UmbilicalArtery",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SDRatio = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults_UmbilicalArtery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Describtion = table.Column<string>(maxLength: -1, nullable: false),
                    ExamTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamTypes",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForeignAddresses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    AddressTypeId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForeignPhoneNumbers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignPhoneNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Height",
                schema: "Vitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitId = table.Column<int>(nullable: false),
                    Units = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Height", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicalCategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PhaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketingMedium",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingMedium", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MediaTypeId = table.Column<int>(nullable: false),
                    Value = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileExtension = table.Column<string>(maxLength: 50, nullable: false),
                    MediaTypeName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalCategory",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonResidentCompanyInfo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonResidentCompanyInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonResidentHotelInfo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonResidentHotelInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Occupations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationAddress",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    OrganisationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationPhoneNumbers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrganisationId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationPhoneNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryTimeStamp = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    VATNumber = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations_Companys",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations_Companys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations_Hotels",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParishCities",
                schema: "dbo",
                columns: table => new
                {
                    ParishId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParishCities", x => x.ParishId);
                });

            migrationBuilder.CreateTable(
                name: "Parishes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParishName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientAllergies",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllergyId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAllergies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientDoctor",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDoctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientReligon",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    ReligionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientReligon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientResponses",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientSyntomId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientResponses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientResults",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExamId = table.Column<int>(nullable: false),
                    PatientVisitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientSyntoms",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SyntomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientSyntoms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientVisit",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfVisit = table.Column<DateTime>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVisit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddresses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    AddressTypeId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonCountryOfResidence",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCountryOfResidence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonEmailAddress",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: -1, nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEmailAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonJob",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OccupationId = table.Column<int>(nullable: false),
                    OrganisationId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonJob", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonMaritalStatus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaritalStatusId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMaritalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonMedia",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MediaId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMedia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonNames",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    PersonName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonPhoneNumbers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 8, nullable: false),
                    PhoneTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhoneNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons_ArrivalDepartureInfo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArrivalDate = table.Column<DateTime>(nullable: false),
                    DepartureDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons_ArrivalDepartureInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons_Doctor",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons_EmergencyContact",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons_EmergencyContact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons_NextOfKin",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(nullable: false),
                    Relationship = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons_NextOfKin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons_NonResidentPatient",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons_NonResidentPatient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons_Nurses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons_Nurses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons_Patient",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phase",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryPersonAddress",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonAddressesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryPersonAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryPersonPhoneNumber",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonPhoneNumberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryPersonPhoneNumber", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pulse",
                schema: "Vitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitId = table.Column<int>(nullable: false),
                    Units = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pulse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: -1, nullable: false),
                    InterviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religons",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Respiration",
                schema: "Vitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitId = table.Column<int>(nullable: false),
                    Units = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respiration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Response",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientResponseId = table.Column<int>(nullable: false),
                    ResponseOptionId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponseImages",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MediaId = table.Column<int>(nullable: false),
                    PatientResponseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponseOptions",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponseSuggestions",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseSuggestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponseSuggestions_CarePlans",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarePlanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseSuggestions_CarePlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponseSuggestions_Interviews",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InterviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseSuggestions_Interviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResultFieldNames",
                schema: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultFieldNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sex",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentInfo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Syntoms",
                schema: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Syntoms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temperature",
                schema: "Vitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitId = table.Column<int>(nullable: false),
                    Units = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UltraSoundGeneralEvaluation",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardiaActivity = table.Column<string>(maxLength: 50, nullable: false),
                    CordVessels = table.Column<string>(maxLength: 50, nullable: false),
                    FetalMovements = table.Column<string>(maxLength: 50, nullable: false),
                    Placenta = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UltraSoundGeneralEvaluation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VitalSigns",
                schema: "Vitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientVisitId = table.Column<int>(nullable: false),
                    ReaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weight",
                schema: "Vitals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitId = table.Column<int>(nullable: false),
                    Units = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZipCodes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCodes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AddressCountries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AddressLines",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AddressParishes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AddressStates",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AddressTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AddressZipCodes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Allergies",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ApplicationSettings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AssignedDating",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "BloodPressure",
                schema: "Vitals");

            migrationBuilder.DropTable(
                name: "BoatInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CarePlan",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "CarePlanDetails",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "CarePlanDetailsSuggestedMedication",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Components",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DefaultImages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ExamDetails",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "ExamResults",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "ExamResults_AnioticFluid",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "ExamResults_FetalDates",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "ExamResults_SimpleValues",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "ExamResults_UmbilicalArtery",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "Exams",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "ExamTypes",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "ForeignAddresses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ForeignPhoneNumbers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Height",
                schema: "Vitals");

            migrationBuilder.DropTable(
                name: "Interviews",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "MaritalStatus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MarketingMedium",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Media",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MediaTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MedicalCategory",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "NonResidentCompanyInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "NonResidentHotelInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Occupations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrganisationAddress",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrganisationPhoneNumbers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Organisations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Organisations_Companys",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Organisations_Hotels",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ParishCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Parishes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PatientAllergies",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PatientDoctor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PatientReligon",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PatientResponses",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "PatientResults",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "PatientSyntoms",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "PatientVisit",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonAddresses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonCountryOfResidence",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonEmailAddress",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonJob",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonMaritalStatus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonMedia",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonNames",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonPhoneNumbers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons_ArrivalDepartureInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons_Doctor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons_EmergencyContact",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons_NextOfKin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons_NonResidentPatient",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons_Nurses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons_Patient",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Phase",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "PhoneTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrimaryPersonAddress",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrimaryPersonPhoneNumber",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Pulse",
                schema: "Vitals");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "Religons",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Respiration",
                schema: "Vitals");

            migrationBuilder.DropTable(
                name: "Response",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "ResponseImages",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "ResponseOptions",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "ResponseSuggestions",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "ResponseSuggestions_CarePlans",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "ResponseSuggestions_Interviews",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "ResultFieldNames",
                schema: "Diagnostics");

            migrationBuilder.DropTable(
                name: "Sex",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "States",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudentInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Syntoms",
                schema: "Interview");

            migrationBuilder.DropTable(
                name: "Temperature",
                schema: "Vitals");

            migrationBuilder.DropTable(
                name: "UltraSoundGeneralEvaluation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "VitalSigns",
                schema: "Vitals");

            migrationBuilder.DropTable(
                name: "Weight",
                schema: "Vitals");

            migrationBuilder.DropTable(
                name: "ZipCodes",
                schema: "dbo");
        }
    }
}
