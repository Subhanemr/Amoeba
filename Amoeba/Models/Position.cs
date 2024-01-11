namespace Amoeba.Models
{
    public class Position : BaseNameEntity
    {
        public ICollection<Team>? Teams { get; set; }
    }
}
