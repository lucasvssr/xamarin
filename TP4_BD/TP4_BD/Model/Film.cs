using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TP4_BD.Model
{
    [Table("Films")]
    public class Film
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(250), Unique, NotNull]
        public string Titre { get; set; }
        public int Année { get; set; }
        public string Réalisateur { get; set; }
        public Categorie Type { get; set; }
    }
}
