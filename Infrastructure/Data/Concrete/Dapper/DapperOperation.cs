using Infrastructure.Data.Abstract;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data.Concrete.Dapper
{
    public class DapperOperation<T> : IDapperOperation<T>
    {
        private string strconnection;

        public DapperOperation()
        {
            strconnection = "server=LAB202-OGRETMEN;database=SparkMarketDB;trusted_connection=true;TrustServerCertificate=true;";
        }
        public T SelectFirstOrDefault<T>(string sql, object parameters, out string hata)
        {
            T sonuc = default(T);
            hata = "";
            SqlConnection con = new SqlConnection(strconnection);
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                    sonuc = con.QueryFirstOrDefault<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                hata = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return sonuc;
        }

        public List<T> SelectList<T>(string sql, object parameters, out string hata)
        {

            List<T> sonuc = new List<T>();
            hata = "";
            SqlConnection con = new SqlConnection(strconnection);
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                    sonuc = con.Query<T>(sql, parameters).ToList();
                }
            }
            catch (Exception ex)
            {
                hata = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return sonuc;
        }

        public T SelectSingleOrDefault<T>(string sql, object parameters , out string hata)
        {

            T sonuc = default(T);
            hata = "";
            SqlConnection con = new SqlConnection(strconnection);
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                    sonuc = con.QuerySingleOrDefault<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                hata = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return sonuc;



        }

        public int SqlCommand(string sql, object parameters , out string hata)
        {

            hata = "";
            SqlConnection con = new SqlConnection(strconnection);
            int sonuc = 0;
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                    sonuc = con.Execute(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                hata = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return sonuc;




        }
    }
}
