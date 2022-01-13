using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FARO.Manager3d.Application.ViewModels;
using FARO.Manager3d.Application.Service.Interfaces;
using System.Threading;

namespace FARO.Manager3d.Presentation.Controllers
{
    [Route("nominals")]
    public class NominalController : Controller
    {
        private readonly INominalPointAppService _nominalPointAppService;

        public NominalController(INominalPointAppService nominalPointAppService)
        {
            _nominalPointAppService = nominalPointAppService;
        }

        [HttpGet]
        // GET: Nominal
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await _nominalPointAppService.GetAsync(cancellationToken));
        }

        [HttpGet("create")]
        // GET: Nominal/Create
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("X,Y,Z")] NominalPointViewModel nominalPointViewModel, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                
                await _nominalPointAppService.AddAsync(nominalPointViewModel.X, nominalPointViewModel.Y, nominalPointViewModel.Z, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(nominalPointViewModel);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nominalPointViewModel = await _nominalPointAppService.GetByIDAsync(id.Value,cancellationToken);
            if (nominalPointViewModel == null)
            {
                return NotFound();
            }
            return View(nominalPointViewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,X,Y,Z")] NominalPointViewModel nominalPointViewModel, CancellationToken cancellationToken)
        {
            if (id != nominalPointViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _nominalPointAppService.UpdateAsync(id, nominalPointViewModel.X, nominalPointViewModel.Y, nominalPointViewModel.Z, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(nominalPointViewModel);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nominalPointViewModel = await _nominalPointAppService.GetByIDAsync(id.Value,cancellationToken);
            if (nominalPointViewModel == null)
            {
                return NotFound();
            }

            return View(nominalPointViewModel);
        }
        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken cancellationToken)
        {
            await _nominalPointAppService.DeleteAsync(id,cancellationToken);
            return RedirectToAction(nameof(Index));
        }

    }
}
