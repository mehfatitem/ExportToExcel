using FaceDetectService.DAL;
using FaceDetectService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FaceDetectService.Controllers
{
    [ApiController]
    [Route("api/operations")]
    public class DetectOperaitonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetectOperaitonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("getOperations")]
        public ActionResult<IEnumerable<detectoperation>> GetOperations()
        {
            var products = _context.Operations.OrderByDescending(p => p.operationTime).ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<detectoperation> GetOperation(int id)
        {
            var product = _context.Operations.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<detectoperation> CreateOperation(detectoperation operation)
        {
            _context.Operations.Add(operation);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOperation), new { id = operation.id }, operation);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, detectoperation operation)
        {
            if (id != operation.id)
            {
                return BadRequest();
            }

            _context.Entry(operation).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOperaiton(int id)
        {
            var product = _context.Operations.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Operations.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }
    }

}
