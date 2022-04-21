using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Models;

namespace TaskManager.API.Data.Repository
{
   public interface ITarefasRepository
    {
        void Adicionar(Tarefa tarefa);

        void Atualizar(string id, Tarefa tarefaAtualizada);

        IEnumerable<Tarefa> Buscar();

        Tarefa Buscar(string id);

        void Remove(String id);


    }
}
