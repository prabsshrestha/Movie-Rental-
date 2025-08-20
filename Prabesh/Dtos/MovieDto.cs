using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prabesh.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public byte GenreId { get; set; }
        public GenreDto Genre { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Range(1, 100)]
        public byte NumberInStock { get; set; }
        public byte NumberAvailable { get; set; }
        public int Price { get; set; }
    }

    public class TopMovie
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int total { get; set; }
    }
}