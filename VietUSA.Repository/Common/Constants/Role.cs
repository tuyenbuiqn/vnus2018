using System.Collections.Generic;

namespace VietUSA.Repository.Common.Constants
{
    public static class Role
    {
        /*
            C   = Create
            R   = Read
            U   = Update
            D   = Delete
            IM  = Import
            EX  = Export
            
            RO      = RepresentativeOffice
            RE      = Restaurant 
            AF      = AccommodationFacility
            MF      = MainFood
            CO      = Complex
            AF_T    = AccommodationFacility Type
            CO_T    = Complex Type
            FL      = Foreign Language
            RA	    = Rating
            NA	    = Nation
            PRO	    = Province
            DIS	    = District
            DEP	    = Department
            TC_T	= Tourism Guide Type
            C       = Complex
        */

        #region Abtraction Submission
        /// <summary>
        /// Create Abtraction Submission
        /// </summary>
        public const int C_A_S = 1;
        /// <summary>
        /// Read Abtraction Submission
        /// </summary>
        public const int R_A_S = 2;
        /// <summary>
        /// Update Abtraction Submission
        /// </summary>
        public const int U_A_S = 3;
        /// <summary>
        /// Delete Abtraction Submission
        /// </summary>
        public const int D_A_S = 4;
        /// <summary>
        /// Import Abtraction Submission
        /// </summary>
        public const int IM_A_S = 5;
        /// <summary>
        /// Export Abtraction Submission
        /// </summary>
        public const int EX_A_S = 6;
        #endregion End Abtraction Submission

        #region User
        /// <summary>
        /// Create User
        /// </summary>
        public const int C_USER = 7;
        /// <summary>
        /// Read User
        /// </summary>
        public const int R_USER = 8;
        /// <summary>
        /// Update User
        /// </summary>
        public const int U_USER = 9;
        /// <summary>
        /// Delete User
        /// </summary>
        public const int D_USER = 10;
        /// <summary>
        /// Import User
        /// </summary>
        public const int IM_USER = 11;
        /// <summary>
        /// Export User
        /// </summary>
        public const int EX_USER = 12;
        #endregion End User

        #region Article
        /// <summary>
        /// Create Article
        /// </summary>
        public const int C_ART = 13;
        /// <summary>
        /// Read Article
        /// </summary>
        public const int R_ART = 14;
        /// <summary>
        /// Update Article
        /// </summary>
        public const int U_ART = 15;
        /// <summary>
        /// Delete Article
        /// </summary>
        public const int D_ART = 16;
        /// <summary>
        /// Import Article
        /// </summary>
        public const int IM_ART = 17;
        /// <summary>
        /// Export Article
        /// </summary>
        public const int EX_ART = 18;
        #endregion End Article

        #region Article Category
        /// <summary>
        /// Create Article Category
        /// </summary>
        public const int C_ART_CATE = 19;
        /// <summary>
        /// Read Article Category
        /// </summary>
        public const int R_ART_CATE = 20;
        /// <summary>
        /// Update Article Category
        /// </summary>
        public const int U_ART_CATE = 21;
        /// <summary>
        /// Delete Article Category
        /// </summary>
        public const int D_ART_CATE = 22;
        /// <summary>
        /// Import Article Category
        /// </summary>
        public const int IM_ART_CATE = 23;
        /// <summary>
        /// Export Article Category
        /// </summary>
        public const int EX_ART_CATE = 24;
        #endregion End Article Category


        #region Member
        /// <summary>
        /// Create Member
        /// </summary>
        public const int C_MEM = 25;
        /// <summary>
        /// Read Member
        /// </summary>
        public const int R_MEM = 26;
        /// <summary>
        /// Update Member
        /// </summary>
        public const int U_MEM = 27;
        /// <summary>
        /// Delete Member
        /// </summary>
        public const int D_MEM = 28;
        /// <summary>
        /// Import Member
        /// </summary>
        public const int IM_MEM = 29;
        /// <summary>
        /// Export Member
        /// </summary>
        public const int EX_MEM = 30;
        #endregion End Member


        #region Category
        /// <summary>
        /// Create Category
        /// </summary>
        public const int C_CATE = 31;
        /// <summary>
        /// Read Category
        /// </summary>
        public const int R_CATE = 32;
        /// <summary>
        /// Update Category
        /// </summary>
        public const int U_CATE = 33;
        /// <summary>
        /// Delete Category
        /// </summary>
        public const int D_CATE = 34;
        /// <summary>
        /// Import Category
        /// </summary>
        public const int IM_CATE = 35;
        /// <summary>
        /// Export Category
        /// </summary>
        public const int EX_CATE = 36;
        #endregion End Category

        #region Organisation
        /// <summary>
        /// Create Organisation
        /// </summary>
        public const int C_ORG = 37;
        /// <summary>
        /// Read Organisation
        /// </summary>
        public const int R_ORG = 38;
        /// <summary>
        /// Update Organisation
        /// </summary>
        public const int U_ORG = 39;
        /// <summary>
        /// Delete Organisation
        /// </summary>
        public const int D_ORG = 40;
        /// <summary>
        /// Import Organisation
        /// </summary>
        public const int IM_ORG = 41;
        /// <summary>
        /// Export Organisation
        /// </summary>
        public const int EX_ORG = 42;
        #endregion End Organisation
    }

    public static class RoleByGroup
    {
        //  RO      = RepresentativeOffice
        //  RE      = Restaurant 
        //  AF      = AccommodationFacility
        //  MF      = MainFood
        //  CO      = Complex
        //  AF_T    = AccommodationFacility Type
        //  CO_T    = Complex Type
        //  FL      = Foreign Language
        //  RA	    = Rating
        //  NA	    = Nation
        //  PRO	    = Province
        //  DIS	    = District
        //  DEP	    = Department
        //  TC_T	= Tourism Guide Type
    }
}
