using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Kamikaze : Character, IAlive
    {
        private int sameJet = 0; // Variable permettant de stocker le même jet d'attaque (dans OnEachRound)
        private int CountAttackOff = 0;  // Compteur de Round attaque off

        Type IPain.CharacterType { get => GetType(); set => GetType(); }
        string IPain.Name { get => Name; set => Name = value; }
        int IPain.CurrentLife { get => CurrentLife; set => CurrentLife = value; }
        int IPain.CurrentAttackNumber { get => CurrentAttackNumber; set => CurrentAttackNumber = value; }
        int IPain.TotalAttackNumber { get => TotalAttackNumber; set => TotalAttackNumber = value; }
        int IPain.CountAttackOff { get => CountAttackOff; set => CountAttackOff = value; }


        public Kamikaze(string name) : base(name, 50, 125, 20, 75, 500, 500, 6, 6)
        {
        }


        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions

            // Kamikaze : Tous les perso qui se défendent contre une attaque du kamikaze se défendent contre le même jet d’attaque
            sameJet = new Random().Next(1, 101);

            (this as IPain).IsSensitiveToPain();    // Check si on est affecté par la douleur
        }



        // =======================================================================
        // Method override : (Kamikaze) Même jet d'attaque
        // =======================================================================
        public override int JetAttack()
        {
            return Attack + sameJet;    // Même jet d'attaque
        }


        // =======================================================================
        // Method override : (Kamikaze) Le Kamikaze ne peut pas contre-attaquer
        // =======================================================================
        public override void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            // Le Kamikaze ne peut pas contre-attaquer
            return;
        }


        // =======================================================================
        // Method override : (Kamikaze) Les attaques du kamikaze ne sont pas contre-attaquable
        // =======================================================================
        public override void DealDamage(List<Tuple<int, Character>> characters, Character target, int margeAttack, int damageDeal)
        {
            CurrentAttackNumber -= 1;   // On retire -1 point d'attaque

            switch (margeAttack)
            {
                //============================ Attaque réussi ===========================================================
                case int n when n > 0:

                    Console.WriteLine("{0} : -{1} PDV", target.Name, damageDeal);
                    target.CurrentLife -= damageDeal;

                    //============================ Cas de la cible ===========================================================

                    // Si cible est sensible à la douleur
                    if (target is IPain)
                    {
                        (target as IPain).Pain(damageDeal);     // damageDeal = dégat subis
                    }

                    IsCharacterDead(characters, target);
                    break;

                //============================ Defense de l'adversaire réussi ===========================================================
                case int n when n <= 0:
                    Console.WriteLine("Echec de l'attaque...");

                    // Kamikaze : Les attaques du kamikaze ne sont pas contre-attaquable

                    break;
            }
        }


        // =======================================================================
        // Method override : (Kamikaze) chaque personnage présent sur le champ de bataille (y compris lui) a 50% de chances d’être ciblé par son attaque
        // =======================================================================
        public override Character RandomTarget(List<Tuple<int, Character>> characters)
        {
            Character target;
            int numbCharactersRemaining = characters.Count; // Nombre de personnage restant
            int index = 0;

            index = new Random().Next(0, numbCharactersRemaining);

            target = characters[index].Item2;

            return target;
        }




    }
}
