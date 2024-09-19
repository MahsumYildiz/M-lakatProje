namespace MülakatProje.Models
{
    public class CalmaListesi
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public List<CalmaListesiSarkilari>? CalmaListesiSarkilari { get; set; }  

    }
}
