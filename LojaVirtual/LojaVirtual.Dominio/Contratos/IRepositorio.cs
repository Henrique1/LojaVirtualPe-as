﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.Dominio.Contratos
{
    public interface IRepositorio<T> where T : class
    {
        T Salvar(T entidade);

        T Alterar(T entidade);

        void Excluir(T entidade);

        T Buscar(string id);

        IEnumerable<T> BuscarTodos();

        IEnumerable<T> BuscarPorFiltro(Func<T, bool> filtro);
    }
}
