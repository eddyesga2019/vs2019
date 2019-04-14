using App.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    public class ArtistDA : BaseConnection
    {
        /// <summary>
        /// permite obtener la cantidad de registros
        /// que existen en la tabla artista
        /// </summary>
        /// <returns>Retorna el numero de registros</returns>
        public int GetCount()
        {
            var result = 0;
            var sql = "SELECT COUNT(1) FROM Artist";
            /*1.- Creando la instancia del objeto connection*/
            using (IDbConnection
                cn = new SqlConnection(base.ConnectionString))
            {
                /*2.- Creando el objeto command*/
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
                cn.Open(); //Abriendo la conexxion a la base de datos

                result = (int)cmd.ExecuteScalar();

            }

            return result;
        }
          
            public List<Artist> GetAll(String FilterByName="")
            {
                var result = new List<Artist>();
                var sql = "Select * from artist where name LIKE @paramFilterByName";

                using (IDbConnection cn = new SqlConnection
                    (this.ConnectionString))
                {
                    IDbCommand cmd = new SqlCommand(sql);
                    cmd.Connection = cn;
                    cn.Open();

                    FilterByName = $"%{FilterByName}%";

                    cmd.Parameters.Add(
                    new SqlParameter("@paramFilterByName",FilterByName));  

                    var reader = cmd.ExecuteReader();
                    var indice = 0;

                    while (reader.Read())
                    {
                        var artist = new Artist();
                        indice = reader.GetOrdinal("ArtistId");
                        artist.ArtistId = reader.GetInt32(indice);

                        indice = reader.GetOrdinal("Name");
                        artist.Name = reader.GetString(indice);

                        result.Add(artist);
                    }


                }

                return result;
            }
        public Artist Get(int id)
        {
            var result = new Artist();
            var sql = "select * from Artist where ArtistId =@paramID";

            using (IDbConnection cn = new SqlConnection
                    (this.ConnectionString))
            {
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
                cn.Open();

                /*configurando los parametros */
                cmd.Parameters.Add(

                    new SqlParameter("@paramID", id)  


                    );

                var reader = cmd.ExecuteReader();
                var indice = 0;

                while (reader.Read())
                {
                  
                    indice = reader.GetOrdinal("ArtistId");
                    result.ArtistId = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("Name");
                    result.Name = reader.GetString(indice);

                }


            }

            return result;
        }
        public List<Artist> GetAllSP(String FilterByName = "")
        {
            var result = new List<Artist>();
            var sql = "ups_GetAll";

            using (IDbConnection cn = new SqlConnection
                (this.ConnectionString))
            {
                IDbCommand cmd = new SqlCommand(sql);
                /*ahora que ahora es un  procedimiento de almacenado*/
                cmd.CommandType = CommandType.StoredProcedure;      
                cmd.Connection = cn;
                cn.Open();

                FilterByName = $"%{FilterByName}%";

                cmd.Parameters.Add(
                new SqlParameter("@FilterByName", FilterByName));

                var reader = cmd.ExecuteReader();
                var indice = 0;

                while (reader.Read())
                {
                    var artist = new Artist();
                    indice = reader.GetOrdinal("ArtistId");
                    artist.ArtistId = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("Name");
                    artist.Name = reader.GetString(indice);

                    result.Add(artist);
                }


            }

            return result;
        }


    }
    
    
}
