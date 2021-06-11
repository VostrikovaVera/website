using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Contracts.Models;

namespace Website.Contracts.Interfaces
{
    public interface IMoviesService
    {
        public IEnumerable<Movie> GetAll();

        public Movie GetById(int id);

        public Movie Add(Movie movie);

        public Movie Update(int id, string name, string directorName);

        public Movie Delete(int id);
    }
}
