using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Gcm.Gestao.Campanha.Marketing.Application.Interfaces;
using Gcm.Gestao.Campanha.Marketing.Application.Models;
using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using Gcm.Gestao.Campanha.Marketing.Domain.Repositories;
using Gcm.Gestao.Campanha.Marketing.Domain.Resources;
using Gcm.Gestao.Campanha.Marketing.Domain.ValueObjects;
using Flunt.Notifications;
using System.Collections.Generic;
using System;

namespace Gcm.Gestao.Campanha.Marketing.Application
{
    /// <summary>
    /// Classe de application de campanha
    /// </summary>
    public class CampanhaApplication : ICampanhaApplication
    {
        private readonly IMapper _mapper;
        private readonly ICampanhaRepository _campanhaRepository;        

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="campanhaRepository"></param>        
        public CampanhaApplication(IMapper mapper,
                                  ICampanhaRepository campanhaRepository)
        {
            _mapper = mapper;
            _campanhaRepository = campanhaRepository;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de campanhas 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<CampanhaModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaCampanhas = await _campanhaRepository.ListarTodos(ctx);

            return Result<List<CampanhaModel>>.Ok(_mapper.Map<List<CampanhaModel>>(listaCampanhas));
        }

        /// <summary>
        /// Obtem dados de uma campanha
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<CampanhaModel>> ObterCampanha(int codigo, CancellationToken ctx)
        {
            var output = new CampanhaModel();

            var campanha = await _campanhaRepository.ObterPorCodigo(codigo, ctx);
            if (campanha == null)
            {
                var notification = new List<Notification> { new Notification(nameof(CampanhaMkt.Codigo), MensagensInfo.Campanha_NaoEncontrada) };
                return Result<CampanhaModel>.Error(notification);
            }

            output = _mapper.Map<CampanhaMkt, CampanhaModel>(campanha);

            return Result<CampanhaModel>.Ok(output);

        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de uma campanha
        /// </summary>
        /// <param name="campanhaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<CampanhaMkt>> CadastrarCampanha(CampanhaModel campanhaModel, CancellationToken ctx)
        {
            var campanha = _mapper.Map<CampanhaModel, CampanhaMkt>(campanhaModel);

            if (campanha.Valid)
            {               

                if (!await _campanhaRepository.VerificarSeExiste(campanha, ctx))
                {
                    await _campanhaRepository.Salvar(campanha, ctx);
                    return Result<CampanhaMkt>.Ok((CampanhaMkt)campanha);
                }

                campanha.AddNotification(nameof(CampanhaMkt.Codigo), MensagensInfo.Campanha_CodigoExistente);
            }

            return Result<CampanhaMkt>.Error((IReadOnlyCollection<Notification>)campanha.Notifications);
        }

        #endregion

        #region Atualizar

        /// <summary>
        /// Atualiza dados da campanha
        /// </summary>
        /// <param name="campanhaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<CampanhaMkt>> AtualizarCampanha(CampanhaModel campanhaModel, CancellationToken ctx)
        {
            var campanha = _mapper.Map<CampanhaModel, CampanhaMkt>(campanhaModel);

            if (campanha.Valid)
            {
                if (await _campanhaRepository.VerificarSeExiste(campanha, ctx))
                {                    
                    await _campanhaRepository.Atualizar(campanha, ctx);
                    await _campanhaRepository.AtualizarCliente(campanha, ctx);
                    return Result<CampanhaMkt>.Ok((CampanhaMkt)campanha);
                }

                campanha.AddNotification(nameof(CampanhaMkt.Codigo), MensagensInfo.Campanha_NaoEncontrada);
            }

            return Result<CampanhaMkt>.Error((IReadOnlyCollection<Notification>)campanha.Notifications);
        }

        #endregion

        #region Deletar

        /// <summary>
        /// Deleta uma campanha pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<CampanhaMkt>> DeletarCampanha(int codigo, CancellationToken ctx)
        {
            try
            {
                await _campanhaRepository.Deletar(codigo, ctx);
            }
            catch (Exception)
            {
                var notification = new List<Notification> { new Notification(nameof(CampanhaMkt.Codigo), MensagensInfo.Campanha_ErroDeletar) };
                return Result<CampanhaMkt>.Error(notification);
            }

            return Result<CampanhaMkt>.Ok(new CampanhaMkt());
        }

        #endregion

    }
}
