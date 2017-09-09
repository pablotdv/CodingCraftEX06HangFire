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
using PagedList.EntityFramework;

namespace CodingCraftEX06HangFire.Controllers
{
    [Authorize(Roles = "Administradores")]
    public class AcoesHistoricosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AcoesHistoricos
        public async Task<ActionResult> Index(AcoesHistoricosViewModel viewModel)
        {
            var query = db.AcoesHistoricos.AsQueryable();

            if (viewModel.DataOperacao.HasValue)
            {
                var dataOperacao = viewModel.DataOperacao.Value.Date;
                query = query.Where(a => DbFunctions.TruncateTime(a.DataHora) == dataOperacao);
            }

            viewModel.Resultados = await query.OrderBy(a => a.DataHora).ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            return View(viewModel);
        }

        // GET: AcoesHistoricos/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcaoHistorico acaoHistorico = await db.AcoesHistoricos.FindAsync(id);
            if (acaoHistorico == null)
            {
                return HttpNotFound();
            }
            return View(acaoHistorico);
        }

        // GET: AcoesHistoricos/Create
        public ActionResult Create()
        {
            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao");
            return View();
        }

        // POST: AcoesHistoricos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AcaoHistoricoId,AcaoId,Preco,ValorVariacao,PercentualVariacao,DataHora")] AcaoHistorico acaoHistorico)
        {
            if (ModelState.IsValid)
            {
                acaoHistorico.AcaoHistoricoId = Guid.NewGuid();
                db.AcoesHistoricos.Add(acaoHistorico);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao", acaoHistorico.AcaoId);
            return View(acaoHistorico);
        }

        // GET: AcoesHistoricos/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcaoHistorico acaoHistorico = await db.AcoesHistoricos.FindAsync(id);
            if (acaoHistorico == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao", acaoHistorico.AcaoId);
            return View(acaoHistorico);
        }

        // POST: AcoesHistoricos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AcaoHistoricoId,AcaoId,Preco,ValorVariacao,PercentualVariacao,DataHora")] AcaoHistorico acaoHistorico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acaoHistorico).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao", acaoHistorico.AcaoId);
            return View(acaoHistorico);
        }

        // GET: AcoesHistoricos/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcaoHistorico acaoHistorico = await db.AcoesHistoricos.FindAsync(id);
            if (acaoHistorico == null)
            {
                return HttpNotFound();
            }
            return View(acaoHistorico);
        }

        // POST: AcoesHistoricos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            AcaoHistorico acaoHistorico = await db.AcoesHistoricos.FindAsync(id);
            db.AcoesHistoricos.Remove(acaoHistorico);
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
