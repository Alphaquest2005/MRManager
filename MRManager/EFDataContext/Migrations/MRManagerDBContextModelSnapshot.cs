using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using EF.DBContexts;

namespace EF.DBContext.Migrations
{
    [DbContext(typeof(MRManagerDBContext))]
    partial class MRManagerDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EF.Entities.AddressCities", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AddressId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnName("CityId");

                    b.Property<int>("Id");

                    b.HasKey("AddressId");

                    b.ToTable("AddressCities","dbo");
                });

            modelBuilder.Entity("EF.Entities.AddressCountries", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AddressId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnName("CountryId");

                    b.Property<int>("Id");

                    b.HasKey("AddressId");

                    b.ToTable("AddressCountries","dbo");
                });

            modelBuilder.Entity("EF.Entities.Addresses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Addresses","dbo");
                });

            modelBuilder.Entity("EF.Entities.AddressLines", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnName("AddressId");

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasColumnName("AddressLine")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("AddressLines","dbo");
                });

            modelBuilder.Entity("EF.Entities.AddressParishes", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AddressId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Id");

                    b.Property<int>("ParishId")
                        .HasColumnName("ParishId");

                    b.HasKey("AddressId");

                    b.ToTable("AddressParishes","dbo");
                });

            modelBuilder.Entity("EF.Entities.AddressStates", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AddressId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Id");

                    b.Property<int>("StateId")
                        .HasColumnName("StateId");

                    b.HasKey("AddressId");

                    b.ToTable("AddressStates","dbo");
                });

            modelBuilder.Entity("EF.Entities.AddressTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("AddressTypes","dbo");
                });

            modelBuilder.Entity("EF.Entities.AddressZipCodes", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AddressId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Id");

                    b.Property<int>("ZipCodeId")
                        .HasColumnName("ZipCodeId");

                    b.HasKey("AddressId");

                    b.ToTable("AddressZipCodes","dbo");
                });

            modelBuilder.Entity("EF.Entities.Allergies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Allergies","dbo");
                });

            modelBuilder.Entity("EF.Entities.ApplicationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnName("CompanyName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("SoftwareName")
                        .IsRequired()
                        .HasColumnName("SoftwareName")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("ApplicationSettings","dbo");
                });

            modelBuilder.Entity("EF.Entities.AssignedDating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExamResultsId")
                        .HasColumnName("ExamResultsId");

                    b.HasKey("Id");

                    b.ToTable("AssignedDating","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.BloodPressure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Diastolic")
                        .HasColumnName("Diastolic");

                    b.Property<int>("Systolic")
                        .HasColumnName("Systolic");

                    b.Property<int>("UnitId")
                        .HasColumnName("UnitId");

                    b.HasKey("Id");

                    b.ToTable("BloodPressure","Vitals");
                });

            modelBuilder.Entity("EF.Entities.BoatInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BoatName")
                        .IsRequired()
                        .HasColumnName("BoatName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnName("Comments")
                        .HasAnnotation("MaxLength", -1);

                    b.Property<string>("MarinaList")
                        .IsRequired()
                        .HasColumnName("MarinaList")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("BoatInfo","dbo");
                });

            modelBuilder.Entity("EF.Entities.CarePlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasColumnName("Diagnosis")
                        .HasAnnotation("MaxLength", -1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("CarePlan","Interview");
                });

            modelBuilder.Entity("EF.Entities.CarePlanDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarePlanId")
                        .HasColumnName("CarePlanId");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnName("Instructions")
                        .HasAnnotation("MaxLength", -1);

                    b.HasKey("Id");

                    b.ToTable("CarePlanDetails","Interview");
                });

            modelBuilder.Entity("EF.Entities.CarePlanDetailsSuggestedMedication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarePlanDetailId")
                        .HasColumnName("CarePlanDetailId");

                    b.Property<int>("ItemId")
                        .HasColumnName("ItemId");

                    b.HasKey("Id");

                    b.ToTable("CarePlanDetailsSuggestedMedication","Interview");
                });

            modelBuilder.Entity("EF.Entities.Cities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Cities","dbo");
                });

            modelBuilder.Entity("EF.Entities.Components", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Components","dbo");
                });

            modelBuilder.Entity("EF.Entities.Countries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Countries","dbo");
                });

            modelBuilder.Entity("EF.Entities.DefaultImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MediaId")
                        .HasColumnName("MediaId");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("Type")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("DefaultImages","dbo");
                });

            modelBuilder.Entity("EF.Entities.ExamDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExamId")
                        .HasColumnName("ExamId");

                    b.Property<string>("Section")
                        .IsRequired()
                        .HasColumnName("Section")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("ExamDetails","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.ExamResults", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExamDetailsId")
                        .HasColumnName("ExamDetailsId");

                    b.Property<int>("PatientResultsId")
                        .HasColumnName("PatientResultsId");

                    b.HasKey("Id");

                    b.ToTable("ExamResults","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.ExamResults_AnioticFluid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AFI")
                        .HasColumnName("AFI");

                    b.Property<double>("MVP")
                        .HasColumnName("MVP");

                    b.Property<double>("Q1")
                        .HasColumnName("Q1");

                    b.Property<double>("Q2")
                        .HasColumnName("Q2");

                    b.Property<double>("Q3")
                        .HasColumnName("Q3");

                    b.Property<double>("Q4")
                        .HasColumnName("Q4");

                    b.HasKey("Id");

                    b.ToTable("ExamResults_AnioticFluid","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.ExamResults_FetalDates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnName("Details")
                        .HasAnnotation("MaxLength", -1);

                    b.Property<int>("EstimatedDays")
                        .HasColumnName("EstimatedDays");

                    b.Property<int>("ExamResultsId")
                        .HasColumnName("ExamResultsId");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnName("Method")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("PatientResultsId")
                        .HasColumnName("PatientResultsId");

                    b.Property<DateTime>("StartDate")
                        .HasColumnName("StartDate");

                    b.HasKey("Id");

                    b.ToTable("ExamResults_FetalDates","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.ExamResults_SimpleValues", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExamResultsId")
                        .HasColumnName("ExamResultsId");

                    b.Property<int>("Field")
                        .HasColumnName("Field");

                    b.Property<int>("PatientResultsId")
                        .HasColumnName("PatientResultsId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("Value")
                        .HasAnnotation("MaxLength", -1);

                    b.HasKey("Id");

                    b.ToTable("ExamResults_SimpleValues","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.ExamResults_UmbilicalArtery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("SDRatio")
                        .HasColumnName("SDRatio");

                    b.HasKey("Id");

                    b.ToTable("ExamResults_UmbilicalArtery","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.Exams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Describtion")
                        .IsRequired()
                        .HasColumnName("Describtion")
                        .HasAnnotation("MaxLength", -1);

                    b.Property<int>("ExamTypeId")
                        .HasColumnName("ExamTypeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Exams","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.ExamTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("ExamTypes","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.ForeignAddresses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnName("AddressId");

                    b.Property<int>("AddressTypeId")
                        .HasColumnName("AddressTypeId");

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.HasKey("Id");

                    b.ToTable("ForeignAddresses","dbo");
                });

            modelBuilder.Entity("EF.Entities.ForeignPhoneNumbers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnName("PhoneNumber")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("PhoneTypeId")
                        .HasColumnName("PhoneTypeId");

                    b.HasKey("Id");

                    b.ToTable("ForeignPhoneNumbers","dbo");
                });

            modelBuilder.Entity("EF.Entities.Height", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UnitId")
                        .HasColumnName("UnitId");

                    b.Property<double>("Units")
                        .HasColumnName("Units");

                    b.HasKey("Id");

                    b.ToTable("Height","Vitals");
                });

            modelBuilder.Entity("EF.Entities.Interviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MedicalCategoryId")
                        .HasColumnName("MedicalCategoryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("PhaseId")
                        .HasColumnName("PhaseId");

                    b.HasKey("Id");

                    b.ToTable("Interviews","Interview");
                });

            modelBuilder.Entity("EF.Entities.MaritalStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("MaritalStatus","dbo");
                });

            modelBuilder.Entity("EF.Entities.MarketingMedium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("MarketingMedium","dbo");
                });

            modelBuilder.Entity("EF.Entities.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MediaTypeId")
                        .HasColumnName("MediaTypeId");

                    b.Property<byte[]>("Value")
                        .IsRequired()
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.ToTable("Media","dbo");
                });

            modelBuilder.Entity("EF.Entities.MediaTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnName("FileExtension")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("MediaTypeName")
                        .IsRequired()
                        .HasColumnName("MediaTypeName")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("MediaTypes","dbo");
                });

            modelBuilder.Entity("EF.Entities.MedicalCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnName("Category")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("MedicalCategory","Interview");
                });

            modelBuilder.Entity("EF.Entities.NonResidentCompanyInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnName("CompanyName")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("NonResidentCompanyInfo","dbo");
                });

            modelBuilder.Entity("EF.Entities.NonResidentHotelInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HotelId")
                        .HasColumnName("HotelId");

                    b.HasKey("Id");

                    b.ToTable("NonResidentHotelInfo","dbo");
                });

            modelBuilder.Entity("EF.Entities.Occupations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Occupations","dbo");
                });

            modelBuilder.Entity("EF.Entities.OrganisationAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnName("AddressId");

                    b.Property<int>("OrganisationId")
                        .HasColumnName("OrganisationId");

                    b.HasKey("Id");

                    b.ToTable("OrganisationAddress","dbo");
                });

            modelBuilder.Entity("EF.Entities.OrganisationPhoneNumbers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrganisationId")
                        .HasColumnName("OrganisationId");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnName("PhoneNumber")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("PhoneTypeId")
                        .HasColumnName("PhoneTypeId");

                    b.HasKey("Id");

                    b.ToTable("OrganisationPhoneNumbers","dbo");
                });

            modelBuilder.Entity("EF.Entities.Organisations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("EntryTimeStamp")
                        .IsRequired()
                        .HasColumnName("EntryTimeStamp");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("VATNumber")
                        .IsRequired()
                        .HasColumnName("VATNumber")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Organisations","dbo");
                });

            modelBuilder.Entity("EF.Entities.Organisations_Companys", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Organisations_Companys","dbo");
                });

            modelBuilder.Entity("EF.Entities.Organisations_Hotels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Organisations_Hotels","dbo");
                });

            modelBuilder.Entity("EF.Entities.ParishCities", b =>
                {
                    b.Property<int>("ParishId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ParishId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnName("CityId");

                    b.Property<int>("Id");

                    b.HasKey("ParishId");

                    b.ToTable("ParishCities","dbo");
                });

            modelBuilder.Entity("EF.Entities.Parishes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ParishName")
                        .IsRequired()
                        .HasColumnName("ParishName")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Parishes","dbo");
                });

            modelBuilder.Entity("EF.Entities.PatientAllergies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AllergyId")
                        .HasColumnName("AllergyId");

                    b.Property<int>("PatientId")
                        .HasColumnName("PatientId");

                    b.HasKey("Id");

                    b.ToTable("PatientAllergies","dbo");
                });

            modelBuilder.Entity("EF.Entities.PatientDoctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DoctorId")
                        .HasColumnName("DoctorId");

                    b.Property<int>("PatientId")
                        .HasColumnName("PatientId");

                    b.HasKey("Id");

                    b.ToTable("PatientDoctor","dbo");
                });

            modelBuilder.Entity("EF.Entities.PatientReligon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.Property<int>("ReligionId")
                        .HasColumnName("ReligionId");

                    b.HasKey("Id");

                    b.ToTable("PatientReligon","dbo");
                });

            modelBuilder.Entity("EF.Entities.PatientResponses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PatientSyntomId")
                        .HasColumnName("PatientSyntomId");

                    b.Property<int>("QuestionId")
                        .HasColumnName("QuestionId");

                    b.HasKey("Id");

                    b.ToTable("PatientResponses","Interview");
                });

            modelBuilder.Entity("EF.Entities.PatientResults", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExamId")
                        .HasColumnName("ExamId");

                    b.Property<int>("PatientVisitId")
                        .HasColumnName("PatientVisitId");

                    b.HasKey("Id");

                    b.ToTable("PatientResults","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.PatientSyntoms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SyntomId")
                        .HasColumnName("SyntomId");

                    b.HasKey("Id");

                    b.ToTable("PatientSyntoms","Interview");
                });

            modelBuilder.Entity("EF.Entities.PatientVisit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfVisit")
                        .HasColumnName("DateOfVisit");

                    b.Property<int>("DoctorId")
                        .HasColumnName("DoctorId");

                    b.Property<int>("PatientId")
                        .HasColumnName("PatientId");

                    b.HasKey("Id");

                    b.ToTable("PatientVisit","dbo");
                });

            modelBuilder.Entity("EF.Entities.PersonAddresses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnName("AddressId");

                    b.Property<int>("AddressTypeId")
                        .HasColumnName("AddressTypeId");

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.HasKey("Id");

                    b.ToTable("PersonAddresses","dbo");
                });

            modelBuilder.Entity("EF.Entities.PersonCountryOfResidence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnName("CountryId");

                    b.Property<DateTime>("Date")
                        .HasColumnName("Date");

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.HasKey("Id");

                    b.ToTable("PersonCountryOfResidence","dbo");
                });

            modelBuilder.Entity("EF.Entities.PersonEmailAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasAnnotation("MaxLength", -1);

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.HasKey("Id");

                    b.ToTable("PersonEmailAddress","dbo");
                });

            modelBuilder.Entity("EF.Entities.PersonJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OccupationId")
                        .HasColumnName("OccupationId");

                    b.Property<int>("OrganisationId")
                        .HasColumnName("OrganisationId");

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.HasKey("Id");

                    b.ToTable("PersonJob","dbo");
                });

            modelBuilder.Entity("EF.Entities.PersonMaritalStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaritalStatusId")
                        .HasColumnName("MaritalStatusId");

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.HasKey("Id");

                    b.ToTable("PersonMaritalStatus","dbo");
                });

            modelBuilder.Entity("EF.Entities.PersonMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MediaId")
                        .HasColumnName("MediaId");

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.HasKey("Id");

                    b.ToTable("PersonMedia","dbo");
                });

            modelBuilder.Entity("EF.Entities.PersonNames", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnName("PersonName")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("PersonNames","dbo");
                });

            modelBuilder.Entity("EF.Entities.PersonPhoneNumbers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonId");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnName("PhoneNumber")
                        .HasAnnotation("MaxLength", 8);

                    b.Property<int>("PhoneTypeId")
                        .HasColumnName("PhoneTypeId");

                    b.HasKey("Id");

                    b.ToTable("PersonPhoneNumbers","dbo");
                });

            modelBuilder.Entity("EF.Entities.Persons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Persons","dbo");
                });

            modelBuilder.Entity("EF.Entities.Persons_ArrivalDepartureInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnName("ArrivalDate");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnName("DepartureDate");

                    b.HasKey("Id");

                    b.ToTable("Persons_ArrivalDepartureInfo","dbo");
                });

            modelBuilder.Entity("EF.Entities.Persons_Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("Code")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Persons_Doctor","dbo");
                });

            modelBuilder.Entity("EF.Entities.Persons_EmergencyContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PatientId")
                        .HasColumnName("PatientId");

                    b.HasKey("Id");

                    b.ToTable("Persons_EmergencyContact","dbo");
                });

            modelBuilder.Entity("EF.Entities.Persons_NextOfKin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PatientId")
                        .HasColumnName("PatientId");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasColumnName("Relationship")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Persons_NextOfKin","dbo");
                });

            modelBuilder.Entity("EF.Entities.Persons_NonResidentPatient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Persons_NonResidentPatient","dbo");
                });

            modelBuilder.Entity("EF.Entities.Persons_Nurses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Persons_Nurses","dbo");
                });

            modelBuilder.Entity("EF.Entities.Persons_Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Persons_Patient","dbo");
                });

            modelBuilder.Entity("EF.Entities.Phase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("Code")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Phase","Interview");
                });

            modelBuilder.Entity("EF.Entities.PhoneTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("PhoneTypes","dbo");
                });

            modelBuilder.Entity("EF.Entities.PrimaryPersonAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonAddressesId")
                        .HasColumnName("PersonAddressesId");

                    b.HasKey("Id");

                    b.ToTable("PrimaryPersonAddress","dbo");
                });

            modelBuilder.Entity("EF.Entities.PrimaryPersonPhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonPhoneNumberId")
                        .HasColumnName("PersonPhoneNumberId");

                    b.HasKey("Id");

                    b.ToTable("PrimaryPersonPhoneNumber","dbo");
                });

            modelBuilder.Entity("EF.Entities.Pulse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UnitId")
                        .HasColumnName("UnitId");

                    b.Property<int>("Units")
                        .HasColumnName("Units");

                    b.HasKey("Id");

                    b.ToTable("Pulse","Vitals");
                });

            modelBuilder.Entity("EF.Entities.Questions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasAnnotation("MaxLength", -1);

                    b.Property<int>("InterviewId")
                        .HasColumnName("InterviewId");

                    b.HasKey("Id");

                    b.ToTable("Questions","Interview");
                });

            modelBuilder.Entity("EF.Entities.Religons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Religons","dbo");
                });

            modelBuilder.Entity("EF.Entities.Respiration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UnitId")
                        .HasColumnName("UnitId");

                    b.Property<int>("Units")
                        .HasColumnName("Units");

                    b.HasKey("Id");

                    b.ToTable("Respiration","Vitals");
                });

            modelBuilder.Entity("EF.Entities.Response", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PatientResponseId")
                        .HasColumnName("PatientResponseId");

                    b.Property<int>("ResponseOptionId")
                        .HasColumnName("ResponseOptionId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("Value")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Response","Interview");
                });

            modelBuilder.Entity("EF.Entities.ResponseImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MediaId")
                        .HasColumnName("MediaId");

                    b.Property<int>("PatientResponseId")
                        .HasColumnName("PatientResponseId");

                    b.HasKey("Id");

                    b.ToTable("ResponseImages","Interview");
                });

            modelBuilder.Entity("EF.Entities.ResponseOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("QuestionId")
                        .HasColumnName("QuestionId");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("Type")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("ResponseOptions","Interview");
                });

            modelBuilder.Entity("EF.Entities.ResponseSuggestions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("ResponseSuggestions","Interview");
                });

            modelBuilder.Entity("EF.Entities.ResponseSuggestions_CarePlans", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarePlanId")
                        .HasColumnName("CarePlanId");

                    b.HasKey("Id");

                    b.ToTable("ResponseSuggestions_CarePlans","Interview");
                });

            modelBuilder.Entity("EF.Entities.ResponseSuggestions_Interviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InterviewId")
                        .HasColumnName("InterviewId");

                    b.HasKey("Id");

                    b.ToTable("ResponseSuggestions_Interviews","Interview");
                });

            modelBuilder.Entity("EF.Entities.ResultFieldNames", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("ResultFieldNames","Diagnostics");
                });

            modelBuilder.Entity("EF.Entities.Sex", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Sex","dbo");
                });

            modelBuilder.Entity("EF.Entities.States", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("States","dbo");
                });

            modelBuilder.Entity("EF.Entities.StudentInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("StudentInfo","dbo");
                });

            modelBuilder.Entity("EF.Entities.Syntoms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Syntoms","Interview");
                });

            modelBuilder.Entity("EF.Entities.Temperature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UnitId")
                        .HasColumnName("UnitId");

                    b.Property<double>("Units")
                        .HasColumnName("Units");

                    b.HasKey("Id");

                    b.ToTable("Temperature","Vitals");
                });

            modelBuilder.Entity("EF.Entities.UltraSoundGeneralEvaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CardiaActivity")
                        .IsRequired()
                        .HasColumnName("CardiaActivity")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("CordVessels")
                        .IsRequired()
                        .HasColumnName("CordVessels")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("FetalMovements")
                        .IsRequired()
                        .HasColumnName("FetalMovements")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Placenta")
                        .IsRequired()
                        .HasColumnName("Placenta")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("UltraSoundGeneralEvaluation","dbo");
                });

            modelBuilder.Entity("EF.Entities.Units", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnName("ShortName")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("Units","dbo");
                });

            modelBuilder.Entity("EF.Entities.VitalSigns", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PatientVisitId")
                        .HasColumnName("PatientVisitId");

                    b.Property<int>("ReaderId")
                        .HasColumnName("ReaderId");

                    b.HasKey("Id");

                    b.ToTable("VitalSigns","Vitals");
                });

            modelBuilder.Entity("EF.Entities.Weight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UnitId")
                        .HasColumnName("UnitId");

                    b.Property<double>("Units")
                        .HasColumnName("Units");

                    b.HasKey("Id");

                    b.ToTable("Weight","Vitals");
                });

            modelBuilder.Entity("EF.Entities.ZipCodes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("ZipCodes","dbo");
                });
        }
    }
}
