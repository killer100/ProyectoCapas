using ProyectoCapas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ProyectoCapas.DataAccess.Base;
using System.Data.SqlClient;

namespace ProyectoCapas.DataAccess
{
    public class ArticuloDao : DaoBase
    {

        public IList<Articulo> ListarArticulos(int page, int pageSize, string filter = null)
        {
            List<Articulo> lista = new List<Articulo>();
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    string where = "";
                    if (!string.IsNullOrEmpty(filter))
                        where = String.Format("where cod_art + ' '+ descrip like '%{0}%'", filter);

                    var query = string.Format(@"DECLARE @PageNumber AS INT, @RowspPage AS INT 
                                SET @PageNumber = {0} 
                                SET @RowspPage = {1} 
                                SELECT * FROM (
                                             SELECT ROW_NUMBER() OVER(ORDER BY cod_art) AS NUMBER, * FROM articulo {2}
                                              ) AS TBL
                                WHERE NUMBER BETWEEN ((@PageNumber - 1) * @RowspPage + 1) AND (@PageNumber * @RowspPage)
                                ORDER BY cod_art", page, pageSize, where);
                    this.cmd = new SqlCommand(query, cn);
                    this.cn.Open();
                    this.dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var articulo = new Articulo()
                        {
                            cod_art = dr[1].ToString(),
                            descrip = dr[2].ToString(),
                            prec_unic = Convert.ToDecimal(dr[3]),
                            stock = Convert.ToDecimal(dr[4])
                        };
                        lista.Add(articulo);
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

        public int ContarArticulos()
        {
            var count = 0;
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "select count(1) from articulo";
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

        public Articulo ListarArticulo(string cod_art)
        {
            Articulo articulo = null;
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "select * from articulo where cod_art = @cod_art";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@cod_art", cod_art);
                    this.cn.Open();
                    this.dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        articulo = new Articulo()
                        {
                            cod_art = dr[0].ToString(),
                            descrip = dr[1].ToString(),
                            prec_unic = Convert.ToDecimal(dr[2]),
                            stock = Convert.ToDecimal(dr[3])
                        };
                    }
                    this.cn.Close();
                }

                return articulo;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + " " + e.InnerException);
            }
        }

        public void ActualizarArticulo(string cod_art, Articulo articulo)
        {
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "update articulo set prec_unic = @prec_unic, descrip = @descrip, stock=@stock where cod_art = @cod_art";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@cod_art", cod_art);
                    cmd.Parameters.AddWithValue("@prec_unic", articulo.prec_unic);
                    cmd.Parameters.AddWithValue("@descrip", articulo.descrip);
                    cmd.Parameters.AddWithValue("@stock", articulo.stock);
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

        public void EliminarArticulo(string cod_art)
        {
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "delete from articulo where cod_art = @cod_art";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@cod_art", cod_art);
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
        public void GuardarArticulo(Articulo articulo)
        {
            try
            {
                using (this.cn = new SqlConnection(this.GetConnectionString()))
                {
                    var query = "insert into articulo(cod_art,prec_unic,descrip,stock) values(@cod_art,@prec_unic,@descrip,@stock)";
                    this.cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@cod_art", articulo.cod_art);
                    cmd.Parameters.AddWithValue("@prec_unic", articulo.prec_unic);
                    cmd.Parameters.AddWithValue("@descrip", articulo.descrip);
                    cmd.Parameters.AddWithValue("@stock", articulo.stock);
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
