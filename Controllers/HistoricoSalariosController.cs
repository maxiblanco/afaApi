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
    public class HistoricoSalariosController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHistoricoList()
        {
            string sql = "SELECT * from historicoSalarios";
            List<HistoricoSalarios> HistoricoSalariosList = new List<HistoricoSalarios>();

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            HistoricoSalarios historicoSalarios = new HistoricoSalarios();
                            historicoSalarios.id = dr.GetFieldValue<int>(dr.GetOrdinal("id"));
                            historicoSalarios.clubId = dr.GetFieldValue<int>(dr.GetOrdinal("clubId"));
                            historicoSalarios.jugadorId = dr.GetFieldValue<int>(dr.GetOrdinal("jugadorId"));
                            historicoSalarios.año = dr.GetFieldValue<int>(dr.GetOrdinal("año"));
                            historicoSalarios.salarioPrm = dr.GetFieldValue<double>(dr.GetOrdinal("salarioPrm"));
                            HistoricoSalariosList.Add(historicoSalarios);
                        }

                    }
                    return new OkObjectResult(HistoricoSalariosList);
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
            string sql = $"SELECT * from historicoSalarios WHERE id = {id}";
            HistoricoSalarios historicoSalarios = new HistoricoSalarios();

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            historicoSalarios.id = dr.GetFieldValue<int>(dr.GetOrdinal("id"));
                            historicoSalarios.clubId = dr.GetFieldValue<int>(dr.GetOrdinal("clubId"));
                            historicoSalarios.jugadorId = dr.GetFieldValue<int>(dr.GetOrdinal("jugadorId"));
                            historicoSalarios.año = dr.GetFieldValue<int>(dr.GetOrdinal("año"));
                            historicoSalarios.salarioPrm = dr.GetFieldValue<double>(dr.GetOrdinal("salarioPrm"));

                        }

                    }
                    return new OkObjectResult(historicoSalarios);
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
        public IActionResult Post(HistoricoSalarios historicoSalarios)
        {
            string sql = $"INSERT INTO historicoSalarios (id, clubId, jugadorId, año, salarioPrm)";
            sql += "VALUES(@id, @clubId, @jugadorId, @año, @salarioPrm)";

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

                                cmd.Parameters.Add(new SqlParameter("@id", historicoSalarios.id));
                                cmd.Parameters.Add(new SqlParameter("@nombre", historicoSalarios.clubId));
                                cmd.Parameters.Add(new SqlParameter("@ciudad", historicoSalarios.jugadorId));
                                cmd.Parameters.Add(new SqlParameter("@provincia", historicoSalarios.año));
                                cmd.Parameters.Add(new SqlParameter("@fundacion", historicoSalarios.salarioPrm));

                                var rowsAffected = cmd.ExecuteNonQuery();
                                trn.Commit();
                                return new OkObjectResult(historicoSalarios);
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
        public IActionResult Update(HistoricoSalarios historicoSalarios, int id)
        {
            string sql = $"UPDATE historicoSalarios SET clubId = {historicoSalarios.clubId}, jugadorId = {historicoSalarios.jugadorId}, año = {historicoSalarios.año}, salarioPrm = {historicoSalarios.salarioPrm}";

            try
            {
                using (SqlConnection cnn = new SqlConnection(AfaDB.cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.ExecuteNonQuery();
                        return new OkObjectResult(historicoSalarios);
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
            string sql = $"DELETE * FROM historicoSalarios  WHERE id = {id} ";
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
