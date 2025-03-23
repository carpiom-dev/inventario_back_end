using System.Net;
using Plantilla.Infraestructura.Constantes;
using Plantilla.Infraestructura.Utilidades.Logger;
using Plantilla.Negocio.Procesos.Clientes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Plantilla.Dto.Modelo.Procesos.Clientes.ClientesDto;

namespace Plantilla.Api.Controllers.Procesos
{
    [ApiController]
    [Route("api/v1")]
    public class ClientesController(INeClientes neClientes) : ControllerBase
    {
        private readonly INeClientes neClientes = neClientes;

        //[Authorize]
        [HttpPost("consultar-cliente-id")]
        public async Task<IActionResult> ConsultarClientes(ConsultarCliente consultar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var consulta = await neClientes.ConsultarPorId(consultar);

                return consulta.Respuesta.ExisteExcepcion
                    ? StatusCode((int)HttpStatusCode.InternalServerError, consulta)
                    : Ok(consulta);
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return StatusCode((int)HttpStatusCode.InternalServerError, MensajeControladorConstante.HaOcurridoError);
            }
        }

        //[Authorize]
        [HttpPost("consultar-clientes")]
        public async Task<IActionResult> ConsultarClienteses(ConsultarClientes consultar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var consulta = await neClientes.ConsultarTodos(consultar);

                return consulta.Respuesta.ExisteExcepcion
                    ? StatusCode((int)HttpStatusCode.InternalServerError, consulta)
                    : Ok(consulta);
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, MensajeControladorConstante.HaOcurridoError);
            }
        }

        //[Authorize]
        [HttpPost("actualizar-cliente")]
        public async Task<IActionResult> ActualizarClientes(ActualizarCliente actualizar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neClientes.Actualizar(actualizar);

                return respuesta.ExisteExcepcion
                    ? StatusCode((int)HttpStatusCode.InternalServerError, respuesta)
                    : Ok(respuesta);
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, actualizar);
                return StatusCode((int)HttpStatusCode.InternalServerError, MensajeControladorConstante.HaOcurridoError);
            }
        }

        //[Authorize]
        [HttpPost("eliminar-cliente")]
        public async Task<IActionResult> EliminarClientes(EliminarCliente eliminar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neClientes.Eliminar(eliminar);

                return respuesta.ExisteExcepcion
                    ? StatusCode((int)HttpStatusCode.InternalServerError, respuesta)
                    : Ok(respuesta);
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, eliminar);
                return StatusCode((int)HttpStatusCode.InternalServerError, MensajeControladorConstante.HaOcurridoError);
            }
        }

        //[Authorize]
        [HttpPost("crear-cliente")]
        public async Task<IActionResult> CrearClientes(CrearCliente crear)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neClientes.Crear(crear);

                return respuesta.ExisteExcepcion
                    ? StatusCode((int)HttpStatusCode.InternalServerError, respuesta)
                    : Ok(respuesta);
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, crear);
                return StatusCode((int)HttpStatusCode.InternalServerError, MensajeControladorConstante.HaOcurridoError);
            }
        }
    }
}