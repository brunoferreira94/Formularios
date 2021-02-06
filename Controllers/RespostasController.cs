using Formularios.Data;
using Formularios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formularios.Controllers
{
    public class RespostasController : Controller
    {
        private readonly FormulariosContext _context;

        public RespostasController(FormulariosContext context)
        {
            _context = context;
        }

        // GET: Respostas
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Index), "Formularios");
        }

        // GET: Respostas/Create
        public IActionResult Create(int formularioId)
        {
            Formulario _formulario = _context.Formularios.Include(i => i.Perguntas)
                                                     .FirstOrDefault(f => f.Id == formularioId);
            ViewBag.Formulario = _formulario;
            List<Resposta> _respostas = new List<Resposta>();
            _formulario.Perguntas.ToList().ForEach(f => _respostas.Add(new Resposta() { PerguntaId = f.Id, Pergunta = f }));
            return View(_respostas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Texto,DataCadastro,Latitude,Longitude,PerguntaId")] List<Resposta> respostas)
        {
            if (ModelState.IsValid)
            {
                respostas.ForEach(f =>
                {
                    f.DataCadastro = DateTime.Now;
                    f.Latitude = respostas[0].Latitude;
                    f.Longitude = respostas[0].Longitude;
                });

                respostas = respostas.Where(w => !string.IsNullOrWhiteSpace(w.Texto)).ToList();

                for (int i = 0; i < respostas.Count; i++)
                {
                    _context.Add(respostas[i]);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index), "Formularios");
            }
            return View(respostas);
        }

        private bool RespostaExists(int id)
        {
            return _context.Respostas.Any(e => e.Id == id);
        }
    }
}