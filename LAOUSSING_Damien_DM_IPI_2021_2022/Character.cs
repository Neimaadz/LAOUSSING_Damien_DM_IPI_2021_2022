using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public abstract class Character
    {
        public string Name;
        public int Attack;
        public int Defense;
        public int Initiative;
        public int Damage;
        public int MaximumLife;
        public int CurrentLife;
        public int CurrentAttackNumber;
        public int TotalAttackNumber;

        public Character()
        {
            
        }

        public Character(string name, int attack, int defense, int initiative, int damage, int maximumLife, int currentLife, int currentAttackNumber, int totalAttackNumber)
        {
            this.Name = name;
            this.Attack = attack;
            this.Defense = defense;
            this.Initiative = initiative;
            this.Damage = damage;
            this.MaximumLife = maximumLife;
            this.CurrentLife = currentLife;
            this.CurrentAttackNumber = currentAttackNumber;
            this.TotalAttackNumber = totalAttackNumber;
        }


        // Appler au début du combat
        public void OnInit()
        {
            Console.WriteLine("{0} ({1}) rejoint la partie", Name, this.GetType().Name);
        }

        // Appler à chaque début round
        public virtual void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions
        }

        public virtual int JetAttack()
        {
            return Attack + new Random().Next(1, 101);
        }

        public virtual int JetDefense()
        {
            return Defense + new Random().Next(1, 101);
        }

        public virtual void ActionAttack(List<Tuple<int, Character>> characters, Character target)
        {
            Console.WriteLine("{0} lance Attaque", Name);

            int jetAttack = JetAttack();
            int targetJetDefense = target.JetDefense();
            int margeAttack = jetAttack - targetJetDefense;
            int damageDeal = margeAttack * Damage / 100;

            DealDamage(characters, this, target, margeAttack, damageDeal);
        }

        public virtual void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            Console.WriteLine("{0} lance Contre-attaque", Name);
            int bonusAttack = margeAttack * (-1);

            int jetAttack = JetAttack() + bonusAttack;
            int targetJetDefense = target.JetDefense();
            int margeCounterAttack = jetAttack - targetJetDefense;
            int damageDeal = margeCounterAttack * Damage / 100;

            DealDamage(characters, this, target, margeCounterAttack, damageDeal);
        }









        /*
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
        /****************************************************************************************************************************
         *********************************                     FONCTION DIVERS                          *****************************
         ****************************************************************************************************************************/


        // Methode permettant de savoir si une attaque et réussi ou pas selon la margeAttack
        public static void DealDamage(List<Tuple<int, Character>> characters, Character character, Character target, int margeAttack, int damageDeal)
        {
            character.CurrentAttackNumber -= 1;   // On retire -1 point d'attaque

            switch (margeAttack)
            {
                //============================ Attaque réussi ===========================================================
                case int n when n > 0:

                    //============================ Dégats selon type de perso ===========================================================

                    if (target is ICursed && character is IHolyDamage)
                    {
                        Console.WriteLine("{0} inflige des dégats sacrés", character.Name);
                        Console.WriteLine("{0} : -{1} PDV", target.Name, (character as IHolyDamage).DealHolyDamage(damageDeal));
                        target.CurrentLife -= (character as IHolyDamage).DealHolyDamage(damageDeal);
                    }
                    else if (target is IBlessed && character is IUnholyDamage)
                    {
                        Console.WriteLine("{0} inflige des dégats impies", character.Name);
                        Console.WriteLine("{0} : -{1} PDV", target.Name, (character as IUnholyDamage).DealUnholyDamage(damageDeal));
                        target.CurrentLife -= (character as IUnholyDamage).DealUnholyDamage(damageDeal);
                    }
                    else
                    {
                        Console.WriteLine("{0} : -{1} PDV", target.Name, damageDeal);
                        target.CurrentLife -= damageDeal;
                    }


                    //============================ Cas propre au perso ===========================================================

                    // Si on est un Vampire
                    if (character is Vampire)
                    {
                        int damageHeal = damageDeal / 2;
                        Console.WriteLine("{0} vole de la vie", character.Name);
                        Console.WriteLine("{0} : +{1} PDV", character.Name, damageHeal);
                        character.CurrentLife += damageHeal;

                        if (character.CurrentLife >= character.MaximumLife)  // Pour caper la vie
                        {
                            character.CurrentLife = character.MaximumLife;
                        }
                    }


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

                    if ( !(character.GetType() == typeof(Kamikaze)) )   // On ne peut pas contre-attaquer un Kamikaze
                    {
                        if (target.CurrentAttackNumber > 0)    // Si le défenseur qui contre-attaque possède assez de point d'attaque
                        {
                            target.ActionCounterAttack(characters, character, margeAttack);  // Defenseur contre attaque
                        }
                    }

                    break;
            }
        }

        

        // Methode permettant de check s'il y a un personnage qui est mort
        public static void IsCharacterDead(List<Tuple<int, Character>> characters, Character character)
        {
            int index = 0;

            if (character.CurrentLife <= 0)
            {
                while (characters[index].Item2.Name != character.Name)  // Afin de trouver, parmi la liste, le perso qui est mort selon son Nom
                {
                    index++;
                }
                Console.WriteLine("{0} est mort", character.Name);

                for (int i = 0; i < characters.Count; i++)
                {
                    if (characters[i].Item2 is IScavenger && character.Name != characters[i].Item2.Name)    // Si il y a des charognards dans la partie et pour ne pas s'auto manger
                    {
                        (characters[i].Item2 as IScavenger).EatDeadCharacter();
                    }
                }

                characters.RemoveAt(index); // On retire le personnage mort de liste des personnages
            }
        }





    }
}
