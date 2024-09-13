using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManegmentSystem.Context;
using ProductManegmentSystem.Models;

namespace ProductManegmentSystem.Controllers
{
    public class ProductController : Controller
    {
        ProductManegmentContext db=new ProductManegmentContext();
        private readonly IWebHostEnvironment _enviroment;
        public ProductController(IWebHostEnvironment enviroment)
        {
            _enviroment=enviroment;
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("Id")==null)
            {
                return RedirectToAction("Login", "User");
            }
            var products=db.Products.Include(p=>p.Category);
            return View(products);
        }

        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            if(id == 0)
            {
                return RedirectToAction("Index");
            }
            var singleProduct=db.Products.Include(p=> p.Category).FirstOrDefault(p=>p.Id==id);
            if(singleProduct == null)
            {
                return RedirectToAction("Index");
            }
            return View(singleProduct);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var Categories = new SelectList(db.Categories,"Id", "Name");
            ViewBag.Categories = Categories;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModelUsedInCreateAndEdit newProduct)
        {
            ModelState.Remove("Category");
            if(newProduct.ImageFile==null)
            {
                ModelState.AddModelError("","The Image is required");
            }
            if(!ModelState.IsValid)
            {
                var categories = new SelectList(db.Categories, "Id", "Name");
                ViewBag.Categories = categories;
                return View(newProduct);
            }

            //Saving the new image file
            var imageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            imageFileName += Path.GetExtension(newProduct.ImageFile!.FileName);
            var ImageFullPath = _enviroment.WebRootPath + "/products/" + imageFileName;
            
            using(var stream=System.IO.File.Create(ImageFullPath))
            {
                newProduct.ImageFile.CopyTo(stream);
            }

            Product product = new Product()
            {
                Title = newProduct.Title,
                Description = newProduct.Description,
                Price = newProduct.Price,
                Quantity = newProduct.Quantity,
                ImageFileName = imageFileName,
                CategoryId = newProduct.CategoryId,

            };
            db.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");


        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            if(id==0)
            {
                return RedirectToAction("Index");
            }
            var singleProduct=db.Products.Include(p=>p.Category).FirstOrDefault(p=>p.Id==id);
            if(singleProduct==null)
            {
                return RedirectToAction("Index");
            }
            return View(singleProduct);
        }

        [HttpPost]
        public IActionResult Delete(int id,string confirm)
        {
            if(id==0)
            {
                return RedirectToAction("Index");
            }
            var singleProduct = db.Products.Find(id);
            if (singleProduct==null)
            {
                return RedirectToAction("Index");
            }
            db.Remove(singleProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id==0)
            {
                return RedirectToAction("Index");
            }
            var singleProduct=db.Products.Include(p=>p.Category).FirstOrDefault(p=>p.Id==id);
            if(singleProduct==null)
            {
                return RedirectToAction("Index");
            }

            ProductModelUsedInCreateAndEdit product = new ProductModelUsedInCreateAndEdit()
            {
                Title = singleProduct.Title,
                Description = singleProduct.Description,
                Quantity = singleProduct.Quantity,
                Price = singleProduct.Price,
                CategoryId = singleProduct.CategoryId,
            };
            ViewData["Id"]=singleProduct.Id;
            ViewData["ImageFileName"] = singleProduct.ImageFileName;
            var categories = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Categories = categories;
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductModelUsedInCreateAndEdit product)
        {
            ModelState.Remove("Category");
            var oldProduct = db.Products.FirstOrDefault(p => p.Id == id);
            if (oldProduct == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                var categories = new SelectList(db.Categories, "Id", "Name");
                ViewData["Id"] = oldProduct.Id;
                ViewData["ImageFileName"] = oldProduct.ImageFileName;
                ViewBag.Categories = categories;
                return View(product);
            }
            var oldImageFileName = oldProduct.ImageFileName;
            if (product.ImageFile == null) 
            {
                
                Product updateProduct = new Product()
                {
                    Id = id,
                    Title = product.Title,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    ImageFileName = oldImageFileName
                };
                oldProduct.Title= updateProduct.Title;
                oldProduct.Description= updateProduct.Description;
                oldProduct.Quantity= updateProduct.Quantity;
                oldProduct.Price= updateProduct.Price;
                oldProduct.CategoryId= updateProduct.CategoryId;
                oldProduct.ImageFileName=updateProduct.ImageFileName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if(product.ImageFile!=null)
            {
                var newImageFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newImageFileName += Path.GetExtension(product.ImageFile!.FileName);
                var ImageFullPath = _enviroment.WebRootPath + "/products/" + newImageFileName;

                using (var stream = System.IO.File.Create(ImageFullPath))
                {
                    product.ImageFile.CopyTo(stream);
                }

                //delete the old image
                //var oldImageFullPath = _enviroment.WebRootPath + "/products/" + oldImageFileName;
                //System.IO.File.Delete(oldImageFullPath);

                Product updateProduct = new Product()
                {
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    ImageFileName = newImageFileName,
                    CategoryId = product.CategoryId,

                };
                oldProduct.Title = updateProduct.Title;
                oldProduct.Description = updateProduct.Description;
                oldProduct.Quantity = updateProduct.Quantity;
                oldProduct.Price = updateProduct.Price;
                oldProduct.CategoryId = updateProduct.CategoryId;
                oldProduct.ImageFileName = updateProduct.ImageFileName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
            
        }
    }
}
