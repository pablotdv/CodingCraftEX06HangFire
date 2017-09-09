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
    public class UsuariosAcoesHistoricosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UsuariosAcoesHistoricos
        public async Task<ActionResult> Index()
        {
            var usuarioAcaoHistoricoes = db.UsuariosAcoesHistoricos.Include(u => u.UsuarioAcao);
            return View(await usuarioAcaoHistoricoes.ToListAsync());
        }

        // GET: UsuariosAcoesHistoricos/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioAcaoHistorico usuarioAcaoHistorico = await db.UsuariosAcoesHistoricos.FindAsync(id);
            if (usuarioAcaoHistorico == null)
            {
                return HttpNotFound();
            }
            return View(usuarioAcaoHistorico);
        }

        // GET: UsuariosAcoesHistoricos/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioAcaoId = new SelectList(db.UsuariosAcoes, "UsuarioAcaoId", "UsuarioAcaoId");
            return View();
        }

        // POST: UsuariosAcoesHistoricos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UsuarioAcaoHistoricoId,UsuarioAcaoId")] UsuarioAcaoHistorico usuarioAcaoHistorico)
        {
            if (ModelState.IsValid)
            {
                usuarioAcaoHistorico.UsuarioAcaoHistoricoId = Guid.NewGuid();
                db.UsuariosAcoesHistoricos.Add(usuarioAcaoHistorico);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioAcaoId = new SelectList(db.UsuariosAcoes, "UsuarioAcaoId", "UsuarioAcaoId", usuarioAcaoHistorico.UsuarioAcaoId);
            return View(usuarioAcaoHistorico);
        }

        // GET: UsuariosAcoesHistoricos/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioAcaoHistorico usuarioAcaoHistorico = await db.UsuariosAcoesHistoricos.FindAsync(id);
            if (usuarioAcaoHistorico == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioAcaoId = new SelectList(db.UsuariosAcoes, "UsuarioAcaoId", "UsuarioAcaoId", usuarioAcaoHistorico.UsuarioAcaoId);
            return View(usuarioAcaoHistorico);
        }

        // POST: UsuariosAcoesHistoricos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UsuarioAcaoHistoricoId,UsuarioAcaoId")] UsuarioAcaoHistorico usuarioAcaoHistorico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioAcaoHistorico).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioAcaoId = new SelectList(db.UsuariosAcoes, "UsuarioAcaoId", "UsuarioAcaoId", usuarioAcaoHistorico.UsuarioAcaoId);
            return View(usuarioAcaoHistorico);
        }

        // GET: UsuariosAcoesHistoricos/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioAcaoHistorico usuarioAcaoHistorico = await db.UsuariosAcoesHistoricos.FindAsync(id);
            if (usuarioAcaoHistorico == null)
            {
                return HttpNotFound();
            }
            return View(usuarioAcaoHistorico);
        }

        // POST: UsuariosAcoesHistoricos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            UsuarioAcaoHistorico usuarioAcaoHistorico = await db.UsuariosAcoesHistoricos.FindAsync(id);
            db.UsuariosAcoesHistoricos.Remove(usuarioAcaoHistorico);
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
