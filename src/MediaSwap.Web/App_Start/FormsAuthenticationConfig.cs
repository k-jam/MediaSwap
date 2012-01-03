using System.Web;
using MediaSwap.Web.App_Start;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using AspNetHaack;
[assembly: PreApplicationStartMethod(typeof(FormsAuthenticationConfig), "Register")]
namespace MediaSwap.Web.App_Start {
    public static class FormsAuthenticationConfig {
        public static void Register() {
            DynamicModuleUtility.RegisterModule(typeof(SuppressFormsAuthenticationRedirectModule));
        }
    }



}
