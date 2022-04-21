using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TaskManager.API.Data.Configurations;
using TaskManager.API.Models;

namespace TaskManager.API.Data.Repository
{
    public class TarefasRepository : ITarefasRepository
    {

        private readonly IMongoCollection<Tarefa> _tarefas;

        public TarefasRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _tarefas = database.GetCollection<Tarefa>("tarefas");
        }

        public void Adicionar(Tarefa tarefa)
        {
            _tarefas.InsertOne(tarefa);
        }

        public void Atualizar(string id, Tarefa tarefaAtualizada)
        {
            _tarefas.ReplaceOne(t => t.Id == id, tarefaAtualizada);
        }

        public IEnumerable<Tarefa> Buscar()
        {
            return _tarefas.Find(tarefa => true).ToList();
        }

        public Tarefa Buscar(string id)
        {
            return _tarefas.Find(tarefa => tarefa.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _tarefas.DeleteOne(t => t.Id == id);
        }
    }
}
