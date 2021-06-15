using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Website.Contracts.Interfaces;
using Website.Contracts.Models;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMoviesService _moviesService;
        private readonly Random _random = new Random();

        public HomeController(ILogger<HomeController> logger, IMoviesService moviesService)
        {
            _logger = logger;
            _moviesService = moviesService;
        }

        [HttpPost]
        public IActionResult Create(string name, string directorName)
        {
            _moviesService.Add(new Movie { Id = _random.Next(), Name = name, DirectorName = directorName });
            var response = $"New movie added: {name} by {directorName}";

            return Content(response);
        }

        [HttpPost]
        public IActionResult Update(int id, string name, string directorName)
        {
            _moviesService.Update(id, name, directorName);
            var response = $"Movie updated: {name} by {directorName}";

            return Content(response);
        }

        /*[HttpPost]
        public IActionResult Delete(int id)
        {
            _moviesService.Delete(id);
            var response = $"Movie with id {id} deleted";

            return Content(response);
        }*/

        public IActionResult Index()
        {
            var movies = _moviesService.GetAll();

            return View(movies);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            var movie = _moviesService.GetById(id);

            return View(movie);
        }

        public IActionResult Delete(int id)
        {
            var movie = _moviesService.GetById(id);

            return View(movie);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}