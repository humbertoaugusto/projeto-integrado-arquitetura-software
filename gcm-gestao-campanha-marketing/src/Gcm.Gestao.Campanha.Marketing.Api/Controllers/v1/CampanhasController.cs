using AutoMapper;
using Gcm.Gestao.Campanha.Marketing.Application.Interfaces;
using Gcm.Gestao.Campanha.Marketing.Application.Models;
using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gcm.Gestao.Campanha.Marketing.Api.Controllers.v1
{
    /// <summary>
    /// Controller de campanhas
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CampanhasController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICampanhaApplication _campanhaApplication;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="campanhaApplication"></param>
        public CampanhasController(IMapper mapper, ICampanhaApplication campanhaApplication)
        {
            _mapper = mapper;
            _campanhaApplication = campanhaApplication;
        }

        /// <summary>
        /// Retorna a lista de campanhass cadastradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<AudienciaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodasCampanhas(CancellationToken ctx)
        {
            var result = await _campanhaApplication.ListarTodos(ctx);

            if (result.Valid && result.Object.Any())
                return Ok(result.Object);

            return NoContent();
        }

        /// <summary>
        /// Obtem os dados da campanha
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(AudienciaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterCampanha([FromRoute, Required] int codigo, CancellationToken ctx)
        {
            var result = await _campanhaApplication.ObterCampanha(codigo, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de uma campanha
        /// </summary>
        /// <param name="campanhaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Audiencia), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarCampanha(CampanhaModel campanhaModel, CancellationToken ctx)
        {
            var result = await _campanhaApplication.CadastrarCampanha(campanhaModel, ctx);

            if (result.Valid)
                return Created("/audiencias", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza a atualização de uma campanha
        /// </summary>
        /// <param name="campanhaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Audiencia), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarCampanha(CampanhaModel campanhaModel, CancellationToken ctx)
        {
            var result = await _campanhaApplication.AtualizarCampanha(campanhaModel, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Deleta uma campanha
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpDelete("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarCampanha([FromRoute, Required] int codigo, CancellationToken ctx)
        {
            var result = await _campanhaApplication.DeletarCampanha(codigo, ctx);

            if (result.Valid)
                return Ok();

            return UnprocessableEntity(result.Notifications);
        }
    }
}
