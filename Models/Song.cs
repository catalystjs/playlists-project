// Common Libraries
using System;
using System.Collections.Generic;
// Validation Annotations
using System.ComponentModel.DataAnnotations;

namespace beltexam3.Models
{
    public class Song : BaseEntity
    {
        [Key]
        public int Id { get; set;}
        [Required]
        [MinLength(2)]
        public string Title {get; set; }
        [Required]
        [MinLength(2)]
        public string Artist { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
        public DateTime CreatedAt { set; get; }
        // This is the constructor
        public Song()
        {
            CreatedAt = System.DateTime.Now;
            Playlists = new List<Playlist>();
        }
        // String override
        public override string ToString()
        {
            return $"Song Data: ID: {this.Id}, Title: {this.Title}, Artist: {this.Artist}";
        }
        // Count the songs in the playlists
        public int CountSongInPlaylists()
        {
            int count = 0;
            foreach (var playlist in Playlists)
            {
                if (playlist.SongId == Id)
                {
                    count += 1;
                }
            }
            return count;
        }
    }
}