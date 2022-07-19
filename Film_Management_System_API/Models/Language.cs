using System;
using System.Collections.Generic;

namespace Film_Management_System_API.Models
{
    public partial class Language
    {
        public Language()
        {
            FilmLanguages = new HashSet<Film>();
            FilmOriginalLanguages = new HashSet<Film>();
        }

        public decimal LanguageId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Film> FilmLanguages { get; set; }
        public virtual ICollection<Film> FilmOriginalLanguages { get; set; }
    }
}
