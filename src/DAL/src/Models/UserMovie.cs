using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MovieAPI.DAL;

public class UserMovie : TableEntity
{
    public long? UserId { get; set; }

    [IgnoreDataMember]
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    public long? MovieId { get; set; }

    [IgnoreDataMember]
    [ForeignKey(nameof(MovieId))]
    public Movie? Movie { get; set; }

    public bool IsDislike { get; set; }

    public bool IsStar { get; set; }
}
