using UnityEngine;


public class Araba : Dusman
{
    private void Start()
    {

        yerlestirFonk(50, 0.008f, 0, 10, 5, 0);
    }


    protected override void Olum()//poliformizim kýsmý bura override yapýlan yer

    {
        if (Txt_yazýcý.ornk != null)
        {
            Txt_yazýcý.ornk.LogEkle("BÝR ARABA HURDAYA DÖNDÜ!");
        }

        base.Olum();
    }
}