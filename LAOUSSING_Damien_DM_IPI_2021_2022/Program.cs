using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<int, Type>> listTypes = new List<Tuple<int, Type>>();
            List<Tuple<int, Character>> characters = new List<Tuple<int, Character>>(); // Tuple contenant le jetInitiative (de chaque round) associé au personnage


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
            characters.Add(new Tuple<int, Character>(0, new Warrior("Yasuo")));
            characters.Add(new Tuple<int, Character>(0, new Warrior("Riven")));
            characters.Add(new Tuple<int, Character>(0, new Guardian("Kayle")));
            characters.Add(new Tuple<int, Character>(0, new Berserk("Olaf")));
            characters.Add(new Tuple<int, Character>(0, new Zombie("Zomboy")));
            characters.Add(new Tuple<int, Character>(0, new Robot("Blitzcrank")));
            characters.Add(new Tuple<int, Character>(0, new Lich("Karthus")));
            characters.Add(new Tuple<int, Character>(0, new Ghoul("Yorick")));
            characters.Add(new Tuple<int, Character>(0, new Vampire("Vladimir")));
            characters.Add(new Tuple<int, Character>(0, new Priest("Priester")));
            characters.Add(new Tuple<int, Character>(0, new Kamikaze("Ziggs")));

            //characters.Add(new Tuple<int, Character>(0, new Berserk("Tryndamere")));
            //characters.Add(new Tuple<int, Character>(0, new Berserk("Arnwulf")));
            //characters.Add(new Tuple<int, Character>(0, new Vampire("Edward")));
            //characters.Add(new Tuple<int, Character>(0, new Warrior("Pantheon")));
            //characters.Add(new Tuple<int, Character>(0, new Ghoul("Gunlawd")));
            //characters.Add(new Tuple<int, Character>(0, new Robot("Roobocop")));
            //characters.Add(new Tuple<int, Character>(0, new Robot("Terminator")));
            //characters.Add(new Tuple<int, Character>(0, new Ghoul("Khox")));
            //characters.Add(new Tuple<int, Character>(0, new Kamikaze("BOUM")));
            //characters.Add(new Tuple<int, Character>(0, new Kamikaze("Sacrifice")));
            //characters.Add(new Tuple<int, Character>(0, new Guardian("Nasus")));
            //characters.Add(new Tuple<int, Character>(0, new Guardian("Zerator")));
            //characters.Add(new Tuple<int, Character>(0, new Guardian("Tarmac")));
            //characters.Add(new Tuple<int, Character>(0, new Zombie("Pzoml")));
            //characters.Add(new Tuple<int, Character>(0, new Zombie("Voilme")));



            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("*************************************************************");
            Console.WriteLine("*************                                    ************");
            Console.WriteLine("*************        BIENVENUE SUR LE JEU        ************");
            Console.WriteLine("*************                                    ************");
            Console.WriteLine("*************************************************************");
            Console.WriteLine();
            Console.WriteLine();


            // ********************************** Init choix du Joueur **********************************
            string playerCharacterName = PlayerActions.ChooseCharacterName(characters);
            Console.WriteLine();

            Type playerCharacterType = PlayerActions.ChooseCharacterType(listTypes);

            // Création de l'instance Character selon le type choisi par le joueur
            Character playerCharacter = (Character)Activator.CreateInstance(playerCharacterType, playerCharacterName);
            characters.Add(new Tuple<int, Character>(0, playerCharacter));

            Console.WriteLine();

            // ********************************** FIN : Init choix du Joueur **********************************


            Battle battle = new Battle(characters, playerCharacter);
            battle.StartBattle();
        }





    }
}
