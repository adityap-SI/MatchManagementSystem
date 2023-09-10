using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManagementSystem
{
    internal class MatchDetails
    {
        int matchid;
        public string Sport;
        public DateTime MatchDatetime;
        public string Location { get; }
        public string HomeTeam;
        public string AwayTeam;
        uint homeTeamScore;
        uint awayTeamScore;

        public int Matchid { get { return matchid;} }
        public uint HomeTeamScore { get { return homeTeamScore;} set { homeTeamScore = value; } }
        public uint AwayTeamScore { get { return awayTeamScore; } set { awayTeamScore = value; } }


        public MatchDetails() { }
        public MatchDetails(int id, string sport, DateTime date, string location, string hometeam, string awayteam,uint homescore,uint awayteamscore) {
            matchid = id;
            Sport = sport;
            MatchDatetime = date;
            Location = location;
            HomeTeam = hometeam;
            AwayTeam = awayteam;
            homeTeamScore = homescore;
            awayTeamScore = awayteamscore;
        
        }

        public void UpdateScore(uint newhs,uint newas)
        {
            this.HomeTeamScore = newhs;
            this.AwayTeamScore = newas;
        }

        
        public string Details()
        {
            return $"Match ID : {Matchid}\nSport : {Sport}\nDate : {MatchDatetime}\nVenue : {Location}\nHome Team : {HomeTeam}\nAway Team : {AwayTeam}\nHome Score : {homeTeamScore}\nAway Score : {awayTeamScore}";
        }
    }
}
