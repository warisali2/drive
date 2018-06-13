using System.Web;
using System.Web.Optimization;

namespace Drive
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/dialog-polyfill").Include(
                    "~/Scripts/dialog-polyfill.js"));

            bundles.Add(new ScriptBundle("~/bundles/handlebars").Include(
                    "~/Scripts/handlebars-v4.0.11.js"));

            bundles.Add(new ScriptBundle("~/bundles/material").Include(
                    "~/Scripts/material.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/drive").Include(
                        "~/Scripts/drive-*"));

            bundles.Add(new ScriptBundle("~/bundles/uglipop").Include(
                        "~/Scripts/uglipop.js"));

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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/material.min.css",
                      "~/Content/dialog-polyfill.css",
                      "~/Content/site.css"));
        }
    }
}
