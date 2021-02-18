using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsORM.Models;


namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.WomensLeague = _context.Leagues
                .Where(l => l.Name.Contains("Women"))
                .ToList();
            ViewBag.HockeyLeague = _context.Leagues
                .Where(t => t.Sport.Contains("Hockey"))
                .ToList();
            ViewBag.noFootballLeague = _context.Leagues
                .Where(t => t.Sport != "Football")
                .ToList();
            ViewBag.ConferenceLeague = _context.Leagues
                .Where(t => t.Name.Contains("Conference"))
                .ToList();
            ViewBag.AtlanticLeague = _context.Leagues
                .Where(t => t.Name.Contains("Atlantic"))
                .ToList();
            ViewBag.DallasTeam = _context.Teams
                .Where(t => t.Location.Contains("Dallas"))
                .ToList();
            ViewBag.RaptorTeam = _context.Teams
                .Where(t => t.TeamName.Contains("Raptor"))
                .ToList();
            ViewBag.CityTeam = _context.Teams
                .Where(t => t.Location.Contains("City"))
                .ToList();
            ViewBag.TeeTeam = _context.Teams
                .Where(t => t.TeamName.Contains("T"))
                .ToList();
            ViewBag.listTeam = _context.Teams
                .OrderBy(t => t.Location)
                .ToList();
            ViewBag.RevOrder = _context.Teams
                .OrderByDescending(t => t.TeamName)
                .ToList();
            ViewBag.CooperPlayer = _context.Players
                .Where(t => t.LastName.Contains("Cooper"))
                .ToList();
            ViewBag.JustJoshin = _context.Players
                .Where(t => t.FirstName.Contains("Joshua"))
                .ToList();
            ViewBag.NotJosh = _context.Players
                .Where(t => t.LastName.Contains("Cooper") && t.FirstName != "Joshua")
                .ToList();
            ViewBag.AlexWyatt = _context.Players
                .Where(t => t.FirstName.Contains("Alexander") || t.FirstName.Contains("Wyatt"))
                .OrderBy(t => t.FirstName)
                .ToList();
            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            ViewBag.atlanticTeams = _context.Teams
                .Where(t => t.LeagueId == 5)
                .ToList();
            ViewBag.playersBoston = _context.Players
                .Where(p => p.TeamId == 2)
                .ToList();
            ViewBag.playersCollegiate = _context.Teams
                .Where(t => t.LeagueId == 2)
                .SelectMany(p => p.CurrentPlayers)
                .ToList();
            ViewBag.playersLopez = _context.Teams
                .Where(t => t.LeagueId == 7)
                .SelectMany(p => p.CurrentPlayers)
                .Where(currentPlayers => currentPlayers.LastName.Contains("Lopez"))
                .ToList();
            ViewBag.allFootballPlayers = _context.Teams
                .Where(t => t.CurrLeague.Sport.Contains("Football"))
                .SelectMany(p => p.CurrentPlayers)
                .OrderBy(u => u.LastName)
                .ToList();
            ViewBag.sophiaTeams = _context.Teams
                .Include(t => t.CurrentPlayers)
                .Where(t => t.CurrentPlayers.Any(p => p.FirstName.Contains("Sophia")));
            ViewBag.leagueSophia = _context.Leagues
                .Include(l => l.Teams)
                .Where(t => t.Teams.Any(p => p.CurrentPlayers
                .Any(player => player.FirstName.Contains("Sophia"))));
            ViewBag.teamFlores = _context.Players
                .Include(l => l.CurrentTeam)
                .Where(player => player.LastName.Contains("Flores") && player.CurrentTeam.TeamName != "Roughriders");   
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            ViewBag.samEvans = _context.Teams
                .Include(t => t.AllPlayers)
                .Include(u => u.CurrentPlayers)
                .Where(play => play.CurrentPlayers.Any(z => z.FirstName.Contains("Samuel") && z.LastName.Contains("Evans") || play.AllPlayers.Any(player => player.PlayerOnTeam.FirstName.Contains("Samuel") && player.PlayerOnTeam.LastName.Contains("Evans"))));
            ViewBag.tigerPlayers = _context.Players
                .Include(t => t.AllTeams)
                .ThenInclude(u => u.TeamOfPlayer)
                .Include(c => c.CurrentTeam)
                .Where(team => team.CurrentTeam.TeamName == "Tiger-Cats" || team.AllTeams.Any(r => r.TeamOfPlayer.TeamName.Contains("Tiger-Cats")));
            ViewBag.xVikings = _context.Players
                .Include(t => t.AllTeams)
                .Where(team => team.AllTeams.Any(player => player.TeamOfPlayer.TeamName == "Vikings"))
                .OrderBy(name => name.LastName);
            ViewBag.xTeamJacob = _context.Teams
                .Include(t => t.AllPlayers)
                .Where(player => player.AllPlayers.Any(player => player.PlayerOnTeam.FirstName.Contains("Jacob") && player.PlayerOnTeam.LastName.Contains("Gray")));
            ViewBag.teamJoshua = _context.Players
                .Include(player => player.CurrentTeam)
                .ThenInclude(team => team.CurrLeague)
                .Where(p => p.FirstName.Contains("Joshua"));
            return View();
        }

    }
}