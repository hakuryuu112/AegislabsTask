using System.Data;
using AegislabsProjectAPI.Models;
using AegislabsProjectAPI.Models.ModelDTO;
using AegislabsProjectAPI.Models.ModelRequest;
using Microsoft.Data.SqlClient;

namespace AegislabsProjectAPI.Data
{
    public interface IKRSRepository
    {
        List<KRSDto> GetAllKRS();
        KRSDto GetById(Guid Id);

        void Add(KRSRequest model);
        void Update(Guid id, KRSRequest model); 
        void Delete(Guid id);
    }
    public class KRSRepository : IKRSRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public KRSRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<KRSDto> GetAllKRS()
        {
            var list = new List<KRSDto>();

            using var conn = _dbHelper.GetConnection();
            using var cmd = new SqlCommand("sp_GetAllKRS", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new KRSDto
                {
                    Id = reader.GetGuid(reader.GetOrdinal("KRSId")),
                    NamaMahasiswa = reader.GetString(reader.GetOrdinal("NamaMahasiswa")),
                    NamaMataKuliah = reader.GetString(reader.GetOrdinal("NamaMataKuliah")),
                    SKS = reader.GetInt32(reader.GetOrdinal("SKS")),
                    Semester = reader.GetInt32(reader.GetOrdinal("Semester")),
                    TahunAjaran = reader.GetString(reader.GetOrdinal("TahunAjaran"))
                });
            }

            return list;
        }

        public KRSDto GetById(Guid id)
        {
            using var conn = _dbHelper.GetConnection();
            using var cmd = new SqlCommand("sp_GetKRSById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new KRSDto
                {
                    Id = reader.GetGuid(reader.GetOrdinal("KRSId")),
                    NamaMahasiswa = reader.GetString(reader.GetOrdinal("NamaMahasiswa")),
                    NamaMataKuliah = reader.GetString(reader.GetOrdinal("NamaMataKuliah")),
                    SKS = reader.GetInt32(reader.GetOrdinal("SKS")),
                    Semester = reader.GetInt32(reader.GetOrdinal("Semester")),
                    TahunAjaran = reader.GetString(reader.GetOrdinal("TahunAjaran"))
                };
            }

            return null;
        }

        public void Add(KRSRequest model)
        {
            using var conn = _dbHelper.GetConnection();
            using var cmd = new SqlCommand(@"
                INSERT INTO KRS (Id, MahasiswaId, MataKuliahId, Semester, TahunAjaran, CreatedAt, UpdatedAt)
                VALUES (NEWID(), @MahasiswaId, @MataKuliahId, @Semester, @TahunAjaran, GETDATE(), GETDATE())
            ", conn);

            cmd.Parameters.AddWithValue("@MahasiswaId", model.MahasiswaId);
            cmd.Parameters.AddWithValue("@MataKuliahId", model.MataKuliahId);
            cmd.Parameters.AddWithValue("@Semester", model.Semester);
            cmd.Parameters.AddWithValue("@TahunAjaran", model.TahunAjaran);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(Guid id, KRSRequest model)
        {
            using var conn = _dbHelper.GetConnection();
            using var cmd = new SqlCommand(@"
                UPDATE KRS SET
                    MahasiswaId = @MahasiswaId,
                    MataKuliahId = @MataKuliahId,
                    Semester = @Semester,
                    TahunAjaran = @TahunAjaran,
                    UpdatedAt = GETDATE()
                WHERE Id = @Id
            ", conn);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@MahasiswaId", model.MahasiswaId);
            cmd.Parameters.AddWithValue("@MataKuliahId", model.MataKuliahId);
            cmd.Parameters.AddWithValue("@Semester", model.Semester);
            cmd.Parameters.AddWithValue("@TahunAjaran", model.TahunAjaran);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(Guid id)
        {
            using var conn = _dbHelper.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM KRS WHERE Id = @Id", conn);

            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
