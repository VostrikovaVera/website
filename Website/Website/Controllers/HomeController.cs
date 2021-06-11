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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _moviesService.Delete(id);

            var response = $"Movie with id {id} was deleted";

            return Content(response);
        }

        [HttpPost]
        public IActionResult Post(string name, string directorName)
        {
            _moviesService.Add(new Movie { Id = _random.Next(), Name = name, DirectorName = directorName });
            var response = $"New movie added {name} by {directorName}";

            return Content(response);
        }

        [HttpPut]
        public IActionResult Put(int id, string name, string directorName)
        {
            var updatedMovie = _moviesService.Update(id, name, directorName);

            return updatedMovie is null ? NotFound($"Movie with id {id} was not found") : Ok(updatedMovie);
        }

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


/*[ApiController]
[Route("[controller]")]
public class ManageController : ControllerBase
{
    private readonly ILogger<ManageController> _logger;
    private readonly IMoviesService _moviesService;
    private readonly Random _random = new Random();

    public ManageController(ILogger<ManageController> logger, IMoviesService moviesService)
    {
        _logger = logger;
        _moviesService = moviesService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var movies = _moviesService.GetAll();

        return movies is null ? NotFound("Movies not found") : Ok(movies.ToArray());
    }

    [HttpGet]
    [Route("/[controller]/{id}")]
    public IActionResult Get([FromRoute] int id)
    {
        var movie = _moviesService.GetById(id);

        return movie is null ? NotFound($"Movie with id {id} was not found") : Ok(movie);
    }

    [HttpPost]
    public IActionResult Post([FromBody] MovieRequest request)
    {
        var response = _moviesService.Add(new Movie { Id = _random.Next(), Name = request.Name, DirectorName = request.DirectorName });

        return Ok(response);
    }

    [HttpPut]
    [Route("/[controller]/{id}")]
    public IActionResult Put([FromRoute] int id, [FromBody] MovieRequest request)
    {
        var updatedMovie = _moviesService.Update(id, request);

        return updatedMovie is null ? NotFound($"Movie with id {id} was not found") : Ok(updatedMovie);
    }

    [HttpDelete]
    [Route("/[controller]/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var deletedMovie = _moviesService.Delete(id);

        return deletedMovie is null ? NotFound($"Movie with id {id} was not found") : Ok();
    }
}*/
