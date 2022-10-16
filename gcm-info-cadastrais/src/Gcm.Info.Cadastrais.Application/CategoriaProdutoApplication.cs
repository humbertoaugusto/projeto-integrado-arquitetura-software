using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Gcm.Info.Cadastrais.Application.Interfaces;
using Gcm.Info.Cadastrais.Application.Models;
using Gcm.Info.Cadastrais.Domain.Entities;
using Gcm.Info.Cadastrais.Domain.Repositories;
using Gcm.Info.Cadastrais.Domain.Resources;
using Flunt.Notifications;
using System.Collections.Generic;
using System;

namespace Gcm.Info.Cadastrais.Application
{
    /// <summary>
    /// Classe de application de categoriaProduto
    /// </summary>
    public class CategoriaProdutoApplication : ICategoriaProdutoApplication
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="categoriaProdutoRepository"></param>
        public CategoriaProdutoApplication(IMapper mapper,
                                     ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _mapper = mapper;
            _categoriaProdutoRepository = categoriaProdutoRepository;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de categoriaProdutoes 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<CategoriaProdutoModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaCategoriaProdutoes = await _categoriaProdutoRepository.ListarTodos(ctx);

            return Result<List<CategoriaProdutoModel>>.Ok(_mapper.Map<List<CategoriaProdutoModel>>(listaCategoriaProdutoes));
        }

        /// <summary>
        /// Obtem dados de uma categoriaProduto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<CategoriaProdutoModel>> ObterCategoriaProduto(int codigo, CancellationToken ctx)
        {
            var output = new CategoriaProdutoModel();

            var categoriaProduto = await _categoriaProdutoRepository.ObterPorCodigo(codigo, ctx);
            if (categoriaProduto == null)
            {
                var notification = new List<Notification> { new Notification(nameof(CategoriaProduto.Codigo), MensagensInfo.CategoriaProduto_NaoEncontrado) };
                return Result<CategoriaProdutoModel>.Error(notification);
            }

            output = _mapper.Map<CategoriaProduto, CategoriaProdutoModel>(categoriaProduto);

            return Result<CategoriaProdutoModel>.Ok(output);
        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de uma categoriaProduto
        /// </summary>
        /// <param name="categoriaProdutoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<CategoriaProduto>> CadastrarCategoriaProduto(CategoriaProdutoModel categoriaProdutoModel, CancellationToken ctx)
        {
            var categoriaProduto = _mapper.Map<CategoriaProdutoModel, CategoriaProduto>(categoriaProdutoModel);

            if (categoriaProduto.Valid)
            {
                if (!await _categoriaProdutoRepository.VerificarSeExiste(categoriaProduto, ctx))
                {
                    await _categoriaProdutoRepository.Salvar(categoriaProduto, ctx);
                    return Result<CategoriaProduto>.Ok(categoriaProduto);
                }

                categoriaProduto.AddNotification(nameof(CategoriaProduto.Codigo), MensagensInfo.CategoriaProduto_CpnjExiste);
            }

            return Result<CategoriaProduto>.Error(categoriaProduto.Notifications);
        }

        #endregion

        #region Atualizar

        /// <summary>
        /// Atualiza dados do categoriaProduto
        /// </summary>
        /// <param name="categoriaProdutoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<CategoriaProduto>> AtualizarCategoriaProduto(CategoriaProdutoModel categoriaProdutoModel, CancellationToken ctx)
        {
            var categoriaProduto = _mapper.Map<CategoriaProdutoModel, CategoriaProduto>(categoriaProdutoModel);

            if (categoriaProduto.Valid)
            {
                if (await _categoriaProdutoRepository.VerificarSeExiste(categoriaProduto, ctx))
                {
                    await _categoriaProdutoRepository.Atualizar(categoriaProduto, ctx);
                    return Result<CategoriaProduto>.Ok(categoriaProduto);
                }

                categoriaProduto.AddNotification(nameof(CategoriaProduto.Codigo), MensagensInfo.CategoriaProduto_NaoEncontrado);
            }

            return Result<CategoriaProduto>.Error(categoriaProduto.Notifications);
        }

        #endregion

        #region Deletar

        /// <summary>
        /// Deleta uma categoriaProduto pelo Codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<CategoriaProduto>> DeletarCategoriaProduto(int codigo, CancellationToken ctx)
        {
            try
            {
                await _categoriaProdutoRepository.Deletar(codigo, ctx);
            }
            catch (Exception)
            {
                var notification = new List<Notification> { new Notification(nameof(CategoriaProduto.Codigo), MensagensInfo.CategoriaProduto_ErroDeletar) };
                return Result<CategoriaProduto>.Error(notification);
            }

            return Result<CategoriaProduto>.Ok(new CategoriaProduto());
        }

        #endregion

    }
}
