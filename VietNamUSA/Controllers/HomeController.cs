using System.Web.Mvc;
using VietNamUSA.Areas.XPanel.Controllers;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Enums;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietNamUSA.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMemberBusiness _memberBusiness;
        private readonly IOrganisationBusiness _organisationBusiness;

        public HomeController()
        {
            _memberBusiness = new MemberBusiness(FrontEndUser);
            _organisationBusiness = new OrganisationBusiness(FrontEndUser);
        }
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Contact()
        //{
        //    return View(new ContactModel());
        //}
        public ActionResult ConferenceVenue()
        {
            return View();
        }
        public ActionResult Poster()
        {
            return View();
        }
        public ActionResult VisaUsefulInformation()
        {
            return View();
        }
        public ActionResult Organizers()
        {
            var data = _organisationBusiness.GetOrganisation(new OrganisationModel()
            {
                IsPublished = true,
                DisplayInContentPage = true
            }).Data;
            return View(data);
        }
        public ActionResult HomePageOrganisation()
        {
            var data = _organisationBusiness.GetOrganisation(new OrganisationModel()
            {
                IsPublished = true,
                DisplayInHomePage = true
            }).Data;
            return PartialView("_Organisations",data);
        }
        public ActionResult Program()
        {
            return View();
        }
      
        public ActionResult WelcomeMessage()
        {
            return View();
        }
        public ActionResult AboutQuyNhon()
        {
            return View();
        }
        public ActionResult AbstractSubmission()
        {
            return View();
        }
        public ActionResult ToBeUpdated()
        {
            return View();
        }
        public ActionResult ProgramCommittee()
        {
            var data = _memberBusiness.SearchPaging(new SearchModel<MemberModel>()
            {
                ColumnOrder = "order_asc",
                PageSize = PagingConstant.PageSize5000,
                PageIndex = PagingConstant.MinPageIndex,
                Cretia = new MemberModel()
                {
                    MemberTypeId = (int) MemberTypeEnum.ProgramCommittee,
                    IsPublished = true
                }
            });
            return View(data);
        }
        public ActionResult InvitedAddresses()
        {
            var data = _memberBusiness.SearchPaging(new SearchModel<MemberModel>()
            {
                ColumnOrder = "order_asc",
                PageSize = PagingConstant.PageSize5000,
                PageIndex = PagingConstant.MinPageIndex,
                Cretia = new MemberModel()
                {
                    MemberTypeId = (int)MemberTypeEnum.InvitedAddresses,
                    IsPublished = true
                }
            });
            return View(data);
        }

        public ActionResult SpecialSessions()
        {
            return View();
        }
        public ActionResult Sponsors()
        {
            var data = _organisationBusiness.GetOrganisation(new OrganisationModel()
            {
                IsPublished = true,
                CategoryId = 26
                
            }).Data;
            return View(data);
        }
        public ActionResult ImportantDates()
        {
            return View();
        }
    }
}