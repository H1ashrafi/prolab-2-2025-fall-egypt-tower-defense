using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zirhli : Dusman
{
    private void Start()
    {
        yerlestirFonk(75, 0.004f, 50, 20, 10, 0);
    }

    protected override void Olum()
    {
        if (Txt_yazýcý.ornk != null)
        {
            Txt_yazýcý.ornk.LogEkle("O ZIRHIN ÝÇÝNDE KANINDA BOÐULDU!");
        }

        base.Olum();
    }
}