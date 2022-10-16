using AutoMapper;
using Gcm.Info.Cadastrais.Application.Interfaces;
using Gcm.Info.Cadastrais.Application.Models;
using Gcm.Info.Cadastrais.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gcm.Info.Cadastrais.Api.Controllers.v1
{
    /// <summary>
    /// Controller de categoriaProdutos
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriaProdutosController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaProdutoApplication _categoriaProdutoApplication;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="categoriaProdutoApplication"></param>
        public CategoriaProdutosController(IMapper mapper, ICategoriaProdutoApplication categoriaProdutoApplication)
        {
            _mapper = mapper;
            _categoriaProdutoApplication = categoriaProdutoApplication;
        }

        /// <summary>
        /// Retorna a lista de clientes cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoriaProdutoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodosCategoriaProdutoes(CancellationToken ctx)
        {
            var result = await _categoriaProdutoApplication.ListarTodos(ctx);

            if (result.Valid && result.Object.Any())
                return Ok(result.Object);

            return NoContent();
        }

        /// <summary>
        /// Obtem os dados do categoriaProduto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(CategoriaProdutoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterCategoriaProduto([FromRoute, Required] int codigo, CancellationToken ctx)
        {
            var result = await _categoriaProdutoApplication.ObterCategoriaProduto(codigo, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de uma categoriaProduto
        /// </summary>
        /// <param name="categoriaProdutoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CategoriaProduto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarCategoriaProduto(CategoriaProdutoModel categoriaProdutoModel, CancellationToken ctx)
        {
            var result = await _categoriaProdutoApplication.CadastrarCategoriaProduto(categoriaProdutoModel, ctx);

            if (result.Valid)
                return Created("/categoriaProdutoes", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza a atualização de uma categoriaProduto
        /// </summary>
        /// <param name="categoriaProdutoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(CategoriaProduto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarCategoriaProduto(CategoriaProdutoModel categoriaProdutoModel, CancellationToken ctx)
        {
            var result = await _categoriaProdutoApplication.AtualizarCategoriaProduto(categoriaProdutoModel, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Deleta uma categoriaProduto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpDelete("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarCategoriaProduto([FromRoute, Required] int codigo, CancellationToken ctx)
        {
            var result = await _categoriaProdutoApplication.DeletarCategoriaProduto(codigo, ctx);

            if (result.Valid)
                return Ok();

            return UnprocessableEntity(result.Notifications);
        }
    }
}
