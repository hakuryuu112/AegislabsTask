using AegislabsProjectAPI.Data;
using AegislabsProjectAPI.DBContexts;
using AegislabsProjectAPI.Models;
using AegislabsProjectAPI.Models.ModelRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AegislabsProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KRSController : ControllerBase
    {
        private readonly IKRSRepository _repository;

        public KRSController(IKRSRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = _repository.GetAllKRS();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var data = _repository.GetById(id);

            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create([FromBody] KRSRequest model)
        {
            _repository.Add(model);

            return Ok(new { message = "Data berhasil ditambahkan." });
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] KRSRequest model)
        {
            _repository.Update(id, model);

            return Ok(new { message = "Data berhasil diperbarui." });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _repository.Delete(id);

            return Ok(new { message = "Data berhasil dihapus." });
        }
    }
}
