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
    public class HistoricoGolesController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHistoricoList()
        {
            string sql = "SELECT * from historicoGoles";
            List<HistoricoGoles> historicoGolesList = new List<HistoricoGoles>();

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            HistoricoGoles historicoGoles = new HistoricoGoles();
                            historicoGoles.id = dr.GetFieldValue<int>(dr.GetOrdinal("id"));
                            historicoGoles.jugadorId = dr.GetFieldValue<int>(dr.GetOrdinal("jugadorId"));
                            historicoGoles.cantGoles = dr.GetFieldValue<int>(dr.GetOrdinal("cantGoles"));
                            historicoGolesList.Add(historicoGoles);
                        }

                    }
                    return new OkObjectResult(historicoGolesList);
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
        public IActionResult GetHistoric(int id)
        {
            string sql = $"SELECT * from historicoGoles WHERE id = {id}";
            HistoricoGoles historicoGoles = new HistoricoGoles();

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            historicoGoles.id = dr.GetFieldValue<int>(dr.GetOrdinal("id"));
                            historicoGoles.jugadorId = dr.GetFieldValue<int>(dr.GetOrdinal("jugadorId"));
                            historicoGoles.cantGoles = dr.GetFieldValue<int>(dr.GetOrdinal("cantGoles"));

                        }

                    }
                    return new OkObjectResult(historicoGoles);
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
        public IActionResult Post(HistoricoGoles historicoGoles)
        {
            string sql = $"INSERT INTO historicoGoles (id,  jugadorId, cantGoles)";
            sql += "VALUES(@id, @jugadorId, @cantGoles)";

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

                                cmd.Parameters.Add(new SqlParameter("@id", historicoGoles.id));
                                cmd.Parameters.Add(new SqlParameter("@nombre", historicoGoles.jugadorId));
                                cmd.Parameters.Add(new SqlParameter("@ciudad", historicoGoles.cantGoles));
                              

                                var rowsAffected = cmd.ExecuteNonQuery();
                                trn.Commit();
                                return new OkObjectResult(historicoGoles);
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
        public IActionResult Update(HistoricoGoles historicoGoles, int id)
        {
            string sql = $"UPDATE historicoGoles SET  jugadorId = {historicoGoles.jugadorId}, cantGoles = {historicoGoles.cantGoles}";

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.ExecuteNonQuery();
                        return new OkObjectResult(historicoGoles);
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
            string sql = $"DELETE * FROM historicoGoles WHERE id = {id} ";
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
