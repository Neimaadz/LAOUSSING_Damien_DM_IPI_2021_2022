using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<int, Type>> listTypes = new List<Tuple<int, Type>>();
            //List<Tuple<int, Character>> characters = new List<Tuple<int, Character>>(); // Tuple contenant le jetInitiative (de chaque round) associé au personnage


            // ********************************** Liste des types de personnage disponible **********************************
            listTypes.Add(new Tuple<int, Type>(1, typeof(Warrior)));
            listTypes.Add(new Tuple<int, Type>(2, typeof(Guardian)));
            listTypes.Add(new Tuple<int, Type>(3, typeof(Berserk)));
            listTypes.Add(new Tuple<int, Type>(4, typeof(Zombie)));
            listTypes.Add(new Tuple<int, Type>(5, typeof(Robot)));
            listTypes.Add(new Tuple<int, Type>(6, typeof(Lich)));
            listTypes.Add(new Tuple<int, Type>(7, typeof(Ghoul)));
            listTypes.Add(new Tuple<int, Type>(8, typeof(Vampire)));
            listTypes.Add(new Tuple<int, Type>(9, typeof(Priest)));
            listTypes.Add(new Tuple<int, Type>(10, typeof(Kamikaze)));


            // ********************************** Ajout des personnages dans la liste des participants **********************************
            //characters.Add(new Tuple<int, Character>(0, new Warrior("Yasuo")));
            //characters.Add(new Tuple<int, Character>(0, new Warrior("Riven")));
            //characters.Add(new Tuple<int, Character>(0, new Guardian("Kayle")));
            //characters.Add(new Tuple<int, Character>(0, new Berserk("Olaf")));
            //characters.Add(new Tuple<int, Character>(0, new Zombie("Zomboy")));
            //characters.Add(new Tuple<int, Character>(0, new Robot("Blitzcrank")));
            //characters.Add(new Tuple<int, Character>(0, new Lich("Karthus")));
            //characters.Add(new Tuple<int, Character>(0, new Ghoul("Yorick")));
            //characters.Add(new Tuple<int, Character>(0, new Vampire("Vladimir")));
            //characters.Add(new Tuple<int, Character>(0, new Priest("Priester")));
            //characters.Add(new Tuple<int, Character>(0, new Kamikaze("Ziggs")));




            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                       *****************************************************************                               ");
            Console.WriteLine("                  ***************************************************************************                          ");
            Console.WriteLine("             *************************************************************************************                     ");
            Console.WriteLine("        ******************                                                           ******************                ");
            Console.WriteLine("   ***********************                                                           ***********************           ");
            Console.WriteLine("**************************                  BIENVENUE SUR LE JEU                     **************************        ");
            Console.WriteLine("   ***********************                                                           ***********************           ");
            Console.WriteLine("        ******************                                                           ******************                ");
            Console.WriteLine("             *************************************************************************************                     ");
            Console.WriteLine("                  ***************************************************************************                          ");
            Console.WriteLine("                       *****************************************************************                               ");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("***************************************************");
            Console.WriteLine("*****               MENU DU JEU               *****");
            Console.WriteLine("***************************************************");
            Console.WriteLine("*****                                         *****");
            Console.WriteLine("*****                                         *****");
            Console.WriteLine("*****    1 : Player vs AI     2 : Demo AI     *****");
            Console.WriteLine("*****                                         *****");
            Console.WriteLine("*****                                         *****");
            Console.WriteLine("***************************************************");
            Console.WriteLine();

            int selectgame = SelectGame();
            int numberBot = NumberBot();
            List<Tuple<int, Character>> characters = RandomListBotCharacters(listTypes, numberBot);   // Création de la liste characters BOTS

            Console.WriteLine();
            Console.WriteLine();


            if (selectgame == 1)    // Player vs AI
            {
                // ********************************** Init choix du Joueur **********************************
                string playerCharacterName = PlayerActions.ChooseCharacterName(characters);
                Console.WriteLine();

                Type playerCharacterType = PlayerActions.ChooseCharacterType(listTypes);

                // Création de l'instance Character selon le type choisi par le joueur
                Character playerCharacter = (Character)Activator.CreateInstance(playerCharacterType, playerCharacterName);
                characters.Add(new Tuple<int, Character>(0, playerCharacter));

                Console.WriteLine();

                // ********************************** FIN : Init choix du Joueur **********************************


                Battle battle = new Battle(characters);
                battle.PlayerCharacter = playerCharacter;
                battle.StartBattle();
            }
            else    // Demo AI
            {
                Battle battle = new Battle(characters);
                battle.StartBattle();
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
        // Method : select game
        // =======================================================================
        public static int SelectGame()
        {
            ConsoleKeyInfo playerAnswer;
            int number;

            do
            {
                Console.WriteLine();
                Console.Write("Please select a game : ");
                playerAnswer = Console.ReadKey();
            }
            while (playerAnswer.KeyChar.ToString() != "1" && playerAnswer.KeyChar.ToString() != "2");

            number = int.Parse(playerAnswer.KeyChar.ToString());

            if (number == 1)
            {
                return 1;
            }
            else if (number == 2)
            {
                return 2;
            }

            return 0;
        }


        // =======================================================================
        // Method : return number of Bot
        // =======================================================================
        public static int NumberBot()
        {
            bool isNumber = true;
            int number = 0;

            do
            {
                Console.WriteLine();
                Console.Write("Number of bot (max 20) : ");
                isNumber = int.TryParse(Console.ReadLine(), out number);

                if (isNumber == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : veuillez entrer un nombre !");
                }
                else if (number < 2 || number > 20)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erreur : veuillez entrer un nombre compris entre 2 et 20");
                }
            }
            while (isNumber == false || number < 2 || number > 20);


            return number;
        }


        // =======================================================================
        // Method : return a list of random Bot
        // =======================================================================
        public static List<Tuple<int, Character>> RandomListBotCharacters(List<Tuple<int, Type>> listTypes, int numberBot)
        {
            // Tuple contenant le jetInitiative (de chaque round) associé au personnage
            List<Tuple<int, Character>> characters = new List<Tuple<int, Character>>();

            for (int i=0; i<numberBot; i++)
            {
                int randNumb = new Random().Next(0, listTypes.Count);

                Type botCharacterType = listTypes[randNumb].Item2;
                string botCharacterName = botCharacterType.Name + "_" + (i+1);

                // Création de l'instance Character selon le type choisi aléatoirement
                Character botCharacter = (Character)Activator.CreateInstance(botCharacterType, botCharacterName);

                characters.Add(new Tuple<int, Character>(0, botCharacter));
            }
            return characters;

        }





        }
}
