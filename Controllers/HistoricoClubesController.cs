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
    public class HistoricoClubesController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHistoricoList()
        {
            string sql = "SELECT * from historicoClubes";
            List<HistoricoClubes> historicoList = new List<HistoricoClubes>();

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            HistoricoClubes historicoClubes = new HistoricoClubes();
                            historicoClubes.id = dr.GetFieldValue<int>(dr.GetOrdinal("id"));
                            historicoClubes.clubId = dr.GetFieldValue<int>(dr.GetOrdinal("clubId"));
                            historicoClubes.jugadorId = dr.GetFieldValue<int>(dr.GetOrdinal("jugadorId"));
                            historicoClubes.fechaIngreso = dr.GetFieldValue<DateTime>(dr.GetOrdinal("fechaIngreso"));
                            historicoClubes.fechaEgreso = dr.GetFieldValue<DateTime>(dr.GetOrdinal("fechaEgreso"));
                            historicoList.Add(historicoClubes);
                        }

                    }
                    return new OkObjectResult(historicoList);
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
            string sql = $"SELECT * from historicoClubes WHERE id = {id}";
            HistoricoClubes historicoClubes = new HistoricoClubes();

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            historicoClubes.id = dr.GetFieldValue<int>(dr.GetOrdinal("id"));
                            historicoClubes.clubId = dr.GetFieldValue<int>(dr.GetOrdinal("clubId"));
                            historicoClubes.jugadorId = dr.GetFieldValue<int>(dr.GetOrdinal("jugadorId"));
                            historicoClubes.fechaIngreso = dr.GetFieldValue<DateTime>(dr.GetOrdinal("fechaIngreso"));
                            historicoClubes.fechaEgreso = dr.GetFieldValue<DateTime>(dr.GetOrdinal("fechaEgreso"));

                        }

                    }
                    return new OkObjectResult(historicoClubes);
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
        public IActionResult Post(HistoricoClubes historicoClubes)
        {
            string sql = $"INSERT INTO historicoClubes (id, clubId, jugadorId, fechaIngreso, fechaEgreso)";
            sql += "VALUES(@id, @clubId, @jugadorId, @fechaIngreso, @fechaEgreso)";

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

                                cmd.Parameters.Add(new SqlParameter("@id", historicoClubes.id));
                                cmd.Parameters.Add(new SqlParameter("@nombre", historicoClubes.clubId));
                                cmd.Parameters.Add(new SqlParameter("@ciudad", historicoClubes.jugadorId));
                                cmd.Parameters.Add(new SqlParameter("@provincia", historicoClubes.fechaIngreso));
                                cmd.Parameters.Add(new SqlParameter("@fundacion", historicoClubes.fechaEgreso));

                                var rowsAffected = cmd.ExecuteNonQuery();
                                trn.Commit();
                                return new OkObjectResult(historicoClubes);
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
        public IActionResult Update(HistoricoClubes historicoClubes, int id)
        {
            string sql = $"UPDATE historicoClubes SET clubId = {historicoClubes.clubId}, jugadorId = {historicoClubes.jugadorId}, fechaIngreso = {historicoClubes.fechaIngreso}, fechaEgreso = {historicoClubes.fechaEgreso}";

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.ExecuteNonQuery();
                        return new OkObjectResult(historicoClubes);
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
            string sql = $"DELETE * FROM historicoClubes WHERE id = {id} ";
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
