using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Interfaces
{
    public interface IServicoObjetoGenerico :IServico
    {
        ObjetoGenerico Cadastrar(string nomeServico);
    }
}
