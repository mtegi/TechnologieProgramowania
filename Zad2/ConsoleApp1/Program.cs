using Library;
using Serializer;
using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. Serializacja wlasna");
            Console.WriteLine("2. Deserializacja wlsana");
            Console.WriteLine("3. Serializacja JSON");
            Console.WriteLine("4. Deserializacja JSON");
            Console.WriteLine("5. Wyjdz");

            int choice = 0;

            DataContext data = new DataContext();
            JSONSerializer jSONSerializer = new JSONSerializer();
            CustomSerializer customSerializer = new CustomSerializer();

            while (choice != 5)
            {
                choice = Console.Read() - '0';
                switch (choice)
                {
                    case 1:
                        string path = GetFile();
                        FileStream fs = File.OpenWrite(path);
                        customSerializer.Serialize(data, fs);
                        Console.WriteLine("Serializacja zakonczona");
                        break;
                    case 2:
                        fs = File.OpenRead(GetFile());
                        data = customSerializer.Deserialize(fs);
                        Console.WriteLine("Deserializacja zakonczona");
                        break;
                    case 3:
                        jSONSerializer.Serialize(GetFile(), data);
                        Console.WriteLine("Serializacja zakonczona");
                        break;
                    case 4:
                        data = jSONSerializer.Deserialize(GetFile());
                        Console.WriteLine("Deserializacja zakonczona");
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static private string GetFile()
        {
            Console.WriteLine("Podaj sciezke do pliku:");
            Console.ReadLine();
            string path = Console.ReadLine();
            return path;
        }
    }
}
