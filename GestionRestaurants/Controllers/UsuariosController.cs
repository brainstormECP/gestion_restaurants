using GestionRestaurants.Models;
using GestionRestaurants.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : BaseController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuariosController(DbContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var usuarios = _db.Set<Usuario>().Select(u => new UsuarioViewModel { Id = u.Id, Usuario = u.UserName ,Nombre = u.Nombre, Activo = u.Activo, Roles = _userManager.GetRolesAsync(u).Result.ToList() });
            return View(await usuarios.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _db.Set<Reserva>()
                .Include(r => r.Hora)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Nuevo()
        {
            ViewData["Roles"] = new MultiSelectList(_db.Set<IdentityRole>().ToList(), "Name", "Name");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Nuevo([Bind("Roles,Nombre,Usuario,Contraseña,ConfirmarContraseña")] NuevoUsuarioViewModel nuevoUsuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario
                {
                    UserName = nuevoUsuarioViewModel.Usuario + "@restaurant.cu",
                    Activo = true,
                    Email = nuevoUsuarioViewModel.Usuario + "@restaurant.cu",
                    Nombre = nuevoUsuarioViewModel.Nombre,
                };
                var result = await _userManager.CreateAsync(user,nuevoUsuarioViewModel.Contraseña);
                if (result.Succeeded)
                {
                    foreach (var rol in nuevoUsuarioViewModel.Roles)
                    {
                        await _userManager.AddToRoleAsync(user, rol);
                    }                    
                    return RedirectToAction(nameof(Index));
                }                
            }
            ViewData["Roles"] = new MultiSelectList(_db.Set<IdentityRole>().ToList(), "Name", "Name",nuevoUsuarioViewModel.Roles);
            return View(nuevoUsuarioViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditarRoles(string usuarioId)
        {
            var user = await _userManager.FindByIdAsync(usuarioId);
            if (user == null)
            {
                throw new ApplicationException($"No se pudo cargar el usuario con ID '{usuarioId}'.");
            }
            ViewBag.Roles = new MultiSelectList(_db.Set<IdentityRole>().ToList(), "Name", "Name", _userManager.GetRolesAsync(user).Result.ToList());
            return View(new UsuarioViewModel { Id = usuarioId, Nombre = user.UserName, Roles = _userManager.GetRolesAsync(user).Result.ToList() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarRoles(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(Index));
            }
            var roles = _userManager.GetRolesAsync(user).Result.ToList();
            var succeded = true;
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                //AddErrors(result);
                succeded = false;
            }
            result = await _userManager.AddToRolesAsync(user, model.Roles);
            if (!result.Succeeded)
            {
                //AddErrors(result);
                succeded = false;
            }
            if (succeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string usuarioId)
        {
            var user = await _userManager.FindByIdAsync(usuarioId);
            if (user == null)
            {
                throw new ApplicationException($"No se pudo cargar el usuario con ID '{usuarioId}'.");
            }
            return View(new ResetPasswordViewModel { Id = usuarioId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(Index));
            }            
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!result.Succeeded)
            {
                TempData["error"] = "Error al resetear la contraseña";
                return View(model);
            }
            TempData["exito"] = "Contraseña cambiada correctamente";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Activar(string usuarioId)
        {
            var user = await _userManager.FindByIdAsync(usuarioId);
            if (user == null)
            {
                throw new ApplicationException($"No se pudo cargar el usuario con ID '{usuarioId}'.");
            }
            user.Activo = !user.Activo;
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
