using AutoMapper;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        #region Before Adding Services
        //private readonly ApplicationDbContext _context = ApplicationDbContext.Context;

        //public MoviesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        #endregion

        #region After Adding Services
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;

        public MoviesController(IGenresService genresService, IMoviesService moviesService, IMapper mapper)
        {
            _genresService = genresService;
            _moviesService = moviesService;
            _mapper = mapper;
        }

        private readonly new List<string> _allowedExtensions = new List<string> { ".jpg", ".png" };
        private readonly long _maxAllowedPosterSize = 1048576;
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            #region Before Adding Services

            //var movies = await _context.Movies
            //    .OrderByDescending(m => m.Rate)
            //    .Include(m => m.Genre)
            //    .Select(m => new MoviesDetailsDto
            //    {
            //        Id = m.Id,
            //        GenreId = m.GenreId,
            //        Title = m.Title,
            //        Rate = m.Rate,
            //        Year = m.Year,
            //        Storeline = m.Storeline,
            //        Poster = m.Poster,
            //        GenreName = m.Genre.Name
            //    })
            //    .ToListAsync();
            //return Ok(movies);
            #endregion

            #region After Adding Services

            //TODO:Map Movies to Dto

            var movies = await _moviesService.GetAll();
            var data = _mapper.Map<IEnumerable<MoviesDetailsDto>>(movies);
            return Ok(data);
            #endregion

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            #region Before Adding Services

            //var movie = await _context.Movies
            //    .Include(m => m.Genre)
            //    .SingleOrDefaultAsync(m => m.Id == id);
            //if (movie == null)
            //{
            //    return NotFound("Movie Id Not Found");
            //}

            //var dto = new MoviesDetailsDto
            //{
            //    Id = movie.Id,
            //    GenreId = movie.GenreId,
            //    Title = movie.Title,
            //    Rate = movie.Rate,
            //    Year = movie.Year,
            //    Storeline = movie.Storeline,
            //    Poster = movie.Poster,
            //    GenreName = movie.Genre?.Name
            //};
            ////var movie = await _context.Movies.ToListAsync();

            //return Ok(movie);
            #endregion

            #region After Adding Services

            var movie = await _moviesService.GetById(id);
            if (movie == null)
            {
                return NotFound("Movie Id Not Found");
            }

            var dto = _mapper.Map<MoviesDetailsDto>(movie);
            //var movie = await _context.Movies.ToListAsync();

            return Ok(dto);
            #endregion

        }

        [HttpGet("GetByGenreId")]

        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            #region Before Adding Services

            //var movies = await _context.Movies
            //    .Where(m => m.GenreId == genreId)
            //    .OrderByDescending(m => m.Rate)
            //    .Include(m => m.Genre)
            //    .Select(m => new MoviesDetailsDto
            //    {
            //        Id = m.Id,
            //        GenreId = m.GenreId,
            //        Title = m.Title,
            //        Rate = m.Rate,
            //        Year = m.Year,
            //        Storeline = m.Storeline,
            //        Poster = m.Poster,
            //        GenreName = m.Genre.Name
            //    })
            //    .ToListAsync();
            //return Ok(movies);
            #endregion


            #region After Adding Services

            var movies = await _moviesService.GetAll();
            var data = _mapper.Map<IEnumerable<MoviesDetailsDto>>(movies);
            return Ok(data);
            #endregion


        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MoviesDto moviesDto)
        {
            #region Before Adding Services

            //if (moviesDto.Poster == null)
            //    return BadRequest("Poster is Required");
            //if (!_allowedExtensions.Contains(Path.GetExtension(moviesDto.Poster.FileName).ToLower()))
            //    return BadRequest("Only .png and .jpg are allowed!");

            //if (moviesDto.Poster.Length > _maxAllowedPosterSize)
            //    return BadRequest(" Max allowed size for Poster is 1MB");

            //var isValidGenre = await _context.Genres.AnyAsync(g => g.Id == moviesDto.GenreId);
            //if (!isValidGenre)
            //    return BadRequest(" Invalid Genre Id");

            ////Convert from IformFile to Byte[]
            //using var datastream = new MemoryStream();
            //await moviesDto.Poster.CopyToAsync(datastream);
            //var movie = new Movie
            //{
            //    GenreId = moviesDto.GenreId,
            //    Poster = datastream.ToArray(),
            //    Rate = moviesDto.Rate,
            //    Storeline = moviesDto.Storeline,
            //    Title = moviesDto.Title,
            //    Year = moviesDto.Year,
            //};

            //await _context.AddAsync(movie);
            //_context.SaveChanges();
            //return Ok($"{movie.Title} Created Successfully");
            #endregion


            #region After Adding Services

            if (moviesDto.Poster == null)
                return BadRequest("Poster is Required");
            if (!_allowedExtensions.Contains(Path.GetExtension(moviesDto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg are allowed!");

            if (moviesDto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest(" Max allowed size for Poster is 1MB");

            var isValidGenre = await _genresService.IsValidGenre(moviesDto.GenreId);
            if (!isValidGenre)
                return BadRequest(" Invalid Genre Id");

            //Convert from IformFile to Byte[]
            using var datastream = new MemoryStream();
            await moviesDto.Poster.CopyToAsync(datastream);

            var movie = _mapper.Map<Movie>(moviesDto);
            movie.Poster = datastream.ToArray();
            _moviesService.Add(movie);
            return Ok($"{movie.Title} Created Successfully");
            #endregion

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MoviesDto moviesDto)
        {
            #region Before Adding Services

            //var movie = await _context.Movies.FindAsync(id);
            //if (movie == null)
            //    return NotFound($"No Movies was found With ID: {id}");
            //var isValidGenre = await _context.Genres.AnyAsync(g => g.Id == moviesDto.GenreId);
            //if (!isValidGenre)
            //    return BadRequest(" Invalid Genre Id");


            //if (moviesDto.Poster != null)
            //{
            //    if (!_allowedExtensions.Contains(Path.GetExtension(moviesDto.Poster.FileName).ToLower()))
            //        return BadRequest("Only .png and .jpg are allowed!");

            //    if (moviesDto.Poster.Length > _maxAllowedPosterSize)
            //        return BadRequest(" Max allowed size for Poster is 1MB");

            //    using var datastream = new MemoryStream();
            //    await moviesDto.Poster.CopyToAsync(datastream);
            //    movie.Poster = datastream.ToArray();
            //}


            //movie.Title = moviesDto.Title;
            //movie.Year = moviesDto.Year;
            //movie.Storeline = moviesDto.Storeline;
            //movie.Year = moviesDto.Year;
            //movie.Rate = moviesDto.Rate;


            //_context.SaveChanges();

            //return Ok($"{movie.Title} Updated Successfully");
            #endregion

            #region After Adding Services

            var movie = await _moviesService.GetById(id);
            if (movie == null)
                return NotFound($"No Movies was found With ID: {id}");
            var isValidGenre = await _genresService.IsValidGenre(moviesDto.GenreId); ;
            if (!isValidGenre)
                return BadRequest(" Invalid Genre Id");


            if (moviesDto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(moviesDto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg are allowed!");

                if (moviesDto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest(" Max allowed size for Poster is 1MB");

                using var datastream = new MemoryStream();
                await moviesDto.Poster.CopyToAsync(datastream);
                movie.Poster = datastream.ToArray();
            }


            movie.Title = moviesDto.Title;
            movie.Year = moviesDto.Year;
            movie.Storeline = moviesDto.Storeline;
            movie.Year = moviesDto.Year;
            movie.Rate = moviesDto.Rate;


            _moviesService.Update(movie);

            return Ok($"{movie.Title} Updated Successfully");
            #endregion

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            #region Before Adding Services

            //var movie = await _context.Movies.FindAsync(id);
            //if (movie == null)
            //    return NotFound($"No Movies was found With ID: {id}");

            //_context.Remove(movie);
            //_context.SaveChanges();
            //return Ok($"{movie.Title} Movie Deleted Successfyly");
            #endregion

            #region After Adding Services

            var movie = await _moviesService.GetById(id);
            if (movie == null)
                return NotFound($"No Movies was found With ID: {id}");

            _moviesService.Delete(movie);
            return Ok($"{movie.Title} Movie Deleted Successfyly");
            #endregion

        }
    }
}
