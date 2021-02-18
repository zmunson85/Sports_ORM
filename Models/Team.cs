using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SportsORM.Models
{
    public class Team
    {
        [Key]
        public int TeamId {get;set;}
        public string Location {get;set;}
        public string TeamName {get;set;}
        public int LeagueId {get;set;}
        public League CurrLeague {get;set;}
        public List<Player> CurrentPlayers {get;set;}
        public List<PlayerTeam> AllPlayers {get;set;}
    }
}