using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        public TodoController() { }

        // Obter todas as tarefas
        [HttpGet]
        public ActionResult<List<Todo>> ObterTodas() => TodoService.ObterTodas();

        // Obter todas as tarefas concluídas
        [HttpGet("{done}")]
        public ActionResult<List<Todo>> ObterTodasConcluidas(string done)
        {
            if (!(done.ToLower() == "done"))
                return BadRequest();
            return TodoService.ObterTodasConcluidas();
        }

        
        // Obter todas uma tarefa específica
        [HttpGet("{id:int}")]
        public ActionResult<Todo> Obter(int id)
        {
            var todo = TodoService.Obter(id);

            if (todo == null)
                return NotFound();

            return todo;
        }
        

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            TodoService.Inserir(todo);
            return CreatedAtAction(nameof(Create), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Todo todo)
        {
            if (id != todo.Id)
                return BadRequest();

            var existingTodo = TodoService.Obter(id);
            if (existingTodo is null)
                return NotFound();

            TodoService.Alterar(todo);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = TodoService.Obter(id);

            if (todo is null)
                return NotFound();

            TodoService.Remover(id);

            return NoContent();
        }
    }
}
