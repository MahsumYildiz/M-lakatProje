using MülakatProje.Models;

namespace MülakatProje.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public DateTime CikisTarihi { get; set; }
        public int SanatciId { get; set; } 
        public Sanatci? Sanatci { get; set; }
        public List<Sarki>? Sarkilar { get; set; }
    }
}
