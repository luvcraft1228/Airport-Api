using System;
using AirShawi;
namespace AirportConsole
{
    class Program
    {
        static Airports airports;
        static System.Net.Http.HttpClient HttpAirShawi = new System.Net.Http.HttpClient();
        static void Main(string[] args)
        {
            HttpAirShawi.BaseAddress = new Uri("http://localhost/Airport-WebAPI");
            airports = new Airports(HttpAirShawi);
            int choice;
            do
            {
                printMenu();
                choice = getChoice();

                switch (choice)
                {
                    case 0:
                        {
                            Console.WriteLine("erreure. Entrer un chiffre entre 1 et 3");
                            Console.ReadKey();
                        }
                        break;
                    case 1:
                        ListAirport();
                        break;
                    case 2:
                        ModifyAirport();
                        break;


                    default:
                        break;
                }

            } while (choice != 3);
            
        }

        static void ListAirport()
        {
            foreach(Airport airport in airports.GetAll().Result)
            {
                Console.WriteLine($"{airport.IataCode} - {airport.Name}");
            }
            Console.ReadKey();
        }

        static void ModifyAirport()
        {
            Console.WriteLine("modify Airport TODO");
            Console.ReadKey();
        }

        static void printMenu()
        {
            Console.Clear();
            Console.WriteLine("Bienvenue à AirShawi\n"+ 
                "Choisissez une action parmi les suivantes:\n"+
                "1 - Lister les aéroports\n"+
                "2 - Modifier un aéroport\n"+
                "3 - Quitter ");
        }
        
        static int getChoice()
        {
            int input;
            bool check = int.TryParse(Console.ReadLine(), out input);
            if (check) return input;
            return 0;
        }
    }
}
