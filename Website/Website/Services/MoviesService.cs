using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Contracts.Interfaces;
using Website.Contracts.Models;

namespace Website.Services
{
    public class MoviesService : IMoviesService
    {
        private static readonly List<Movie> _movies = new List<Movie>();

        public MoviesService()
        {
            _movies.Add(new Movie { Id = 1, Name = "Midnight in Paris", DirectorName = "Woody Allen" });
            _movies.Add(new Movie { Id = 2, Name = "Jackie Brown", DirectorName = "Quentin Tarantino" });
        }

        public IEnumerable<Movie> GetAll()
        {
            return _movies;
        }

        public Movie GetById(int id)
        {
            return _movies.Where(m => m.Id == id).FirstOrDefault();
        }

        public Movie Add(Movie movie)
        {
            _movies.Add(movie);

            return movie;
        }

        public Movie Update(int id, string name, string directorName)
        {
            var movie = GetById(id);

            if (movie != null)
            {
                movie.Name = name;
                movie.DirectorName = directorName;
            }

            return movie;
        }

        public Movie Delete(int id)
        {
            var movie = GetById(id);

            if (movie != null)
            {
                _movies.RemoveAll(m => m.Id == id);
            }

            return movie;
        }
    }
}
