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
        
        /// <summary>
        /// Insere uma nova tarefa na lista
        /// </summary>
        /// <param name="todo">Tarefa a ser adicionada na lista</param>
        /// <returns>A nova tarefa adicionada</returns>
        /// <remarks>
        /// Inserção simples:
        /// 
        ///     Post -c "{"title":"Nova tarefa"}"
        ///     
        /// </remarks>
        /// <response code="201">Adiciona a nova tarefa na lista e a retorna</response>
        [HttpPost(Name = nameof(Create))]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(Todo todo)
        {
            TodoService.Inserir(todo);
            return CreatedAtAction(nameof(Create), new { id = todo.Id }, todo);
        }

        /// <summary>
        /// Altera uma tarefa existente na lista de tarefas
        /// </summary>
        /// <param name="id">ID da tarefa a ser alterada</param>
        /// <param name="todo">A tarefa alterada</param>
        /// <returns>Retorna uma mensagem de NoContent (sem conteúdo) no caso da alteração ser realizada com sucesso</returns>
        /// <remarks>
        /// Alteração simples:
        /// 
        ///     Put 4 -c "{"id": 4, "title":"Novo título", "isDone": false}"
        ///     
        /// </remarks>
        /// <response code="204">Tarefa alterada com sucesso</response>
        /// <response code="400">Ocorre quando o id depois do Put for diferente do id da tarefa</response>
        /// <response code="404">Ocorre quando não existe uma tarefa com o id passado como parâmetro</response>
        [HttpPut("{id}", Name = nameof(Update))]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Remove uma tarefa da lista de tarefas
        /// </summary>
        /// <param name="id">Identificador da tarefa a ser removida</param>
        /// <returns>Retorna uma mensagem de NoContent (sem conteúdo) no caso da remoção ser realizada com sucesso</returns>
        /// <remarks>
        /// Remoção simples
        /// 
        ///     Delete 2
        ///     
        /// </remarks>
        /// <response code="204">Tarefa removida com sucesso</response>
        /// <response code="404">Ocorre quando não existe uma tarefa com o id passado como parâmatro</response>
        [HttpDelete("{id}")]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
