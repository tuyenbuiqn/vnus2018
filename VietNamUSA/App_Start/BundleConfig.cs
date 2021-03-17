using System.Web.Optimization;

namespace VietNamUSA
{
    public class BundleConfig
    {
        /*_LayOut - DOT*/
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/_adminLte").Include(
                     "~/Scripts/app.js",
                     "~/Scripts/dashboard.js",
                     "~/Scripts/demo.js"
         ));

         


            bundles.Add(new ScriptBundle("~/bundles/CommonScript").Include(
                       "~/Scripts/jquery-1.9.1.js",
                       "~/Scripts/moment/moment.js",
                       "~/Scripts/moment/moment-with-locales.js",
                       "~/Scripts/bootstrap.js"
                       //"~/Scripts/fontawesome-markers.js",
                       //"~/Scripts/kartik-v-bootstrap-fileinput-da964b7/js/fileinput.min.js",
                       //"~/Scripts/kartik-v-bootstrap-fileinput-da964b7/js/locales/vi.js",
                       //"~/Scripts/bootstrap-select.min.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/PluginScript").Include(
                       "~/Scripts/select2/select2.js",
                       "~/Scripts/select2/select2.multi-checkboxes.js",
                       "~/Scripts/bootstrap-paginator.js",
                       "~/Scripts/bootstrap-datetimepicker.js",
                       "~/Scripts/formValidation/bootstrapValidator.js",
                       "~/Scripts/sortable/Sortable.js"
           //, "~/Scripts/josor/jssor.slider.min.js"
           ));

            bundles.Add(new ScriptBundle("~/bundles/DoTScript").Include(
                        "~/Scripts/DOTCommonConstant.js",
                        "~/Scripts/DOTCommonScript.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/tmpl").Include(
                       "~/Scripts/tmpl/jquery.tmpl.js",
                       "~/Scripts/tmpl/jquery.tmplPlus.js"
            ));

            //bundles.Add(new ScriptBundle("~/bundles/LoginScript").Include(
            //       "~/Scripts/jquery-1.9.1.js",
            //       "~/Scripts/backstretch.js",
            //       "~/Scripts/DOTCommonScript.js",
            //       "~/Scripts/formValidation/bootstrapValidator.js"
            //));

            bundles.Add(new ScriptBundle("~/bundles/GoogleChart").Include(
            "~/Scripts/google-chart-loader.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/TableExport").Include(
            "~/Scripts/tableExport.js",
            "~/Scripts/jquery.base64.js"
            ));


            bundles.Add(new StyleBundle("~/bundles/_adminLteStyle").Include(
                  "~/Content/AdminLTE.css",
                  "~/Content/Style.css",
                  "~/Content/ionicons.css",
                  "~/Content/Skins/_all-skins.css"
      ));

            bundles.Add(new StyleBundle("~/bundles/CommonStyle").Include(
                         "~/Content/normalize.css",
                         "~/Content/bootstrap.css",
                         "~/Content/bootstrap-theme.css",
                         "~/Content/bootstrap-datetimepicker.css",
                         "~/Content/select2/select2.css",
                         "~/Content/font-awesome.css",
                         "~/Content/bootstrap-select.min.css"
                         ));


            bundles.Add(new StyleBundle("~/bundles/DoTStyle").Include(
                         "~/Content/Layout.css",
                         "~/Content/Style.css",
                         "~/Content/theme.css"
                         ));


            bundles.Add(new StyleBundle("~/bundles/LoginStyle").Include(
                         "~/Content/normalize.css",
                         "~/Content/bootstrap.css",
                         "~/Content/bootstrap-responsive.css",
                         "~/Content/typica-login.css"
                        ));
        }
    }
}