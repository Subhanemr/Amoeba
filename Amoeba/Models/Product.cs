using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Amoeba.Models
{
    public class Product : BaseNameEntity
    {
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string Img { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
