using System.Web;
using System.Web.Optimization;

namespace RAM.Admin.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.10.4.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/tagit").Include(
                        "~/Scripts/tag-it.*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                       "~/Scripts/angular.js",
                       "~/Scripts/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/dropzone").Include(
                        "~/Scripts/dropzone.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/basic.css",
                       "~/Content/dropzone.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-responsive.css",
                      "~/Content/datepicker.css",
                      "~/Content/datepicker3.css",
                      "~/Content/tagit.ui-zendesk.css",
                      "~/Content/jquery.tagit.css"));
        }
    }
}
