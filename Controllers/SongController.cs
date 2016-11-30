using System;
// ASP.NET CORE Libraries
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
// Project Models
using beltexam3.Models;

namespace beltexam3.Controllers
{
    [Route("songs")]
    public class SongController : Controller
    {
        // Attach the Database Context to the class
        private PlaylistContext _context;
        public SongController(PlaylistContext context)
        {
            // This attaches the Quote Database Context to the controller
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // Run the index dynamic method
            return index();
        }

        // GET: add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("add")]
        public IActionResult Add(Song song)
        {
            Console.WriteLine("This is the add Song method");
            // Fetch the current user
            User user = fetchuser();
            // Check model validations
            if (ModelState.IsValid)
            {
                Console.WriteLine("Validations passed");
                // Add the table and save the changes
                _context.Songs.Add(song);
                _context.SaveChanges();
                // Render the View with the User model
                return RedirectToAction("Index");
            }
            // Navbar variables
            ViewBag.user_name = user.name();
            // General Viewbag settings
            ViewBag.dashboard = true;
            // Return the original Index
            SongWrapper songwrapper = new SongWrapper(_context.PopulateSongsAllOrderbyCreatedAt(), song);
            // Return the view of index with list of auctions attached
            return View("Index", songwrapper);
        }
        // GET: /{songid}/
        [HttpGet]
        [Route("{songid}")]
        public IActionResult Show(int songid)
        {
            // Run the index dynamic method
            return show_song(songid);
        }
        /*  This is the user login check method
            Takes in a view name and a model for prepping the view returned */
        public dynamic user_login()
        {
            // Pull the controller and action out of the HTTP Context
            var currentcontroller = RoutingHttpContextExtensions.GetRouteData(this.HttpContext).Values["controller"];
            var currentaction = RoutingHttpContextExtensions.GetRouteData(this.HttpContext).Values["action"];
            var currentid = RoutingHttpContextExtensions.GetRouteData(this.HttpContext).Values["id"];
            // Redirect to the login page with the returnURL passed as parameters
            return RedirectToAction("Login", "User", new { ReturnNamedRoute = currentaction, ReturnController = currentcontroller, ReturnID = currentid });
        }
        // Fetch the user object
        public User fetchuser()
        {
            // Get the user id from the Session if it exists
            int? user_id = HttpContext.Session.GetInt32("user");
            // Return the user or null
            if (user_id != null)
            {
                return _context.PopulateUserSingle((int)user_id);
            }
            else
            {
                return null;
            }
        }
        // This handles the index logic
        private dynamic index()
        {
            User user = fetchuser();
            if (user == null)
            {
                // Check to make sure user is logged in
                return user_login();
            }
            // Navbar ViewBag
            ViewBag.user_name = user.name();
            //ViewBag.user_wallet = user.Money;
            ViewBag.userid = user.Id;
            // General Viewbag settings
            ViewBag.dashboard = true;
            SongWrapper songwrapper = new SongWrapper(_context.PopulateSongsAllOrderbyCreatedAt(), new Song());
            // Return the view of index with list of auctions attached
            return View(songwrapper);
        }
        private dynamic show_song(int songid)
        {
            // Get the current user id
            User user = fetchuser();
            if (user == null)
            {
                // Check to make sure user is logged in
                return user_login();
            }
            // Get the song
            Song song = _context.PopulateSongSingle(songid);
            ViewBag.dashboard = false;
            ViewBag.userid = user.Id;
            // Render the page
            return View("Song", song);
        }
    }
}
