# GestaoProdutos
Api para gerenciamento de produtos

## Descrição do Projeto
<p align="center">* API para gerenciamento de produtos</p>
<p align="center">* O sistema conterá alguns endpoints responsavel pelo gerenciamento de produtos</p>
<p align="center">* Foi desenvolvido autenticação com JWT</p>

<p align="center">
 <a href="#tecnologias">Tecnologias</a> • 
 <a href="#requisitos">Requisitos</a> • 
 <a href="#funcionalidades">Funcionalidades</a>
</p>

<h4 align="center"> 
	🚧  .NET 6 Finalizado...  🚧
</h4>

### Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [.Net6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Mysql](https://www.webtutorial.com.br/como-instalar-o-mysql-usando-o-docker/): Biblioteca [Pomelo.EntityFrameworkCore.MySql] por ser um projeto simples a escolha do MYSQL se deu por conta da utilização do Docker para instanciar o banco, versão Mysql/8.0.32.
- [AutoMapper](https://docs.automapper.org/en/stable/Getting-started.html) 
- [EntityFrameworkCore](https://learn.microsoft.com/pt-br/ef/core/)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/) - Validar Campos
- [GitFlow](https://www.atlassian.com/br/git/tutorials/comparing-workflows/gitflow-workflow) - Padrão de versionamento

### Requisitos

Antes de começar, você vai precisar ter as seguintes ferramentas:

* MYSQL (instalação local ou Docker): Verifiquei se as configurações no arquivo appsettings.json especificamente na linha 3 estão corretas.
* Visual Studio = preferivel o 2022, ao iniciar o projeto pela primeira vez favor rodar o comando "add-migration (nomedamigration)" e após execute o comando "update-database" no Package Manager COnsole do Visual Studio para gerar o banco de dados inicial.

### Funcionalidades

#### Auth
- `Post/Register`: Efetua o registo do usuário para usar o sistema

- `Post/Auth`: Autentica o usuário na API para uso dos demais EndPoins

Após autenticação é possivel informar o token de autenticação direto no Swagger informando Bearer (Token)

![Screenshot_17](https://github.com/Jucimario/GestaoProdutos/assets/8194957/ebd0a78b-56fc-4441-b3d6-f85673d969a9)

#### Produto

- `Post/AdicionaProduto`: Efetua o registo de um produto, contém validação no campo descricao que deve ser obrigatório e outra validação sobre a DataFabricacao que não pode ser maior ou igual a data de validade

- `Get/ListaProdutos`: Retorna uma lista de produtos de acordo parametros informados, 
     Campos: 
     * Skip(obrigatório): Informa o indice do item a ser buscado apartir do numero informado.
     * Take(Obrigatório): Informa a quantidade de item a ser retornado.
     * nome(Não Obrigtatório): Consulta produtos por nome retornando todos que contenha o nome informado, Caso não seja informado um valor é retornado todos os produtos.

- `Get/ConsultaProdutoID`: Retorna um produto de acordo o ID informado.

- `Put/AtualizaProduto`: Responsável por efetuar a atualização do produto com as mesmas validações do EndPoint "AdicionaProduto"

- `Delete/DeletarProduto`: Efetua o delete lógico de um produto bastando informar o Id do mesmo.


![Screenshot_16](https://github.com/Jucimario/GestaoProdutos/assets/8194957/1c61613b-be85-4a35-aa33-3250886ca4f3)

