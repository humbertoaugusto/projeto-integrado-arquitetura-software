using AutoMapper;
using Gcm.Gestao.Campanha.Marketing.Application.Interfaces;
using Gcm.Gestao.Campanha.Marketing.Application.Models;
using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gcm.Gestao.Campanha.Marketing.Api.Controllers.v1
{
    /// <summary>
    /// Controller de estoques
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AudiecnciaController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IAudienciaApplication _audienciaApplication;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="audienciaApplication"></param>
        public AudiecnciaController(IMapper mapper, IAudienciaApplication audienciaApplication)
        {
            _mapper = mapper;
            _audienciaApplication = audienciaApplication;
        }

        /// <summary>
        /// Retorna a lista de estoques cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<AudienciaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodosEstoques(CancellationToken ctx)
        {
            var result = await _audienciaApplication.ListarTodos(ctx);

            if (result.Valid && result.Object.Any())
                return Ok(result.Object);

            return NoContent();
        }

        /// <summary>
        /// Obtem os dados do audiencia
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(AudienciaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterAudiencia([FromRoute, Required] int codigo, CancellationToken ctx)
        {
            var result = await _audienciaApplication.ObterAudiencia(codigo, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de uma audiencia
        /// </summary>
        /// <param name="audienciaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Audiencia), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarAudiencia(AudienciaModel audienciaModel, CancellationToken ctx)
        {
            var result = await _audienciaApplication.CadastrarAudiencia(audienciaModel, ctx);

            if (result.Valid)
                return Created("/audiencias", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza a atualização de um audiencia
        /// </summary>
        /// <param name="audienciaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Audiencia), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarAudiencia(AudienciaModel audienciaModel, CancellationToken ctx)
        {
            var result = await _audienciaApplication.AtualizarAudiencia(audienciaModel, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Deleta uma audiencia
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpDelete("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarAudiencia([FromRoute, Required] int codigo, CancellationToken ctx)
        {
            var result = await _audienciaApplication.DeletarAudiencia(codigo, ctx);

            if (result.Valid)
                return Ok();

            return UnprocessableEntity(result.Notifications);
        }
    }
}
