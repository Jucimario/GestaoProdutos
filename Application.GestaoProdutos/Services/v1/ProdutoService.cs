using Application.GestaoProdutos.Context;
using Application.GestaoProdutos.Services.v1.Generic;
using Application.GestaoProdutos.Services.v1.Interfaces;
using AutoMapper;
using Domain.GestaoProdutos.Dtos.ProdutoDtos;
using Domain.GestaoProdutos.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Xml.Linq;

namespace Application.GestaoProdutos.Services.v1;

public class ProdutoService : GenericService<Produto>, IProdutoService
{
    private IMapper _mapper;
    public ProdutoService(AppDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<FilterProdutoDto> FindAll(string nome, int skip = 0, int take = 20)
    {

        if (skip <= 0 & take < 0)
            throw new ArgumentException("Número de paginação incorreta");

        if (take > 200)
            throw new ArgumentException("Limite de registro excedeu o maximo permitido, favor informe um valor abaixo de 200 registros.");

        return EfetuarFiltro(nome, skip, take);
    }

    public async Task<Produto?> Disable(int id)
    {
        try
        {
            var produto = _context.Produtos.SingleOrDefault(p => p.Id.Equals(id));
            if (produto == null)
                return null;

            produto.Situacao = false;

            _context.Entry(produto).CurrentValues.SetValues(produto);
            _context.SaveChanges();

            return produto;
        }
        catch (Exception)
        {
            throw;
        }
    }
    private FilterProdutoDto EfetuarFiltro(string nome, int skip, int take)
    {
        var totalResults = string.IsNullOrEmpty(nome) ?
                                                         _context.Produtos.Count() :
                                                         _context.Produtos.Where(p => p.Descricao.ToUpper().Contains(nome)).Count();

        var produtos = string.IsNullOrEmpty(nome) ?
                                                   _context.Produtos.AsNoTracking().Skip(skip).Take(take).ToList() :
                                                   _context.Produtos.AsNoTracking().Where(p => p.Descricao.ToUpper().Contains(nome)).Skip(skip).Take(take).ToList();

        var produtosMap = _mapper.Map<ICollection<ProdutoDto>>(produtos);

        return new FilterProdutoDto
        {
            Skip = skip,
            Take = take,
            PaginaAtual = (skip / take) + ((skip % take) > 0 ? 1 : 0),
            TotalPagina = (totalResults / take) + ((totalResults % take) > 0 ? 1 : 0),
            TotalRegistro = totalResults,
            Produtos = produtosMap
        };
    }
}
