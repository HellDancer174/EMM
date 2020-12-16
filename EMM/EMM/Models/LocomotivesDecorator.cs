using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class LocomotivesDecorator : RouteDecorator
    {
        public LocomotivesDecorator(Route route) : base(route)
        {
        }

        public bool HasDiselLocomotive()
        {
            foreach(var loc in locomotives)
            {
                if (Contains(loc, "ЧМЭ", "2ТЭ", "ТЭ")) return true;
            }
            return false;
        }

        private bool Contains(Locomotive loc, params string[] types)
        {
            var name = loc.ToString();
            foreach(var type in types)
            {
                if (name.StartsWith(type)) return true;
            }
            return false;
            
        }

        public bool HasElectroLocomotive()
        {
            foreach (var loc in locomotives)
            {
                if (Contains(loc, "2ЭС", "ЭП", "ВЛ", "2*2ЭС", "2*ВЛ", "1.5*ВЛ", "ЧС")) return true;
            }
            return false;
        }
    }
}
