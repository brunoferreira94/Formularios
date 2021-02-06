using Formularios.Data;
using Formularios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Formularios.Controllers
{
    public class FormulariosController : Controller
    {
        private readonly FormulariosContext _context;

        public FormulariosController(FormulariosContext context)
        {
            _context = context;
        }

        // GET: Formularios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Formularios.ToListAsync());
        }

        // GET: Formularios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formularios
                .Include(i => i.Perguntas)
                .ThenInclude(ti => ti.Respostas)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        public IActionResult CreateQuestion(int id)
        {
            return RedirectToAction(nameof(Create), "Perguntas", new { formularioId = id });
        }

        public IActionResult QuestionsById(int id)
        {
            return RedirectToAction("QuestionsById", "Perguntas", new { formularioId = id });
        }

        public IActionResult Answer(int id)
        {
            return RedirectToAction("Create", "Respostas", new { formularioId = id });
        }

        // GET: Formularios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formularios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo")] Formulario model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create), "Perguntas", new { formularioId = model.Id });
            }
            return View(model);
        }

        // GET: Formularios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formularios.FindAsync(id);
            if (formulario == null)
            {
                return NotFound();
            }
            return View(formulario);
        }

        // POST: Formularios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Titulo")] Formulario formulario)
        {
            if (id != formulario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formulario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormularioExists(formulario.Id))
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
            return View(formulario);
        }

        // GET: Formularios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formularios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        // POST: Formularios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formulario = await _context.Formularios.FindAsync(id);
            _context.Formularios.Remove(formulario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormularioExists(int id)
        {
            return _context.Formularios.Any(e => e.Id == id);
        }
    }
}