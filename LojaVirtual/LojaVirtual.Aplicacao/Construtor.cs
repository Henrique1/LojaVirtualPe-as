using LojaVirtual.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.Aplicacao
{
    public static class Construtor
    {
        public static UsuarioAplicacao UsuarioAplicacao()
        {
            return new UsuarioAplicacao(new UsuarioRepositorio());
        }
    }
}
