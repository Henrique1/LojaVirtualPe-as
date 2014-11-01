using LojaVirtual.Dominio;
using LojaVirtual.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace LojaVirtual.Repositorio
{
   public class UsuarioRepositorio:IRepositorio<Usuario>
    {
       private readonly SqlConnection minhaConexao;
       public UsuarioRepositorio()
       {
           minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaConfig"].ConnectionString);

       }
        public Usuario Salvar(Usuario entidade)
        {
            minhaConexao.Open();
            string sql = @"
                INSERT INTO Usuario (Nome, Email, Senha, Permissoes) 
                VALUES (@Nome, @Email, @Senha, @Permissoes);
                SELECT CAST (SCOPE_IDENTITY() as int)";
            
            var id = minhaConexao.Query<int>(sql, 
                new {Nome = entidade.Nome, Email = entidade.Email, Senha = entidade.Senha, Permissoes = entidade.Permissoes }).Single();
           
            entidade.Id = id;

            minhaConexao.Close();
            return entidade;
        }

        public Usuario Alterar(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public Usuario Buscar(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> BuscarTodos()
        {
            minhaConexao.Open();
            string sql = @"SELECT Id, Nome, Senha, Email, Permissoes FROM Usuario";
            return minhaConexao.Query<Usuario>(sql);
        }

        public IEnumerable<Usuario> BuscarPorFiltro(Func<Usuario, bool> filtro)
        {
            throw new NotImplementedException();
        }
    }
}
