using Microsoft.AspNetCore.Mvc;
using Personal.Movies.API.Interfaces;
using Personal.Movies.API.Models;
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


        [HttpPost]
        public IActionResult Post([FromBody]Movie model)
        {
            try
            {
                var movie = _repository.Create(model);
                return Created($"api/movies/{movie.Id}", movie);
            }
            catch (Exception e)
            {

                return BadRequest(new { e.Message });
            }
        }

        //[HttpPut] -> Cria se não existe ou Alterar se objeto já existe
        //[HttpPatch] -> Alterar parcial objeto já existente
        [HttpPatch]
        public IActionResult Patch(Guid id, [FromBody]Movie movie)
        {
            // Guid id -> Via Url | movie -> Via Body
            try
            {
                _repository.Update(id, movie);
                return NoContent(); // NoContent() -> Realizado com sucesso e sem retorno
            }
            catch (Exception e)
            {

                return BadRequest(new { e.Message });
            }

        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _repository.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(new { e.Message });
            }
        }



    }
}
