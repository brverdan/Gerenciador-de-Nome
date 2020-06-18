using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT_ASPNET.Repository;
using AT_ASPNET.Models;

namespace AT_ASPNET.Controllers
{
    public class PessoaController : Controller
    {
        private PessoaRepository PessoaRepository { get; set; }

        public PessoaController(PessoaRepository pessoaRepository)
        {
            PessoaRepository = pessoaRepository;
        }

        [Route("Pessoa/")]
        // GET: Pessoa
        public ActionResult Index()
        {
            var pessoa = PessoaRepository.GetNextBirthday();
            return View(pessoa);
        }

        [Route("Pessoa/Search")]
        public ActionResult Search(string nome)
        {
            var pessoa = PessoaRepository.GetByName(nome);
            return View(pessoa);
        }

        [Route("Pessoa/CompleteList")]
        public ActionResult CompleteList()
        {
            var pessoa = PessoaRepository.GetAll();
            return View(pessoa);
        }

        //GET: Pessoa/Details/5
        [Route("Pessoa/Details/{id}")]
        public ActionResult Details(int id)
        {
            var pessoa = PessoaRepository.GetById(id);
            return View(pessoa);
        }

        [Route("Pessoa/Create")]
        // GET: Pessoa/Create
        public ActionResult Create()
        {
            return View();
        }

        [Route("Pessoa/Create")]
        // POST: Pessoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pessoa pessoa)
        {
            try
            {
                PessoaRepository.Save(pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Pessoa/Edit/{id}")]
        // GET: Pessoa/Edit/5
        public ActionResult Edit(int id)
        {
            var pessoa = PessoaRepository.GetById(id);
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        [Route("Pessoa/Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pessoa pessoa)
        {
            try
            {
                var pessoaEditada = PessoaRepository.GetById(id);
                pessoaEditada.Nome = pessoa.Nome;
                pessoaEditada.Sobrenome = pessoa.Sobrenome;
                pessoaEditada.DataDeAniversario = pessoa.DataDeAniversario;

                PessoaRepository.Update(pessoaEditada);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Pessoa/Delete/{id}")]
        // GET: Pessoa/Delete/5
        public ActionResult Delete(int id)
        {
            var pessoa = PessoaRepository.GetById(id);
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Pessoa/Delete/{id}")]
        public ActionResult Delete(int id, Pessoa pessoa)
        {
            try
            {
                PessoaRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
