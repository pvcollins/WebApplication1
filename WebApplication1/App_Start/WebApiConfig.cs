//using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Web.Http;
using WebApplication1.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

         // New code:
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Accounts>("Accounts");
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());

        // Web API routes
//        config.MapHttpAttributeRoutes();
//            config.Routes.MapHttpRoute(
//                name: "DefaultApi",
//                routeTemplate: "api/{controller}/{id}",
//                defaults: new { id = RouteParameter.Optional }
//            );
        }
    }
}
