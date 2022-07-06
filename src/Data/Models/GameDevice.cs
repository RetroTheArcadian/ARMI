using SQLite;

namespace ARMI.Data.Models
{
    [Table("gamedevice")]
    public class GameDevice
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(250), Unique]
        public string Name { get; set; }
    }
}