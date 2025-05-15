namespace AegislabsProjectAPI.Models
{
    public class KRSModel
    {
        public Guid Id { get; set; }
        public Guid MahasiswaId { get; set; }
        public Guid MataKuliahId { get; set; }
        public int Semester { get; set; }
        public string TahunAjaran { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
