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
    /// Controller de produtos
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdutoController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProdutoApplication _produtoApplication;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="produtoApplication"></param>
        public ProdutoController(IMapper mapper, IProdutoApplication produtoApplication)
        {
            _mapper = mapper;
            _produtoApplication = produtoApplication;
        }

        /// <summary>
        /// Retorna a lista de produtos cadastradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProdutoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodasProdutos(CancellationToken ctx)
        {
            var result = await _produtoApplication.ListarTodos(ctx);

            if (result.Valid && result.Object.Any())
                return Ok(result.Object);

            return NoContent();
        }

        /// <summary>
        /// Obtem os dados do produto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(ProdutoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterProduto([FromRoute, Required] int codigo, CancellationToken ctx)
        {
            var result = await _produtoApplication.ObterProduto(codigo, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de um produto
        /// </summary>
        /// <param name="produtoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarProduto(ProdutoModel produtoModel, CancellationToken ctx)
        {
            var result = await _produtoApplication.CadastrarProduto(produtoModel, ctx);

            if (result.Valid)
                return Created("/produtos", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza a atualização de um produto
        /// </summary>
        /// <param name="produtoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarProduto(ProdutoModel produtoModel, CancellationToken ctx)
        {
            var result = await _produtoApplication.AtualizarProduto(produtoModel, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Deleta um produto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpDelete("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarProduto([FromRoute, Required] int codigo, CancellationToken ctx)
        {
            var result = await _produtoApplication.DeletarProduto(codigo, ctx);

            if (result.Valid)
                return Ok();

            return UnprocessableEntity(result.Notifications);
        }
    }
}
