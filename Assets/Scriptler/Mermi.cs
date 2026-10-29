using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    private Transform Hedef;
    private Rigidbody2D rb;

    [Header("Mermi Ayarlarý")]
    [SerializeField] private float Mermi_hizi = 15f;
    [SerializeField] private float Mermi_hasari = 20f;
    [SerializeField] private string KuleTipi = "Okcu";

    [Header("Topçu Ayarlarý")]
    [Tooltip("Hasar verilecek gerçek alanýn yarýçapý")]
    [SerializeField] private float Patlama_Yaricapi = 3f;
    [SerializeField] private GameObject PatlamaEfektiPrefab;

    [Header("Efekt Ýnce Ayarý")]
    [SerializeField] private float Efekt_Boyut_Carpani = 1f;

    [Header("Görsel Ayar")]
    [SerializeField] private float Rotasyon_Ofseti = 0f;

    private void Awake() { rb = GetComponent<Rigidbody2D>(); }
    public void Hedeff_sec(Transform A) { Hedef = A; }

    private void FixedUpdate()
    {
        if (!Hedef) { Destroy(gameObject); return; }

        Vector2 Atis_yonu = (Hedef.position - transform.position).normalized;
        rb.linearVelocity = Atis_yonu * Mermi_hizi;

        float aci = Mathf.Atan2(Atis_yonu.y, Atis_yonu.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, aci + Rotasyon_Ofseti);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (KuleTipi == "Topcu")
        {

            if (PatlamaEfektiPrefab != null)
            {
                Vector3 patlamaYeri = transform.position;
                patlamaYeri.z = -5f;
                GameObject efekt = Instantiate(PatlamaEfektiPrefab, patlamaYeri, Quaternion.identity);

                float nihaiBoyut = (Patlama_Yaricapi * 2f) * Efekt_Boyut_Carpani;
                efekt.transform.localScale = new Vector3(nihaiBoyut, nihaiBoyut, 1f);

                Destroy(efekt, 0.5f);
            }


            GameObject anaHedefObjesi = collision.gameObject;

            AlanHasariVer(anaHedefObjesi);
        }
        else
        {
            Dusman d = collision.gameObject.GetComponentInParent<Dusman>();
            if (d != null)
            {
                d.HasarAl(Mermi_hasari, KuleTipi);
                if (KuleTipi == "Buz") d.BuzEtkisi();
            }
        }

        Destroy(gameObject);
    }


    void AlanHasariVer(GameObject direkCarpilan)
    {
        Collider2D[] vurulanlar = Physics2D.OverlapCircleAll(transform.position, Patlama_Yaricapi);


        if (Txt_yazýcý.ornk != null)
        {
            Txt_yazýcý.ornk.LogEkle("Güneþ (TopcuKulesi) ateþ etti. Alan vuruþu: Merkez (" + transform.position.x.ToString("F1") + ", " + transform.position.y.ToString("F1") + ")");
        }

        Dusman anaDusmanScripti = direkCarpilan.GetComponentInParent<Dusman>();

        foreach (Collider2D obje in vurulanlar)
        {
            Dusman d = obje.GetComponentInParent<Dusman>();

            if (d != null)
            {

                if (d.ucanMi == 0)
                {
                    string gonderilenTip = KuleTipi;

                    if (d != anaDusmanScripti)
                    {

                        gonderilenTip = "Topcu (Alan Patlamasý)";
                    }


                    d.HasarAl(Mermi_hasari, gonderilenTip);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (KuleTipi == "Topcu")
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Patlama_Yaricapi);
        }
    }
}