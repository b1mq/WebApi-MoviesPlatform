using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Dtos;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmService _filmService;
        public FilmsController(IFilmService filmService)
        {
            _filmService = filmService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetAll()
        {
            var res = await _filmService.GetAllFilmsAsync();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Film?>> GetFilmById(int id)
        {
            var res = await _filmService.GetFilmByIdAsync(id);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFilm([FromBody] CreateFilmDto dto)
        {
            var result = await _filmService.AddFilmAsync(dto);
            if(!result.isSuccess)
            {
                return BadRequest(result.Error);  
            }
            return Ok(new { message = "Film added succesfull" });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var res = await _filmService.DeleteFilmAsync(id);
            if(!res.isSuccess)
            {
                return BadRequest(res.Error);
            }
            return Ok(new { message = "Film succesfull deleted" });
        }


    }
}
