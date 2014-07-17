using System.Web.Optimization;

namespace ProEvo45.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include("~/css/ProEvo.css"));

            // Code removed for clarity.
            BundleTable.EnableOptimizations = true;
        }
    }
}