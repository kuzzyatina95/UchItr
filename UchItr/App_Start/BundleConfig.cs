using System.Web;
using System.Web.Optimization;

namespace UchItr
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var fontsGoogleCdn = "http://fonts.googleapis.com/css?family=Open+Sans:400,700,600,800";
            var edgeFontCdn = "//use.edgefonts.net/bebas-neue.js";

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                      "~/Scripts/jquery.easing.min.js",
                      "~/Scripts/scrolling-nav.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

           

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/ckeditor", edgeFontCdn).Include(
                      "~/Scripts/ckeditor/ckeditor.js"));

            bundles.Add(new StyleBundle("~/Content/css",
                      fontsGoogleCdn).Include(
                      "~/Content/bootstrap.css",
                      "~/Content/custom.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/icomoon-social.css",
                      "~/Content/main.css",
                      "~/Content/icomoon-social.css"));
            

        }
    }
}
