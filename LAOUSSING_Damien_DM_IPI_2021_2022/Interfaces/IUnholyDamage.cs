using System;
namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public interface IUnholyDamage
    {
        public int DealUnholyDamage(int damageDeal)
        {
            return damageDeal * 2;
        }
    }
}
