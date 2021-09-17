using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoesController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Todoes
        /// <summary>
        /// Retorna todas as tarefas da base de dados.
        /// </summary>
        /// <returns>Uma lista com todas as tarefas.</returns>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     Get /todoes
        ///     
        /// </remarks>
        /// <response code="200">Retorna uma lista com todas as tarefas.</response>
        [HttpGet(Name = nameof(GetTodos))]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET: api/Todoes/5
        /// <summary>
        /// Retorna uma tarefa específica com base em um ID
        /// </summary>
        /// <param name="id">Idenficador da tarefa. É um número inteiro</param>
        /// <returns>A tarefa de ID igual ao ID passado como parâmetro</returns>
        /// <remarks>
        /// Requisição simples:
        /// 
        ///     Get /todoes/1
        ///     
        /// </remarks>
        /// <response code="200">Retorna a tarefa cujo ID é igual ao ID passado como parâmetro</response>
        /// <response code="404">Não existe uma tarefa na lista com o ID passado por parâmetro</response>
        [HttpGet("{id:int}", Name = nameof(GetTodo))]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // PUT: api/Todoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Altera uma tarefa existente na base de dados de tarefas
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
        [HttpPut("{id}", Name = nameof(PutTodo))]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insere uma nova tarefa na base de dados
        /// </summary>
        /// <param name="todo">Tarefa a ser adicionada na lista</param>
        /// <returns>A nova tarefa adicionada</returns>
        /// <remarks>
        /// Inserção simples:
        /// 
        ///     Post -c "{"title":"Nova tarefa"}"
        ///     
        /// </remarks>
        /// <response code="201">Adiciona a nova tarefa na base de dados e a retorna</response>
        [HttpPost(Name = nameof(PostTodo))]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        // DELETE: api/Todoes/5
        /// <summary>
        /// Remove uma tarefa da base de dados de tarefas
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
        [HttpDelete("{id}", Name = nameof(DeleteTodo))]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
