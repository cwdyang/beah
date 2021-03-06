﻿using System.Web;
using System.Web.Optimization;

namespace beah.WebAPI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));


            //DAVIDY JQUERY
            bundles.Add(new StyleBundle("~/Content/jquerycss").Include("~/Content/jquery.*"));
            bundles.Add(new ScriptBundle("~/bundles/jquerymobile").Include(

               
                "~/Scripts/jquery-1.8.2.intellisense.js",
"~/Scripts/jquery-1.8.2.js",
"~/Scripts/jquery-1.8.2.min.js",
"~/Scripts/jquery-ui-1.8.24.js",
"~/Scripts/jquery-ui-1.8.24.min.js",
"~/Scripts/jquery.mobile-1.4.5.js",
"~/Scripts/jquery.mobile-1.4.5.min.js",

"~/Scripts/jquery.unobtrusive-ajax.js",
"~/Scripts/jquery.unobtrusive-ajax.min.js",
"~/Scripts/jquery.validate-vsdoc.js",
"~/Scripts/jquery.validate.js",
"~/Scripts/jquery.validate.min.js",
"~/Scripts/jquery.validate.unobtrusive.js",
"~/Scripts/jquery.validate.unobtrusive.min.js",
"~/Scripts/knockout-2.2.0.debug.js",
"~/Scripts/knockout-2.2.0.js",
"~/Scripts/modernizr-2.6.2.js",
"~/Scripts/_references.js",

 "~/Scripts/jquery.auto-grow-input.js",
                "~/Scripts/jquery.auto-grow-input.min.js"));


            ///DAVIDY GOOGLE PLACES
            bundles.UseCdn = true;

            //DAVIDY BEAH
            bundles.Add(new StyleBundle("~/Content/themes/beah/css").Include(
                 "~/Content/themes/beah/jquery.mobile.icons.min.css",
                 "~/Content/themes/beah/beah.min.css",
                "~/Content/themes/beah/beah.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}