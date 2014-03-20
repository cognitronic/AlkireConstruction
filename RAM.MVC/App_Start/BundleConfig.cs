using System.Web;
using System.Web.Optimization;

namespace RAM.MVC
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/alkire").Include(
                        "~/Scripts/jquery.inview.min.js",
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/jquery.magnific-popup.min.js",
                        "~/Scripts/jquery.isotope.min.js",
                        "~/Scripts/jquery.jribbble.min.js",
                        "~/Scripts/jquery.embedagram.min.js",
                        "~/Scripts/scripts.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/owl.carousel.css",
                      "~/Content/animate.custom.css",
                      "~/Content/magnific-popup.css",
                      "~/Content/style.css",
                      "~/Content/skin/magma.css",
                      "~/Content/site.css"));
        }
    }
}