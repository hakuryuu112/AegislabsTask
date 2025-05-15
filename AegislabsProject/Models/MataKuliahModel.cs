namespace AegislabsProject.Models
{
    public class MataKuliahModel
    {
        public Guid Id { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public int SKS { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
