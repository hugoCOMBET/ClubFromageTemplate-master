using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Business
{
    class FamilleFromage
    {
        private int _id;
        private string _nom;
        private List<Fromage> _lesFromages;

        public FamilleFromage()
        {
            _id = 0;
            _nom = "";
            _lesFromages = new List<Fromage>();
        }
    }
}
