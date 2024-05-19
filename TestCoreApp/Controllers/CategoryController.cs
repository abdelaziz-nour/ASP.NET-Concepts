using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestCoreApp.Models;
using TestCoreApp.Repo.Base;

namespace TestCoreApp.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private readonly IUnitOfWork _unitOfWork;

        public async Task<IActionResult> Index()
        {
            var oneCat = _unitOfWork.categorries.SelectOne(x => x.Name == "SSD");
            return View(await _unitOfWork.categorries.FindAllAsync("Items"));
        }


        //GET
        public IActionResult New()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Category category)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (category.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    category.clientFile.CopyTo(stream);
                    category.dbImage = stream.ToArray();
                }
                _unitOfWork.categorries.addOne(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        //GET
        public IActionResult Edit(int? Id)
        {
            Category category = _unitOfWork.categorries.FindById(Id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.categorries.updateOne(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        //GET
        public IActionResult Delete(int? Id)
        {
            Category category = _unitOfWork.categorries.FindById(Id.Value);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.categorries.deleteOne(category);
            }
            return RedirectToAction("Index");
        }




    }

}
