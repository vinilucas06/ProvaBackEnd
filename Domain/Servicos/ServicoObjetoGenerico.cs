using Domain.Entities;
using Dominio.Interfaces;
using Dominio.Servicos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Servicos
{
    public class ServicoObjetoGenerico : Servico, IServicoObjetoGenerico
    {
        private readonly IRepositorioObjetoGenerico repositorioObjetoGenerico;

        public ServicoObjetoGenerico(IRepositorioObjetoGenerico repositorioObjetoGenerico)
        {
            this.repositorioObjetoGenerico = repositorioObjetoGenerico;
        }

        public ObjetoGenerico Cadastrar(string nomeServico) => this.repositorioObjetoGenerico.Cadastrar(nomeServico);
    
    }
}
