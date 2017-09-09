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
    public class UsuariosAcoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UsuariosAcoes
        public async Task<ActionResult> Index()
        {
            var usuarioAcaos = db.UsuariosAcoes.Include(u => u.Acao).Include(u => u.Usuario);
            return View(await usuarioAcaos.ToListAsync());
        }

        // GET: UsuariosAcoes/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioAcao usuarioAcao = await db.UsuariosAcoes.FindAsync(id);
            if (usuarioAcao == null)
            {
                return HttpNotFound();
            }
            return View(usuarioAcao);
        }

        // GET: UsuariosAcoes/Create
        public ActionResult Create()
        {
            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao");
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: UsuariosAcoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UsuarioAcaoId,UsuarioId,AcaoId")] UsuarioAcao usuarioAcao)
        {
            if (ModelState.IsValid)
            {
                usuarioAcao.UsuarioAcaoId = Guid.NewGuid();
                db.UsuariosAcoes.Add(usuarioAcao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao", usuarioAcao.AcaoId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", usuarioAcao.UsuarioId);
            return View(usuarioAcao);
        }

        // GET: UsuariosAcoes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioAcao usuarioAcao = await db.UsuariosAcoes.FindAsync(id);
            if (usuarioAcao == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao", usuarioAcao.AcaoId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", usuarioAcao.UsuarioId);
            return View(usuarioAcao);
        }

        // POST: UsuariosAcoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UsuarioAcaoId,UsuarioId,AcaoId")] UsuarioAcao usuarioAcao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioAcao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AcaoId = new SelectList(db.Acoes, "AcaoId", "CodigoNegociacao", usuarioAcao.AcaoId);
            ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Email", usuarioAcao.UsuarioId);
            return View(usuarioAcao);
        }

        // GET: UsuariosAcoes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioAcao usuarioAcao = await db.UsuariosAcoes.FindAsync(id);
            if (usuarioAcao == null)
            {
                return HttpNotFound();
            }
            return View(usuarioAcao);
        }

        // POST: UsuariosAcoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            UsuarioAcao usuarioAcao = await db.UsuariosAcoes.FindAsync(id);
            db.UsuariosAcoes.Remove(usuarioAcao);
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
