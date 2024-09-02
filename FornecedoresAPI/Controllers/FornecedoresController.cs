using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FornecedoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly FornecedoresContext _context;

        private const string NOT_FOUND_MESSAGE = "Fornecedor não encontrado no sistema";
        private const string INVALID_MAIL_MESSAGE = "O e-mail fornecido não é válido: ";
        private const string DELETE_SUCESS_MESSAGE = "Fornecedor removido com sucesso.";
        private const string UPDATE_SUCESS_MESSAGE = "Fornecedor alterado com sucesso.";


        public FornecedoresController(FornecedoresContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedores()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> GetFornecedor(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);

            if (fornecedor == null)
            {
                return NotFound(NOT_FOUND_MESSAGE);
            }

            return fornecedor;
        }

        [HttpPost]
        public async Task<ActionResult<Fornecedor>> Post(Fornecedor fornecedor)
        {
            if (!fornecedor.ValidMail())
            {
                return BadRequest(String.Concat(INVALID_MAIL_MESSAGE, fornecedor.Email));
            }

            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFornecedor), new { id = fornecedor.Id }, fornecedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Fornecedor fornecedor)
        {
            if (!FornecedorExists(id))
            {
                return BadRequest(String.Concat("O id fornecido não existe: ", id));
            }

            fornecedor.Id = id;

            if (!fornecedor.ValidMail())
            {
                return BadRequest(String.Concat(INVALID_MAIL_MESSAGE, fornecedor.Email));
            }

            _context.Entry(fornecedor).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(UPDATE_SUCESS_MESSAGE);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound(NOT_FOUND_MESSAGE);
            }

            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();

            return Ok(DELETE_SUCESS_MESSAGE);
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedores.Any(e => e.Id == id);
        }
    }

}
