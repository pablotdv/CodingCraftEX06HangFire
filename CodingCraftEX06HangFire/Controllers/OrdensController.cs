﻿using System;
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
using CodingCraftEX06HangFire.Models.Enums;
using Microsoft.AspNet.Identity;

namespace CodingCraftEX06HangFire.Controllers
{
    [Authorize]
    public class OrdensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ordens
        public async Task<ActionResult> Index(OrdensViewModel viewModel)
        {
            var query = db.Ordens.Include(o => o.Acao).Include(o => o.Usuario);

            if (viewModel.Tipo.HasValue)
            {
                query = query.Where(a => a.Tipo == viewModel.Tipo);
            }

            viewModel.Resultados = await query.OrderBy(a => a.DataHora).ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            return View(viewModel);
        }

        // GET: Ordens/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = await db.Ordens.FindAsync(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            return View(ordem);
        }

        // GET: Ordens/Create
        public async Task<ActionResult> Create(OrdemTipo? tipo)
        {
            await AcoesViewBag();
            Ordem model = null;
            if (tipo.HasValue)
                model = new Ordem() { Tipo = tipo.Value };
            return View(model);
        }

        private async Task AcoesViewBag()
        {
            var acoes = (await db.Acoes.OrderBy(a => a.CodigoNegociacao)
                            .ToListAsync())
                            .Select(a => new { AcaoId = a.AcaoId, Text = $"{a.CodigoNegociacao} ({a.Preco.ToString("N2")})" });
            ViewBag.AcaoId = new SelectList(acoes, "AcaoId", "Text");
        }

        // POST: Ordens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrdemId,Tipo,Preco,Quantidade,AcaoId")] Ordem ordem)
        {
            if (ModelState.IsValid)
            {
                ordem.OrdemId = Guid.NewGuid();
                ordem.UsuarioId = Guid.Parse(User.Identity.GetUserId());
                ordem.DataHora = DateTime.Now;
                db.Ordens.Add(ordem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            await AcoesViewBag();
            return View(ordem);
        }

        // GET: Ordens/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = await db.Ordens.FindAsync(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            await AcoesViewBag();
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
            await AcoesViewBag();
            return View(ordem);
        }

        // GET: Ordens/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = await db.Ordens.FindAsync(id);
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
            Ordem ordem = await db.Ordens.FindAsync(id);
            db.Ordens.Remove(ordem);
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
