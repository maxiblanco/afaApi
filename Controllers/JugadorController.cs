using AfaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AfaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadorController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllClubs()
        {
            string sql = "SELECT * from jugador";
            List<Jugador> jugadorList = new List<Jugador>();

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            Jugador jugador = new Jugador();
                            jugador.id = dr.GetFieldValue<int>(dr.GetOrdinal("id"));
                            jugador.clubId = dr.GetFieldValue<int>(dr.GetOrdinal("clubId"));
                            jugador.nombre = dr.GetFieldValue<string>(dr.GetOrdinal("nombre"));
                            jugador.numeroInscripcion = dr.GetFieldValue<int>(dr.GetOrdinal("numeroInscripcion"));
                            jugador.posicion = dr.GetFieldValue<int>(dr.GetOrdinal("posicion"));
                            jugadorList.Add(jugador);
                        }

                    }
                    return new OkObjectResult(jugadorList);
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
            string sql = $"SELECT * from jugador WHERE id= {id}";
            Jugador jugador = new Jugador();

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {

                            jugador.id = dr.GetFieldValue<int>(dr.GetOrdinal("id"));
                            jugador.clubId = dr.GetFieldValue<int>(dr.GetOrdinal("clubId"));
                            jugador.nombre = dr.GetFieldValue<string>(dr.GetOrdinal("nombre"));
                            jugador.numeroInscripcion = dr.GetFieldValue<int>(dr.GetOrdinal("numeroInscripcion"));
                            jugador.posicion = dr.GetFieldValue<int>(dr.GetOrdinal("posicion"));

                        }

                    }
                    return new OkObjectResult(jugador);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new NotFoundResult();
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post(Jugador jugador)
        {
            string sql = $"INSERT INTO clubs (id, clubId, nombre, numeroInscripcion, posicion)";
            sql += "VALUES(@id, @clubId, @nombre, @numeroInscripcion, @posicion)";

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

                                cmd.Parameters.Add(new SqlParameter("@id", jugador.id));
                                cmd.Parameters.Add(new SqlParameter("@nombre", jugador.clubId));
                                cmd.Parameters.Add(new SqlParameter("@ciudad", jugador.nombre));
                                cmd.Parameters.Add(new SqlParameter("@provincia", jugador.numeroInscripcion));
                                cmd.Parameters.Add(new SqlParameter("@fundacion", jugador.posicion));

                                var rowsAffected = cmd.ExecuteNonQuery();
                                trn.Commit();
                                return new OkObjectResult(jugador);
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
        public IActionResult Update(Jugador jugador, int id)
        {
            string sql = $"UPDATE jugador SET clubId = {jugador.clubId}, nombre = {jugador.nombre}, numeroInscripcion = {jugador.numeroInscripcion}, posicion = {jugador.posicion}";

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.ExecuteNonQuery();
                        return new OkObjectResult(jugador);
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
            string sql = $"DELETE * FROM jugador WHERE id = {id} ";
            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
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
