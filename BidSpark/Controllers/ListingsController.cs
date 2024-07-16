using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BidSpark.Data;
using BidSpark.Models;
using BidSpark.Data.Services;

namespace BidSpark.Controllers
{
    public class ListingsController : Controller
    {
        private readonly IListingService _listingService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ListingsController(IListingService listingService, IWebHostEnvironment webHostEnvironment)
        {
            _listingService = listingService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Listings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _listingService.GetAll();
            return View(await applicationDbContext.ToListAsync());
        }

        //        GET: Listings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _listingService.GetById(id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // GET: Listings/Create
        public IActionResult Create()
        {
            return View();
        }

        //        POST: Listings/Create
        //        To protect from overposting attacks, enable the specific properties you want to bind to.
        //        For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListingVM listing)
        {
            if (listing.Image != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images"); //wwwroot folder access...   add at constructor 
                string fileName = listing.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)) // using is a keyword used to ensure that the resources (like file streams) are properly cleaned up and released after they are used. It's like borrowing something and then returning it after you're done.
                {
                    listing.Image.CopyTo(fileStream);
                }

                var listObj = new Listing
                {
                    Title = listing.Title,
                    Description = listing.Description,
                    Price = listing.Price,
                    IdentityUserId = listing.IdentityUserId,
                    ImagePath = fileName,

                };
                await _listingService.Add(listObj);
                return RedirectToAction("Index");
            }
            return View(listing);
        }

        //        GET: Listings/Edit/5
        //        public async Task<IActionResult> Edit(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var listing = await _context.Listings.FindAsync(id);
        //            if (listing == null)
        //            {
        //                return NotFound();
        //            }
        //            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", listing.IdentityUserId);
        //            return View(listing);
        //        }

        //        POST: Listings/Edit/5
        //         To protect from overposting attacks, enable the specific properties you want to bind to.
        //         For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,ImagePath,IsSold,IdentityUserId")] Listing listing)
        //        {
        //            if (id != listing.Id)
        //            {
        //                return NotFound();
        //            }

        //            if (ModelState.IsValid)
        //            {
        //                try
        //                {
        //                    _context.Update(listing);
        //                    await _context.SaveChangesAsync();
        //                }
        //                catch (DbUpdateConcurrencyException)
        //                {
        //                    if (!ListingExists(listing.Id))
        //                    {
        //                        return NotFound();
        //                    }
        //                    else
        //                    {
        //                        throw;
        //                    }
        //                }
        //                return RedirectToAction(nameof(Index));
        //            }
        //            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", listing.IdentityUserId);
        //            return View(listing);
        //        }

        //        GET: Listings/Delete/5
        //        public async Task<IActionResult> Delete(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var listing = await _context.Listings
        //                .Include(l => l.IdentityUser)
        //                .FirstOrDefaultAsync(m => m.Id == id);
        //            if (listing == null)
        //            {
        //                return NotFound();
        //            }

        //            return View(listing);
        //        }

        //        POST: Listings/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> DeleteConfirmed(int id)
        //        {
        //            var listing = await _context.Listings.FindAsync(id);
        //            if (listing != null)
        //            {
        //                _context.Listings.Remove(listing);
        //            }

        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }

        //        private bool ListingExists(int id)
        //        {
        //            return _context.Listings.Any(e => e.Id == id);
        //        }
        //    }
    }
}
