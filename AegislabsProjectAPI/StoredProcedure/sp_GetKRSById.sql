CREATE OR ALTER PROCEDURE sp_GetKRSById
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    WITH KRS_CTE AS (
        SELECT
            a.Id AS KRSId,
            a.Semester,
            a.TahunAjaran,
            b.Nama AS NamaMahasiswa,
            c.Nama AS NamaMataKuliah,
            c.SKS
        FROM KRS a
        INNER JOIN Mahasiswa b ON a.MahasiswaId = b.Id
        INNER JOIN MataKuliah c ON a.MataKuliahId = c.Id
    )
    SELECT * FROM KRS_CTE WHERE KRSId = @Id
END