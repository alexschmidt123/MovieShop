using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;


namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        //readonly variables can only be edited in constructors.
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;

        // depend on higher level abstraction
        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
            // _movieService = new MovieService();
            // you want to have control over which implentation that you want to use
            // var homeController = new HomeController();
        }
        [HttpGet]
        public async Task< IActionResult> Index()
        {
            //home page
            // top 30 movies -> Movie Service
            //instance of MovieService class
            //newing up
            //refactor code
            //var movieService =new MovieService();
            //var movies = movieService.GetTopGrossingMovies();
            //var homeController = new HomeController(new Logger(),)
            //T1
            //var x = 9;
            //I/O bound operation, database over network, send the SQL and SQL will be excuted on DB get back with resu   lts
            //T1 will wait for this operation
            var movies = await _movieService.GetTopGrossingMovies();
            //method(int x, IMovieService service)

            //var movieService =new MovieService();
            //var movieService3 =new MovieTestService();

            //method(3, movieService3)
            //passing the data from Controller/action method to the View
            return View(movies);
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}