using Library;
using Filler;
using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    { 
        static private void Menu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. Serializacja wlasna");
            Console.WriteLine("2. Deserializacja wlsana");
            Console.WriteLine("3. Serializacja JSON");
            Console.WriteLine("4. Deserializacja JSON");
            Console.WriteLine("5. Wyjdz");
        }
        static void Main(string[] args)
        {   
            int choice = 0;

            DataContext data = new DataContext();
            JSONSerializer jSONSerializer = new JSONSerializer();
            CustomSerializer customSerializer = new CustomSerializer();
            ContextFiller filler = new ContextFiller();
            filler.Fill(data);

            while (choice != 5)
            {
                Menu();
                Console.Write("Wybieram: ");
                choice = Console.Read() - '0';
                switch (choice)
                {
                    case 1:
                        string path = GetFile();
                        using (FileStream stream = File.Open(path, FileMode.OpenOrCreate))
                        {
                            customSerializer.Serialize(data, stream);
                        }
                        Console.WriteLine("Serializacja wlasna zakonczona");
                        break;
                    case 2:
                        path = GetFile();
                        if (File.Exists(path))
                        {
                            using (FileStream stream = File.Open(path, FileMode.Open))
                            {
                                data = new DataContext(customSerializer.Deserialize(stream));
                                Console.WriteLine("Deserializacja wlasna zakonczona, liczba event�w: " + data.Events.Count.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Plik nie istnieje");
                        }
                        break;
                    case 3:
                        path = GetFile();
                        jSONSerializer.Serialize(path, data);
                        Console.WriteLine("Serializacja JSON zakonczona");
                        break;
                    case 4:
                        path = GetFile();
                        if (File.Exists(path))
                        {
                            data = jSONSerializer.Deserialize(path);
                            Console.WriteLine("Deserializacja JSON zakonczona");
                        }
                        else
                        {
                            Console.WriteLine("Plik nie istnieje");
                        }
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Blad");
                        break;
                }
            }
        }

        static private string GetFile()
        {
            Console.WriteLine("Podaj sciezke do pliku:");
            Console.ReadLine();
            string path = Console.ReadLine();

            if (String.IsNullOrEmpty(path))
                path = "TP_SERIALIZATION_DEFAULT";

            return path;
        }

       
    }


   
}
