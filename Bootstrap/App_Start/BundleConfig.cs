﻿using System.Web;
using System.Web.Optimization;

namespace Bootstrap
{
    public class BundleConfig
    {
        //Bootstrap - Front-end toolkit for web applications (Uses HTML, CSS, Javascript - all open standards so it ties in to just about any framework), Flexable and Quick 
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
                      "~/Scripts/respond.js")); //Polyfill library (lib that tries to fill in 'holes' depending on the browser) use this because css media queries are not supported by all browsers, so we will use js queries

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/clanbattlesite.css",
                      "~/Content/site.css"));
        }
    }
}
