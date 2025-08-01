Projeto de Gerenciamento de Pessoas (Teste Stefanini)

Este é um projeto Full Stack de uma aplicação para gerenciamento de cadastros de pessoas, desenvolvido como parte de um desafio técnico. A aplicação demonstra uma arquitetura robusta com um backend em .NET e um frontend em React, incluindo funcionalidades como autenticação, versionamento de API e um design responsivo.

✨ Funcionalidades Principais

•
Autenticação de Usuário: Acesso seguro à aplicação utilizando tokens JWT (JSON Web Tokens).

•
CRUD Completo: Funcionalidades para Criar, Ler, Atualizar e Deletar (CRUD) registros de pessoas.

•
Versionamento de API: O backend expõe duas versões da API (/v1 e /v2), cada uma com regras de negócio e modelos de dados distintos.

•
Interface Reativa: Frontend construído com React e Material-UI, proporcionando uma experiência de usuário moderna e responsiva.

•
Validação de Formulários: Validação de dados tanto no frontend (com Formik e Yup) quanto no backend (com FluentValidation) para garantir a integridade dos dados.





🛠️ Tecnologias Utilizadas

O projeto é dividido em duas partes principais:

Backend (API)

•
Plataforma: .NET 8

•
Linguagem: C#

•
Arquitetura: API RESTful seguindo princípios de Arquitetura Limpa (Clean Architecture).

•
Autenticação: JWT (JSON Web Tokens) para proteger os endpoints.

•
Banco de Dados: Entity Framework Core com banco de dados em memória (InMemoryDatabase) para facilitar a execução.

•
Validação: FluentValidation para regras de negócio e validação de DTOs.

•
Documentação da API: Swagger (OpenAPI) com suporte para versionamento e autenticação.

Frontend

•
Framework: React 18 com TypeScript

•
UI Kit: Material-UI (MUI) para componentes de interface.

•
Gerenciamento de Estado: Context API do React para controle de autenticação.

•
Roteamento: React Router DOM.

•
Requisições HTTP: Axios, com interceptors para injeção automática de tokens JWT.

•
Formulários: Formik e Yup para gerenciamento e validação de formulários.





🚀 Como Executar o Projeto

Para executar a aplicação em seu ambiente local, siga os passos abaixo.

Pré-requisitos

•
.NET SDK 6.0 ou superior.

•
Node.js e npm (ou Yarn).

1. Configuração do Backend

Bash


# 1. Clone o repositório
git clone https://github.com/mhmocelin/TesteStefanini.git
cd TesteStefanini

# 2. Navegue até a pasta da API
cd Register.Api

# 3. Execute a aplicação backend
dotnet run


A API estará em execução em https://localhost:5001 (ou outra porta configurada). A documentação do Swagger estará disponível em /swagger.

2. Configuração do Frontend

Bash


# Em um novo terminal, navegue até a pasta do frontend
cd register.app

# 1. Instale as dependências
npm install

# 2. Inicie o servidor de desenvolvimento
npm start


A aplicação React estará acessível em http://localhost:3000.

3. Acessando a Aplicação

Após iniciar ambos os servidores, abra seu navegador e acesse http://localhost:3000.

•
Credenciais de Login Padrão:

•
Usuário: admin / Senha: 123456

•
Usuário: user / Senha: 123456







API Versionamento

A aplicação demonstra o conceito de versionamento de API, com duas versões disponíveis que podem ser testadas na interface:

•
Versão 1 (/api/v1/persons):

•
Modelo de dados principal, contendo: nome, gênero, data de nascimento, nacionalidade, email, local de nascimento e CPF.

•
O CPF é um campo obrigatório e validado.



•
Versão 2 (/api/v2/persons):

•
Uma evolução do modelo, que adiciona um objeto de endereço completo (rua, cidade, estado, etc.) ao cadastro da pessoa.

•
Mantém os campos da V1.



A interface do frontend permite alternar entre as duas versões através de abas, consumindo os endpoints e exibindo os formulários correspondentes.

