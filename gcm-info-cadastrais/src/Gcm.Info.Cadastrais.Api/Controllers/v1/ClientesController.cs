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
    /// Controller de clientes
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientesController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IClienteApplication _clienteApplication;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="clienteApplication"></param>
        public ClientesController(IMapper mapper, IClienteApplication clienteApplication)
        {
            _mapper = mapper;
            _clienteApplication = clienteApplication;
        }

        /// <summary>
        /// Retorna a lista de clientes cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ClienteModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodosClientes(CancellationToken ctx)
        {
            var result = await _clienteApplication.ListarTodos(ctx);

            if (result.Valid && result.Object.Any())
                return Ok(result.Object);

            return NoContent();
        }

        /// <summary>
        /// Obtem os dados do cliente
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpGet("{cpf}")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterCliente([FromRoute, Required] string cpf, CancellationToken ctx)
        {
            var result = await _clienteApplication.ObterCliente(cpf, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza o cadastro de um cliente
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarCliente(ClienteModel clienteModel, CancellationToken ctx)
        {
            var result = await _clienteApplication.CadastrarCliente(clienteModel, ctx);

            if (result.Valid)
                return Created("/clientes", result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Realiza a atualização de um cliente
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarCliente(ClienteModel clienteModel, CancellationToken ctx)
        {
            var result = await _clienteApplication.AtualizarCliente(clienteModel, ctx);

            if (result.Valid)
                return Ok(result.Object);

            return UnprocessableEntity(result.Notifications);
        }

        /// <summary>
        /// Deleta um cliente
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        [HttpDelete("{cpf}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarCategoriaProduto([FromRoute, Required] string cpf, CancellationToken ctx)
        {
            var result = await _clienteApplication.DeletarCliente(cpf, ctx);

            if (result.Valid)
                return Ok();

            return UnprocessableEntity(result.Notifications);
        }
    }
}
