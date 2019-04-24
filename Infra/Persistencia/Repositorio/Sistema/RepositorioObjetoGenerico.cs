using Domain.Entities;
using Dominio.Interfaces;
using Infra.Persistencia.Repositorio.Base;
using Microsoft.Extensions.Configuration;
using System;


namespace Infra.Persistencia.Repositorio.Sistema
{
    public class RepositorioObjetoGenerico : Repositorio<ObjetoGenerico, int>, IRepositorioObjetoGenerico
    {
 
        public override ObjetoGenerico Cadastrar(string nomeServico)
        {
            ObjetoGenerico obj = new ObjetoGenerico();
            obj.NameService = nomeServico;
            obj.Message = "Hello World";
            obj.IdRequest = Guid.NewGuid().ToString("N");
            return obj;
        }



        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
