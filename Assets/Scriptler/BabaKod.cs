using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Dusman : MonoBehaviour //abstract ile düþmaný soyutladýk çünkü düþma bir varlýk deil

{
    [Header("Düþman Özellikleri")]
    [SerializeField] protected int usseHasar;
    [SerializeField] protected float can;
    [SerializeField] protected float zirh;
    [SerializeField] protected float surratt;
    public int ucanMi;
    [SerializeField] protected int olumOdulu;

    protected string sonVuranKule = "Kimse";
    protected float baslangicCani;



    public float IlerlemeMiktari { get { return x; } }

    [Header("Görsel Referanslar")]
    [SerializeField] protected GameObject buzGorseliObjesi;
    [SerializeField] protected Image canBariGorseli;

    protected float x = 0, katsayi = 1;
    protected float a, b, c, d, e;

    private void Start()
    {
        string temizIsim = gameObject.name.Replace("(Clone)", "");
        if (Txt_yazýcý.ornk != null)
        {
            Txt_yazýcý.ornk.LogEkle("Düþman '" + temizIsim + "' (Can: " + can + ", Zýrh: " + zirh + ") haritaya girdi.");
        }
    }

    protected void yerlestirFonk(float f, float g, float h, int i, int j, int k)
    {
        can = f;
        baslangicCani = f;
        olumOdulu = i; usseHasar = j; ucanMi = k; surratt = g; zirh = h;
        Hesapla();
    }

    void Hesapla() { if (surratt == 0) surratt = 0.01f; a = 11 / surratt; b = 13.7f / surratt; c = 27 / surratt; d = 30 / surratt; e = 41 / surratt; }

    public virtual void HasarAl(float gelenHasar, string kuleTipi)
    {
        sonVuranKule = kuleTipi;
        float islenecekHasar = gelenHasar;

        string mesaj = "";
        if (kuleTipi == "Okcu") mesaj = "Piramit düþmana hançer fýrlatýldý";
        else if (kuleTipi == "Topcu") mesaj = "Güneþ alev topu yolladý";
        else if (kuleTipi == "Buz") mesaj = "Bulut yýldýrýmla çarparak düþmanlarý dondurdu";
        else mesaj = "Patlamadan dolayý hasar aldý.";

        string logDetay = mesaj + " (Taban Hasar: " + gelenHasar + ")";

        if (zirh > 0 && kuleTipi == "Okcu")
        {
            islenecekHasar = islenecekHasar * 0.5f;
            logDetay += " -> Zýrhlý olduðu için hasar yarýya indi -> " + islenecekHasar;
        }


        float zirhEtkisi = 1.0f - (zirh / (zirh + 100.0f));
        float netHasar = islenecekHasar * zirhEtkisi;

        logDetay += " -> Zýrh Korumasý Sonrasý Net Hasar: " + netHasar.ToString("F1");

        can -= netHasar;


        if (canBariGorseli != null) canBariGorseli.fillAmount = can / baslangicCani;

        if (Txt_yazýcý.ornk != null)
        {
            string temizIsim = gameObject.name.Replace("(Clone)", "");

            Txt_yazýcý.ornk.LogEkle("--- " + temizIsim + " VURULDU: " + logDetay + ". [Kalan Can: " + can.ToString("F1") + "]");
        }

        if (can <= 0)
        {
            Olum();
        }
    }

    protected virtual void Olum()
    {
        string temizIsim = gameObject.name.Replace("(Clone)", "");
        if (OyunMekanik.main != null) OyunMekanik.main.Para_eklee(olumOdulu);

        if (Txt_yazýcý.ornk != null)
        {
            Txt_yazýcý.ornk.LogEkle("'" + temizIsim + "' öldü. Ödül +" + olumOdulu + ". Toplam Para: " + OyunMekanik.main.sikke);
        }
        Destroy(gameObject);
    }

    public void BuzEtkisi()
    {
        StopCoroutine("BuzSayaci");
        StartCoroutine("BuzSayaci");
    }

    IEnumerator BuzSayaci()
    {
        katsayi = 0.5f;
        if (buzGorseliObjesi != null) buzGorseliObjesi.SetActive(true);

        if (Txt_yazýcý.ornk != null)
        {
            string temizIsim = gameObject.name.Replace("(Clone)", "");
            Txt_yazýcý.ornk.LogEkle("Buz kulesi '" + temizIsim + "' üzerine yavaþlatma uyguladý.");
        }

        yield return new WaitForSeconds(3f);
        katsayi = 1f;
        if (buzGorseliObjesi != null) buzGorseliObjesi.SetActive(false);
    }

    public virtual void Update()
    {
        float anlikHiz = surratt * katsayi;
        if (canBariGorseli != null) canBariGorseli.transform.parent.parent.rotation = Quaternion.identity;

        if (x < a) { transform.Translate(anlikHiz, 0, 0); x += katsayi; }
        else if (x < b) { transform.Rotate(0, 0, -10 * surratt * katsayi); x += katsayi; }
        else if (x < c) { transform.Translate(anlikHiz, 0, 0); x += katsayi; }
        else if (x < d) { transform.Rotate(0, 0, 10 * surratt * katsayi); x += katsayi; }
        else if (x < e) { transform.Translate(anlikHiz, 0, 0); x += katsayi; }
        else
        {

            if (OyunMekanik.main != null)
            {
                OyunMekanik.main.Cani_azalt(usseHasar);

                if (Txt_yazýcý.ornk != null)
                {
                    string temizIsim = gameObject.name.Replace("(Clone)", "");

 
                    if (sonVuranKule == "Buz")
                    {
                        Txt_yazýcý.ornk.LogEkle("Bulutun yavaþlatmasýna raðmen; tek baþýna yetersiz kaldý.");
                    }
                    else if (sonVuranKule == "Okcu")
                    {
                        Txt_yazýcý.ornk.LogEkle("Diðer kuleler menzil dýþýydý; Piramitin hançerleri durdurmaya yetmedi.");
                    }
                    else if (sonVuranKule == "Topcu")
                    {
                        Txt_yazýcý.ornk.LogEkle("Güneþ haþmetiyle kavursada;yavaþlýðýndan dolayý düþmana kül edemedi.");
                    }
                    else
                    {
                        Txt_yazýcý.ornk.LogEkle("Savunma hattý aþýldý; (" + sonVuranKule + ") vurmadýðý için....");
                    }

                    Txt_yazýcý.ornk.LogEkle("'" + temizIsim + "' üsse ulaþtý. Oyuncu Caný: " + OyunMekanik.main.Ra_Kani + " (-" + usseHasar + ").");
                }
            }
            Destroy(gameObject);
        }
    }
}