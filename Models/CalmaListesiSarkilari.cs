namespace MülakatProje.Models
{
    public class CalmaListesiSarkilari
    {
        public int Id { get; set; }
        public int CalmaListesiId { get; set; }
        public int SarkiId { get; set; }
        public CalmaListesi? CalmaListesi { get; set; }
        public Sarki? Sarki { get; set; }
    }
}
