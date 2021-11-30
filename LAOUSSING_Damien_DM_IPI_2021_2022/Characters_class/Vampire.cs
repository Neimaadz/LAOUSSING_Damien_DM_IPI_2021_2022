using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Vampire : Character, IUndead
    {

        public Vampire(string name) : base(name, 100, 100, 120, 50, 300, 300, 2, 2)
        {
        }



    }
}
