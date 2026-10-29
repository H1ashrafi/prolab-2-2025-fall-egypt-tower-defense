using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OyunMekanik : MonoBehaviour
{
    public static OyunMekanik main;

    [Header("Oyun Durumu")]
    public int sikke = 200;
    public int Ra_Kani = 100;

    [Header("UI Baðlantýlarý")]
    [SerializeField] private GameObject sonucPaneli;
    [SerializeField] private TextMeshProUGUI sonucYazisi;

    private bool oyunBittiMi = false;

    private void Awake()
    {
        main = this;
    }

    private void Update()
    {
        if (oyunBittiMi) return;

        if (Spawner.ornk != null && Spawner.ornk.dalgalarBitti)
        {
            Dusman[] kalanDusmanlar = FindObjectsOfType<Dusman>();

            if (kalanDusmanlar.Length == 0)
            {
                OyunBitti(true);
            }
        }
    }

    public void Para_eklee(int miktar)
    {
        sikke += miktar;
    }

    public bool Para_harcaa(int miktar)
    {
        if (sikke >= miktar)
        {
            sikke -= miktar;
            return true;
        }
        return false;
    }

    public void Cani_azalt(int hasar)
    {
        Ra_Kani -= hasar;
        if (Ra_Kani <= 0)
        {
            Ra_Kani = 0;
            OyunBitti(false);
        }
    }

    void OyunBitti(bool kazandi)
    {
        oyunBittiMi = true;

        if (sonucPaneli != null) sonucPaneli.SetActive(true);

        if (kazandi)
        {

            if (sonucYazisi != null)
            {
                sonucYazisi.text = "KAZANDINIZ!";
                sonucYazisi.color = Color.green;
            }

            if (Txt_yazýcý.ornk != null)
            {
                Txt_yazýcý.ornk.LogEkle("\nSON: Tüm dalgalar temizlendi. OYUN KAZANILDI! (Kalan Can: " + Ra_Kani + ", Toplam Sikke: " + sikke + ")");
            }
        }
        else
        {

            if (sonucYazisi != null)
            {
                sonucYazisi.text = "KAYBETTÝNÝZ!";
                sonucYazisi.color = Color.red;
            }

            if (Txt_yazýcý.ornk != null)
            {
                Txt_yazýcý.ornk.LogEkle("\nSON: Rha'nýn Kanýný tükettin. OYUN KAYBEDÝLDÝ! (Kalan Can: 0, Toplam Sikke: " + sikke + ")");
            }
        }

        Time.timeScale = 0f;
    }

    public void TekrarOyna()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuyeDon()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("AnaMenu");
    }
}