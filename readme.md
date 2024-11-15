# Projeto Fazenda Urbana - Sistema de Gerenciamento

Este projeto é um sistema de gerenciamento para uma Fazenda Urbana, desenvolvido em .NET 6.0 com suporte a banco de dados MySQL. Ele permite o gerenciamento de funcionários, insumos, produção, produtos, vendas e usuários, além de um painel de login com perfis de administração e cliente.

## Requisitos

- .NET SDK 6.0
- MySQL Server

## Configuração Inicial

### 1. Configurar o Banco de Dados

1. Abra o MySQL Workbench (ou o cliente MySQL de sua escolha).
2. Execute o script `CreateBDScript.sql` para criar o banco de dados e as tabelas necessárias.

### 2. Configurar a Conexão com o Banco de Dados

1. Localize o arquivo `appsettings.json` no diretório raiz do projeto.
2. Atualize a string de conexão com as credenciais do seu ambiente local:

```json
"ConnectionStrings": {
    "DefaultConnection": "server=127.0.0.1;uid=root;pwd=1234;database=fazenda"
}
