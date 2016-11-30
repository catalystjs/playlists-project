// Project Models
using System.Collections.Generic;
using beltexam3.ViewModels;

namespace beltexam3.Models
{
    public class SongWrapper
    {
        public ICollection<Song> Songs { get; set; }
        public Song Song { get; set; }
        public SongWrapper(ICollection<Song> songlist, Song newsong)
        {
            Songs = songlist;
            Song = newsong;
        }
    }

}