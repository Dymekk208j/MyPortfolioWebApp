using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MyPortfolioWebApp.Models.DatabaseModels;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class ProjectListModelView
    {
        public List<ProjectViewModel> List { get; set; }
    }
}