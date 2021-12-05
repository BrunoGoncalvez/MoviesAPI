using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Movies.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personal.Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        readonly IList<Movie> movies = new List<Movie>
        {
            new Movie{Id = Guid.NewGuid(), Name = "The Lion King", Year = 2000},
            new Movie{Id = Guid.NewGuid(), Name = "Frozen II", Year = 2017},
            new Movie{Id = Guid.NewGuid(), Name = "MIB - Men In Black", Year = 2000}
        };



        [HttpGet]
        public IActionResult Get()
        {
            /* --> IActionResult facilita o tratamento de erros <-- */
            /* Poderiamos trocar IActionResult pelo tipo de retorno, ex: IEnumerable<string> -> dificil tratamento de erros */
            try
            {
                //throw new Exception("Mensagem de erro");
                return Ok(movies);
            }
            catch (Exception e)
            {
                // new {e.Message} cria um objeto anonimo e retorna um json {"message" : "Mensagem de erro"}
                return BadRequest(new { e.Message });
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var movie = movies.FirstOrDefault(x => x.Id == id);

                if (movie == null)
                {
                    return NotFound("Página não encontrada");
                }

                return Ok(movie);
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }


    }
}
