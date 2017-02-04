using ProyectoCapas.DataAccess.Base;
using ProyectoCapas.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapas.DataAccess
{
    public class DetalleDao: DaoBase
    {


        public void GuardarDetalle(Detalle detalle)
        {
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "insert into detalle(num_fact,cod_art,cant)values(@num_fact,@cod_art,@cant)";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@num_fact", detalle.num_fact);
                    cmd.Parameters.AddWithValue("@cod_art", detalle.cod_art);
                    cmd.Parameters.AddWithValue("@cant", detalle.cant);
                    this.cn.Open();
                    cmd.ExecuteNonQuery();
                    this.cn.Close();
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }
    }
}
