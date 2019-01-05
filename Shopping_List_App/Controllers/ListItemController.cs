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
    public class ListItemController : Controller
    {
        private readonly ShoppingListContext _context;

        public ListItemController(ShoppingListContext context)
        {
            _context = context;
        }

        // GET: ListItem
        public async Task<IActionResult> Index(int listId)
        {
            List<ListItem> listitems = _context.ListItems.Where(li => li.ListId == listId).ToList();
            return View(listitems);
            //var shoppingListContext = _context.ListItems.Include(l => l.Category).Include(l => l.List);
            //return View(await shoppingListContext.ToListAsync());
        }

        // GET: ListItem/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ListItem listitem = _context.ListItems.Single(li => li.Id == id);

            return View(listitem);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var listItem = await _context.ListItems.Where(l => l.ListId == id)
            //    .Include(l => l.Category)
            //    .Include(l => l.List)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (listItem == null)
            //{
            //    return NotFound();
            //}

            //return View(listItem);
        }

        // GET: ListItem/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title");
            ViewData["ListId"] = new SelectList(_context.Lists, "Id", "Title");
            return View();
        }

        // POST: ListItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Status,ListId,CategoryId")] ListItem listItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", listItem.CategoryId);
            ViewData["ListId"] = new SelectList(_context.Lists, "Id", "Title", listItem.ListId);
            return View(listItem);
        }

        // GET: ListItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.ListItems.FindAsync(id);
            if (listItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", listItem.CategoryId);
            ViewData["ListId"] = new SelectList(_context.Lists, "Id", "Title", listItem.ListId);
            return View(listItem);
        }

        // POST: ListItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Status,ListId,CategoryId")] ListItem listItem)
        {
            if (id != listItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListItemExists(listItem.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", listItem.CategoryId);
            ViewData["ListId"] = new SelectList(_context.Lists, "Id", "Title", listItem.ListId);
            return View(listItem);
        }

        // GET: ListItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.ListItems
                .Include(l => l.Category)
                .Include(l => l.List)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listItem == null)
            {
                return NotFound();
            }

            return View(listItem);
        }

        // POST: ListItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listItem = await _context.ListItems.FindAsync(id);
            _context.ListItems.Remove(listItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListItemExists(int id)
        {
            return _context.ListItems.Any(e => e.Id == id);
        }
    }
}
