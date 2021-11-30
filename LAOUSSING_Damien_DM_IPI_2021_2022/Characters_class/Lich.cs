using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Lich : Character, IUndead, IUnholyDamage
    {

        public Lich(string name) : base(name, 75, 125, 80, 50, 125, 125, 3, 3)
        {
        }


    }
}
