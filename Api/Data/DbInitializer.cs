using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TodoContext context)
        {
            context.Database.EnsureCreated();

            // Procurar por tarefas na base de dados
            if (context.Todos.Any())
                return; // Base de dados já populada

            var todos = new Todo[]
            {
                new Todo { Title = "Tarefa 01" },
                new Todo { Title = "Tarefa 02", IsDone = true }, // Para testar o GET das tarefas concluídas
                new Todo { Title = "Tarefa 03" },
                new Todo { Title = "Tarefa 04", IsDone = true }, // Para testar o GET das tarefas concluídas
                new Todo { Title = "Tarefa 05" }
            };

            // Adicionar as tarefas na base de dados
            foreach (Todo t in todos)
                context.Todos.Add(t);

            context.SaveChanges();
        }
    }
}
