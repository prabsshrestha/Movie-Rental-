using System.Web;
using System.Web.Optimization;
using System.Web.Hosting;
using Prabesh.Extensions;

namespace Prabesh
{
    public static class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public class LastModifiedBundleTransform : IBundleTransform
        {
            public void Process(BundleContext context, BundleResponse response)
            {
                foreach (var file in response.Files)
                {
                    file.IncludedVirtualPath = string.Concat(file.IncludedVirtualPath, "?build=", HtmlHelperExtensions.DeploymentVersion());
                }
            }
        }
        public static Bundle WithLastVersion(this Bundle sb)
        {
            sb.Transforms.Add(new LastModifiedBundleTransform());
            return sb;
        }
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/datatables/jquery.datatables.js",
                        "~/Scripts/datatables/datatables.bootstrap.js",
                        "~/Scripts/typeahead.bundle.js",
                        "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
              "~/Scripts/Kendo/js/kendo.all.min.js",
              "~/Scripts/Kendo/js/kendo.aspnetmvc.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-lumen.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/datatables/css/datatables.bootstrap.css",
                      "~/Content/typeahead.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Kendo/css").Include(
                       "~/Content/Kendo/Kendo.common.min.css",
                       //"~/Content/Kendo/kendo.metro.min.css",
                       "~/Content/Kendo/kendo.silver.min.css",
                       "~/Content/Kendo/kendo.rtl.min.css",
                       "~/Content/Kendo/Kendo.dataviz.min.css",
                       //"~/Content/Kendo/kendo.dataviz.metro.min.css",
                       "~/Content/Kendo/kendo.dataviz.default.min.css").WithLastVersion());
        }
    }
}
