using Library;
using Filler;
using System;
using System.IO;
using DummyClasses;

namespace ConsoleApp
{
    class Program
    { 
        static private void Menu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. Serializacja wlasna");
            Console.WriteLine("2. Deserializacja wlasna");
            Console.WriteLine("3. Serializacja JSON");
            Console.WriteLine("4. Deserializacja JSON");
            Console.WriteLine("5. Wyjdz");
        }
        static void Main(string[] args)
        {   
            int choice = 0;

            CustomFormatter customFormatter = new CustomFormatter();
            JSONSerializer jSONSerializer = new JSONSerializer();
            DummyClassA a = new DummyClassA();
            a.Id = 1;

            DummyClassB b = new DummyClassB();
            b.Id = 2;

            DummyClassC c = new DummyClassC();
            c.Id = 3;

            a.Other = b;
            b.Other = c;
            c.Other = a;



            b.Text = "HELLO";
            c.Time = new DateTime(2020, 1, 1);

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
                            customFormatter.Serialize(stream,a);
                        }
                        Console.WriteLine("Serializacja wlasna zakonczona");
                        break;
                    case 2:
                        path = GetFile();
                        if (File.Exists(path))
                        {
                            using (FileStream stream = File.Open(path, FileMode.Open))
                            {
                                a = (DummyClassA) customFormatter.Deserialize(stream);
                                Console.WriteLine("Deserializacja wlasna zakonczona");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Plik nie istnieje");
                        }
                        break;
                    case 3:
                        path = GetFile();
                        jSONSerializer.Serialize(path,a);
                        Console.WriteLine("Serializacja JSON zakonczona");
                        break;
                    case 4:
                        path = GetFile();
                        if (File.Exists(path))
                        {
                            a = jSONSerializer.Deserialize<DummyClassA>(path);
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
                path = "TP_SERIALIZTON_DEFAULT";

            return path;
        }

       
    }


   
}
