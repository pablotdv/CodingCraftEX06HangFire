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

namespace CodingCraftEX06HangFire.Controllers
{
    [Authorize]
    public class OrdensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ordens
        public async Task<ActionResult> Index()
        {
            var ordems = db.Ordems.Include(o => o.Acao).Include(o => o.Usuario);
            return View(await ordems.ToListAsync());
        }

        // GET: Ordens/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = await db.Ordems.FindAsync(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            return View(ordem);
        }

        // GET: Ordens/Create
        public ActionResult Create()
        {
            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao");
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Ordens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrdemId,Tipo,Preco,Quantidade,AcaoId,UsuarioId")] Ordem ordem)
        {
            if (ModelState.IsValid)
            {
                ordem.OrdemId = Guid.NewGuid();
                db.Ordems.Add(ordem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao", ordem.AcaoId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", ordem.UsuarioId);
            return View(ordem);
        }

        // GET: Ordens/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = await db.Ordems.FindAsync(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao", ordem.AcaoId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", ordem.UsuarioId);
            return View(ordem);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrdemId,Tipo,Preco,Quantidade,AcaoId,UsuarioId")] Ordem ordem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao", ordem.AcaoId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", ordem.UsuarioId);
            return View(ordem);
        }

        // GET: Ordens/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = await db.Ordems.FindAsync(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            return View(ordem);
        }

        // POST: Ordens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Ordem ordem = await db.Ordems.FindAsync(id);
            db.Ordems.Remove(ordem);
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
