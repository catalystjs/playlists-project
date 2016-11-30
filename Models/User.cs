// Common Libraries
using System;
using System.Collections.Generic;
// Validation Annotations
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
// ASP.NET Core Identity Libraries
using Microsoft.AspNetCore.Identity;


namespace beltexam3.Models
{
    public class User : BaseEntity
    {
        // Parameters for Model
        [Key]
        public int Id { get; private set; }
        [Display(Name = "First Name")]
        [Required]
        [RegularExpression(@"[a-zA-Z]+", ErrorMessage = "First Name can only comprise of letters")]
        [MinLength(2)]
        [MaxLength(100)]
        public string First_name { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"[a-zA-Z]+", ErrorMessage = "First Name can only comprise of letters")]
        [MinLength(2)]
        [MaxLength(100)]
        public string Last_name { get; set; }
        [Required]
        [EmailAddressAttribute]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(Name = "Username")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(255)]
        [RegularExpression(@"[a-zA-Z0-9]+[\!\@\#\$\%]+", ErrorMessage = "Password must contain letters, numbers, and special characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Try Again!")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string Confirmpassword { get; set; }
        // Reverse references for FK's
        public ICollection<Playlist> Playlists { get; set;}
        public DateTime CreatedAt { get; set; }
        // Constructor for Model
        public User()
        {
            CreatedAt = System.DateTime.Now;
            Playlists = new List<Playlist>();
        }
        // Methods for this Model
        public string name()
        {
            return $"{this.First_name} {this.Last_name}";
        }
        // String override
        public override string ToString()
        {
            return $"User Data: ID: {this.Id}, Name: {this.name()}, Email: {this.Email}";
        }
        // Hash the user password
        public void hash_password()
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            Password = Hasher.HashPassword(this, Password);
        }
        // Check the password against the hashed password
        public bool check_password(string password)
        {
            if (Password == null) { throw new Exception("String is required to check password"); }
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            return (0 != Hasher.VerifyHashedPassword(this, this.Password, password));
        }
        // Count the songs in the playlists
        public int CountSongInPlaylists(int songid)
        {
            int count = 0;
            // Iterate over the Playlists
            foreach (var playlist in Playlists)
            {
                if (playlist.SongId == songid)
                {
                    count += 1;
                }
            }
            return count;
        }
    }
}