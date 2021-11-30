using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Robot : Character
    {

        public Robot(string name) : base(name, 10, 100, 50, 50, 200, 200, 1, 1)
        {
        }


        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions

            // Robot : Au début de chaque round, le robot augmente son attaque de 50%
            int damageBoost = (int)(Attack * 0.5);  // +50% attaque
            Attack += damageBoost;
            Console.WriteLine("{0} augmente de 50% son Attaque", Name);
            Console.WriteLine("{0} : +{1} Attaque", Name, damageBoost);
            Console.WriteLine();
        }




        public override void ActionAttack(List<Tuple<int, Character>> characters, Character target)
        {
            Console.WriteLine("{0} lance Attaque", Name);

            int jetAttack = Attack + 50;    // +50 attaque
            int jetDefense = target.Defense + new Random().Next(1, 101);
            int margeAttack = jetAttack - jetDefense;
            int damageDeal = margeAttack * Damage / 100;

            DealDamage(characters, this, target, margeAttack, damageDeal);
        }

        public override void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            Console.WriteLine("{0} lance Contre-attaque", Name);
            int bonusAttack = margeAttack * (-1);

            int jetAttack = Attack + bonusAttack + 50;    // +50 attaque
            int jetDefense = target.Defense + new Random().Next(1, 101);
            int margeCounterAttack = jetAttack - jetDefense;
            int damageDeal = margeCounterAttack * Damage / 100;

            DealDamage(characters, this, target, margeCounterAttack, damageDeal);
        }



    }
}
