using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Interfaces
{
    public interface IRepositorio<TEntidade, TCodigo> : IDisposable
        where TEntidade : class
    {
        TEntidade Cadastrar(string nomeServico);
    }
}
