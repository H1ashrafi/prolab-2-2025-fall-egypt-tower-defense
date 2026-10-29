using UnityEngine;
using System.IO;
using System;

public class Txt_yazýcý : MonoBehaviour
{
    public static Txt_yazýcý ornk;
    private string dosyaYolu;

    private void Awake()
    {
        if (ornk == null) ornk = this;
        else Destroy(gameObject);

        dosyaYolu = Application.dataPath + "/savunma_gunlugu.txt";

        File.WriteAllText(dosyaYolu, "SÝMÜLASYON GÜNLÜÐÜ\n------------------\n");
        File.AppendAllText(dosyaYolu, "Simülasyon Baþladý. Senaryo: Roma Birliklerini Yen" + "\n" +
        "Ra'nýn Kaný:100 Sikke:200" + "\n" + "Tarih:" + DateTime.Now + "\n\n");
    }

    public void LogEkle(string mesaj)
    {

        File.AppendAllText(dosyaYolu, mesaj + "\n");
    }
}