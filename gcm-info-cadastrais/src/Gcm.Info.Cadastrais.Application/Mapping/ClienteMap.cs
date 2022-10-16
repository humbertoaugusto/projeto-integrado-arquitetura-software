using AutoMapper;
using Gcm.Info.Cadastrais.Application.Models;
using Gcm.Info.Cadastrais.Domain.Entities;
using Gcm.Info.Cadastrais.Domain.ValueObjects;

namespace Gcm.Info.Cadastrais.Application.Mapping
{
    /// <summary>
    /// Mapper de cliente
    /// </summary>
    public class ClienteMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public ClienteMap()
        {
            CreateMap<Cliente, ClienteModel>()
                .ForMember(dest => dest.Aniversario, m => m.MapFrom(src => src.Aniversario))
                .ForMember(dest => dest.Endereco, m => m.MapFrom(src => src.Endereco))                
                .ForMember(dest => dest.Cpf, m => m.MapFrom(src => src.Cpf.ToString()))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome));

            CreateMap<EnderecoCompleto, DadosEnderecoModel>()
                .ForMember(dest => dest.Cep, m => m.MapFrom(src => src.Cep))
                .ForMember(dest => dest.Cidade, m => m.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Complemento, m => m.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Logradouro, m => m.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Numero, m => m.MapFrom(src => src.Numero))
                .ReverseMap();

            CreateMap<ClienteModel, Cliente>()
                .ForMember(dest => dest.Aniversario, m => m.MapFrom(src => src.Aniversario))
                .ForMember(dest => dest.Endereco, m => m.Ignore())
                .ForMember(dest => dest.Cpf, m => m.Ignore())
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.CodigoCampanha, m => m.MapFrom(src => src.CodigoCampanha))                
                .ConstructUsing(src =>
                    new Cliente(
                        src.Nome,
                        new CPF(src.Cpf),                        
                        src.Aniversario,
                        new EnderecoCompleto(src.Endereco.Cep, src.Endereco.Logradouro, src.Endereco.Numero, src.Endereco.Complemento, src.Endereco.Cidade, src.Endereco.Estado),
                        src.CodigoCampanha
                    ));
        }
    }
}
