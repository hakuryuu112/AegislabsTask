namespace AegislabsProjectAPI.Models.ModelRequest
{
    public class KRSRequest
    {
        public Guid MahasiswaId { get; set; }
        public Guid MataKuliahId { get; set; }
        public int Semester { get; set; }
        public string TahunAjaran { get; set; }
    }
}
