using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAPI.Models;
using Microsoft.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/[controller]-de-credito")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        //Objeto de DBContext para operaciones CRUD
        public readonly DBAPIContext dbAPIContext;

        //Constructor
        public TarjetaController(DBAPIContext context)
        {
            dbAPIContext = context;
        }

        [HttpGet]
        [Route("Listado")]
        public IActionResult Lista([FromServices] IConfiguration configuration)
        {
            List<Tarjetacredito> lista = new List<Tarjetacredito>();

            try
            {
                using (var conexion = new SqlConnection(configuration.GetConnectionString("conexionSQL")))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("SP_LISTADO_TARJETA_CREDITO", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                lista.Add(new Tarjetacredito()
                                {
                                    IdTarjeta = Convert.ToInt32(rd["id_tarjeta"]),
                                    NumeroTarjeta = rd["numero_tarjeta"].ToString(),
                                    IdCliente = Convert.ToInt32(rd["id_cliente"]),
                                    Nombre = rd["nombre"].ToString(),
                                    Apellido = rd["apellido"].ToString()
                                });
                            }
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", Response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, Response = lista });
            }
        }

        [HttpPost]
        [Route("GuardarMovimiento/{idTarjeta:int}")]
        public IActionResult Guardar([FromBody] Movimiento objeto, [FromServices] IConfiguration configuration, int idTarjeta)
        {
            try
            {
                using (var conexion = new SqlConnection(configuration.GetConnectionString("conexionSQL")))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("SP_GUARDAR_MOVIMIENTO", conexion))
                    {
                        cmd.Parameters.AddWithValue("idTarjeta", idTarjeta);
                        cmd.Parameters.AddWithValue("fechaMovimiento", objeto.Fecha);
                        cmd.Parameters.AddWithValue("descripcion", objeto.Descripcion);
                        cmd.Parameters.AddWithValue("monto", objeto.Monto);
                        cmd.Parameters.AddWithValue("tipoMovimiento", objeto.Tipo);

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarMovimiento/{idMovimiento:int}")]
        public IActionResult Editar([FromBody] Movimiento objeto, [FromServices] IConfiguration configuration, int idMovimiento)
        {
            try
            {
                using (var conexion = new SqlConnection(configuration.GetConnectionString("conexionSQL")))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("SP_EDITAR_MOVIMIENTO", conexion))
                    {
                        cmd.Parameters.AddWithValue("idMovimiento", idMovimiento);
                        cmd.Parameters.AddWithValue("fechaMovimiento", objeto.Fecha is null ? DBNull.Value : objeto.Fecha);
                        cmd.Parameters.AddWithValue("descripcion", objeto.Descripcion is null ? DBNull.Value : objeto.Descripcion);
                        cmd.Parameters.AddWithValue("monto", objeto.Monto is null ? DBNull.Value : objeto.Monto);

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "EDIT" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarMovimiento/{idMovimiento:int}")]
        public IActionResult Eliminar([FromServices] IConfiguration configuration, int idMovimiento)
        {
            try
            {
                using (var conexion = new SqlConnection(configuration.GetConnectionString("conexionSQL")))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("SP_ELIMINAR_MOVIMIENTO", conexion))
                    {
                        cmd.Parameters.AddWithValue("idMovimiento", idMovimiento);

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();

                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "DELETE" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("EstadoCuenta")]
        public IActionResult Estado([FromServices] IConfiguration configuration)
        {
            List<Movimiento> lista = new List<Movimiento>();

            try
            {
                using (var conexion = new SqlConnection(configuration.GetConnectionString("conexionSQL")))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("SP_ESTADO_CUENTA", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                lista.Add(new Movimiento()
                                {
                                    IdMovimiento = Convert.ToInt32(rd["id_movimiento"]),
                                    IdTarjeta = Convert.ToInt32(rd["id_tarjeta"]),
                                    Fecha = Convert.ToDateTime(rd["fecha"]).Date,
                                    Descripcion = rd["descripcion"].ToString(),
                                    Monto = Convert.ToDecimal(rd["monto"]),
                                    Tipo = rd["tipo"].ToString()

                                });
                            }
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", Response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, Response = lista });
            }
        }
    }
}
