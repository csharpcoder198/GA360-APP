using SQLite;

namespace GA360.Models
{
    public class Config
    {
        // Field Automatically Increments - Starts at 1
        [PrimaryKey] [AutoIncrement] public int Id { get; set; }

        [Indexed] [NotNull] public string Key { get; set; }

        [NotNull] public string Value { get; set; }

        public float LowerBound { get; set; }
        public float UpperBound { get; set; }
    }
}