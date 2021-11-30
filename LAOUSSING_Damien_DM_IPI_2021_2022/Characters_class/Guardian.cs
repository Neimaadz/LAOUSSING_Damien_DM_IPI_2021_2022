using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Guardian : Character, IHolyDamage
    {

        public Guardian(string name) : base(name, 50, 150, 50, 50, 150, 150, 3, 3)
        {
        }


        
        public override void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            Console.WriteLine("{0} lance Contre-attaque", Name);
            int bonusAttack = (margeAttack * (-1)) * 2; // Bonus contre-attaque doublé

            int jetAttack = Attack + bonusAttack + new Random().Next(1, 101);
            int jetDefense = target.Defense + new Random().Next(1, 101);
            int margeCounterAttack = jetAttack - jetDefense;
            int damageDeal = margeCounterAttack * Damage / 100;

            DealDamage(characters, this, target, margeCounterAttack, damageDeal);
        }






    }
}
