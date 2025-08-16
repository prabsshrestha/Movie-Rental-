using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Prabesh.Dtos;
using Prabesh.Models;
using System.Data.Entity;


namespace Prabesh.Controllers.Api
{
    public class MoviesController : ApiController
    {
        //get the movies from the database create a private field application db context

        private ApplicationDbContext _context;

        //Initialize the db context in the constructor
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //to get the list of movies 
        //Get /api/movies

        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
               .Include(m => m.Genre)
               .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));


            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }

        //to get a single movie
        //Get /api/movies/1

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //Post /api/movies

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); // validate the input

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //Put /api/movies

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateCustomer(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // check whether the client might send invalid Id
            var MovieInDb = _context.Movies.SingleOrDefault(m => m.Id == id); // this lamda expression returns the movie if it exist in the database

            if (MovieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, MovieInDb);

            /*  //update the movie 
              MovieInDb.Name = movie.Name;
              MovieInDb.GenreId = movie.GenreId;
              MovieInDb.ReleaseDate = movie.ReleaseDate;
              MovieInDb.NumberInStock = movie.NumberInStock;*/

            _context.SaveChanges();

            return Ok();
        }
          
        //Delete /api/movies

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {

            var MovieInDb = _context.Movies.SingleOrDefault(m => m.Id == id); // this lamda expression returns the movie if it exist in the database

            //checks if the movie exists in the database
            if (MovieInDb == null)
                return NotFound();

            //delete the existing movie from the database
            _context.Movies.Remove(MovieInDb);

            _context.SaveChanges();

            return Ok();
        }
    }
}
