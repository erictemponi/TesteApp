# TesteApp

Solução composta de dois projetos: Site e Api.

## Site

Aplicativo web feito com Blazor.

É um site para adicionar e concluir tarefas. Seu funcionamento é bem simples. Você preenche o campo "Tarefa pendente" e clica no botão "Adicionar" para adicionar uma nova tarefa na lista de tarefas pendentes. Ao fazer isso, uma nova tarefa aparecerá na lista de tarefas pendentes, atualizando a contagem de tarefas a serem realizadas.

As tarefas pendentes são exibidas cada uma em uma caixa de texto e podem ser alteradas. Possuem, também, uma caixinha de marcação para marcar a tarefa como concluída. Uma vez concluída, a tarefa vai para a lista de tarefas concluídas e não pode mais ser alterada.

<p align="center"><img src="https://i.ibb.co/qWztKLg/Site.png" /></p>

## Api

API Web RESTful com ASP.NET Core

API RESTful com os ações GET, PUT, POST e DELETE para manipular tarefas. Possui um modelo, o qual contém os atributos das tarefas: ID, Título e se foi concluída ou não. Esse modelo é a classe **Todo**. Além do modelo, possui duas controladoras e um serviço. O serviço trata da lógica por traz da manipulação das tarefas: obter todas as tarefas, obter todas as tarefas concluídas, obter uma única tarefa, bem como inserir, alterar e remover tarefas. A controladora TodoController faz a ligação entre as ações GET, PUT, POST e DELETE com as funções do serviço. A controladora TodoesController faz as mesmas coisas, só que de forma persistente, ou seja, fazendo uso de uma base de dados.

A base de dados utilizada é o SQL Server que já vem embutido no Visual Studio (localdb). Não é necessário criar a tabela para as tarefas, a própria API já faz isso, uma vez que for executada e já adiciona 5 tarefas na base de dados.

A API possui uma lista de tarefas com 5 tarefas já adicionadas e conta com as seguintes ações HTTP:

**1) GET** --> Obtém todas as tarefas.

<img src="https://i.ibb.co/12zfmpm/GET.png" />

**2) GET done** --> Obtém todas as tarefas marcadas como concluídas.

<img src="https://i.ibb.co/TgPDThw/GET-done.png" />

**3) GET id** --> Obtém uma única tarefa, a qual precisa ter o campo *id* igual ao **id** usado na chamada do **GET**.

<img src="https://i.ibb.co/Ctc02FY/GET-id.png" />

**4) POST tarefa** --> Insere uma nova tarefa na lista.

<img src="https://i.ibb.co/hZsJBpZ/POST.png" />

**5) PUT tarefa** --> Altera os atributos de uma tarefa já existente.

<img src="https://i.ibb.co/m6Yy7SV/PUT.png" />

**6) DELETE id** --> Remove uma tarefa da lista.

<img src="https://i.ibb.co/JjfGkK3/DELETE.png" />

## Observações

Além desta documentação do README, existe, também, a documentação do Swagger, a qual explica como usar os métodos de cada controladora. Para acessar a documentação Swagger, basta navegar até a API com o Shell e executar o seguinte comando:

	dotnet run

Após isso, é só acessar a documentação através do endereço: http://localhost:5000/swagger
