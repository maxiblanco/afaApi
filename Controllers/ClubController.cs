using AfaApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace AfaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController
    {

     [HttpGet]
     [ProducesResponseType(StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllClubs()
        {
            string sql = "SELECT * from clubs";
            List<Club> clubList = new List<Club>();

            try
            {
                using(SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        try
                        {
                            cnn.Open();
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                Club club = new Club();
                                club.id = dr.GetFieldValue<int>(dr.GetOrdinal("id"));
                                club.nombre = dr.GetFieldValue<string>(dr.GetOrdinal("nombre"));
                                club.ciudad = dr.GetFieldValue<string>(dr.GetOrdinal("ciudad"));
                                club.provincia = dr.GetFieldValue<string>(dr.GetOrdinal("provincia"));
                                club.fundacion = dr.GetFieldValue<DateTime>(dr.GetOrdinal("fundacion"));
                                clubList.Add(club);
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                       
                    }
                    return new OkObjectResult(clubList);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new NotFoundResult();
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClubs(int id)
        {
            string sql = $"SELECT * from clubs WHERE id= {id}";
            Club club = new Club();

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                         
                            club.id = dr.GetFieldValue<int>(dr.GetOrdinal("id"));
                            club.nombre = dr.GetFieldValue<string>(dr.GetOrdinal("nombre"));
                            club.ciudad = dr.GetFieldValue<string>(dr.GetOrdinal("ciudad"));
                            club.provincia = dr.GetFieldValue<string>(dr.GetOrdinal("provincia"));
                            club.fundacion = dr.GetFieldValue<DateTime>(dr.GetOrdinal("fundacion"));
                            
                        }

                    }
                    return new OkObjectResult(club);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new NotFoundResult();
            }

        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post(Club club)
        {
            string sql = $"INSERT INTO clubs (id, nombre, ciudad, provincia, fundacion)";
            sql += "VALUES(@id, @nombre, @ciudad, @provincia, @fundacion)";

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlTransaction trn = cnn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, cnn))
                            {
                                cmd.Transaction = trn;

                                cmd.Parameters.Add(new SqlParameter("@id", club.id));
                                cmd.Parameters.Add(new SqlParameter("@nombre", club.nombre));
                                cmd.Parameters.Add(new SqlParameter("@ciudad", club.ciudad));
                                cmd.Parameters.Add(new SqlParameter("@provincia", club.provincia));
                                cmd.Parameters.Add(new SqlParameter("@fundacion", club.fundacion));

                                var rowsAffected = cmd.ExecuteNonQuery();
                                trn.Commit();
                                return new OkObjectResult(club);
                            }
                        }
                        catch (Exception ex)
                        {
                            trn.Rollback();
                            Console.WriteLine(ex.StackTrace);
                            return new NotFoundResult();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new NotFoundResult();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Club club, int id)
        {
            string sql = $"UPDATE club SET nombre = {club.nombre}, ciudad = {club.ciudad}, provincia = {club.provincia}, fundacion = {club.fundacion}";

            try
            {
                using(SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using(SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.ExecuteNonQuery();
                        return new OkObjectResult(club);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return new NotFoundResult();
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Delete(int id)
        {
            string sql = $"DELETE * FROM club WHERE id = {id} ";
            try
            {
                using(SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.ExecuteNonQuery();
                        return new OkResult();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new NotFoundResult();
            }
            

        }
    }
}
