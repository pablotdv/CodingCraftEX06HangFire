using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodingCraftEX06HangFire.Models;
using CodingCraftEX06HangFire.ViewModels;
using System.IO;
using FileHelpers;

namespace CodingCraftEX06HangFire.Controllers
{
    public class AcoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Acoes
        public async Task<ActionResult> Index()
        {
            var acoes = db.Acoes.Include(a => a.Empresa);
            return View(await acoes.ToListAsync());
        }

        // GET: Acoes/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acao acao = await db.Acoes.FindAsync(id);
            if (acao == null)
            {
                return HttpNotFound();
            }
            return View(acao);
        }

        public ActionResult Importar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Importar(AcoesImportarViewModel viewModel)
        {
            if (viewModel.Arquivo == null || viewModel.Arquivo.ContentLength <= 0) ModelState.AddModelError("", "Selecione um arquivo!");

            if (ModelState.IsValid)
            {
                using (TextReader textReader = new StreamReader(viewModel.Arquivo.InputStream))
                {
                    var motor = new MultiRecordEngine(typeof(ViewModels.Bovespa.Header), typeof(ViewModels.Bovespa.Detail), typeof(ViewModels.Bovespa.Trailer));
                    motor.ErrorMode = ErrorMode.SaveAndContinue;
                    motor.RecordSelector = new RecordTypeSelector(Infraestrura.Bovespa.BovespaSelector.Selector);
                    var registros = motor.ReadStream(textReader);

                    foreach (var erro in motor.ErrorManager.Errors)
                    {
                        ModelState.AddModelError("", $"Linha {erro.LineNumber}, {erro.ExceptionInfo}.");
                    }
                    var detalhes = registros.Where(a => a is ViewModels.Bovespa.Detail).Select(a => a as ViewModels.Bovespa.Detail).ToList();
                    foreach (var detalhe in detalhes)
                    {

                        var acao = await db.Acoes.FirstOrDefaultAsync(a => a.CodigoIsin == detalhe.CodIsi && a.CodigoNegociacao == detalhe.CodNeg);
                        if (acao != null)
                        {
                            var ultimoHistorico = await db.AcoesHistoricos.Where(a => a.AcaoId == acao.AcaoId).OrderByDescending(a => a.DataHora).FirstOrDefaultAsync();

                            db.AcoesHistoricos.Add(new AcaoHistorico()
                            {
                                AcaoHistoricoId = Guid.NewGuid(),
                                AcaoId = acao.AcaoId,
                                DataHora = detalhe.DataDoPregao,
                                Preco = detalhe.PreUlt,
                                ValorVariacao = detalhe.PreUlt - ultimoHistorico?.Preco ?? 0,
                                PercentualVariacao = detalhe.PreUlt * 100 / ultimoHistorico?.Preco - 100 ?? 0,
                            });
                        }
                    }

                    await db.SaveChangesAsync();

                    if (ModelState.IsValid)
                        return View(registros.ToList());
                }
            }
            return View(viewModel);
        }

        // GET: Acoes/Create
        public ActionResult Create()
        {
            ViewBag.EmpresaId = new SelectList(db.Empresas, "EmpresaId", "RazaoSocial");
            return View();
        }

        // POST: Acoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AcaoId,EmpresaId,CodigoNegociacao,CodigoIsin,Preco")] Acao acao)
        {
            if (ModelState.IsValid)
            {
                acao.AcaoId = Guid.NewGuid();
                db.Acoes.Add(acao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmpresaId = new SelectList(db.Empresas, "EmpresaId", "RazaoSocial", acao.EmpresaId);
            return View(acao);
        }

        // GET: Acoes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acao acao = await db.Acoes.FindAsync(id);
            if (acao == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaId = new SelectList(db.Empresas, "EmpresaId", "RazaoSocial", acao.EmpresaId);
            return View(acao);
        }

        // POST: Acoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AcaoId,EmpresaId,CodigoNegociacao,CodigoIsin,Preco")] Acao acao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmpresaId = new SelectList(db.Empresas, "EmpresaId", "RazaoSocial", acao.EmpresaId);
            return View(acao);
        }

        // GET: Acoes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acao acao = await db.Acoes.FindAsync(id);
            if (acao == null)
            {
                return HttpNotFound();
            }
            return View(acao);
        }

        // POST: Acoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Acao acao = await db.Acoes.FindAsync(id);
            db.Acoes.Remove(acao);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
