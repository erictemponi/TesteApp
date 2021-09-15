using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Retorna todas as tarefas da lista.
        /// </summary>
        /// <returns>Uma lista com todas as tarefas.</returns>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     Get /todo
        ///     
        /// </remarks>
        /// <response code="200">Retorna uma lista com todas as tarefas.</response>
        [HttpGet(Name = nameof(ObterTodas))]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Todo>> ObterTodas() => TodoService.ObterTodas();

        /// <summary>
        /// Retorna todas as tarefas da lista marcadas como concluídas
        /// </summary>
        /// <param name="done">Precisa digitar done depois do get</param>
        /// <returns>Uma lista com todas as tarefas concluídas</returns>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     Get /todo/done
        ///     
        /// </remarks>
        /// <response code="200">Retorna uma lista com todas as tarefas concluídas</response>
        [HttpGet("{done}", Name = nameof(ObterTodasConcluidas))]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Todo>> ObterTodasConcluidas(string done)
        {
            if (!(done.ToLower() == "done"))
                return BadRequest();
            return TodoService.ObterTodasConcluidas();
        }
        
        /// <summary>
        /// Retorna uma tarefa específica com base em um ID
        /// </summary>
        /// <param name="id">Idenficador da tarefa. É um número inteiro</param>
        /// <returns>A tarefa de ID igual ao ID passado como parâmetro</returns>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     Get /todo/1
        ///     
        /// </remarks>
        /// <response code="200">Retorna a tarefa cujo ID é igual ao ID passado como parâmetro</response>
        /// <response code="404">Não existe uma tarefa na lista com o ID passado por parâmetro</response>
        [HttpGet("{id:int}", Name = nameof(Obter))]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Todo> Obter(int id)
        {
            var todo = TodoService.Obter(id);

            if (todo == null)
                return NotFound();

            return todo;
        }
        
        // Inserir uma nova tarefa
        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            TodoService.Inserir(todo);
            return CreatedAtAction(nameof(Create), new { id = todo.Id }, todo);
        }

        // Alterar uma tarefa existente
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

        // Remover uma tarefa existente
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
