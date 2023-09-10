namespace MatchManagementSystem
{
    internal class Program
    {

        public static void SwitchStatement()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("1 for displaying details of all");
            Console.WriteLine("2 for searching details of team");
            Console.WriteLine("3 for removing team");
            Console.WriteLine("4 for updating score");
            Console.WriteLine("5 for searching by sport");
            Console.WriteLine("6 for searching by location");
            Console.WriteLine("7 for searching by date range");
            Console.WriteLine("8 for getting statistics by sport");
            Console.WriteLine("9 for getting statistics by team");
            Console.WriteLine("10 to search by keyword");
            Console.WriteLine("11 to Add Match");


            Console.Write("\nEnter Choice : ");
        }
        public static void Main(string[] args)
        {
            MatchManagement matchManagement = new MatchManagement();
            Console.WriteLine("-----Welcome-----");
            int choice=-1;
            bool run = true;
            int nhs = 0;
            int nas = 0;
            

            while (run)
            {
                SwitchStatement();

                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    choice = 100;
                }
                Console.WriteLine("------------------------------");
                switch (choice)
                {
                    case 0:
                        run = false;
                        break;
                    case 1:
                        matchManagement.DisplayAll();
                        break;
                    case 2:
                        Console.Write("Enter Id : ");

                        
                        
                        var match = matchManagement.Search(int.Parse(Console.ReadLine()));
                        Console.WriteLine("-----------");
                        if (match.GetType() == typeof(string))
                        {
                            Console.WriteLine(match);
                        }
                        else
                        {
                            Console.WriteLine(match.Display());
                        }

                        break;
                    case 3:
                        Console.Write("Enter Id : ");


                        matchManagement.RemoveMatch(int.Parse(Console.ReadLine()));

                        break;
                    case 4:
                        Console.Write("Enter Id : ");

                        

                        match = matchManagement.Search(int.Parse(Console.ReadLine()));
                        Console.WriteLine("-----------");
                        if (match.GetType() == typeof(string))
                        {
                            Console.WriteLine(match);
                        }
                        else
                        {
                            Console.WriteLine("Enter Home Score ");
                            nhs = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Away Score : ");
                            nas = int.Parse(Console.ReadLine());
                            match.UpdateScore(nhs,nas);
                        }

                        break;
                    case 5:
                        Console.WriteLine("Enter Sport name : ");
                        string inp = Console.ReadLine().ToUpper();
                        matchManagement.Display(matchManagement.FilterbySport(inp));
                        break;
                    case 6:
                        matchManagement.FilterbyVenue();
                        break;
                    case 7:
                        try
                        {
                            matchManagement.FilterbyDate();
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Wrong Date Format or invalid input");
                        }
                        break;
                    case 8:

                        matchManagement.StatsbySport();
                        break;
                    case 9:
                        matchManagement.StatsbyTeam();
                        break;
                    case 10:
                        matchManagement.SearchbyKeyword();
                        break;
                    case 11:
                        matchManagement.AddMatch();
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
            Console.WriteLine("Byeee");
            
            
            
  

        }
    }
}