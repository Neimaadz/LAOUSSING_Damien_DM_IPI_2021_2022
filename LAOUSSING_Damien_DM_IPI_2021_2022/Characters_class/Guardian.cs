using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Guardian : Character, IAlive, IHolyDamage
    {
        private int CountAttackOff = 0;  // Compteur de Round attaque off

        Type IPain.CharacterType { get => GetType(); set => GetType(); }
        string IPain.Name { get => Name; set => Name = value; }
        int IPain.CurrentLife { get => CurrentLife; set => CurrentLife = value; }
        int IPain.CurrentAttackNumber { get => CurrentAttackNumber; set => CurrentAttackNumber = value; }
        int IPain.TotalAttackNumber { get => TotalAttackNumber; set => TotalAttackNumber = value; }
        int IPain.CountAttackOff { get => CountAttackOff; set => CountAttackOff = value; }


        public Guardian(string name) : base(name, 50, 150, 50, 50, 150, 150, 3, 3)
        {
        }


        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions
            (this as IPain).IsSensitiveToPain();
        }



        public override void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            Console.WriteLine("{0} lance Contre-attaque", Name);
            int bonusAttack = (margeAttack * (-1)) * 2; // Bonus contre-attaque doublé

            int jetAttack = JetAttack() + bonusAttack;
            int targetJetDefense = target.JetDefense();
            int margeCounterAttack = jetAttack - targetJetDefense;
            int damageDeal = margeCounterAttack * Damage / 100;

            DealDamage(characters, this, target, margeCounterAttack, damageDeal);
        }






    }
}
