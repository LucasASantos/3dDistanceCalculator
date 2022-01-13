using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FARO.Manager3d.Application.ViewModels;
using FARO.Manager3d.Application.Service.Interfaces;
using System.Threading;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace FARO.Manager3d.Presentation.Controllers
{
    [Route("nominals/{nominalId:Guid}/actuals")]
    public class ActualController : Controller
    {
        private readonly IActualPointAppService _actualPointAppService;
        private readonly IPointAppService _pointAppService;
        private readonly INominalPointAppService _nominalPointAppService;

        public ActualController(
            IActualPointAppService actualPointAppService,
            IPointAppService pointAppService,
            INominalPointAppService nominalPointAppService)
        {
            _actualPointAppService = actualPointAppService;
            _pointAppService = pointAppService;
            _nominalPointAppService = nominalPointAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByNominalId(Guid nominalId, CancellationToken cancellationToken)
        {
            ViewBag.NominalId = nominalId;
            ViewBag.Method = "async";
            var actualPoints = await _actualPointAppService.GetByNominalPointAsync(nominalId, cancellationToken);
            
            var nominalPoint = await _nominalPointAppService.GetByIDAsync(nominalId, cancellationToken);
            if (nominalPoint == null)
            {   
                return View("Index");
            }
            nominalPoint =  _pointAppService.CalculateAvg(
                nominalPoint, 
                actualPoints,
                cancellationToken);
            ViewBag.Nominal = nominalPoint;

            return View("Index", actualPoints);
        }
        [HttpGet("calculate")]
        public async Task<IActionResult> GetCalculatedByNominalId(Guid nominalId, CancellationToken cancellationToken)
        {
            ViewBag.NominalId = nominalId;

            var method = string.IsNullOrEmpty(ViewBag.Method)? "async": ViewBag.Method;
            
            var nominalPoint = await _nominalPointAppService.GetByIDAsync(nominalId, cancellationToken);
            if (nominalPoint == null)
            {   
                return View("Index");
            }

            var actualPoints = await _pointAppService.CalculateDistance(nominalPoint,  method, cancellationToken);
            nominalPoint =  _pointAppService.CalculateAvg(
                nominalPoint, 
                actualPoints,
                cancellationToken);

            ViewBag.Nominal = nominalPoint;

            return View("Index", actualPoints);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(Guid nominalId, Guid? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actualPointViewModel = await _actualPointAppService.GetByIDAsync(id.Value, cancellationToken);
            if (actualPointViewModel == null)
            {
                return NotFound();
            }

            return View(actualPointViewModel);
        }

        [HttpGet("create")]
        public IActionResult Create(Guid nominalId)
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ActualPointViewModel actualPointViewModel, Guid nominalId, CancellationToken cancellationToken)
        {
            var nominalPoint = await _nominalPointAppService.GetByIDAsync(nominalId, cancellationToken);
            if (nominalPoint == null)
            {   
                return View(actualPointViewModel);
            }
            try
            {
                await _actualPointAppService.AddAsync(actualPointViewModel.X, actualPointViewModel.Y, actualPointViewModel.Z, nominalPoint, cancellationToken);
                return RedirectToAction(nameof(GetCalculatedByNominalId), new{nominalId});
            }
            catch (ValidationException ex)
            {
                ViewBag.MessageError = ex.Message;
                return View(actualPointViewModel);
            }
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(Guid nominalId, Guid? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actualPointViewModel = await _actualPointAppService.GetByIDAsync(id.Value, cancellationToken);
            if (actualPointViewModel == null)
            {
                return NotFound();
            }
            return View(actualPointViewModel);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid nominalId, Guid id, [Bind("X,Y,Z")] ActualPointViewModel actualPointViewModel, CancellationToken cancellationToken)
        {
            try
            {
                await _actualPointAppService.UpdateAsync(id,actualPointViewModel.X, actualPointViewModel.Y, actualPointViewModel.Z, cancellationToken);
                return RedirectToAction(nameof(GetCalculatedByNominalId), new{nominalId});
            }
            catch (ValidationException ex)
            {
                ViewBag.MessageError = ex.Message;
                return View(actualPointViewModel);
            }
        }
        [HttpGet("delete")]
        public async Task<IActionResult> Delete(Guid nominalId, Guid? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actualPointViewModel = await _actualPointAppService.GetByIDAsync(id.Value, cancellationToken);
            if (actualPointViewModel == null)
            {
                return NotFound();
            }

            return View(actualPointViewModel);
        }

        [HttpPost("delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid nominalId, Guid id, CancellationToken cancellationToken)
        {
            await _actualPointAppService.DeleteAsync(id, cancellationToken);
            return RedirectToAction(nameof(GetCalculatedByNominalId),  new{nominalId});
        }

    }
}
