using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManager.API.Data.Repository;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {

        private readonly ITarefasRepository _tarefasRepository;

        public TarefasController(ITarefasRepository tarefasRepository)
        {
            _tarefasRepository = tarefasRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tarefas = _tarefasRepository.Buscar();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var tarefa = _tarefasRepository.Buscar(id);
            if (tarefa == null) 
                return NotFound();
            
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TarefaInputModel novaTarefa)
        {
            var tarefa = new Tarefa(novaTarefa.Nome, novaTarefa.Detalhes);
            _tarefasRepository.Adicionar(tarefa);
            return Created("", tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] TarefaInputModel tarefaAtualizada)
        {
            var tarefa = _tarefasRepository.Buscar(id);
            if (tarefa == null) 
                return NotFound();
            
            tarefa.AtualizaTarefa(tarefaAtualizada.Nome, tarefaAtualizada.Detalhes, tarefaAtualizada.Concluido);     
            _tarefasRepository.Atualizar(id, tarefa);

            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var tarefa = _tarefasRepository.Buscar(id);
            if (tarefa == null) 
                return NotFound();

            _tarefasRepository.Remove(id);
            
            return NoContent();
        }
    }
}
