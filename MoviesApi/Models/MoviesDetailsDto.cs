namespace MoviesApi.Models
{
    public class MoviesDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public double Rate { get; set; }

        public int Year { get; set; }
        public string Storeline { get; set; }
        public byte[] Poster { get; set; }

        //ForeignKey
        public byte GenreId { get; set; }

        public String GenreName { get; set; }
    }
}
