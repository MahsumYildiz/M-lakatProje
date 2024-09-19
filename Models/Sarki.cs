using System;
using MülakatProje.Models;

namespace MülakatProje.Models
{
    public class Sarki
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int AlbumId { get; set; }
        public int SanatciId { get; set; }
        public Album? Album { get; set; }
        public Sanatci? Sanatci { get; set; }
    }
}
