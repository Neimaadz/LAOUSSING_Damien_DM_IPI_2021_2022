using System;
namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public interface IHolyDamage
    {
        public int DealHolyDamage(int damageDeal)
        {
            return damageDeal * 2;
        }
    }
}
