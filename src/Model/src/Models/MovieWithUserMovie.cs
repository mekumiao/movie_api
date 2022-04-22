using MovieAPI.DAL;

namespace MovieAPI.Model;

public class MovieWithUserMovie
{
    public Movie Movie { get; set; } = default!;
    public UserMovie? UserMovie { get; set; }
}
