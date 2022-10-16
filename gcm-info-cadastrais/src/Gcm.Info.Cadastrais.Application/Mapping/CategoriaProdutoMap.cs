using AutoMapper;
using Gcm.Info.Cadastrais.Application.Models;
using Gcm.Info.Cadastrais.Domain.Entities;
using Gcm.Info.Cadastrais.Domain.ValueObjects;

namespace Gcm.Info.Cadastrais.Application.Mapping
{
    /// <summary>
    /// Mapper de categoriaProduto
    /// </summary>
    public class CategoriaProdutoMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public CategoriaProdutoMap()
        {
            CreateMap<CategoriaProduto, CategoriaProdutoModel>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Descricao, m => m.MapFrom(src => src.Descricao));

            CreateMap<EnderecoCompleto, DadosEnderecoModel>()
                .ForMember(dest => dest.Cep, m => m.MapFrom(src => src.Cep))
                .ForMember(dest => dest.Cidade, m => m.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Complemento, m => m.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Logradouro, m => m.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Numero, m => m.MapFrom(src => src.Numero))
                .ReverseMap();

            CreateMap<CategoriaProdutoModel, CategoriaProduto>()
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Descricao, m => m.MapFrom(src => src.Descricao))                
                .ConstructUsing(src =>
                    new CategoriaProduto(
                        src.Codigo,
                        src.Nome,
                        src.Descricao                        
                    ));
        }
    }
}
