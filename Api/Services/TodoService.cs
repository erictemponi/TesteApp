using Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api.Services
{
    public class TodoService
    {
        static List<Todo> Todos { get; }
        static int nextId = 4;
        static TodoService()
        {
            Todos = new List<Todo>
            {
                new Todo { Id = 1, Title = "Tarefa 01" },
                new Todo { Id = 2, Title = "Tarefa 02", IsDone = true },
                new Todo { Id = 3, Title = "Tarefa 03" }
            };
        }

        public static List<Todo> ObterTodas() => Todos;
        public static List<Todo> ObterTodasConcluidas() => Todos.FindAll(t => t.IsDone == true);
        public static Todo Obter(int id) => Todos.FirstOrDefault(t => t.Id == id);
        public static void Inserir(Todo todo)
        {
            todo.Id = nextId++;
            Todos.Add(todo);
        }
        public static void Remover(int id)
        {
            var todo = Todos.Find(t => t.Id == id);
            if (todo is null)
                return;
            Todos.Remove(todo);
        }

        public static void Alterar(Todo todo)
        {
            var index = Todos.FindIndex(t => t.Id == todo.Id);
            if (index == -1)
                return;
            Todos[index] = todo;
        }
    }
}
