using Amoeba.Areas.Admin.ViewModels;
using Amoeba.DAL;
using Amoeba.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amoeba.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<Category> items = await _context.Categories.Include(x => x.Products).ToListAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM create)
        {
            if (!ModelState.IsValid) return View(create);

            bool result = await _context.Categories.AnyAsync(x => x.Name.Trim().ToLower() == create.Name.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Name", "Is exists");
                return View(create);
            }

            Category item = new Category { Name = create.Name };

            await _context.Categories.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Category item = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();

            UpdateCategoryVM update = new UpdateCategoryVM { Name = item.Name };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateCategoryVM update)
        {
            if (!ModelState.IsValid) return View(update);

            Category item = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();

            bool result = await _context.Categories.AnyAsync(x => x.Name.Trim().ToLower() == update.Name.Trim().ToLower() && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Is exists");
                return View(update);
            }

            item.Name = update.Name;

            await _context.Categories.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Category item = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();

            _context.Categories.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
