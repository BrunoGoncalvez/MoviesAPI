using Personal.Movies.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personal.Movies.API.Interfaces
{
    public interface IMovieRepository
    {

        IEnumerable<Movie> GetAll();

        Movie GetById(Guid id);

        Movie Create(Movie model);

        void Update(Guid id, Movie model);

        void Delete(Guid id);

    }
}
