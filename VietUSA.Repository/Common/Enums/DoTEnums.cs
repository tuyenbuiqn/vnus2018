using System.ComponentModel;

namespace VietUSA.Repository.Common.Enums
{
    //https://blogs.msdn.microsoft.com/abhinaba/2005/10/21/c-3-0-using-extension-methods-for-enum-tostring/
    /*
     How to use
     1. Get [DescriptionAttribute]
     ((TravelCompanyTypeEnum)travelCompanyType).ToDescription();
     2. Check value is exist in enum
     var exists = Enum.IsDefined(typeof(TravelCompanyTypeEnum), travelCompanyType);
     3. Get value
     travelCompanyType = (int)TravelCompanyTypeEnum.Domestic
    */
    public enum ButtonTextEnum
    {
        [Description("Lưu")]
        Save = 0,
        [Description("Thoát")]
        Exit = 1,
        [Description("Thêm mới theo file")]
        Import = 2,
        [Description("Kết xuất dữ liệu")]
        Export = 3,
        [Description("Tìm kiếm")]
        Search = 4,
        [Description("Thêm")]
        Add = 5,
        [Description("Thêm mới")]
        AddNew = 6,
        [Description("Tùy biến kết xuất dữ liệu")]
        CustomizeExport = 7,
    }

    public enum ObjectTypeEnum
    {
        [Description("UserExcelFile")]
        UserExcelFile = 5000,

        [Description("Abstract Submission")]
        AbstractSubmission = 1,
        [Description("Article")]
        Article = 2,

        [Description("Document")]
        Document = 3,
        [Description("Role")]
        Role = 4,
        [Description("User")]
        User = 5,
        [Description("UserGroup")]
        UserGroup = 6,
        [Description("Group")]
        Group = 7,
        [Description("Contact")]
        Contact = 8,
        [Description("Member")]
        Member = 9,
        [Description("Category")]
        Category = 10,
        [Description("Organisation")]
        Organisation = 11
    }

    public enum MemberTypeEnum
    {
        [Description("Program Committee")]
        ProgramCommittee = 19,
        [Description("Invited Addresses")]
        InvitedAddresses = 20
    }
    public enum CExcelFormatTypeEnum
    {
        [Description("Header format")]
        Header = 1,
        [Description("Body format")]
        Body = 2
    }
    public enum CExcelUpdateColumnType
    {
        [Description("Add")]
        Add = 1,
        [Description("Remove")]
        Remove = 0
    }
    public enum GroupCategoryEnum
    {
        [Description("User Type")]
        UserType = 2,
        [Description("Member Type")]
        MemberType = 3,
        [Description("Organizer Type")]
        OrganizerType = 6
    }

    public enum UserTypeEnum
    {
        [Description("System User")]
        SystemUser = 4,
        [Description("Anonymous User")]
        AnonymousUser = 5
    }

    public enum CExcelObjectTypeEnum
    {
        [Description("AbtractionSubmission")]
        AbtractionSubmission = 1
    }
    public enum TableNameEnum
    {
        [Description("dbo.AccomInspection")]
        AccomInspection,
        [Description("dbo.AccommodationFacility")]
        AccommodationFacility,
        [Description("dbo.AdministrativeDivision")]
        AdministrativeDivision,
        [Description("dbo.Announcement")]
        Announcement,
        [Description("dbo.Category")]
        Category,
        [Description("dbo.Complex")]
        Complex,
        [Description("dbo.Config")]
        Config,
        [Description("dbo.Degree")]
        Degree,
        [Description("dbo.Department")]
        Department,
        [Description("dbo.ForeignLanguage")]
        ForeignLanguage,
        [Description("dbo.Geography")]
        Geography,
        [Description("dbo.Group")]
        Group,
        [Description("dbo.GroupRole")]
        GroupRole,
        [Description("dbo.HeadCompany")]
        HeadCompany,
        [Description("dbo.LicensePaper")]
        LicensePaper,
        [Description("dbo.Log")]
        Log,
        [Description("dbo.LogDbError")]
        LogDbError,
        [Description("dbo.LogTravelCompany")]
        LogTravelCompany,
        [Description("dbo.MainFood")]
        MainFood,
        [Description("dbo.Media")]
        Media,
        [Description("dbo.RepresentativeOffice")]
        RepresentativeOffice,
        [Description("dbo.Restaurant")]
        Restaurant,
        [Description("dbo.Role")]
        Role,
        [Description("dbo.Search")]
        Search,
        [Description("dbo.Statistical_Data")]
        StatisticalData,
        [Description("dbo.Statistical_Targets")]
        StatisticalTargets,
        [Description("dbo.TemplateFile")]
        TemplateFile,
        [Description("dbo.TourismStatistic")]
        TourismStatistic,
        [Description("dbo.TouristGuide")]
        TouristGuide,
        [Description("dbo.ObjectLanguage")]
        TouristGuideForeignLanguage,
        [Description("dbo.TransportCompany")]
        TransportCompany,
        [Description("dbo.TravelBusiness")]
        TravelBusiness,
        [Description("dbo.TravelCompany")]
        TravelCompany,
        [Description("dbo.TravelCompanyReference")]
        TravelCompanyReference,
        [Description("dbo.User")]
        User,
        [Description("dbo.UserGroup")]
        UserGroup,
        [Description("dbo.Vehicle")]
        Vehicle,
        [Description("dbo.Qh_Project")]
        QhProject,
        [Description("dbo.Qh_Planning")]
        QhPlanning,
        [Description("dbo.Qh_PlanningProcess")]
        QhPlanningProcess,
        [Description("dbo.Qh_Cooperation")]
        QhCooperation,
        [Description("dbo.Qh_CooperationActivity")]
        QhCooperationActivity,
        [Description("dbo.Qh_Place")]
        QhPlace,
        [Description("dbo.Qh_LanguageSupport")]
        QhLanguageSupport,
        [Description("dbo.VP_Publication")]
        VpPublication,
        [Description("dbo.VP_PublicationDetail")]
        VpPublicationdetail,
        [Description("dbo.VP_Internationalorganization")]
        VpInternationalorganization,
        [Description("dbo.TT_Inspection")]
        TtInspection,
        [Description("dbo.VP_Embassy")]
        VpEmbassy,
        [Description("dbo.VP_AdministrativeFormality")]
        VpAdministrativeformality,
        [Description("dbo.VP_CadresTrainning")]
        VpCadrestrainning,
        [Description("dbo.VP_EmulationReward")]
        VpEmulationReward,
        [Description("dbo.VP_HrTrainingTourist")]
        VpHrtrainingtourist,
        [Description("dbo.TT_Complain")]
        TtComplain,
        [Description("dbo.Vp_RewardCriteria")]
        VpRewardCriteria,
        [Description("dbo.VP_SynthesisAdminFormalities")]
        VPSynthesisAdminFormalities
    }
}
