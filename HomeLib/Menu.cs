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
            Console.WriteLine("O que gostaria de fazer?");
            Console.WriteLine("1 - Cadastrar livro pelo ISBN");
            Console.WriteLine("2 - Consultar livro");

            var option = int.Parse(Console.ReadLine()!);

            switch (option)
            {
                case 1:
                    APICall.Call().GetAwaiter().GetResult();
                    break;
                case 2:
                    Submenu();
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
            Console.WriteLine("5 - Por categoria");

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
                    break;
                case 5:
                    break;
            }



        }
    }
}
