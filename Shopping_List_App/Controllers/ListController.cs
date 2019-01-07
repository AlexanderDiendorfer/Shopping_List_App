using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping_List_App.Models;

namespace Shopping_List_App.Controllers
{
    public class ListController : Controller
    {
        private readonly ShoppingListContext _context;

        public ListController(ShoppingListContext context)
        {
            _context = context;
        }

        // GET: List
        public async Task<IActionResult> Index()
        {
            List<List> lists = _context.Lists.ToList();
            return View(lists);
            //return View(await _context.Lists.ToListAsync());
        }

        // GET: List/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var list = await _context.Lists
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (list == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(list);
        //}

        // GET: List/Create
        public IActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
                return View(new List());
            else
                return View(_context.Lists.Find(id));
        }

        // POST: List/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,Title,UserId")] List list)
        {
            if (ModelState.IsValid)
            {
                if (list.Id == 0)
                    _context.Add(list);
                else
                    _context.Update(list);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(list);
        }

        //GET: List/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }

        // POST: List/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,UserId")] List list)
        {
            if (id != list.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(list);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListExists(list.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(list);
        }

        // GET: List/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var list = await _context.Lists.FindAsync(id);
            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ////POST: List/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var list = await _context.Lists.FindAsync(id);
        //    _context.Lists.Remove(list);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.Id == id);
        }
    }
}
