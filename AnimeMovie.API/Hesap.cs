using System;
namespace AnimeMovie.API
{
    public class Hesap
    {
        private decimal bakiye
        {
            get
            {
                return bakiye;
            }
            set { bakiye = value; }
        }
        public Hesap(decimal bakiye) => this.bakiye = bakiye;
        public Hesap() { }
        public bool paraYatir(decimal yatirilacakBakiye)
        {
            if (yatirilacakBakiye >= 1)
            {
                bakiye = bakiye + yatirilacakBakiye;
                return true;
            }
            return false;
        }
        public virtual bool paraCek(decimal cekilecekBakiye)
        {
            if (cekilecekBakiye - bakiye >= 0)
            {
                bakiye = cekilecekBakiye - bakiye;
                return true;
            }
            return false;
        }
        public decimal getBakiye()
        {
            return bakiye;
        }
    }
    public class MevduatHesabi : Hesap
    {
        private int karorani
        {
            get
            {
                if( karorani<0)
                {
                    karorani = 1;
                }
                else if(karorani > 1)
                {
                    karorani= 2;
                }
                return karorani;
            }
            set
            {
                karorani = value;
            }
        }
        public decimal mevduatMiktari { get; set; }

        public MevduatHesabi(int karOrani, decimal mevduatMiktari)
        {
            this.mevduatMiktari = mevduatMiktari;
            karorani = karOrani;
        }
        public decimal faizHesapla()
        {
            return karorani * mevduatMiktari;
        }

    }
    public class CekHesabi:Hesap {

        public override bool paraCek(decimal cekilecekBakiye)
        {
            return false;
        }
        
    }
}
