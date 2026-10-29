using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        // 1. Zaten kule varsa çýk
        if (tower != null) return;

        // 2. Yöneticiler yoksa çýk (Hata almamak için)
        if (BuildManager.main == null || OyunMekanik.main == null) return;

        // 3. Fiyatý öðren
        int kuleFiyati = BuildManager.main.GetTowerCost();

        // 4. Parayý Harca ve Kuleyi Dik
        if (OyunMekanik.main.Para_harcaa(kuleFiyati))
        {
            // Kuleyi oluþtur
            GameObject kulePrefabi = BuildManager.main.GetSelectedTower();
            tower = Instantiate(kulePrefabi, transform.position, Quaternion.identity);

            // Rengi düzelt
            sr.color = startColor;

            Debug.Log("Kule dikildi! Harcanan: " + kuleFiyati);

            // --- ÝÞTE EKSÝK OLAN LOG KISMI BURASI ---
            if (Txt_yazýcý.ornk != null)
            {
                string kuleAdi = kulePrefabi.name;
                string konum = "(" + transform.position.x + ", " + transform.position.y + ")";
                string kalanPara = OyunMekanik.main.sikke.ToString();

                // Dosyaya yazdýr: "Kullanýcý (X,Y) konumuna OkçuKulesi inþa etti..."
                Txt_yazýcý.ornk.LogEkle("Kullanýcý, " + konum + " konumuna '" + kuleAdi + "' inþa etti. Kalan Para: " + kalanPara + ".");
            }
            // ----------------------------------------
        }
        else
        {
            Debug.Log("PARAN YETMÝYOR! Gereken: " + kuleFiyati);
        }
    }
}