using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kule : MonoBehaviour
{
    [Header("Kule Özellikleri")]
    [SerializeField] private float menzil = 5f;
    [SerializeField] private float SnMermi = 1f;

    [Tooltip("Eðer bu bir Topçu kulesiyse bunu iþaretle!")]
    [SerializeField] private bool SadeceYerdekileriVur = false;

    private float AtesSuresi = 0f;


    private float logZamanlayici = 0f;

    [Header("Genel özellikler")]
    [SerializeField] private Transform Kule_DonusAci;
    [SerializeField] private LayerMask Dusman;
    [SerializeField] private Transform Namlu_ucu;
    [SerializeField] private GameObject Mermi;

    private Transform Hedef;

    private void Update()
    {
        AtesSuresi += Time.deltaTime;

        if (Hedef == null)
        {
            DusmanTespit();
            return;
        }

        if (!DusmanMenzil_Demi())
        {
            Hedef = null;
            return;
        }

        DusmanaDon();

        if (AtesSuresi >= 1f / SnMermi)
        {
            Firee();
            AtesSuresi = 0f;
        }
    }

    private void Firee()
    {
        GameObject mermi_Objesi = Instantiate(Mermi, Namlu_ucu.position, Quaternion.identity);
        Mermi Mermi_kod = mermi_Objesi.GetComponent<Mermi>();

        if (Mermi_kod != null)
        {
            Mermi_kod.Hedeff_sec(Hedef);
        }
    }

    private void DusmanTespit()
    {
        RaycastHit2D[] Vur = Physics2D.CircleCastAll(Kule_DonusAci.position, menzil, (Vector2)Kule_DonusAci.position, 0f, Dusman);

        if (Vur.Length > 0)
        {
            Transform oncelikli_dusman = null;
            float enfazla_giden_dusman = -1f;

            foreach (RaycastHit2D hit in Vur)
            {
                Dusman d = hit.transform.GetComponent<Dusman>();

                if (d != null)
                {

                    if (SadeceYerdekileriVur == true && d.ucanMi == 1)
                    {

                        if (Time.time > logZamanlayici)
                        {
                            if (Txt_yazýcý.ornk != null)
                            {
                                string temizIsim = d.name.Replace("(Clone)", "");
                                Txt_yazýcý.ornk.LogEkle("Güneþ (TopcuKulesi)" + temizIsim + "' (Uçan) hedefini gördü ama ateþ etmedi.");
                            }
                            logZamanlayici = Time.time + 2f;
                        }

                        continue;
                    }

                    if (d.IlerlemeMiktari > enfazla_giden_dusman)
                    {
                        enfazla_giden_dusman = d.IlerlemeMiktari;
                        oncelikli_dusman = hit.transform;
                    }
                }
            }
            Hedef = oncelikli_dusman;
        }
    }

    private void DusmanaDon()
    {
        if (Hedef == null) return;
        float Aci = Mathf.Atan2(Hedef.position.y - Kule_DonusAci.position.y, Hedef.position.x -
            Kule_DonusAci.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion DusmanKonumu = Quaternion.Euler(new Vector3(0f, 0f, Aci));
        Kule_DonusAci.rotation = DusmanKonumu;
    }

    private bool DusmanMenzil_Demi()
    {
        if (Hedef == null) return false;
        return Vector2.Distance(Hedef.position, Kule_DonusAci.position) <= menzil;
    }
}