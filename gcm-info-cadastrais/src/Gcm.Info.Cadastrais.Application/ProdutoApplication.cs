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
    /// Classe de application de produto
    /// </summary>
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="produtoRepository"></param>
        public ProdutoApplication(IMapper mapper,
                                     IProdutoRepository produtoRepository)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de produtos 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<ProdutoModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaProdutos = await _produtoRepository.ListarTodos(ctx);

            return Result<List<ProdutoModel>>.Ok(_mapper.Map<List<ProdutoModel>>(listaProdutos));
        }

        /// <summary>
        /// Obtem dados de um produto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<ProdutoModel>> ObterProduto(int codigo, CancellationToken ctx)
        {
            var output = new ProdutoModel();

            var produto = await _produtoRepository.ObterPorCodigo(codigo, ctx);
            if (produto == null)
            {
                var notification = new List<Notification> { new Notification(nameof(Produto.Codigo), MensagensInfo.Produto_NaoEncontrada) };
                return Result<ProdutoModel>.Error(notification);
            }

            output = _mapper.Map<Produto, ProdutoModel>(produto);            

            return Result<ProdutoModel>.Ok(output);
        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de um produto
        /// </summary>
        /// <param name="produtoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Produto>> CadastrarProduto(ProdutoModel produtoModel, CancellationToken ctx)
        {
            var produto = _mapper.Map<ProdutoModel, Produto>(produtoModel);

            if (produto.Valid)
            {
                if (!await _produtoRepository.VerificarSeExiste(produto, ctx))
                {
                    await _produtoRepository.Salvar(produto, ctx);
                    return Result<Produto>.Ok(produto);
                }

                produto.AddNotification(nameof(Produto.Codigo), MensagensInfo.Produto_CodigoExiste);
            }

            return Result<Produto>.Error(produto.Notifications);
        }

        #endregion

        #region Atualizar

        /// <summary>
        /// Atualiza um produto pelo codigo
        /// </summary>
        /// <param name="produtoModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Produto>> AtualizarProduto(ProdutoModel produtoModel, CancellationToken ctx)
        {
            var produto = _mapper.Map<ProdutoModel, Produto>(produtoModel);

            if (produto.Valid)
            {
                if (await _produtoRepository.VerificarSeExiste(produto, ctx))
                {
                    await _produtoRepository.Atualizar(produto, ctx);
                    return Result<Produto>.Ok(produto);
                }

                produto.AddNotification(nameof(Produto.Codigo), MensagensInfo.Produto_NaoEncontrada);
            }

            return Result<Produto>.Error(produto.Notifications);
        }

        #endregion

        #region Deletar

        /// <summary>
        /// Deleta um produto pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Produto>> DeletarProduto(int codigo, CancellationToken ctx)
        {
            try
            {
                await _produtoRepository.Deletar(codigo, ctx);
            }
            catch(Exception)
            {
                var notification = new List<Notification> { new Notification(nameof(Produto.Codigo), MensagensInfo.Produto_ErroDeletar) };
                return Result<Produto>.Error(notification);
            }

            return Result<Produto>.Ok(new Produto());
        }

        #endregion

    }
}
