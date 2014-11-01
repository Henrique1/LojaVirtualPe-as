using LojaVirtual.Dominio;
using LojaVirtual.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.Aplicacao
{
    public class UsuarioAplicacao
    {
        private readonly IRepositorio<Usuario> repositorio;
        public UsuarioAplicacao(IRepositorio<Usuario> repo)
        {
            repositorio = repo;
        }

        public Usuario Salvar(Usuario entidade)
        {
            if (entidade.Id > 0)
                return repositorio.Alterar(entidade);

            return repositorio.Salvar(entidade);
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            return repositorio.BuscarTodos();
        }
    }
}
