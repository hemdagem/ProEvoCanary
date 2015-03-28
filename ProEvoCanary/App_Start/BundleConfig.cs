using System.Web.Optimization;

namespace ProEvoCanary.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include("~/content/css/normalize.css").Include("~/content/css/foundation.css"));
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/scripts/proevo.js"));

            // Code removed for clarity.
            BundleTable.EnableOptimizations = true;
        }
    }
}