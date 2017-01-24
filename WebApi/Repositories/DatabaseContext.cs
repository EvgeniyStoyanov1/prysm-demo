using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Prysm.Monitoring.WebApi.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("PrysmDemoDatabase")
        {

        }
    }
}