using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bocek : Dusman
{
    private void Start()
    {
        yerlestirFonk(50,0.012f,0,15,5,1);
    }

    protected override void Olum()
    {
        if (Txt_yazýcý.ornk != null)
        {
            Txt_yazýcý.ornk.LogEkle("BÖCEÐÝN SUYU ÇIKTI!");
        }

        base.Olum();
    }
}