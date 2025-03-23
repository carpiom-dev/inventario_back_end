using System.Net;
using Plantilla.Infraestructura.Constantes;
using Plantilla.Infraestructura.Utilidades.Logger;
using Plantilla.Negocio.Procesos.Factura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Plantilla.Dto.Modelo.Procesos.Facturas.FacturaDto;

namespace Plantilla.Api.Controllers.Procesos
{
    [ApiController]
    [Route("api/v1")]
    public class FacturaController(INeFactura neFactura) : ControllerBase
    {
        private readonly INeFactura neFactura = neFactura;

        //[Authorize]
        [HttpPost("consultar-factura-id")]
        public async Task<IActionResult> ConsultarFactura(ConsultarFactura consultar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var consulta = await neFactura.ConsultarPorId(consultar);

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
        [HttpPost("consultar-facturas")]
        public async Task<IActionResult> ConsultarFacturaes()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var consulta = await neFactura.ConsultarTodos();

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
        [HttpPost("eliminar-factura")]
        public async Task<IActionResult> EliminarFactura(EliminarFactura eliminar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neFactura.Eliminar(eliminar);

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
        [HttpPost("crear-factura")]
        public async Task<IActionResult> CrearFactura(CrearFactura crear)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neFactura.Crear(crear);

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