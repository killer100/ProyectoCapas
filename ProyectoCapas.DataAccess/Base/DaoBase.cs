using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace ProyectoCapas.DataAccess.Base
{
    public class DaoBase
    {
        protected SqlConnection cn;
        protected SqlCommand cmd;
        protected SqlDataReader dr;
        public string GetConnectionString() {
            return ConfigurationManager.ConnectionStrings["capas"].ConnectionString;
        }
    }
}
