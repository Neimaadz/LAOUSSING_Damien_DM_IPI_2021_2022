using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Kamikaze : Character
    {
        private int sameJet = 0; // Variable permettant de stocker le même jet d'attaque (dans OnEachRound)


        public Kamikaze(string name) : base(name, 150, 50, 20, 75, 500, 500, 6, 6)
        {
        }


        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions

            // Kamikaze : Tous les perso qui se défendent contre une attaque du kamikaze se défendent contre le même jet d’attaque
            sameJet = new Random().Next(1, 101);
        }


        public override void ActionAttack(List<Tuple<int, Character>> characters, Character target)
        {
            Console.WriteLine("{0} lance Attaque", Name);

            int jetAttack = Attack + sameJet;   // Même jet d'attaque
            int jetDefense = target.Defense + new Random().Next(1, 101);
            int margeAttack = jetAttack - jetDefense;
            int damageDeal = margeAttack * Damage / 100;

            DealDamage(characters, this, target, margeAttack, damageDeal);
        }

        public override void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            // Le Kamikaze ne peut pas contre-attaquer
            return;
        }


    }
}
