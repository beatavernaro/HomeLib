using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLib
{
    public class Menu
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("O que gostaria de fazer?\r\n");
            Console.WriteLine("1 - Cadastrar livro pelo ISBN");
            Console.WriteLine("2 - Consultar livro");
            Console.WriteLine("3 - Deletar um livro");
            Console.WriteLine("4 - Atualizar um livro");
            Console.WriteLine("5 - Sair");

            var option = int.Parse(Console.ReadLine()!);
            Console.Clear();
            switch (option)
            {
                case 1:
                    APICall.Call().GetAwaiter().GetResult();
                    break;
                case 2:
                    Submenu();
                    break;
                case 3:
                    FindBook.DeleteOneBook().GetAwaiter().GetResult();
                    break;
                case 4:
                    FindBook.UpdateOneBook().GetAwaiter().GetResult();
                    break;
                case 5:
                    Console.WriteLine("Obrigada!");
                    Environment.Exit(1);
                    break;
            }
        }

        public static void Submenu()
        {
            Console.Clear();
            Console.WriteLine("CONSULTAR: ");
            Console.WriteLine("1 - Por ordem alfabética");
            Console.WriteLine("2 - Por ano de lançamento");
            Console.WriteLine("3 - Por autor");
            Console.WriteLine("4 - Por título");
            Console.WriteLine("5 - Voltar");


            var option = int.Parse(Console.ReadLine()!);
            Console.Clear();

            switch (option)
            {
                case 1:
                    FindBook.GetAll().GetAwaiter().GetResult();
                    break;

                case 2:
                    Console.Write("Digite o ano: ");
                    var year = Console.ReadLine()!;
                    FindBook.ShowBookByYear(year).GetAwaiter().GetResult();
                    break;

                case 3:
                    Console.Write("Digite o autor: ");
                    var name = Console.ReadLine()!;
                    FindBook.ShowBookByAuthor(name).GetAwaiter().GetResult();
                    break;

                case 4:
                    Console.Write("Digite o título: ");
                    var title = Console.ReadLine()!;
                    FindBook.ShowBookByTitle(title).GetAwaiter().GetResult();
                    break;
                case 5:
                    MainMenu();
                    break;
            }
        }
    }
}
