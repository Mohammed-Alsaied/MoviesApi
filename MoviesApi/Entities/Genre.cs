namespace MoviesApi.Models
{
    public class Genre
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Set Id Auto Increment
        public byte Id { get; set; }

        //[MaxLength(100)]//Data Annotaion
        public string Name { get; set; }
    }
}
