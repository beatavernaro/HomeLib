namespace HomeLib;

public class Menu
{
    public static void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("O que gostaria de fazer?\r\n");
        Console.WriteLine("1 - Cadastrar livro");
        Console.WriteLine("2 - Consultar livro");
        Console.WriteLine("3 - Deletar um livro");
        Console.WriteLine("4 - Atualizar um livro");
        Console.WriteLine("5 - Sair");

        _ = int.TryParse(Console.ReadLine()!, out int option);
        while (option == 0 || option > 5)
        {
            Console.WriteLine("OPÇÃO INVÁLIDA");
            Thread.Sleep(1000);
            MainMenu();
        }
        Console.Clear();
        switch (option)
        {
            case 1:
                SubmenuPost();
                break;
            case 2:
                SubmenuGet();
                break;
            case 3:
                ManipulateBook.DeleteOneBook().GetAwaiter().GetResult();
                break;
            case 4:
                ManipulateBook.UpdateOneBook().GetAwaiter().GetResult();
                break;
            case 5:
                Console.WriteLine("Obrigada!");
                Environment.Exit(1);
                break;
        }

    }


    public static void SubmenuGet()
    {
        Console.Clear();
        Console.WriteLine("CONSULTAR: ");
        Console.WriteLine("1 - Por ordem alfabética");
        Console.WriteLine("2 - Por ano de lançamento");
        Console.WriteLine("3 - Por autor");
        Console.WriteLine("4 - Por título");
        Console.WriteLine("5 - Voltar");


        _ = int.TryParse(Console.ReadLine()!, out int option);

        while (option == 0 || option > 5)
        {
            Console.WriteLine("OPÇÃO INVÁLIDA");
            Thread.Sleep(1000);
            SubmenuGet();
        }
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

    public static void SubmenuPost()
    {
        Console.Clear();
        Console.WriteLine("1 - Cadastrar pelo ISBN");
        Console.WriteLine("2 - Cadastrar Manualmente");

        _ = int.TryParse(Console.ReadLine()!, out int option);
        while (option == 0 || option > 2)
        {
            Console.WriteLine("OPÇÃO INVÁLIDA");
            Thread.Sleep(1000);
            SubmenuPost();
        }
        Console.Clear();

        switch (option)
        {
            case 1:
                APICall.Call().GetAwaiter().GetResult();
                break;
            case 2:
                APICall.EnterBook().GetAwaiter().GetResult();
                break;

        }
    }

    public static void GoBack()
    {
        Console.WriteLine("\r\nPressione qualquer tecla para voltar ao menu inicial");
        Console.ReadKey();
        MainMenu();
    }
}
