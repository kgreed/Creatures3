using System;
using System.Collections.Generic;
using System.Text;
using Creatures3.Module.BusinessObjects;
namespace Creatures3.Module
{
    public static class DataHelper
    {
        public static Creatures3DbContext MakeConnect()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            return new Creatures3DbContext(connectionString);
        }
    }
}
