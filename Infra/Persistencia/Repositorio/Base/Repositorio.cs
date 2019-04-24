using Dominio.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Persistencia.Repositorio.Base
{
    public abstract class Repositorio<TEntidade, TCodigo> : IDisposable, IRepositorio<TEntidade, TCodigo>
        where TEntidade : class
    { 

        public abstract TEntidade Cadastrar(string nomeServico);
        public abstract void Dispose();
    }
}
