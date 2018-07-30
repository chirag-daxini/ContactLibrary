using System.Web.Optimization;

namespace ContactLibrary.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/other").Include(
                "~/Scripts/jsoneditor.min.js",
                "~/Scripts/bootstrap-tagsinput.js",
                "~/Scripts/typeahead.bundle.js",
                "~/Scripts/jquery.jsonview.js",
                "~/Scripts/jquery-dateFormat.min.js",
                "~/Scripts/select2.full.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/purl.js",
                "~/Scripts/jquery.ui.widget.js",
                "~/Scripts/jquery.iframe-transport.js",
                "~/Scripts/jquery.fileupload.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/demandmap").Include(
                "~/Scripts/map/markerwithlabel.js",
                "~/Scripts/demandmap/demandmap.js"));

            bundles.Add(new ScriptBundle("~/bundles/bundles").Include(
                "~/Scripts/map/markerwithlabel.js",
                "~/Scripts/bundles/bundles.js",
                "~/Scripts/circlehelper.js"));

            bundles.Add(new ScriptBundle("~/bundles/optimization").Include(
                "~/Scripts/map/markerwithlabel.js",
                "~/Scripts/optimization/optimization.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                "~/Scripts/jquery.dataTables.js"));

            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                "~/Content/jquery.dataTables.css"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                "~/Scripts/chart.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-tagsinput.css",
                      "~/Content/jsoneditor.css",
                      "~/Content/jquery.jsonview.css",
                      "~/Content/select2.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/site.css",
                      "~/Content/jquery.fileupload.css"));

        }
    }
}