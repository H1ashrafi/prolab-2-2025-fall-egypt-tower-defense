using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MENU : MonoBehaviour
{
    [Header("UI Metinleri")]
    [SerializeField] private TextMeshProUGUI sikkeText;
    [SerializeField] private TextMeshProUGUI canText;

    [SerializeField] private TextMeshProUGUI dalgaText;

    [Header("Animasyon Ayarlarý")]
    [SerializeField] private Animator anim;

    private bool isMenuOpen = true;
    void Update()
    {

        if (OyunMekanik.main != null)
        {
            if (canText != null)
                canText.text = OyunMekanik.main.Ra_Kani.ToString();
            
            
            if (sikkeText != null)
                sikkeText.text = OyunMekanik.main.sikke.ToString();


        }


        if (Spawner.ornk != null && dalgaText != null)
        {

            int guncelDalga = Spawner.ornk.Suanki_Dalga + 1;
            dalgaText.text = "DALGA: " + guncelDalga.ToString();
        }
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (anim != null)
        {

            anim.SetBool("menuacil", isMenuOpen);
        }
    }

    public void SetSelected() { }
}