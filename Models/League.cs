using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SportsORM.Models
{
    public class League
    {
        [Key]
        public int LeagueId {get;set;}
        public string Name {get;set;}
        public string Sport {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<Team> Teams {get;set;}
    }
}