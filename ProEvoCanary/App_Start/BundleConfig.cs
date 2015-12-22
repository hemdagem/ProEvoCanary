using System.Web.Optimization;

namespace ProEvoCanary
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/scripts/proevo.js"));

            // Code removed for clarity.
            BundleTable.EnableOptimizations = true;
        }
    }
}