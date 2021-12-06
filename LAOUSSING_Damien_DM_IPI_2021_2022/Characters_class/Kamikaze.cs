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


            (this as IPain).IsSensitiveToPain();
        }



        public override int JetAttack()
        {
            return Attack + sameJet;    // Même jet d'attaque
        }

        public override void ActionAttack(List<Tuple<int, Character>> characters, Character target)
        {
            Console.WriteLine("{0} lance Attaque", Name);

            int jetAttack = JetAttack();
            int targetJetDefense = target.JetDefense();
            int margeAttack = jetAttack - targetJetDefense;
            int damageDeal = margeAttack * Damage / 100;

            DealDamage(characters, this, target, margeAttack, damageDeal);
        }

        public override void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            // Le Kamikaze ne peut pas contre-attaquer
            return;
        }



        // Kamikaze : chaque personnage présent sur le champ de bataille (y compris lui) a 50% de chances d’être ciblé par son attaque
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
