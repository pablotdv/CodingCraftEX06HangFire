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
    public class UsuariosAcoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UsuariosAcoes
        public async Task<ActionResult> Index(UsuariosAcoesViewModel viewModel)
        {
            var query = db.UsuariosAcoes
                .Where(a => a.Ativo);

            viewModel.Rentabilidade = await query.SumAsync(a => a.Rentabilidade);
            viewModel.Resultados = await query.OrderBy(a => a.Acao.CodigoNegociacao).ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            return View(viewModel);
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

        // GET: UsuariosAcoes/Venda/5
        public async Task<ActionResult> Venda(Guid? id)
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

            return View(new VendaViewModel()
            {
                Acao = usuarioAcao.Acao,
                UsuarioAcao = usuarioAcao
            });

        }

        // POST: UsuariosAcoes/Venda/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Venda(VendaViewModel viewModel)
        {
            var keys = ModelState.Keys.Where(a => a.Contains("Acao.") || a.Contains("UsuarioAcao.")).ToList();
            foreach (var key in keys)
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {
                var rd = new Random((int)DateTime.Now.Ticks);

                var usuarioAcao = await db.UsuariosAcoes.Include(a => a.OrdensUsuariosAcoes)
                    .Include(a => a.UsuariosAcoesHistoricos)
                    .FirstOrDefaultAsync(a => a.UsuarioAcaoId == viewModel.UsuarioAcao.UsuarioAcaoId);

                var ordemUsuarioAcao = new OrdemUsuarioAcao
                {
                    OrdemUsuarioAcaoId = Guid.NewGuid(),
                    Ordem = new Ordem
                    {
                        OrdemId = Guid.NewGuid(),
                        AcaoId = viewModel.Acao.AcaoId,
                        Chance = Infraestrura.OrdensChances.Venda(viewModel.Acao.Preco, viewModel.Preco),
                        DataHora = DateTime.Now,
                        Preco = viewModel.Preco,
                        Quantidade = viewModel.Quantidade,
                        Tipo = OrdemTipo.Venda,
                        UsuarioId = Guid.Parse(User.Identity.GetUserId()),
                    }
                };

                var percentual = (decimal)rd.NextDouble() * 100;
                if (ordemUsuarioAcao.Ordem.Chance >= percentual)
                {
                    var historico = new UsuarioAcaoHistorico()
                    {
                        UsuarioAcaoHistoricoId = Guid.NewGuid(),
                        Preco = viewModel.Preco,
                        PercentualVariacao = viewModel.Preco * 100 / viewModel.Acao.Preco - 100,
                        ValorVariacao = viewModel.Preco - viewModel.Acao.Preco,
                        Rentabilidade = (viewModel.Preco - viewModel.Acao.Preco) * viewModel.Quantidade
                    };
                    usuarioAcao.Quantidade -= viewModel.Quantidade;
                    usuarioAcao.Ativo = usuarioAcao.Quantidade > 0;
                    usuarioAcao.UsuariosAcoesHistoricos.Add(historico);
                    ordemUsuarioAcao.Ordem.Rentabilidade = historico.Rentabilidade;
                    usuarioAcao.OrdensUsuariosAcoes.Add(ordemUsuarioAcao);

                    await db.SaveChangesAsync();
                }


                return RedirectToAction("Index");
            }

            return View(viewModel);
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
