# Task Manager

Esse monorepo contém um exemplo de gerenciador de tarefas com frontend em Next.js e backend em C# utilizando layered architecture no backend.

## Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- Node.js (com npm)
- .NET SDK 9.x
- Docker e Docker Compose

## Estrutura do Projeto

O projeto segue uma estrutura monorepo com frontend e backend separados:
```
TaskManager/
├── client/
│   ├── task-manager/
│   ├── .next/
│   ├── node_modules/
│   ├── public/
│   ├── src/
│   ├── .gitignore
│   ├── eslint.config.mjs
│   ├── next-env.d.ts
│   ├── next.config.ts
│   ├── package.json
│   ├── package-lock.json
│   ├── postcss.config.mjs
│   ├── README.md
│   └── tsconfig.json
│
├── server/
│   ├── .idea/
│   ├── src/
│   ├── TaskManager.API/
│   ├── TaskManager.Application/
│   ├── TaskManager.Domain/
│   └── TaskManager.Infrastructure/
│   └── tests/
│   ├── TaskManager.API.Tests/
│   ├── TaskManager.Application.Tests/
│   └── TaskManager.Domain.Tests/
│
├── .env
├── .gitignore
├── docker-compose.yml
├── Dockerfile
├── TaskManager.sln
└── TaskManager.sln.DotSettings.user
```
## Configuração Inicial

1. **Banco de Dados**:
   - Crie um arquivo `.env` em `TaskManager/server/` com as credenciais do PostgreSQL:
     ```
     POSTGRES_USER=postgres
     POSTGRES_PASSWORD=sua_senha_segura_aqui
     ```
   - Atualize a connection string em `TaskManager/server/src/TaskManager.API/appsettings.Development.json` para corresponder às credenciais definidas no `.env`

2. **Iniciar Containers**:
   ```bash
   docker-compose up -d
   ```
**Executando o Projeto**

Siga as instruções abaixo para iniciar o frontend e o backend do projeto.

**Frontend (Next.js)**

Para iniciar o frontend, navegue até o diretório TaskManager/client/task-manager e execute os seguintes comandos:
```
cd TaskManager/client/task-manager
npm install
npm run dev
```
**Backend (C# .NET)**

Para iniciar o backend, navegue até o diretório TaskManager/server e execute o comando abaixo:
```
cd TaskManager/server
dotnet run watch
```

**Notas Importantes**

- Este projeto foi desenvolvido principalmente em ambiente Linux, utilizando editores como VSCode, Zed ou Vim.
- Caso esteja utilizando Visual Studio no Windows, após configurar o Docker, você pode carregar a solution (TaskManager.sln) no .NET.
- Certifique-se de que todas as dependências estão instaladas corretamente antes de tentar executar o projeto.

**Testes**

Para executar os testes unitários do projeto, navegue até a pasta de testes específica e use o seguinte comando:
```
cd TaskManager/server/tests
dotnet test
```
