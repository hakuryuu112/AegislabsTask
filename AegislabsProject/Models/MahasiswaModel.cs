namespace AegislabsProject.Models
{
    public class MahasiswaModel
    {
        public Guid Id { get; set; }
        public string NIM { get; set; }
        public string Nama { get; set; }
        public DateTime TanggalLahir { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid JurusanId { get; set; }
    }
}
