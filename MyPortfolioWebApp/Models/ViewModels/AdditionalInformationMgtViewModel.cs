using AutoMapper;
using MyPortfolioWebApp.Models.DatabaseModels;
using System.Collections.Generic;
using System.Linq;

namespace MyPortfolioWebApp.Models.ViewModels
{
    public class AdditionalInformationMgtViewModel
    {
        public List<AdditionalInfoViewModel> List { get; }

        public AdditionalInformationMgtViewModel()
        {
            List = new List<AdditionalInfoViewModel>();
            ApplicationDbContext db = new ApplicationDbContext();

            var additionalInfoList = from u in db.AdditionalInfos select u;
            foreach (AdditionalInfo ai in additionalInfoList)
            {
                var addInfo = Mapper.Map<AdditionalInfo, AdditionalInfoViewModel>(ai);

                List.Add(addInfo);
            }

        }
    }
}