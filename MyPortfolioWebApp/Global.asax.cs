using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using MyPortfolioWebApp.Models;
using MyPortfolioWebApp.Models.DatabaseModels;
using MyPortfolioWebApp.Models.DatabaseModels.DatabaseContext.Initializers;
using MyPortfolioWebApp.Models.ViewModels;

namespace MyPortfolioWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(config =>
            {
                config.CreateMap<AboutMeViewModel, AboutMe>().ReverseMap();
                config.CreateMap<ContactViewModel, Contact>().ReverseMap();
                config.CreateMap<PrivateInformationViewModel, PrivateInformation>().ReverseMap();
                config.CreateMap<TechnologyViewModel, Technology>().ReverseMap();
                config.CreateMap<AchivmentViewModel, Achivment>().ReverseMap();
                config.CreateMap<AdditionalInfoViewModel, AdditionalInfo>().ReverseMap();
                config.CreateMap<EmploymentHistoryViewModel, EmploymentHistory>().ReverseMap();
                config.CreateMap<EducationViewModel, Education>().ReverseMap();
                config.CreateMap<ProjectViewModel, Project>().ReverseMap();
                config.CreateMap<Project, TempProject>().ReverseMap();
                config.CreateMap<TempProjectTechnology, ProjectTechnology>().ReverseMap();
                config.CreateMap<TempProjectViewModel, TempProject>().ReverseMap();
                config.CreateMap<UpdateViewModel, ApplicationUser>().ReverseMap();
            });

            Database.SetInitializer(new DbInitializer());
            
        }

    }
}