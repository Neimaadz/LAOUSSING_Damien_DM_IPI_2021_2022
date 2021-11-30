using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Priest : Character, IBlessed, IHolyDamage
    {

        public Priest(string name) : base(name, 75, 125, 50, 50, 150, 150, 1, 1)
        {
        }


        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions

            // Priest : Se soigne de 10% de MaximumLife au début de chaque tour
            int heal = (int)(MaximumLife * 0.1);

            Console.WriteLine("{0} se soigne", Name);
            Console.WriteLine("{0} : +{1} PDV", Name, heal);
            Console.WriteLine();

            CurrentLife += heal;

            if (CurrentLife >= MaximumLife)  // Pour caper la vie
            {
                CurrentLife = MaximumLife;
            }
        }





    }
}
