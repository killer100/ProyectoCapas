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
    public class FacturaDao : DaoBase
    {

        public IList<vw_facturas> ListarFacturas(int page, int pageSize)
        {
            List<vw_facturas> lista = new List<vw_facturas>();
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = string.Format(@"DECLARE @PageNumber AS INT, @RowspPage AS INT 
                                SET @PageNumber = {0} 
                                SET @RowspPage = {1} 
                                SELECT * FROM (
                                             SELECT ROW_NUMBER() OVER(ORDER BY num_fact) AS NUMBER, * FROM vw_facturas
                                              ) AS TBL
                                WHERE NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND (@PageNumber * @RowspPage)
                                ORDER BY num_fact", page, pageSize);
                    this.cmd = new SqlCommand(query, cn);
                    this.cn.Open();
                    this.dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var Factura = new vw_facturas()
                        {
                            num_fact_format = dr[1].ToString(),
                            num_fact = Convert.ToDecimal(dr[2]),
                            cod_clie = dr[3].ToString(),
                            mon_ape = dr[4].ToString(),
                            fech_vent = Convert.ToDateTime(dr[5])
                        };
                        lista.Add(Factura);
                    }
                    this.cn.Close();
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public int ContarFacturas()
        {
            var count = 0;
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "select count(1) from vw_facturas";
                    this.cmd = new SqlCommand(query, cn);
                    this.cn.Open();
                    this.dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        count = Convert.ToInt32(dr[0]);
                    }
                    this.cn.Close();
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }

            return count;
        }

        public vw_facturas GuardarFactura(Factura factura)
        {
            vw_facturas vw_factura = null;
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = @"declare @num_fact numeric = (select max(isnull(num_fact,0))+1 from factura);
                                  insert into factura(num_fact,cod_clie,fech_vent)values(@num_fact,@cod_clie,@fech_vent);
                                  select * from vw_facturas where num_fact = @num_fact;";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@cod_clie", factura.cod_clie);
                    cmd.Parameters.AddWithValue("@fech_vent", DateTime.Now);
                    this.cn.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        vw_factura = new vw_facturas()
                        {
                            num_fact_format = dr[0].ToString(),
                            num_fact = Convert.ToDecimal(dr[1]),
                            cod_clie = dr[2].ToString(),
                            mon_ape = dr[3].ToString(),
                            fech_vent = Convert.ToDateTime(dr[4])
                        };
                    }
                    this.cn.Close();
                    return vw_factura;
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public vw_facturas ListarFactura(decimal num_fact)
        {
            vw_facturas factura = null;
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "select * from vw_facturas where num_fact = @num_fact";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@num_fact", num_fact);
                    this.cn.Open();
                    this.dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        factura = new vw_facturas()
                        {
                            num_fact_format = dr[0].ToString(),
                            num_fact = Convert.ToDecimal(dr[1]),
                            cod_clie = dr[2].ToString(),
                            mon_ape = dr[3].ToString(),
                            fech_vent = Convert.ToDateTime(dr[4])
                        };
                    }
                    this.cn.Close();
                }

                return factura;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public IList<vw_detalle> ListarDetallePorFactura(decimal num_fact)
        {
            List<vw_detalle> lista = new List<vw_detalle>();
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "select * from vw_detalle where num_fact = @num_fact";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@num_fact", num_fact);
                    this.cn.Open();
                    this.dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var detalle = new vw_detalle()
                        {
                            num_fact = dr[0].ToString(),
                            cod_art = dr[1].ToString(),
                            cant = Convert.ToDecimal(dr[2].ToString()),
                            descrip = dr[3].ToString(),
                            prec_unic = Convert.ToDecimal(dr[4])
                        };
                        lista.Add(detalle);
                    }
                    this.cn.Close();
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }
    }
}
