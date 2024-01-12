using Amoeba.Areas.Admin.ViewModels;
using Amoeba.DAL;
using Amoeba.Models;
using Amoeba.Utilities.Extention;
using Amoeba.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amoeba.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page <= 0) return BadRequest();
            double totalPage = await _context.Teams.CountAsync();
            ICollection<Team> items = await _context.Teams.Skip((page - 1) * 4).Take(4)
                .Include(x => x.Position).ToListAsync();

            PaginationVM<Team> vm = new PaginationVM<Team>
            {
                CurrentPage = page,
                TotalPage = Math.Ceiling(totalPage / 4),
                Items = items
            };
            return View(vm);
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create()
        {
            CreateTeamVM create = new CreateTeamVM
            {
                Positions = await _context.Positions.ToListAsync()
            };
            return View(create);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamVM create)
        {
            if (!ModelState.IsValid)
            {
                create.Positions = await _context.Positions.ToListAsync();
                return View(create);
            }

            bool result = await _context.Products.AnyAsync(x => x.Name.Trim().ToLower() == create.Name.Trim().ToLower());
            if (result)
            {
                create.Positions = await _context.Positions.ToListAsync();
                ModelState.AddModelError("Name", "Is exists");
                return View(create);
            }

            if (!create.Photo.IsValid())
            {
                create.Positions = await _context.Positions.ToListAsync();
                ModelState.AddModelError("Photo", "Not valid");
                return View(create);
            }
            if (!create.Photo.LimitSize())
            {
                create.Positions = await _context.Positions.ToListAsync();
                ModelState.AddModelError("Photo", "Limit is 10MB");
                return View(create);
            }

            Team item = new Team
            {
                Name = create.Name,
                TwitLink = create.TwitLink,
                FaceLink = create.FaceLink,
                InstaLink = create.InstaLink,
                LinkedLink = create.LinkedLink,
                PositionId = create.PositionId,
                Img = await create.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img")

            };

            await _context.Teams.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Team item = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();

            UpdateTeamVM update = new UpdateTeamVM
            {
                Name = item.Name,
                Img = item.Img,
                TwitLink = item.TwitLink,
                FaceLink = item.FaceLink,
                InstaLink = item.InstaLink,
                LinkedLink = item.LinkedLink,
                PositionId = item.PositionId,
                Positions = await _context.Positions.ToListAsync()
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateTeamVM update)
        {
            if (!ModelState.IsValid)
            {
                update.Positions = await _context.Positions.ToListAsync();
                return View(update);
            }

            Team item = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();

            bool result = await _context.Teams.AnyAsync(x => x.Name.Trim().ToLower() == update.Name.Trim().ToLower() && x.Id != id);
            if (result)
            {
                update.Positions = await _context.Positions.ToListAsync();
                ModelState.AddModelError("Name", "Is exists");
                return View(update);
            }
            if (update.Photo is not null)
            {

                if (!update.Photo.IsValid())
                {
                    update.Positions = await _context.Positions.ToListAsync();
                    ModelState.AddModelError("Photo", "Not valid");
                    return View(update);
                }
                if (!update.Photo.LimitSize())
                {
                    update.Positions = await _context.Positions.ToListAsync();
                    ModelState.AddModelError("Photo", "Limit is 10MB");
                    return View(update);
                }

                item.Img.DeleteAsync(_env.WebRootPath, "assets", "img");
                item.Img = await update.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
            }

            item.Name = update.Name;
            item.TwitLink = update.TwitLink;
            item.FaceLink = update.FaceLink;
            item.InstaLink = update.InstaLink;
            item.LinkedLink = update.LinkedLink;
            item.PositionId = update.PositionId;

            await _context.Teams.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Team item = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();

            _context.Teams.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
