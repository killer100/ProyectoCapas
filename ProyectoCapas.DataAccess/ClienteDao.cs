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
    public class ClienteDao : DaoBase
    {
        public IList<Cliente> ListarClientes(int page, int pageSize, string filter = null)
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    string where = "";
                    if (!string.IsNullOrEmpty(filter))
                        where = String.Format("where cod_clie + ' '+ mon_ape like '%{0}%'", filter);

                    var query = string.Format(@"DECLARE @PageNumber AS INT, @RowspPage AS INT 
                                SET @PageNumber = {0} 
                                SET @RowspPage = {1} 
                                SELECT * FROM (
                                             SELECT ROW_NUMBER() OVER(ORDER BY cod_clie) AS NUMBER, * FROM cliente {2}
                                              ) AS TBL
                                WHERE NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND (@PageNumber * @RowspPage)
                                ORDER BY cod_clie", page, pageSize, where);
                    this.cmd = new SqlCommand(query, cn);
                    this.cn.Open();
                    this.dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var cliente = new Cliente()
                        {
                            cod_clie = dr[1].ToString(),
                            mon_ape = dr[2].ToString(),
                            telef = dr[3].ToString(),
                            dni = dr[4].ToString(),
                            dir = dr[5].ToString()
                        };
                        lista.Add(cliente);
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

        public int ContarClientes()
        {
            var count = 0;
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "select count(1) from cliente";
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

        public Cliente ListarCliente(string cod_clie)
        {
            Cliente cliente = null;
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "select * from cliente where cod_clie = @cod_clie";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@cod_clie", cod_clie);
                    this.cn.Open();
                    this.dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cliente = new Cliente()
                        {
                            cod_clie = dr[0].ToString(),
                            mon_ape = dr[1].ToString(),
                            telef = dr[2].ToString(),
                            dni = dr[3].ToString(),
                            dir = dr[4].ToString(),
                        };
                    }
                    this.cn.Close();
                }

                return cliente;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public void ActualizarCliente(string cod_clie, Cliente cliente)
        {
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "update cliente set mon_ape = @mon_ape, telef = @telef, dni=@dni, dir=@dir where cod_clie = @cod_clie";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@mon_ape", cliente.mon_ape);
                    cmd.Parameters.AddWithValue("@telef", cliente.telef);
                    cmd.Parameters.AddWithValue("@dni", cliente.dni);
                    cmd.Parameters.AddWithValue("@dir", cliente.dir);
                    cmd.Parameters.AddWithValue("@cod_clie", cod_clie);
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

        public void EliminarCliente(string cod_clie)
        {
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "delete from cliente where cod_clie = @cod_clie";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@cod_clie", cod_clie);
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
        public void GuardarCliente(Cliente cliente)
        {
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "insert into cliente(cod_clie,mon_ape,telef,dni,dir) values(@cod_clie,@mon_ape,@telef,@dni,@dir)";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@cod_clie", cliente.cod_clie);
                    cmd.Parameters.AddWithValue("@mon_ape", cliente.mon_ape);
                    cmd.Parameters.AddWithValue("@telef", cliente.telef);
                    cmd.Parameters.AddWithValue("@dni", cliente.dni);
                    cmd.Parameters.AddWithValue("@dir", cliente.dir);
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
