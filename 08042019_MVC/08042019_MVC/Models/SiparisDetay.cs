//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _08042019_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SiparisDetay
    {
        public int SiparisDetayID { get; set; }
        public Nullable<int> RefSiparisID { get; set; }
        public Nullable<int> RefUrunID { get; set; }
        public Nullable<int> RefSepetID { get; set; }
        public string Kargo { get; set; }
        public Nullable<bool> TeslimEdildi { get; set; }
        public int Adet { get; set; }
        public Nullable<int> ToplamTutar { get; set; }
    
        public virtual Sepet Sepet { get; set; }
        public virtual Siparis Siparis { get; set; }
        public virtual Urunler Urunler { get; set; }
    }
}
