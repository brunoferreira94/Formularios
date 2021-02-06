using Formularios.Data;
using Formularios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formularios.Controllers
{
    public class PerguntasController : Controller
    {
        private readonly FormulariosContext _context;

        public PerguntasController(FormulariosContext context)
        {
            _context = context;
        }

        public IActionResult Index(List<Pergunta> perguntas)
        {
            return View(nameof(Index), perguntas);
        }

        public IActionResult FormulariosIndex()
        {
            return RedirectToAction(nameof(Index), "Formularios");
        }

        // GET: Perguntas
        public async Task<IActionResult> QuestionsById(int formularioId)
        {
            var formulariosContext = _context.Perguntas
                                                .Include(p => p.Formulario)
                                                .Include(p => p.Usuario)
                                                .Where(w => w.FormularioId == formularioId);
            return View(nameof(Index), await formulariosContext.ToListAsync());
        }

        // GET: Perguntas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pergunta = await _context.Perguntas
                .Include(p => p.Formulario)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pergunta == null)
            {
                return NotFound();
            }

            return View(pergunta);
        }

        // GET: Perguntas/Create
        public IActionResult Create(int formularioId = 0, int usuarioId = 0, string sucesso = null)
        {
            if (formularioId > 0)
                ViewData["FormularioId"] = new SelectList(_context.Formularios.Where(w => w.Id == formularioId), "Id", "Titulo");
            else
                ViewData["FormularioId"] = new SelectList(_context.Formularios, "Id", "Titulo");

            if (usuarioId > 0)
                ViewData["UsuarioId"] = new SelectList(_context.Usuarios.Where(w => w.Id == usuarioId), "Id", "Apelido");
            else
                ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Apelido");

            ViewData["Sucesso"] = sucesso;

            return View();
        }

        // POST: Perguntas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,DataCadastro,UsuarioId,FormularioId")] Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                pergunta.DataCadastro = DateTime.Now;
                _context.Add(pergunta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new { formularioId = pergunta.FormularioId, usuarioId = pergunta.UsuarioId, sucesso = pergunta.Titulo });
            }
            ViewData["FormularioId"] = new SelectList(new List<Pergunta> { pergunta }, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Apelido", pergunta.UsuarioId);
            return View(pergunta);
        }

        // GET: Perguntas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pergunta = await _context.Perguntas.FindAsync(id);
            if (pergunta == null)
            {
                return NotFound();
            }
            ViewData["FormularioId"] = new SelectList(_context.Formularios, "Id", "Id", pergunta.FormularioId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Apelido", pergunta.UsuarioId);
            return View(pergunta);
        }

        // POST: Perguntas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,DataCadastro,UsuarioId,FormularioId")] Pergunta pergunta)
        {
            if (id != pergunta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pergunta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerguntaExists(pergunta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormularioId"] = new SelectList(_context.Formularios, "Id", "Id", pergunta.FormularioId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Apelido", pergunta.UsuarioId);
            return View(pergunta);
        }

        // GET: Perguntas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pergunta = await _context.Perguntas
                .Include(p => p.Formulario)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pergunta == null)
            {
                return NotFound();
            }

            return View(pergunta);
        }

        // POST: Perguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pergunta = await _context.Perguntas.FindAsync(id);
            _context.Perguntas.Remove(pergunta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerguntaExists(int id)
        {
            return _context.Perguntas.Any(e => e.Id == id);
        }
    }
}