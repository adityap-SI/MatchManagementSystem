using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManagementSystem
{
    internal class MatchManagement
    {

        List<List<MatchDetails>> FilteredMatches = new List<List<MatchDetails>>();
        string[] sports = { "cricket,football","soccer","basketball","tennis","baseball","golf","running","volleyball","badminton","swimming","boxing","table tennis","skiing","ice skating","roller skating","cricket","rugby","pool","darts","football","bowling","ice hockey","surfing","karate","horse racing","snowboarding","skateboarding","cycling","archery","fishing","gymnastics","figure skating","rock climbing","sumo wrestling","taekwondo","fencing","water skiing","jet skiing","weight lifting","scuba diving","judo","wind surfing","kickboxing","sky diving","hang gliding","bungee jumping","lacrosse","polo","wrestling","squash","handball","rowing","sailing" };
        
        List<MatchDetails> Matches = new List<MatchDetails>() {
            new MatchDetails(1, "Cricket", new DateTime(2013, 10, 2), "Mumbai", "MMI", "CSK", 100, 223),
            new MatchDetails(2, "Football", new DateTime(2001, 10, 2), "Delhi", "FCB", "RMR", 1, 2),
            new MatchDetails(3, "Volleyball", new DateTime(2017, 10, 2), "Mumbai", "BVB", "KUI", 10, 23),
            new MatchDetails(4, "Squash", new DateTime(2010, 10, 2), "Ahmedabad", "BYM", "KKR", 13, 23),
            new MatchDetails(5, "BasketBall", new DateTime(2010, 10, 2), "Mumbai", "CSK", "MIM", 12, 23),
            new MatchDetails(6, "Cricket", new DateTime(2010, 10, 2), "Mumbai", "PKS", "RRS", 180, 200),
    };

        public void AddMatch()
        {
            var ids = from match in Matches select match.Matchid;
            try
            {
                Console.WriteLine("Enter MatchId : ");
                uint idu = uint.Parse(Console.ReadLine());
                int id = Convert.ToInt32(idu);
                if (ids.Contains(id))
                {
                    Console.WriteLine("Match with this Id already exists");
                    return;
                }
                Console.WriteLine("Enter Sport : ");
                string sport = Console.ReadLine();
                if(sport.Count()== 0)
                {
                    Console.WriteLine("Sport can't be empty");
                    return;
                }
                else if (!sports.Any(s=>s.ToUpper() == sport.ToUpper()))
                {
                    Console.WriteLine("Invalid sport");
                    return;
                }
                Console.WriteLine("Enter Datetime (eg : DD/MM/YYYY) :");
                DateTime date = DateTime.Parse(Console.ReadLine());
                if (date <= DateTime.Now)
                {
                    Console.WriteLine("Date must be in future");
                    return;
                }
                Console.WriteLine("Enter Location : ");
                string loc = Console.ReadLine();
                if (loc.Count()== 0)
                {
                    Console.WriteLine("Location can't be empty");
                    return;
                }
                Console.WriteLine("Enter Home Team : ");
                string ht = Console.ReadLine();
                if (ht.Count() == 0)
                {
                    Console.WriteLine("Home Team can't be empty");
                    return;
                }
                Console.WriteLine("Enter Away Team : ");
                string at = Console.ReadLine();
                if (at.Count() == 0)
                {
                    Console.WriteLine("Away Team can't be empty");
                    return;
                }
                if (ht == at)
                {
                    Console.WriteLine("Home and Away team cant be same");
                    return;
                }
                Console.WriteLine("Enter Home Team Score :");
                uint hts = uint.Parse(Console.ReadLine());
                Console.WriteLine("Enter Away Team Score :");
                uint ats = uint.Parse(Console.ReadLine());

                Matches.Add(new MatchDetails(id, sport, date, loc, ht, at, hts, ats));
            }
            catch(Exception ex) { Console.WriteLine("Invalid input"); }
        }

        public void Sortbydate(int order)
        {
            if (order == 1)
            {
                Matches = (from matches in Matches orderby matches.MatchDatetime select matches).ToList();
            }
            else
            {
                Matches = (from matches in Matches orderby matches.MatchDatetime descending select matches).ToList();
            }
        }

        public void SortbyVenue(int order)
        {
            if (order == 1)
            {
                Matches = (from matches in Matches orderby matches.Location select matches).ToList();
            }
            else
            {
                Matches = (from matches in Matches orderby matches.Location descending select matches).ToList();
            }
        }


        public void SortbySport(int order)
        {
            if (order == 1)
            {
                Matches = (from matches in Matches orderby matches.Sport select matches).ToList();
            }
            else
            {
                Matches = (from matches in Matches orderby matches.Sport descending select matches).ToList();
            }
        }

        public List<MatchDetails> FilterbySport(string inp)
        {
            inp = inp.ToUpper();   
            var sp = from matches in Matches where (matches.Sport).ToUpper().Equals(inp) select matches;

            return sp.ToList();
            
        }

        public void FilterbyVenue()
        {
            Console.WriteLine("Enter Venue name : ");
            string inp = Console.ReadLine().ToUpper();
            var sp = from matches in Matches where (matches.Location).ToUpper().Equals(inp) select matches;

            Display(sp.ToList());

        }
        
        public void SearchbyKeyword()
        {
            
            List<MatchDetails> ms;
            string c;

            
                    Console.WriteLine("Enter keyword :");
                    c = Console.ReadLine();
                    c = c.ToLower();

                    ms = (from match in Matches 
                          where ((match.AwayTeam).ToLower().Contains(c)) || ((match.HomeTeam).ToLower().Contains(c)) || ((match.Sport).ToLower().Contains(c)) || ((match.Location).ToLower().Contains(c))
                          select match).ToList();
            if (ms.Count > 0)
            {
                Display(ms);
            }
            else
            {
                Console.WriteLine("No matches found");
            }
                    
                    
            

        }

        public void FilterbyDate()
        {
     
            Console.WriteLine("Enter start date(format : DD/MM/YYYY :");
            DateTime startdate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter end date(format : DD/MM/YYYY :");
            DateTime enddate = DateTime.Parse(Console.ReadLine());

            var sp = from matches in Matches where matches.MatchDatetime <= enddate && matches.MatchDatetime >= startdate select matches;
            Display(sp.ToList());
        }

       
        public void StatsbySport()
        {
            var sports = (from matches in Matches
                         select matches.Sport).Distinct();
            Dictionary<string, List<double>> sportscore = new Dictionary<string, List<double>>();
            


            foreach (var sport in sports)
            {
                double average = (from match in Matches where match.Sport.Equals(sport) select Convert.ToInt32(match.HomeTeamScore + match.AwayTeamScore)/2).Average();
                double max = (from match in Matches where match.Sport.Equals(sport) select (match.HomeTeamScore)).Max();
                double min = (from match in Matches where match.Sport.Equals(sport) select (match.HomeTeamScore)).Min();

                sportscore.Add(sport.ToString(),new List<double> { average,max,min});
            }

            
            foreach (var item in sportscore)
            {
                
                Console.WriteLine($"{item.Key} has average score of {item.Value[0]} max score of {item.Value[1]} min score of {item.Value[2]}");
            }

        }

        public void StatsbyTeam()
        {
            var hometeam = (from match in Matches select (match.HomeTeam)).Distinct();
            var awayteam = (from match in Matches select (match.AwayTeam)).Distinct();
            var teams = hometeam.Union(awayteam).ToList();
            double averagehome=0;
            double maxhome=0;
            double minhome=0;
            double averageaway = 0;
            double maxaway = 0;
            double minaway = 0;

            Dictionary<string, List<double>> teamscore = new Dictionary<string, List<double>>();

            foreach (var team in teams)
            {
                try
                {
                    averagehome = (from match in Matches where (match.HomeTeam.Equals(team)) select Convert.ToInt32(match.HomeTeamScore)).Average();
                    maxhome = (from match in Matches where (match.HomeTeam.Equals(team)) select (match.HomeTeamScore)).Max();
                    minhome = (from match in Matches where (match.HomeTeam.Equals(team)) select (match.HomeTeamScore)).Min();
                }
                catch
                {
                    ;
                }
                Console.WriteLine(1);
                
                try{
                    averageaway = (from match in Matches where (match.AwayTeam.Equals(team)) select Convert.ToInt32(match.AwayTeamScore)).Average();
                    maxaway = (from match in Matches where (match.AwayTeam.Equals(team)) select (match.AwayTeamScore)).Max();
                    minaway = (from match in Matches where (match.AwayTeam.Equals(team)) select (match.AwayTeamScore)).Min();
                }
                catch
                {
                    ;
                }

                teamscore.Add(team.ToString(), new List<double> { averagehome,averageaway, maxhome,maxaway, minhome,minaway });
                averagehome = 0;
                maxhome = 0;
                minhome = 0;
                averageaway = 0;
                maxaway = 0;
                minaway = 0;

            }

            Console.WriteLine("Team name\t Home Average\t Away Average\t Home Max\t Away Max\t Home min\t Away min");
            foreach (var item in teamscore)
            {
                
                Console.WriteLine($"{item.Key}\t\t  {item.Value[0]}\t\t {item.Value[1]}\t\t  {item.Value[2]}\t\t  {item.Value[3]}\t\t  {item.Value[4]}\t\t {item.Value[5]}");
            }
        }

        public dynamic Search(int id)
        {
            foreach (MatchDetails details in Matches)
            {
                if(details.Matchid == id)
                {
                    return details;
                }
            }

            return "No such Match Found";
        }

        public void Display(List<MatchDetails> matchlist)
        {
            foreach (var details in matchlist)
            {

                Console.WriteLine(details.Details());
                Console.WriteLine("-------------------");
            }

        }
        public void DisplayAll()
        {
            foreach (var details in Matches)
            {
               
                Console.WriteLine(details.Details());
                Console.WriteLine("-------------------");
            }
        }

        public void RemoveMatch(int id)
        {
            try
            {
                Matches.Remove(this.Search(id));
                Console.WriteLine($"Match with id {id} removed");
            }catch (Exception ex)
            {
                Console.WriteLine("Match not found");
            }
            
        }

        
     
        
        


    }
}
