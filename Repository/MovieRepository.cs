using Personal.Movies.API.Interfaces;
using Personal.Movies.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Personal.Movies.API.Repository
{
    public class MovieRepository : IMovieRepository
    {

        readonly IList<Movie> movies = new List<Movie>
        {
            new Movie{Id = Guid.NewGuid(), Name = "The Lion King", Year = 2000},
            new Movie{Id = Guid.NewGuid(), Name = "Frozen II", Year = 2017},
            new Movie{Id = Guid.NewGuid(), Name = "MIB - Men In Black", Year = 2000}
        };

        public IEnumerable<Movie> GetAll()
        {
            return movies;
        }

        public Movie GetById(Guid id)
        {
            return movies.FirstOrDefault(x => x.Id == id);
        }


        public Movie Create(Movie model)
        {
            model.Id = Guid.NewGuid();
            movies.Add(model);
            return model;
        }
        
        public void Update(Guid id, Movie model)
        {
            var movie = GetById(id);
            movie.Id = model.Id;
            movie.Name = model.Name;
            movie.Year = model.Year;
        }

        public void Delete(Guid id)
        {
            movies.Remove(GetById(id));
        }

    }
}
