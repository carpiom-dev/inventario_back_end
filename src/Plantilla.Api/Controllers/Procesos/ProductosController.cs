using System.Net;
using Plantilla.Infraestructura.Constantes;
using Plantilla.Infraestructura.Utilidades.Logger;
using Plantilla.Negocio.Procesos.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Plantilla.Dto.Modelo.Procesos.Productos.ProductosDto;

namespace Plantilla.Api.Controllers.Procesos
{
    [ApiController]
    [Route("api/v1")]
    public class ProductosController(INeProductos neProductos) : ControllerBase
    {
        private readonly INeProductos neProductos = neProductos;

        [HttpPost("consultar-producto-id")]
        public async Task<IActionResult> ConsultarProductos(ConsultarProducto consultar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var consulta = await neProductos.ConsultarPorId(consultar);

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

        [HttpPost("consultar-productos")]
        public async Task<IActionResult> ConsultarProductoses()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var consulta = await neProductos.ConsultarTodos();

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

        [HttpPost("actualizar-producto")]
        public async Task<IActionResult> ActualizarProducto(ActualizarProducto actualizar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neProductos.Actualizar(actualizar);

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

        [HttpPost("eliminar-producto")]
        public async Task<IActionResult> EliminarProducto(EliminarProducto eliminar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neProductos.Eliminar(eliminar);

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

        [HttpPost("crear-producto")]
        public async Task<IActionResult> CrearProducto(CrearProducto crear)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neProductos.Crear(crear);

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