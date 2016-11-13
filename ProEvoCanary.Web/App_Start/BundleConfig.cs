using System.Web.Optimization;

namespace ProEvoCanary.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/scripts/proevo.js"));

            bundles.Add(new StyleBundle("~/bundles/styles").Include("~/css/main.css"));

            // Code removed for clarity.
            BundleTable.EnableOptimizations = true;
        }
    }
}