using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using OdataExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OdataExample.App_Start
{
    public static class webconfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute("Odata", "OdataApi", 
            GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
            config.Count().Filter().Select().Expand().OrderBy().MaxTop(null);
            config.EnsureInitialized();
        }

        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<BatchManagement>("Batches");

            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}