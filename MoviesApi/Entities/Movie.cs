namespace MoviesApi.Entities
{
    public class Movie
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public double Rate { get; set; }

        public int Year { get; set; }
        public string Storeline { get; set; }
        public byte[] Poster { get; set; }

        //ForeignKey
        public byte GenreId { get; set; }

        public Genre Genre { get; set; }

    }
}
