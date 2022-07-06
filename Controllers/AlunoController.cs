using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIwebEscola.Contexts;
using APIwebEscola.Models;

namespace APIwebEscola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly EscolaContext _context;

        public AlunoController(EscolaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<JsonResult> GetAlunos()
        {
            if (_context.Aluno == null)
            {
                return new JsonResult("Não há alunos cadastrados");
            }

            //var aluno = await _context.Aluno.ToListAsync();
            List<Aluno> aluno = await _context.Aluno.ToListAsync();
            //List<Aluno> aluno = await _context.Aluno.FindAll(e => e.Turma.ativo == true).ToListAsync();

            return new JsonResult(new
            {
                TotalAlunos = aluno.Count,
                AlunosAtivos = aluno.FindAll(e => e.Turma.ativo == true),
            }
             );
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        //{
        //  if (_context.Aluno == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Aluno.ToListAsync();
        //}


        // GET: api/Aluno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
          if (_context.Aluno == null)
          {
              return NotFound();
          }
            var aluno = await _context.Aluno.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return aluno;
        }

        // PUT: api/Aluno/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.id)
            {
                return BadRequest();
            }

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Aluno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
          if (_context.Aluno == null)
          {
              return Problem("Entity set 'EscolaContext.Aluno'  is null.");
          }

          if (aluno.turmaid > 0 || aluno.Turma != null)
            {
             _context.Aluno.Add(aluno);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetAluno", new { id = aluno.id }, aluno);
           }
           else {
           throw new Exception("não é possível adicionar um aluno sem estar vinculado a uma turma");   
           }
                          
        }

        // DELETE: api/Aluno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            if (_context.Aluno == null)
            {
                return NotFound();
            }
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(int id)
        {
            return (_context.Aluno?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
