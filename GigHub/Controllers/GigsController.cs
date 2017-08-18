using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
          Gig gig = new Gig();
          string currentUserId = User.Identity.GetUserId();
          ApplicationUser currentUser = _context.Users.First();//.FirstOrDefault(x => x.Id == currentUserId);
          gig.Artist = currentUser;
          gig.Venue = viewModel.Venue;
          //gig.Id = 1;
          gig.DateTime = DateTime.Now;

          Genre genre = new Genre();
          //genre.Id = 2;
          genre.Name = "Jazz";

          gig.Gernre = genre;

          _context.Gigs.Add(gig);
          _context.SaveChanges();

          //gig.Artist = UserManager.FindById(User.Identity.GetUserId());
          //gig.Artist = viewModel.a
          //_context.Gigs.Add(new Gig() { Gernre = viewModel.Genre});
          return RedirectToAction("Index", "Home");
        }
    }
}