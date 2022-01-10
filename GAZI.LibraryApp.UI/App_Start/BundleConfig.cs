using System.Web;
using System.Web.Optimization;

namespace GAZI.LibraryApp.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //Styles
            bundles.Add(new StyleBundle("~/Content/Gentelella").Include(
                "~/Content/Gentelella/vendors/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/Gentelella/vendors/font-awesome/css/font-awesome.min.css",
                "~/Content/Gentelella/vendors/nprogress/nprogress.css",
                "~/Content/Gentelella/build/css/custom.min.css"));

            bundles.Add(new StyleBundle("~/Content/GentelellaDatatables").Include(
                "~/Content/Gentelella/vendors/iCheck/skins/flat/green.css",
                "~/Content/Gentelella/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css",
                "~/Content/Gentelella/vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css",
                "~/Content/Gentelella/vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css",
                "~/Content/Gentelella/vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css",
                "~/Content/Gentelella/vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css"
                ));

            //Scriptler
            bundles.Add(new ScriptBundle("~/Content/GentelellaJs").Include(
                "~/Content/Gentelella/vendors/jquery/dist/jquery.min.js",
                "~/Content/alert.js",
                "~/Content/Gentelella/vendors/bootstrap/dist/js/bootstrap.min.js",
                 "~/Content/plugins/bootstrapvalidator/js/bootstrapValidator.js",
                "~/Content/Gentelella/vendors/fastclick/lib/fastclick.js",
                "~/Content/Gentelella/vendors/nprogress/nprogress.js"));

            bundles.Add(new ScriptBundle("~/Content/GentelellaDatatablesJs").Include(
                "~/Content/Gentelella/vendors/iCheck/icheck.min.js",
                "~/Content/Gentelella/vendors/datatables.net/js/jquery.dataTables.min.js",
                "~/Content/Gentelella/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
                "~/Content/Gentelella/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
                "~/Content/Gentelella/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                "~/Content/Gentelella/vendors/datatables.net-buttons/js/buttons.flash.min.js",
                "~/Content/Gentelella/vendors/datatables.net-buttons/js/buttons.html5.min.js",
                "~/Content/Gentelella/vendors/datatables.net-buttons/js/buttons.print.min.js",
                "~/Content/Gentelella/vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                "~/Content/Gentelella/vendors/datatables.net-keytable/js/dataTables.keyTable.min.js",
                "~/Content/Gentelella/vendors/datatables.net-responsive/js/dataTables.responsive.min.js",
                "~/Content/Gentelella/vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js",
                "~/Content/Gentelella/vendors/datatables.net-scroller/js/dataTables.scroller.min.js",
                "~/Content/Gentelella/vendors/jszip/dist/jszip.min.js",
                "~/Content/Gentelella/vendors/pdfmake/build/pdfmake.min.js",
                "~/Content/Gentelella/vendors/pdfmake/build/vfs_fonts.js",
                "~/Content/Gentelella/build/js/custom.min.js"));



        }
    }
}
