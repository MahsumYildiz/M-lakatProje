namespace MülakatProje.Models
{
    public class Sanatci
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public DateTime KurulusTarihi { get; set; }
        public List<Album>? Albumler { get; set; }

    }
}
