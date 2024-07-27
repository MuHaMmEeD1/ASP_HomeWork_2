using ASP_HomeWork_2.Entities;
using ASP_HomeWork_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ASP_HomeWork_2.Controllers
{
    public class ProductController : Controller
    {

        public List<Product> Products { get; set; }=new List<Product>();


        public ProductController()
        {

            string jsonString = System.IO.File.ReadAllText("./FakeDB/products.json");
            Products = JsonSerializer.Deserialize <List<Product>>(jsonString);

        }

        [HttpGet]
        public IActionResult Index()
        {

            var vm = new ProductsViewModel
            {

                Products = Products,

            };


            return View(vm);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var vm = new ProductViewModel
            {
                Product = new Product()
            };


            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel vm)
        {

            if (ModelState.IsValid)
            {
                vm.Product.Id = new Random().Next(100, 1000);
                Products.Add(vm.Product);
                System.IO.File.WriteAllText("./FakeDB/products.json", JsonSerializer.Serialize(Products, new JsonSerializerOptions() { WriteIndented = true }));


                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(vm);
            }

        }


        [HttpGet]

        public IActionResult Edit(int id)
        {


            var vm = new ProductViewModel
            {

                Product = Products.SingleOrDefault(e => e.Id == id)
            };


            return View(vm);


        }

        [HttpPost]

        public IActionResult Edit(ProductViewModel vm, int id)
        {


            if (ModelState.IsValid)
            {
                for (int i = 0; i < Products.Count; i++)
                {
                    if (id == Products[i].Id)
                    {
                        Products[i].Name = vm.Product.Name;
                        Products[i].Price = vm.Product.Price;
                    }
                }

                System.IO.File.WriteAllText("./FakeDB/products.json", JsonSerializer.Serialize(Products, new JsonSerializerOptions { WriteIndented = true }));

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(vm);
            }



        }

        [HttpGet]

        public IActionResult Delete(int id)
        {

            Product product = Products.Find(e => e.Id == id);

            Products.Remove(product);

            System.IO.File.WriteAllText("./FakeDB/products.json", JsonSerializer.Serialize(Products, new JsonSerializerOptions { WriteIndented = true }));
            
            
            return RedirectToAction(nameof(Index));
        }





    }
}
