﻿using APICatalogo.Models;
using System.Security.Cryptography.X509Certificates;

namespace APICatalogo.DTO.Mappings
{
    public static class CategoriaDTOMappingExtensions
    {
        public static CategoriaDTO? ToCategoriaDTO(this CategoriaDTO categoria)
        {
            if (categoria == null)
                return null;

            return new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl,
            };
            
        }
        public static Categoria? ToCategoria(this CategoriaDTO categoriaDto)
        {
            if (categoriaDto == null)
                return null;

            return new Categoria
            {
                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl,
            };
        }

         public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
        {
            if (categorias is null || !categorias.Any())
            {
                return new List<CategoriaDTO>();
            }
            return categorias.Select(categoria => new CategoriaDTO
                {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl,
            }).ToList();
        }
    }
  
}