using System;
using System.Collections.Generic;
using System.Text;
using TP5_Reseau.SharpTrooper.Entities;

namespace TP5_Reseau.ViewModel
{
    public class ViewEntity<T> where T : SharpEntity
    {
        public T Entity { get; set; }
        // T hérite de SharpEntity qui possède la propriété url
        public string Url { get => this.Entity.url; }
        public ViewEntity() { }
    }
}
