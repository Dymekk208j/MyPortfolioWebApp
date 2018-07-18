namespace MyPortfolioWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutMes",
                c => new
                    {
                        AboutMeId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.AboutMeId);
            
            CreateTable(
                "dbo.Achivments",
                c => new
                    {
                        AchivmentId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        ShowInCv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AchivmentId);
            
            CreateTable(
                "dbo.AdditionalInfoes",
                c => new
                    {
                        AdditionalInfoId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Title = c.String(),
                        ShowInCv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AdditionalInfoId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        GitHubLink = c.String(),
                        LinkedInLink = c.String(),
                        FacebookLink = c.String(),
                        Email1 = c.String(),
                        Email2 = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        EducationId = c.Int(nullable: false, identity: true),
                        SchooleName = c.String(),
                        Department = c.String(),
                        Specialization = c.String(),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CurrentPlaceOfEducation = c.Boolean(nullable: false),
                        ShowInCv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EducationId);
            
            CreateTable(
                "dbo.EmploymentHistories",
                c => new
                    {
                        EmploymentHistoryId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Position = c.String(),
                        CityOfEmployment = c.String(),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CurrentPlaceOfEmployment = c.Boolean(nullable: false),
                        ShowInCv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmploymentHistoryId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ProjectId = c.Int(nullable: false),
                        TempraryProject = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.PrivateInformations",
                c => new
                    {
                        PrivateInformationId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        PostCode = c.String(),
                        HouseNumber = c.String(),
                        FlatNumber = c.String(),
                        PhoneNumber = c.String(),
                        HomePage = c.String(),
                        Email = c.String(),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.PrivateInformationId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShordDescription = c.String(),
                        FullDescription = c.String(),
                        Commercial = c.Boolean(nullable: false),
                        ShowInCv = c.Boolean(nullable: false),
                        ImageLink = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                        AuthorId = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.ProjectTechnologies",
                c => new
                    {
                        ProjectTechnologyId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectTechnologyId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechnologyId = c.Int(nullable: false, identity: true),
                        LevelOfKnowledge = c.Int(nullable: false),
                        Name = c.String(),
                        ShowInCv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TechnologyId);
            
            CreateTable(
                "dbo.TempProjects",
                c => new
                    {
                        TempProjectId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShordDescription = c.String(),
                        FullDescription = c.String(),
                        Commercial = c.Boolean(nullable: false),
                        ShowInCv = c.Boolean(nullable: false),
                        ImageLink = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                        AuthorId = c.String(),
                    })
                .PrimaryKey(t => t.TempProjectId);
            
            CreateTable(
                "dbo.TempProjectTechnologies",
                c => new
                    {
                        TempProjectTechnologyId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TempProjectTechnologyId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Blocked = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TempProjectTechnologies");
            DropTable("dbo.TempProjects");
            DropTable("dbo.Technologies");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProjectTechnologies");
            DropTable("dbo.Projects");
            DropTable("dbo.PrivateInformations");
            DropTable("dbo.Images");
            DropTable("dbo.EmploymentHistories");
            DropTable("dbo.Educations");
            DropTable("dbo.Contacts");
            DropTable("dbo.AdditionalInfoes");
            DropTable("dbo.Achivments");
            DropTable("dbo.AboutMes");
        }
    }
}
