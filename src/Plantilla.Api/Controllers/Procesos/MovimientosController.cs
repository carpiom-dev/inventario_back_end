using System.Net;
using Plantilla.Infraestructura.Constantes;
using Plantilla.Infraestructura.Utilidades.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plantilla.Negocio.Procesos.Movimientos;
using Plantilla.Dto.Modelo.Procesos.Movimientos;

namespace Plantilla.Api.Controllers.Procesos
{
    [ApiController]
    [Route("api/v1")]
    public class MovimientosController(INeMovimientos neMovimientos) : ControllerBase
    {
        //[Authorize]
        [HttpPost("agregar-stock")]
        public async Task<IActionResult> CrearProducto(MovimientoDto.CrearMovimiento crear)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neMovimientos.CargaInicialInventario(crear);

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

        //[Authorize]
        [HttpPost("obtener-kardex")]
        public async Task<IActionResult> GenerarKardex(KardexDto.ConsultarKardex consultar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neMovimientos.GenerarKardex(consultar);

                return respuesta.Respuesta.ExisteExcepcion
                    ? StatusCode((int)HttpStatusCode.InternalServerError, respuesta)
                    : Ok(respuesta);
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return StatusCode((int)HttpStatusCode.InternalServerError, MensajeControladorConstante.HaOcurridoError);
            }
        }

        //[Authorize]
        [HttpPost("obtener-kardex-valorizado")]
        public async Task<IActionResult> GenerarKardexValorizado(KardexValorizadoDto.ConsultarKardexValorizado consultar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var respuesta = await neMovimientos.GenerarKardexValorizado(consultar);

                return respuesta.Respuesta.ExisteExcepcion
                    ? StatusCode((int)HttpStatusCode.InternalServerError, respuesta)
                    : Ok(respuesta);
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex, consultar);
                return StatusCode((int)HttpStatusCode.InternalServerError, MensajeControladorConstante.HaOcurridoError);
            }
        }
    }
}