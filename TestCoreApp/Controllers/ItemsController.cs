using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestCoreApp.Data;
using TestCoreApp.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace TestCoreApp.Controllers
{
    [Authorize(Roles = clsRoles.roleAdmin)]
    public class ItemsController : Controller
    {
        public ItemsController(AppDbContext db, IHostingEnvironment host)
        {
            _db = db;
            _host = host;
        }
        private readonly IHostingEnvironment _host;
        private readonly AppDbContext _db;
        public IActionResult Index()
        {
            IEnumerable<Item> itenList = _db.Items.Include(c => c.Category).ToList();
            return View(itenList);
        }

        //GET
        public IActionResult New()
        {
            CreateSelectList();
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            if (item.Name == "aziz")
            {
                ModelState.AddModelError("Name", "Aziz is not an item idiot");
            }
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (item.clientFile != null)
                {
                    string myUpload = Path.Combine(_host.WebRootPath, "images");
                    fileName = item.clientFile.FileName;
                    string fullPath = Path.Combine(myUpload, fileName);
                    item.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    item.imagePath = fileName;
                }
                _db.Items.Add(item);
                _db.SaveChanges();
                TempData["success"] = "item has been saved successfully";
                return RedirectToAction("Index");

            }
            else
            {
                return View(item);
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // method 1
            var item1 = _db.Items.FirstOrDefault(x => x.Id == id);

            // method 1
            var item2 = _db.Items.Find(id);

            if (item1 == null || item2 == null)
            {
                return NotFound();
            }
            CreateSelectList(item2.CategoryId);

            return View(item2);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (item.Name == "aziz")
            {
                ModelState.AddModelError("Name", "Aziz is not an item idiot");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Update(item);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                CreateSelectList();
                return View(item);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var item = _db.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _db.Items.Remove(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public void CreateSelectList(int selectId = 1)
        {
            List<Category> categories = _db.Categories.ToList();
            SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
            ViewBag.CategoryList = listItems;
        }
    }
}
