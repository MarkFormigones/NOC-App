using System.Web;
using System.Web.Optimization;

namespace ProcessAdminApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
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



          
            bundles.Add(new StyleBundle("~/assets/css").Include(

                 //<!-- END PAGE LEVEL PLUGIN STYLES -->
                  "~/assets/plugins/font-awesome/css/font-awesome.min.css",
                   "~/assets/plugins/bootstrap/css/bootstrap.min.css",
               "~/assets/plugins/uniform/css/uniform.default.css",
                       
                       
                   "~/assets/plugins/gritter/css/jquery.gritter.css",
                   "~/assets/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                     "~/assets/plugins/fullcalendar/fullcalendar/fullcalendar.css",
                          
                        "~/assets/plugins/jquery-easy-pie-chart/jquery.easy-pie-chart.css",


//<!-- BEGIN GLOBAL MANDATORY STYLES -->
     
           "~/assets/css/style-responsive.css",
           "~/assets/css/plugins.css",
            "~/assets/css/pages/tasks.css",
         "~/assets/css/themes/light.css",
         "~/assets/css/themes/default.css",
           "~/assets/css/custom.css"

     ));

            bundles.Add(new StyleBundle("~/assets/css/themes").Include("~/assets/css/themes/default.css", "~/assets/css/themes/light.css", "~/assets/css/themes/grey.css","~/assets/css/themes/brown.css"));







            bundles.Add(new ScriptBundle("~/assets/plugins").Include(
             
                "~/assets/plugins/jquery-1.10.2.min.js",
            "~/assets/plugins/jquery-migrate-1.2.1.min.js",
                //<!-- IMPORTANT! Load jquery-ui-1.10.3.custom.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->

                "~/assets/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
                        "~/assets/plugins/bootstrap/js/bootstrap.min.js",
                        "~/assets/plugins/bootstrap-hover-dropdown/twitter-bootstrap-hover-dropdown.min.js",
                        "~/assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/assets/plugins/jquery.blockui.min.js",
                        "~/assets/plugins/jquery.cookie.min.js",
                        "~/assets/plugins/uniform/jquery.uniform.min.js",
                     //   <!-- END CORE PLUGINS -->

                      "~/assets/plugins/jqvmap/jqvmap/jquery.vmap.js",
                        "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js",
                        "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js",
                        "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js",
                        "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js",
                         "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js",
                          "~/assets/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js",
                        "~/assets/plugins/flot/jquery.flot.js",
                            "~/assets/plugins/flot/jquery.flot.resize.js",
                        "~/assets/plugins/jquery.pulsate.min.js",
                        "~/assets/plugins/bootstrap-daterangepicker/moment.min.js",

                          "~/assets/plugins/bootstrap-daterangepicker/daterangepicker.js",
                        "~/assets/plugins/gritter/js/jquery.gritter.js",

                        //<!-- IMPORTANT! fullcalendar depends on jquery-ui-1.10.3.custom.min.js for drag & drop support -->
                        

                        "~/assets/plugins/fullcalendar/fullcalendar/fullcalendar.min.js",
                        "~/assets/plugins/jquery-easy-pie-chart/jquery.easy-pie-chart.js",
                        "~/assets/plugins/jquery.sparkline.min.js" ,

                        "~/assets/plugins/jquery-validation/dist/jquery.validate.min.js"  //Validation changepassword

                      //  <!-- END PAGE LEVEL PLUGINS -->
                        ));




            bundles.Add(new ScriptBundle("~/assets/scripts").Include(

                "~/assets/plugins/jquery-1.10.2.min.js",
            "~/assets/plugins/jquery-migrate-1.2.1.min.js",
                //<!-- IMPORTANT! Load jquery-ui-1.10.3.custom.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->

                "~/assets/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
                        "~/assets/plugins/bootstrap/js/bootstrap.min.js",
                        "~/assets/plugins/bootstrap-hover-dropdown/twitter-bootstrap-hover-dropdown.min.js",
                        "~/assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/assets/plugins/jquery.blockui.min.js",
                        "~/assets/plugins/jquery.cookie.min.js",
                        "~/assets/plugins/uniform/jquery.uniform.min.js",

                         "~/assets/plugins/select2/select2.min.js",
                //   <!-- END CORE PLUGINS -->

                      "~/assets/plugins/jqvmap/jqvmap/jquery.vmap.js",
                        "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js",
                        "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js",
                        "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js",
                        "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js",
                         "~/assets/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js",
                          "~/assets/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js",
                        "~/assets/plugins/flot/jquery.flot.js",
                            "~/assets/plugins/flot/jquery.flot.resize.js",
                        "~/assets/plugins/jquery.pulsate.min.js",
                        "~/assets/plugins/bootstrap-daterangepicker/moment.min.js",

                          "~/assets/plugins/bootstrap-daterangepicker/daterangepicker.js",
                        "~/assets/plugins/gritter/js/jquery.gritter.js",

                        //<!-- IMPORTANT! fullcalendar depends on jquery-ui-1.10.3.custom.min.js for drag & drop support -->


                        "~/assets/plugins/fullcalendar/fullcalendar/fullcalendar.min.js",
                        "~/assets/plugins/jquery-easy-pie-chart/jquery.easy-pie-chart.js",
                        "~/assets/plugins/jquery.sparkline.min.js",

                       
                    


                // <!-- BEGIN PAGE LEVEL SCRIPTS -->

                  "~/assets/scripts/app.js",
                

                    "~/assets/scripts/index.js",
                      "~/assets/scripts/tasks.js"
                       //"~/assets/scripts/startupscript.js"
                //<!-- END PAGE LEVEL SCRIPTS -->  
                ));







        }
    }
}