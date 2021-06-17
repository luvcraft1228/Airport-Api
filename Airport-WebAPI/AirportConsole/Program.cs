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
                        ModifyMenu();
                        break;


                    default:
                        break;
                }

            } while (choice != 3);
            
        }

        static void ListAirport()
        {
            Console.Clear();
            Console.WriteLine("Voici la liste des aéroports:\n\n  {0,-5} {1,-14} {2, 3}", "Code", "Ville", "Nom\n");
            foreach (Airport airport in airports.GetAll().Result)
            {
                Console.WriteLine("  {0,-5} {1,-14} {2, 6}", airport.IataCode, airport.City, airport.Name);
                
            }
            Console.ReadKey();
        }

        static bool AirportExists(string key)
        {
            foreach (Airport a in airports.GetAll().Result)
            {
                if (a.IataCode == key.ToUpper()) return true;
            }return false;
        }

        static void ModifyMenu()
        {
            Console.Clear();
            Console.Write("inscrivez le code de l'aeroport que vous désirez modifier : ");
            string key = Console.ReadLine();
            
            if (AirportExists(key))
            {
                Airport airportToModify = airports.GetById(key).Result;
                Console.WriteLine($"Code IATA : {airportToModify.IataCode}\n" +
                    $"Ville : {airportToModify.City}\n" +
                    $"Nom : {airportToModify.Name}\n");

                if (ModifyAirport(airportToModify)) Console.WriteLine("Les modifications ont été apportées avec succès.");
                else Console.WriteLine("Une erreure s'est produite.");
            }
            else
            {
                Console.WriteLine("Ce code n'existe pas. Veuillez reessayer");
                Console.ReadKey();
                ModifyMenu();
            }

            Console.ReadKey();
        }

        static bool ModifyAirport(Airport airportToModify)
        {
            string newName, newCity;
            Console.WriteLine();
            Console.Write("Nouvelle ville : ");
            newCity = Console.ReadLine();
            Console.Write("Nouveau nom : ");
            newName = Console.ReadLine();
            airportToModify.Name = newName;
            airportToModify.City = newCity;
            airports.Put(airportToModify.IataCode, airportToModify);
            return airports.GetById(airportToModify.IataCode).Result.Name == newName ?  true :  false;
            
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
