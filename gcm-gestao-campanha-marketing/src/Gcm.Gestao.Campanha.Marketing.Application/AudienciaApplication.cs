using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Gcm.Gestao.Campanha.Marketing.Application.Interfaces;
using Gcm.Gestao.Campanha.Marketing.Application.Models;
using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using Gcm.Gestao.Campanha.Marketing.Domain.Repositories;
using Gcm.Gestao.Campanha.Marketing.Domain.Resources;
using Flunt.Notifications;
using System.Collections.Generic;
using System;

namespace Gcm.Gestao.Campanha.Marketing.Application
{
    /// <summary>
    /// Classe de application de audiencia
    /// </summary>
    public class AudienciaApplication : IAudienciaApplication
    {
        private readonly IMapper _mapper;
        private readonly IAudienciaRepository _audienciaRepository;        

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="audienciaRepository"></param>        
        public AudienciaApplication(IMapper mapper,
                                  IAudienciaRepository audienciaRepository)
        {
            _mapper = mapper;
            _audienciaRepository = audienciaRepository;
        }

        #region Obter dados

        /// <summary>
        /// Obtém a lista de audiencias 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<List<AudienciaModel>>> ListarTodos(CancellationToken ctx)
        {
            var listaAudiencias = await _audienciaRepository.ListarTodos(ctx);

            return Result<List<AudienciaModel>>.Ok(_mapper.Map<List<AudienciaModel>>(listaAudiencias));
        }

        /// <summary>
        /// Obtem dados de uma audiencia
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<AudienciaModel>> ObterAudiencia(int codigo, CancellationToken ctx)
        {
            var output = new AudienciaModel();

            var audiencia = await _audienciaRepository.ObterPorCodigo(codigo, ctx);
            if (audiencia == null)
            {
                var notification = new List<Notification> { new Notification(nameof(Audiencia.Codigo), MensagensInfo.Audiencia_NaoEncontrada) };
                return Result<AudienciaModel>.Error(notification);
            }

            output = _mapper.Map<Audiencia, AudienciaModel>(audiencia);

            return Result<AudienciaModel>.Ok(output);

        }

        #endregion

        #region Cadastrar

        /// <summary>
        /// Realiza o cadastro de uma audiencia
        /// </summary>
        /// <param name="audienciaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Audiencia>> CadastrarAudiencia(AudienciaModel audienciaModel, CancellationToken ctx)
        {
            var audiencia = _mapper.Map<AudienciaModel, Audiencia>(audienciaModel);

            if (audiencia.Valid)
            {               

                if (!await _audienciaRepository.VerificarSeExiste(audiencia, ctx))
                {
                    await _audienciaRepository.Salvar(audiencia, ctx);
                    return Result<Audiencia>.Ok((Audiencia)audiencia);
                }

                audiencia.AddNotification(nameof(Audiencia.Codigo), MensagensInfo.Audiencia_CodigoExistente);
            }

            return Result<Audiencia>.Error((IReadOnlyCollection<Notification>)audiencia.Notifications);
        }

        #endregion

        #region Atualizar

        /// <summary>
        /// Atualiza dados da audiencia
        /// </summary>
        /// <param name="audienciaModel"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Audiencia>> AtualizarAudiencia(AudienciaModel audienciaModel, CancellationToken ctx)
        {
            var audiencia = _mapper.Map<AudienciaModel, Audiencia>(audienciaModel);

            if (audiencia.Valid)
            {
                if (await _audienciaRepository.VerificarSeExiste(audiencia, ctx))
                {
                    await _audienciaRepository.Atualizar(audiencia, ctx);
                    return Result<Audiencia>.Ok((Audiencia)audiencia);
                }

                audiencia.AddNotification(nameof(Audiencia.Codigo), MensagensInfo.Audiencia_NaoEncontrada);
            }

            return Result<Audiencia>.Error((IReadOnlyCollection<Notification>)audiencia.Notifications);
        }

        #endregion

        #region Deletar

        /// <summary>
        /// Deleta uma audiencia pelo codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task<Result<Audiencia>> DeletarAudiencia(int codigo, CancellationToken ctx)
        {
            try
            {
                await _audienciaRepository.Deletar(codigo, ctx);
            }
            catch (Exception)
            {
                var notification = new List<Notification> { new Notification(nameof(Audiencia.Codigo), MensagensInfo.Audiencia_ErroDeletar) };
                return Result<Audiencia>.Error(notification);
            }

            return Result<Audiencia>.Ok(new Audiencia());
        }

        #endregion

    }
}
