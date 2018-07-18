using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyPortfolioWebApp.Models.DatabaseModels.DatabaseContext.Initializers
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser();
            user.FirstName = "Damian";
            user.LastName = "Dziura";
            user.UserName = "Dymek";
            user.Blocked = false;
            user.Email = "Kontakt@damiandziura.pl";
            
            userManager.Create(user, "Damian13");
            userManager.Create(new ApplicationUser() { UserName = "Dymekk", FirstName = "Damian2" }, "Damian13");


            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            userManager.AddToRole(user.Id, "Admin");
           
            var aboutMePage = new List<AboutMe>
            {
                new AboutMe() {AboutMeId = 1 ,Title = "Tytuł startowy",
                    Text = "Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.",
                    ImageLink = "http://via.placeholder.com/250x300" }
              };
            aboutMePage.ForEach(g => context.AboutMe.Add(g));
            context.SaveChanges();

            
            var achivments = new List<Achivment>
            {
                new Achivment() { AchivmentId = 1, Description = "Pierwsze osiagniecie, opis", Title = "Pierwsze osiagniecie", Date=DateTime.Now, ShowInCv = true },
                new Achivment() { AchivmentId = 2, Description = "Drugie osiagniecie, opis", Title = "Drugie osiagniecie", Date=DateTime.Now, ShowInCv = false }

              };
            achivments.ForEach(g => context.Achivments.Add(g));
            context.SaveChanges();

            var additionalInfos = new List<AdditionalInfo>
            {
                new AdditionalInfo() { AdditionalInfoId = 1, Type = 0, Title = "Język angielski - Poziom podstawowy", ShowInCv = true},
                new AdditionalInfo() { AdditionalInfoId = 2, Type = 1, Title = "Prawo jazdy B. (Dodatkowa um)", ShowInCv = true },
                new AdditionalInfo() { AdditionalInfoId = 3, Type = 2, Title = "Gry komputerowe (Zainteresowania)", ShowInCv = true }

              };
            additionalInfos.ForEach(g => context.AdditionalInfos.Add(g));
            context.SaveChanges();

            var educations = new List<Education>
            {
                new Education() { EducationId = 1, CurrentPlaceOfEducation = true, SchooleName = "Politechnika koszalinska", Department = "WYDZIAŁ ELEKTRONIKI I INFORMATYKI", Specialization = "Inżynieria testów oprogramowania", EndDate = new DateTime(2020, 1, 1, 01, 01, 01), StartDate = new DateTime(2016, 1, 1, 01, 01, 01), ShowInCv = true},
                new Education() { EducationId = 2, CurrentPlaceOfEducation = false, SchooleName = "ZESPÓŁ SZKÓŁ NR. 9 IM. ROMUALLDA TRAGUTTA", Department = "TECHNIKUM INFORMATYCZNE", Specialization = "Technik informatyk", EndDate = new DateTime(2014, 1, 1, 01, 01, 01), StartDate = new DateTime(2010, 1, 1, 01, 01, 01), ShowInCv = true}
              };
            educations.ForEach(g => context.Educations.Add(g));
            context.SaveChanges();

            var employmentHistorie = new List<EmploymentHistory>
            {
                new EmploymentHistory() { EmploymentHistoryId = 1, CompanyName = "Mediadat software", CityOfEmployment="Koszalin", CurrentPlaceOfEmployment = true, Position="Programista", StartDate = new DateTime(2017, 9, 1, 01, 01, 01), ShowInCv = true},
                new EmploymentHistory() { EmploymentHistoryId = 2, CompanyName = "GEIS", CityOfEmployment="Koszalin", CurrentPlaceOfEmployment = false, Position="Brygadzista", StartDate = new DateTime(2016, 1, 1, 01, 01, 01), EndDate = new DateTime(2017, 9, 1, 00, 00, 00), ShowInCv = true}
              };
            employmentHistorie.ForEach(g => context.EmploymentHistories.Add(g));
            context.SaveChanges();

            var projects = new List<Project>
            {
                new Project() { ProjectId = 1, FullDescription = "Pelen opis pierwszego projektu", ShordDescription = "krotki opis pierwszego projektu",  Title="Tytul pierwszego projektu", Commercial = true, ShowInCv = true, DateTimeCreated = new DateTime(2016, 1, 1, 01, 01, 01), IsIcon = false },
                new Project() { ProjectId = 2, FullDescription = "Pelen opis drugiego projektu", ShordDescription = "krotki opis drugiego projektu",  Title="Tytul drugiego projektu", Commercial = false, ShowInCv = true,  DateTimeCreated = new DateTime(2017, 1, 1, 01, 01, 01), IsIcon = false },
                new Project() { ProjectId = 3, FullDescription = "Pelen opis trzeciego projektu", ShordDescription = "krotki opis trzeciego projektu",  Title="Tytul trzeciego projektu", Commercial = true, ShowInCv = false,  DateTimeCreated = new DateTime(2018, 1, 1, 01, 01, 01), IsIcon = false },
                new Project() { ProjectId = 4, FullDescription = "Pelen opis czwartego projektu", ShordDescription = "krotki opis czwartego projektu",  Title="Tytul czwartego projektu", Commercial = false, ShowInCv = false,  DateTimeCreated = new DateTime(2019, 1, 1, 01, 01, 01), IsIcon = false }

            };
            projects.ForEach(g => context.Projects.Add(g));
            context.SaveChanges();

            var technologies = new List<Technology>
            {
                new Technology() { TechnologyId = 1, Name="JAVA", LevelOfKnowledge=0, ShowInCv = true },
                new Technology() { TechnologyId = 2, Name="C#", LevelOfKnowledge=0, ShowInCv = true },
                new Technology() { TechnologyId = 3, Name="JavaScript", LevelOfKnowledge=1, ShowInCv = false },
                new Technology() { TechnologyId = 4, Name="ASP.net", LevelOfKnowledge=1, ShowInCv = true },
                new Technology() { TechnologyId = 5, Name="UML", LevelOfKnowledge=2, ShowInCv = false },
                new Technology() { TechnologyId = 6, Name="CSS", LevelOfKnowledge=2, ShowInCv = false },
                new Technology() { TechnologyId = 7, Name="Android", LevelOfKnowledge=2, ShowInCv = true },
                new Technology() { TechnologyId = 8, Name="SQLite", LevelOfKnowledge=2, ShowInCv = false }

              };
            technologies.ForEach(g => context.Technologies.Add(g));
            context.SaveChanges();

            var privateInformation = new List<PrivateInformation>
            {
                new PrivateInformation() { PrivateInformationId = 1, City = "Koszalin", Email = "Kontakt@DamianDziura.pl", FirstName = "Damian", LastName = "Dziura"
                , Street = "1 maja", HouseNumber = "1", FlatNumber = "3", PostCode="75-800", HomePage = "www.DamianDziura.pl", PhoneNumber="510-075-067", ImageLink= "http://via.placeholder.com/150x180" },


              };
            privateInformation.ForEach(g => context.PrivateInformation.Add(g));
            context.SaveChanges();


            var contact = new List<Contact>
            {
                new Contact() { ContactId = 1, Email1 = "kontakt@DamianDziura.pl", Email2 = "Dymekk208j@gmail.com", FacebookLink = "https://www.facebook.com/dymekk208j", GitHubLink = "https://github.com/Dymekk208j/", LinkedInLink = "https://www.linkedin.com/in/damian-dziura-27a821114/", PhoneNumber = "+48 510-075-067" }
              };
            contact.ForEach(g => context.Contact.Add(g));
            context.SaveChanges();

            var projectTechnologies = new List<ProjectTechnology>
            {
                new ProjectTechnology() {ProjectTechnologyId = 1, ProjectId = 1, TechnologyId = 1},
                new ProjectTechnology() {ProjectTechnologyId = 2, ProjectId = 1, TechnologyId = 2},

                new ProjectTechnology() {ProjectTechnologyId = 3, ProjectId = 2, TechnologyId = 3},
                new ProjectTechnology() {ProjectTechnologyId = 4, ProjectId = 2, TechnologyId = 4},

                new ProjectTechnology() {ProjectTechnologyId = 5, ProjectId = 3, TechnologyId = 5},
                new ProjectTechnology() {ProjectTechnologyId = 6, ProjectId = 3, TechnologyId = 6},

                new ProjectTechnology() {ProjectTechnologyId = 7, ProjectId = 4, TechnologyId = 7},
                new ProjectTechnology() {ProjectTechnologyId = 8, ProjectId = 4, TechnologyId = 8}
              };
            projectTechnologies.ForEach(g => context.ProjectTechnologies.Add(g));
            context.SaveChanges();

        }

        
    }
}