using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class WeaponRepository
    {
        public List<Weapon> Weapons = new List<Weapon>();

        public WeaponRepository()
        {            
            Weapons.Add(new Weapon { Name = "Fire", Beats = new List<string> { "Paper", "Scissor" } });
            Weapons.Add(new Weapon { Name = "Rock", Beats = new List<string> { "Scissor", "Fire" } });
            Weapons.Add(new Weapon { Name = "Paper", Beats = new List<string> { "Rock" } });
            Weapons.Add(new Weapon { Name = "Scissor", Beats = new List<string> { "Paper" } });
        }          
    }
}
