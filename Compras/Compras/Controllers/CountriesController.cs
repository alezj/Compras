#nullable disable
using Compras.Datos;
using Compras.Datos.Entities;
using Compras.Helpers;
using Compras.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vereyon.Web;
using static Compras.Helpers.ModalHelper;

namespace Compras.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public CountriesController(DataContext context, IFlashMessage flashMessage)
        {
            _context = context;
            _flashMessage = flashMessage;
        }

        // GET: Countries
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.countries
                .Include(c => c.States)
                .ThenInclude(s => s.Cities)
                .ToListAsync());
        }

        // GET: Countries/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.countries
                .Include(c => c.States)
                .ThenInclude(s=> s.Cities)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsState(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            State state = await _context.States
                .Include(s => s.Country)
                .Include(s => s.Cities)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }
        

        // GET: Countries/Create

        [NoDirectAccess]
        public async Task<IActionResult> AddState(int id)
        {
           

            Country country = await _context.countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            StateViewModel model = new()
            {
                CountryID = country.ID,
            };

            return View(model);
        }

        // POS: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddState(StateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    State state = new()
                    {
                        Cities = new List<City>(),
                        Country = await _context.countries.FindAsync(model.CountryID),
                        Name = model.Name,
                    };
                    _context.Add(state);
                    await _context.SaveChangesAsync();
                    Country country = await _context.countries
                .Include(c => c.States)
                .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(c => c.ID == model.CountryID);
                    _flashMessage.Info("Registro creado.");
                    return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAllStates", country) });

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Estado con el mismo nombre en este pais.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "AddState", model) });

        }

        //==============================================AddCity
        [NoDirectAccess]
        public async Task<IActionResult> AddCity(int id)
        {
            

            State state = await _context.States.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            CityViewModel model = new()
            {
                StateID = state.ID,
            };

            return View(model);
        }

        // POS: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    City city = new()
                    {
                        
                        State = await _context.States.FindAsync(model.StateID),
                        Name = model.Name,
                    };
                    _context.Add(city);
                    await _context.SaveChangesAsync();
                    State state = await _context.States
                .Include(s => s.Cities)
                .FirstOrDefaultAsync(c => c.ID == model.StateID);
                _flashMessage.Confirmation("Registro Creeado");
                return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAllCities", state) });
                       

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Ciudad con el mismo nombre en este Estado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "AddCity", model) });

        }
        // GET: Countries/Edit/5
        [NoDirectAccess]
        public async Task<IActionResult> EditState(int? id)
        {
           

            State state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (state == null)
            {
                return NotFound();
            }
            StateViewModel model = new()
            {
                CountryID = state.Country.ID,
                ID = state.ID,
                Name = state.Name,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditState(int id, StateViewModel model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    State state = new()
                    {
                        ID = model.ID,
                        Name = model.Name,
                    };
                    _context.Update(state);
                    Country country = await _context.countries
                 .Include(c => c.States)
                 .ThenInclude(s => s.Cities)
                 .FirstOrDefaultAsync(c => c.ID == model.CountryID);
                    await _context.SaveChangesAsync();
                    _flashMessage.Confirmation("Registro Actualizado");
                    return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAllStates", country) });

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Estado con mismo nombre en este pais.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
             return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "EditState", model) });
        }
        [NoDirectAccess]
        public async Task<IActionResult> EditCity(int id)
        {
           

            City city = await _context.Cities
                .Include(c => c.State)
                .FirstOrDefaultAsync(c => c.ID == id);
            if (city == null)
            {
                return NotFound();
            }
           CityViewModel model = new()
            {
                StateID = city.State.ID,
                ID = city.ID,
                Name = city.Name,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
   
        public async Task<IActionResult> EditCity(int id, CityViewModel model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    City city = new()
                    {
                        ID = model.ID,
                        Name = model.Name,
                        
                    };
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    State state = await _context.States
               .Include(s => s.Cities)
               .FirstOrDefaultAsync(c => c.ID == model.StateID);
                _flashMessage.Confirmation("Registro actualizado.");
                return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAllCities", state) });

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Ciudad con el mismo nombre en este Estado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "EditCity", model) });
        }

        // GET: Countries/Delete/5
        [NoDirectAccess]
        public async Task<IActionResult> Delete(int? id)
        {
           

            Country country = await _context.countries.FirstOrDefaultAsync(c => c.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            try
            {
                _context.countries.Remove(country);
                await _context.SaveChangesAsync();
                _flashMessage.Info("Registro borrado.");
            }
            catch
            {
                _flashMessage.Danger("No se puede borrar el país porque tiene registros relacionados.");
            }

            return RedirectToAction(nameof(Index));
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Country());
            }
            else
            {
                Country country = await _context.countries.FindAsync(id);
                if (country == null)
                {
                    return NotFound();
                }

                return View(country);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0) //Insert
                    {
                        _context.Add(country);
                        await _context.SaveChangesAsync();
                        _flashMessage.Info("Registro creado.");
                    }
                    else //Update
                    {
                        _context.Update(country);
                        await _context.SaveChangesAsync();
                        _flashMessage.Info("Registro actualizado.");
                    }
                    return Json(new
                    {
                        isValid = true,
                        html = ModalHelper.RenderRazorViewToString(
                            this,
                            "_ViewAllCountries",
                            _context.countries
                                .Include(c => c.States)
                                .ThenInclude(s => s.Cities)
                                .ToList())
                    });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("Ya existe un país con el mismo nombre.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                }
            }

            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "AddOrEdit", country) });
        }


        // GET: Countries/Delete/5
        [NoDirectAccess]
        public async Task<IActionResult> DeleteState(int? id)
        {

            State state = await _context.States
                .Include(s => s.Country)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (state == null)
            {
                return NotFound();
            }

            try
            {
                _context.States.Remove(state);
                await _context.SaveChangesAsync();
                _flashMessage.Info("Registro borrado.");
            }
            catch
            {
                _flashMessage.Danger("No se puede borrar el estado  porque tiene registros relacionados.");
            }

            return RedirectToAction(nameof(Details), new { Id = state.Country.ID });
        }

        [NoDirectAccess]
        public async Task<IActionResult> DeleteCity(int id)
        {

            City city = await _context.Cities
                .Include(c => c.State)
                .FirstOrDefaultAsync(c => c.ID == id);
            if (city == null)
            {
                return NotFound();
            }

            try
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
            catch
            {
                _flashMessage.Danger("No se puede borrar la ciudad porque tiene registros relacionados.");
            }

            _flashMessage.Info("Registro borrado.");
            return RedirectToAction(nameof(DetailsState), new { id = city.State.ID });
        }

    }
}
