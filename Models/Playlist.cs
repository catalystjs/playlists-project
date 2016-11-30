// Common Libraries
using System;
using System.Collections.Generic;
// Validation Annotations
using System.ComponentModel.DataAnnotations;

namespace beltexam3.Models
{
    public class Playlist : BaseEntity
    {
        [Key]
        public int Id { get; set;}
        //public int Added { get; set;}
        public int UserId { get; set; }
        public User User { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
        public DateTime CreatedAt { set; get; }
        // This is the constructor
        public Playlist()
        {
            CreatedAt = System.DateTime.Now;
        }
        // String override
        public override string ToString()
        {
            return $"Playlist Data: ID: {this.Id}, User ID: {this.UserId}, Song ID: {this.SongId}";
        }
    }
}