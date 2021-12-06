﻿using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Round
    {
        private List<Tuple<int, Character>> Characters;
        public Character PlayerCharacter;

        public Round(List<Tuple<int, Character>> characters)
        {
            this.Characters = characters;
        }



        public void PlayRound()
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                Character currentCharacter = Characters[i].Item2;

                AlertCantAttack(currentCharacter);


                // ************************* Round du JOUEUR *************************

                if (currentCharacter == PlayerCharacter && PlayerCharacter.CurrentAttackNumber > 0 && PlayerCharacter.CurrentLife > 0 && Battle.HaveWinner(Characters) == false)
                {
                    // Tant que personnage du JOUEUR peux attaquer
                    while (PlayerCharacter.CurrentAttackNumber > 0 && PlayerCharacter.CurrentLife > 0 && Battle.HaveWinner(Characters) == false)
                    {
                        int indexTarget = 0;
                        Character target = PlayerActions.ChooseTarget(Characters, PlayerCharacter);
                        Console.WriteLine();

                        // permet de récup l'index de la cible
                        Characters.ForEach(c => { if (c.Item2 == target) indexTarget = Characters.IndexOf(c); });

                        Console.WriteLine();
                        PlayerCharacter.ActionAttack(Characters, target);
                        Console.WriteLine();


                        // Le personnage JOUEUR meurt (d'une contre-attaque)
                        if (PlayerCharacter.CurrentLife <= 0)
                        {
                            AlertPlayerCharacterDead();
                        }

                        i = UpdateIndex(currentCharacter, target, i, indexTarget);

                        AlertCantAttack(currentCharacter);
                    }

                    PlayerActions.PressSpaceContinue();
                }

                // ************************* Round d'un PNJ *************************

                if (currentCharacter != PlayerCharacter)
                {
                    while (currentCharacter.CurrentAttackNumber > 0 && currentCharacter.CurrentLife > 0 && Battle.HaveWinner(Characters) == false)
                    {
                        int indexTarget = 0;
                        Character target = currentCharacter.RandomTarget(Characters);

                        // permet de récup l'index de la cible
                        Characters.ForEach(c => { if (c.Item2 == target) indexTarget = Characters.IndexOf(c); });

                        currentCharacter.ActionAttack(Characters, target);
                        Console.WriteLine();


                        // Le personnage JOUEUR est la cible et meurt (de l'attaque)
                        if (target == PlayerCharacter && PlayerCharacter.CurrentLife <= 0)
                        {
                            AlertPlayerCharacterDead();
                            PlayerActions.PressSpaceContinue();
                        }

                        i = UpdateIndex(currentCharacter, target, i, indexTarget);

                        AlertCantAttack(currentCharacter);
                    }
                }




                Battle.HaveWinner(Characters); // On check s'il y a un gagnant durant le round
            }

            Battle.AlertHaveWinner(Characters);    // Alert Message : Winner
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


        // =======================================================================
        // Method : met à jour l'indice selon cas précis
        // =======================================================================
        private int UpdateIndex(Character currentCharacter, Character target, int i, int indexTarget)
        {
            // Si attaquant meurt (par contre-attaque) && position attaquant est AVANT celle défenseur
            if (currentCharacter.CurrentLife <= 0 && i <= indexTarget)
            {
                // Si position attaquant <= 0
                if (i <= 0)
                {
                    i = -1; // i sera égal à -1 pour juste après revenir à i=0 (boucle for : i++)
                    return i;
                }
                else
                {
                    i -= 1; // On retire -1 (personnage)
                    return i;
                }
            }
            // Si attaquant tue defenseur et position attaquant est APRES celle defenseur
            else if (target.CurrentLife <= 0 && i > indexTarget)
            {
                // Si position attaquant > 0
                if (i > 0)
                {
                    i -= 1;
                    return i;
                }
            }

            return i;
        }


        // =======================================================================
        // Method : affiche un message si un personnage ne peux plus attaquer
        // =======================================================================
        private void AlertCantAttack(Character currentCharacter)
        {
            if (currentCharacter.CurrentAttackNumber <= 0 && currentCharacter.CurrentLife > 0 && Battle.HaveWinner(Characters) == false)
            {
                Console.WriteLine("{0} ne peut plus attaquer", currentCharacter.Name);
                Console.WriteLine();
            }
        }


        // =======================================================================
        // Method : affiche la liste des personnages restants
        // =======================================================================
        public static void AlertCharactersRemaining(List<Tuple<int, Character>> characters, Character playerCharacter)
        {
            // On affiche la liste des personnages restant
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].Item2 != playerCharacter)
                {
                    Console.WriteLine("     {0} : {1} ({2}) vie : {3}",
                        i + 1, characters[i].Item2.Name, characters[i].Item2.GetType().Name, characters[i].Item2.CurrentLife);
                }
                else
                {
                    Console.WriteLine("===> {0} : {1} ({2}) vie : {3}", i + 1, playerCharacter.Name, playerCharacter.GetType().Name, playerCharacter.CurrentLife);
                }
            }
        }


        // =======================================================================
        // Method : affiche un message alert de la mort du personnage du JOUEUR
        // =======================================================================
        private void AlertPlayerCharacterDead()
        {
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Console.WriteLine("xxxxxxxx                                        xxxxxxxx");
            Console.WriteLine("xxxxxxxx      Game Over : vous êtes mort !      xxxxxxxx");
            Console.WriteLine("xxxxxxxx                                        xxxxxxxx");
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Console.WriteLine();
        }


        // =======================================================================
        // Method : affiche resume du round
        // =======================================================================
        public void AlertResumeRound()
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("=====          Resume du round           =====");
            Console.WriteLine("==============================================");
            Console.WriteLine("=====                                         ");
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].Item2 != PlayerCharacter)
                {
                    Console.WriteLine("=====    {0} ({1}) vie restant : {2}", Characters[i].Item2.Name, Characters[i].Item2.GetType().Name, Characters[i].Item2.CurrentLife);
                }
                else
                {
                    Console.WriteLine("======>  {0} ({1}) vie restant : {2}", PlayerCharacter.Name, Characters[i].Item2.GetType().Name, PlayerCharacter.CurrentLife);
                }
            }
            Console.WriteLine("=====                                         ");
            Console.WriteLine("==============================================");
            Console.WriteLine();
        }







    }
}
