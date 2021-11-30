﻿using System;
namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public interface IPain
    {
        public Type CharacterType { get; set; }
        public string Name { get; set; }
        public int MaximumLife { get; set; }
        public int CurrentLife { get; set; }
        public int CurrentAttackNumber { get; set; }
        public int TotalAttackNumber { get; set; }
        public int CountAttackOff { get; set; }


        // Methode appeler à chaque fois qu'on reçoit des dégats
        public void Pain()
        {
            int damageTaken = MaximumLife - CurrentLife;

            // Si impossibilité d'attaquer N'EST PAS en cours
            if (CountAttackOff == 0)
            {
                if (damageTaken > CurrentLife)
                {
                    int percentChance = ((damageTaken - CurrentLife) * 2 / (CurrentLife + damageTaken)) * 100;
                    int randomValue = new Random().Next(100);

                    if (randomValue < percentChance)
                    {
                        // Cas Guerrier : perd la possibilité d’attaquer durant le tour actuel. Il ne peut pas être affecté par la douleur plus longtemps
                        if ( CharacterType == typeof(Warrior) )
                        {
                            CountAttackOff = 1;
                            Console.WriteLine("{0} est affaiblit durant ce round en cours !", Name);
                        }
                        // Cas général
                        else
                        {
                            int roundAttackOff = new Random().Next(0, 3);

                            switch (roundAttackOff)
                            {
                                case 0: // ne peut pas attaquer durant le round en cours
                                    CountAttackOff = 1;
                                    Console.WriteLine("{0} est affaiblit durant ce round en cours !", Name);

                                    break;
                                case 1: // ne peut pas attaquer durant ce round et le suivant
                                    CountAttackOff = 2;
                                    Console.WriteLine("{0} est affaiblit durant ce round et le suivant !", Name);

                                    break;
                                case 2: // ne peut pas attaquer durant ce round et les 2 suivant
                                    CountAttackOff = 3;
                                    Console.WriteLine("{0} est affaiblit durant ce round et les 2 suivant !", Name);

                                    break;
                            }
                        }
                        CurrentAttackNumber = 0;

                    }
                }
            }

        }

        // Methode appeler à chaque début de round
        public bool IsSensitiveToPain()
        {
            if (CountAttackOff > 0)
            {
                CountAttackOff--;   // On retiré -1 pour le round actuel

                // Si, après avoir retiré 1, on ne peut toujours pas attaquer
                if (CountAttackOff > 0)
                {
                    CurrentAttackNumber = 0;
                    Console.WriteLine("{0} : {1} round avant de pouvoir attaquer", Name, CountAttackOff);
                    Console.WriteLine();

                    return true;
                }
                // Sinon on remet la totalité des points d'attaques
                else if (CountAttackOff == 0)
                {
                    CurrentAttackNumber = TotalAttackNumber;

                    return false;
                }
            }
            return false;
        }

    }
}
