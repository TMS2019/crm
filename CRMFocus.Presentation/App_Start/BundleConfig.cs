using System.Web;
using System.Web.Optimization;

namespace CRMFocus.Presentation
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
             
            bundles.Add(new ScriptBundle("~/CRMFocus/js").Include(
                        "~/frontend/js/jquery.min.js",
                        "~/frontend/js/bootstrap/bootstrap.js",
                        "~/frontend/js/sidebar.js",
                        "~/frontend/js/kendo/kendo.all.min.js",
                        "~/frontend/js/sweetalert.min.js",
                        "~/frontend/js/jquery.serializejson.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
             

            bundles.Add(new StyleBundle("~/CRMFocus/css").Include(
                      "~/frontend/css/style.css",
                      "~/frontend/css/bootstrap/bootstrap.css",
                      "~/frontend/css/bootstrap/bootstrap-theme.css",
                      "~/frontend/css/kendo/kendo.common-material.min.css",
                      "~/frontend/css/kendo/kendo.material.min.css",
                      "~/frontend/css/kendo/kendo.material.mobile.min.css",
                      "~/frontend/css/fontawesome/font-awesome.css",
                      "~/frontend/css/fontawesome/font-awesome.min.css",
                      "~/frontend/css/sweetalert.min.css"));
        }
    }
}
