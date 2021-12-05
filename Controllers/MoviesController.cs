using Microsoft.AspNetCore.Mvc;
using Personal.Movies.API.Interfaces;
using System;

namespace Personal.Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieRepository _repository;

        public MoviesController(IMovieRepository repository)
        {

            // Toda vez que MovieController for chamado irá instaciar o repository IMovieRepository na variável local _repository
            _repository = repository;

        }


        [HttpGet]
        public IActionResult Get()
        {
            /* --> IActionResult facilita o tratamento de erros <-- */
            /* Poderiamos trocar IActionResult pelo tipo de retorno, ex: IEnumerable<string> -> dificil tratamento de erros */
            try
            {
                var movies = _repository.GetAll();
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
                var movie = _repository.GetById(id);

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
