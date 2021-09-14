using Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api.Services
{
    public class TodoService
    {
        static List<Todo> Todos { get; }
        static int nextId = 6;
        static TodoService()
        {
            Todos = new List<Todo>
            {
                new Todo { Id = 1, Title = "Tarefa 01" },
                new Todo { Id = 2, Title = "Tarefa 02", IsDone = true }, // Para testar o GET das tarefas concluídas
                new Todo { Id = 3, Title = "Tarefa 03" },
                new Todo { Id = 4, Title = "Tarefa 04", IsDone = true }, // Para testar o GET das tarefas concluídas
                new Todo { Id = 5, Title = "Tarefa 05" }
            };
        }

        // Obter todas as tarefas
        public static List<Todo> ObterTodas() => Todos;

        // Obter somente as tarefas concluídas
        public static List<Todo> ObterTodasConcluidas() => Todos.FindAll(t => t.IsDone == true);

        // Obter uma tarefa específica pelo ID da mesma
        public static Todo Obter(int id) => Todos.FirstOrDefault(t => t.Id == id);

        // Inserir uma nova tarefa
        public static void Inserir(Todo todo)
        {
            todo.Id = nextId++;
            Todos.Add(todo);
        }

        // Remover uma tarefa existente
        public static void Remover(int id)
        {
            var todo = Todos.Find(t => t.Id == id);
            if (todo is null)
                return;
            Todos.Remove(todo);
        }

        // Alterar uma tarefa existente
        public static void Alterar(Todo todo)
        {
            var index = Todos.FindIndex(t => t.Id == todo.Id);
            if (index == -1)
                return;
            Todos[index] = todo;
        }
    }
}
