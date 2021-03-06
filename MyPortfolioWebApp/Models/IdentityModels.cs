﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortfolioWebApp.Models.DatabaseModels;

namespace MyPortfolioWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Blocked { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AppConnectionString", throwIfV1Schema: false)
        {
            Database.CommandTimeout = 900;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<Achivment> Achivments { get; set; }
        public DbSet<AdditionalInfo> AdditionalInfos { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<PrivateInformation> PrivateInformation { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }
        public DbSet<TempProjectTechnology> TempProjectTechnologies { get; set; }
        public DbSet<TempProject> TempProjects { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}