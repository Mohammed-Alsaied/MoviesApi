
global using MoviesApi.Models;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        //private readonly ApplicationDbContext _context = ApplicationDbContext.Context;

        //public GenresController(ApplicationDbContext context)
        //{
        //    _context = context ??
        //        throw new ArgumentNullException(nameof(context));
        //}

        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }





        [HttpGet] //EndPoint
        public async Task<IActionResult> GetAllAsync()
        {
            //var getGenres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            //return Ok(getGenres);

            var getGenres = await _genresService.GetAll();

            return Ok(getGenres);
        }

        [HttpPost]//EndPoint
        //Dto -> Data Transver Object:Class can Teansver Data From and To Api

        //[FromBody]CreateGenreDto createGenre
        public async Task<IActionResult> CreateAsync([FromBody] MainDto genreDto)////[FromBody]
        {
            //Object Inializer
            //var genre = new Genre { Name = genreDto.Name, };

            ////await _context.Genres.AddAsync(genre);
            //await _context.AddAsync(genre);
            //_context.SaveChanges();//must to update Database

            //return Ok(genre);

            var genre = new Genre { Name = genreDto.Name, };
            await _genresService.Add(genre);
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, MainDto dto)
        {
            //var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            //if (genre == null)
            //    return NotFound($"No genres was found With ID: {id}");

            //genre.Name = dto.Name;
            //_context.SaveChanges();

            //return Ok(genre);

            var genre = await _genresService.GetById(id);
            if (genre == null)
                return NotFound($"No genres was found With ID: {id}");
            genre.Name = dto.Name;
            _genresService.Update(genre);
            return Ok($"{genre.Name} Genre Updated Successfyly");

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            //var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            //if (genre == null)
            //    return NotFound($"No genres was found With ID: {id}");

            //_context.Remove(genre);
            //_context.SaveChanges();
            //return Ok($"{genre.Name} Genre Deleted Successfyly");

            var genre = await _genresService.GetById(id);
            if (genre == null)
                return NotFound($"No genres was found With ID: {id}");
            _genresService.Delete(genre);
            return Ok($"{genre.Name} Genre Deleted Successfyly");

        }
    }
}
