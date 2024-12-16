using Microsoft.AspNetCore.Mvc;
using ITS.Data;
using ITS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Make sure to include this for async
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ITS.Controllers
{
    [Authorize]
    public class IssuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var issues = await _context.Issues.ToListAsync();  
            return View(issues);  
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Issues.Add(issue);  
                _context.SaveChanges();  
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        public IActionResult Edit(int id)
        {
            var issue = _context.Issues.Find(id); // Fetch issue by ID
            if (issue == null) return NotFound();
            return View(issue);
        }

        [HttpPost]
        public IActionResult Edit(Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Issues.Update(issue); // Update issue in the database
                _context.SaveChanges(); // Save changes
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        // GET: Issues/Delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue); // Return the Delete view with the issue model
        }

        // POST: Issues/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Redirect to Index after deletion
        }
    }
}