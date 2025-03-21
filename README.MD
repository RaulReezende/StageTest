# Nome do Projeto
Case Tech
## Visão Geral
Case tech. Desenvolvido para gerenciamento de processos

## Tecnologias
- C#
- DotNet Core
- SQLSERVER
- TDD
- Clean Architecture

## Setup
1. Clone o repositório:
https://github.com/RaulReezende/StageTest/

2. Instale as dependências:
npm install

3. Rode o servidor:

## Endpoint principais
- **GET /Departamento**: Busca todos os departamento cadastrados.
- **POST /Departamento**: Insere um departamento.
- **PUT /Departamento**: Atualiza um departamento.
- **DELETE /Departamento/:id**: Deleta um departamento.

- **GET /Processo/:DepartamentoId**: Busca todos os processos cadastrados do departamento.
- **GET /Processo/:id**: Busca o processos pelo Id.
- **POST /Processo/**: Insere um Processo e atualiza.
- **PUT /Processo/**: Atualiza um Processo.
- **DELETE /Process/:Id**: Deleta um Processo.

- **GET /Equipes**: Busca todos as equipes cadastrados.
- **GET /Equipes**: Busca todos as equipes cadastrados.
- **POST /Equipes/**: Insere uma equipes.
- **PUT /Equipes/:id**: Atualiza um departamento.
- **DELETE /Equipes/:id**: Deleta uma equipe.

- **GET /responsaveis**: Busca todos os resposáveis.
- **GET /responsaveis/:equipeId**: Busca um resposáveis pela equipe.
- **GET /responsaveis/:id**: Busca um resposável.
- **POST /responsaveis/add**: Insere um responsável.
- **PUT /responsaveis/:id**: Atualiza um departamento.