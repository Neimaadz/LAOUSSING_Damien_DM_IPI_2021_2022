using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Berserk : Character, IAlive
    {
        private int CountAttackOff = 0;  // Compteur de Round attaque off

        Type IPain.CharacterType { get => GetType(); set => GetType(); }
        string IPain.Name { get => Name; set => Name = value; }
        int IPain.CurrentLife { get => CurrentLife; set => CurrentLife = value; }
        int IPain.CurrentAttackNumber { get => CurrentAttackNumber; set => CurrentAttackNumber = value; }
        int IPain.TotalAttackNumber { get => TotalAttackNumber; set => TotalAttackNumber = value; }
        int IPain.CountAttackOff { get => CountAttackOff; set => CountAttackOff = value; }


        public Berserk(string name) : base(name, 100, 100, 80, 20, 300, 300, 1, 1)
        {
        }


        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions

            // Berserk : TotalAttackNumber passe à 4 si sa vie est en dessous de 50%
            if (CurrentLife < (MaximumLife * 0.5))  // inférieur à 50% de sa vie max
            {
                Console.WriteLine("{0} entre dans l'état Berseker", Name);
                Console.WriteLine("{0} : +4 PA", Name);
                Console.WriteLine();

                TotalAttackNumber = 4;
                CurrentAttackNumber = TotalAttackNumber;
            }


            // Pas de check IsSensitiveToPain() : Le berseker n’est pas affecté par la douleur
        }



        public override void ActionAttack(List<Tuple<int, Character>> characters, Character target)
        {
            Console.WriteLine("{0} lance Attaque", Name);

            int jetAttack = JetAttack();
            int targetJetDefense = target.JetDefense();
            int margeAttack = jetAttack - targetJetDefense;

            // Ajoute tous les points de vie qu'il a perdu a ses dégâts au moment d’attaquer
            int damageTaken = MaximumLife - CurrentLife;
            int damageDeal = margeAttack * (Damage + damageTaken) / 100;

            DealDamage(characters, this, target, margeAttack, damageDeal);
        }

        public override void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            Console.WriteLine("{0} lance Contre-attaque", Name);
            int bonusAttack = margeAttack * (-1);
            
            int jetAttack = JetAttack() + bonusAttack;
            int targetJetDefense = target.JetDefense();
            int margeCounterAttack = jetAttack - targetJetDefense;

            // Ajoute tous les points de vie qu'il a perdu a ses dégâts au moment d’attaquer
            int damageTaken = MaximumLife - CurrentLife;
            int damageDeal = margeCounterAttack * (Damage + damageTaken) / 100;

            DealDamage(characters, this, target, margeCounterAttack, damageDeal);
        }






    }
}
