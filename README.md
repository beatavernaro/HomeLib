# 
### **Projeto Final - Tecnicas de Programação**
**Professor: [Ronaldo Senha](https://ronaldosena.com/)**
Para o projeto final você deve criar uma API utilizando o template de projeto 'ASP.NET Core Web API', conforme visto em sala.
Você deve criar 4 endpoints para efetuar as operações de CRUD e ao menos 1 endpoint para voltar alguma forma de análise, como por exemplo algum dado estatístico. Mas sinta-se livre para criar quantos você quiser =)
Você é livre para escolher sua base de dados (e.g.: pokemons, bandas, suas partidas lendárias de xadrez...). Além disso, você deve escolher uma forma de persistência de dados, fique a vontade para escolher salvar em um arquivo ou um banco de dados.
IMPORTANTE: você deve "carregar" toda base de dados e trabalhar as "consultas" usando LINQ. O objetivo não é treinar banco de dados, mas sim os conceitos vistos durante o módulo.
EXTRA: crie um projeto para consumir os dados da API que você acabou de construir. Kudos extras se você conseguir encaixar todos os conceitos vistos em sala, como: filas, pilhas, lambdas...
#
### Solução
Uma aplicação simples que cadastra seus livros pelo código [ISBN](https://www.cblservicos.org.br/isbn/o-que-e-isbn/#:~:text=O%20ISBN%20%28International%20Standard%20Book,como%20livros,%20artigos%20e%20apostilas.) e pode retornar algumas formas de pesquisa no banco de dados.
Console App - 'Front End' Não precisa ter um banco de dados populado
https://github.com/beatavernaro/HomeLib

WEB API - 'BackEnd' https://github.com/beatavernaro/LibHomeFinalProj

ISBN para utilizar de exemplos: 
- 8533613377 (O Senhor dos Anéis - A Sociedade do Anel)
- 8532511015 (Harry Potter e a pedra filosofal)

#
### Como funciona
**Cadastro - Create:**
 1. O usuário digita o ISBN do livro que quer adicionar ao banco de dados;
 2. Utilizando esse ISBN é feita uma busca na API do Google Livros que retorna o 1° resultado para aquele código;
 3. Depois da autorização do usuário, a aplicação salva o livro no banco de dados local para consulta posterior

**Consulta - Read**

 1. As buscas podem ser feitas por autor, ano de publicação, título, categoria ou exibir todos os livros. O retorno é sempre em ordem alfabética.
 2. O usuário seleciona o tipo de busca e entra com os dados pedidos para realizar a consulta
 3. O caminho abaixo é feito:
	 - Console solicita os dados > API faz a busca no banco de dados interno > Retorna todos os resultados encontrados

**Atualização - Update**
Ainda não disponível

**Deletar - Delete**

 1. Usuário procura o livro que quer deletar pelo título e a aplicação retorna todos os livros que encontrar
 2. Através do ID do livro o usuário confirma se quer deletar
 3. Aplicação manda a requisição para a API que deleta o livro do banco de dados
#
### Problemas Conhecidos
Listados por ordem de relevância e futuras atualizações:
- Entradas do usuário sem validações. Para o programa funcionar corretamente é necessário que as entradas sejam exatamente as mesmas propostas pela aplicação;
- Update ainda não está disponível. Está retornando Bad Request por erro de Json;
- Organização das classes e métodos. Organizar melhor as classes seguindo os princípios SOLID;
- Limpeza do código seguindo os princípios de Clean Code;
- Melhoria nos menus e submenus;
