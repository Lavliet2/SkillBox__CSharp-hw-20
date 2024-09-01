using Homework_20.Context;
using Homework_20.Models;
using Homework_20.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Homework_20.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        public HomeController()
        {
            _context = new DataContext();
        }
        public IActionResult Index()
        {
            ViewBag.Contacts = _context.Contacts;
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.ID == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }
        [HttpPost]
        public IActionResult GetDataFromViewField(string lastName, string firstName, string middleName, string phoneNumber, string address, string description)
        {
            _context.Contacts.Add(
                new Contact()
                {
                    LastName = lastName,
                    FirstName = firstName,
                    MiddleName = middleName,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Description = description
                });
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contact = new DataContext().Contacts.FirstOrDefault(c => c.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            var existingContact = _context.Contacts.FirstOrDefault(c => c.ID == contact.ID);
            if (existingContact == null)
            {
                return NotFound();
            }
            existingContact.LastName = contact.LastName;
            existingContact.FirstName = contact.FirstName;
            existingContact.MiddleName = contact.MiddleName;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.Address = contact.Address;
            existingContact.Description = contact.Description;
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = contact.ID });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}
