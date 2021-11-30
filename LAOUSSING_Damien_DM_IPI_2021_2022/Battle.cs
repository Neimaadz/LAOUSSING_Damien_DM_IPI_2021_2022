﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Battle
    {
        private List<Tuple<int, Character>> Characters;
        private Character PlayerCharacter;
        public int countRound = 0;

        public Battle(List<Tuple<int, Character>> characters, Character playerCharacter)
        {
            this.Characters = new List<Tuple<int, Character>>(characters);
            this.PlayerCharacter = playerCharacter;
        }



        public void StartBattle()
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                Characters[i].Item2.OnInit();
            }
            Console.WriteLine();


            while (PlayerActions.ContinueOrQuit() == false && HaveWinner(Characters) == false)
            {
                Console.WriteLine("***********************************************************");
                Console.WriteLine("*****************                         *****************");
                Console.WriteLine("***********                                     ***********");
                Console.WriteLine("                        Round {0}                          ", countRound+1);
                Console.WriteLine("***********                                     ***********");
                Console.WriteLine("*****************                         *****************");
                Console.WriteLine("***********************************************************");
                Console.WriteLine();

                // On lance d'abord la methode à chaque début de round (pour check, réinit les points d'attaques ect...)
                for (int i = 0; i < Characters.Count; i++)
                {
                    Characters[i].Item2.OnEachRound();
                }

                // Puis on lance les jet d'initiative pour chaque personnages
                for (int i = 0; i < Characters.Count; i++)
                {
                    int jetInitiative = Characters[i].Item2.Initiative + new Random().Next(1, 101);

                    // On va chercher, parmi la liste de persos, si le jetInitiative qu'on vient de lancer (pour perso actuel)
                    // est déjà égal à celui parmi la liste de persos
                    while (Characters.Any(x => x.Item1 == jetInitiative))
                    {
                        jetInitiative = Characters[i].Item2.Initiative + new Random().Next(1, 101); // On relance tant que c'est égal
                    }

                    Characters[i] = Tuple.Create(jetInitiative, Characters[i].Item2);   // On OVERRIDE les données de la liste du Tuple
                }

                // On trie dans l'ordre décroisant des jet initiatives
                Characters = Characters.OrderByDescending(x => x.Item1).ToList();

                for (int i = 0; i < Characters.Count; i++)
                {
                    Console.WriteLine("Jet d'initiative {0} : {1}", Characters[i].Item1, Characters[i].Item2.Name);
                }

                Console.WriteLine();
                Console.WriteLine("C'est {0} qui commence", Characters[0].Item2.Name);
                Console.WriteLine();

                // On lance le round avec une liste de personnage déjà trié (1er de la liste == 1er qui commence ect...)
                Round round = new Round(Characters, PlayerCharacter);
                round.PlayRound();

                round.AlertResumeRound();

                countRound++;
            }
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
        // Method : return true si on a un gagnant
        // =======================================================================
        public static bool HaveWinner(List<Tuple<int, Character>> Characters)
        {
            if (Characters.Count == 1)
            {
                return true;
            }
            return false;
        }


        // =======================================================================
        // Method : affiche un message du personnage qui a gagné
        // =======================================================================
        public static void AlertHaveWinner(List<Tuple<int, Character>> Characters)
        {
            if (Characters.Count == 1)
            {
                Console.WriteLine();
                Console.WriteLine("========================================================================");
                Console.WriteLine("=============oooooooooooooooooooooooooooooooooooooooooooooo=============");
                Console.WriteLine("========oooooooooooooooooooooooooooooooooooooooooooooooooooooooo========");
                Console.WriteLine("====oooooooooooooooooo                            oooooooooooooooooo====");
                Console.WriteLine("==oooooooooo                                                oooooooooo==");
                Console.WriteLine("==oooooooooo                                                oooooooooo==");
                Console.WriteLine("                        VAINQUEUR : {0} !!!                             ", Characters[0].Item2.Name);
                Console.WriteLine("==oooooooooo                                                oooooooooo==");
                Console.WriteLine("==oooooooooo                                                oooooooooo==");
                Console.WriteLine("====oooooooooooooooooo                            oooooooooooooooooo====");
                Console.WriteLine("========oooooooooooooooooooooooooooooooooooooooooooooooooooooooo========");
                Console.WriteLine("=============oooooooooooooooooooooooooooooooooooooooooooooo=============");
                Console.WriteLine("========================================================================");
                Console.WriteLine();
            }
        }






    }
}
