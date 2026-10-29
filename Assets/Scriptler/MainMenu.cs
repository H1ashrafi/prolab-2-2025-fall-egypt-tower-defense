using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void basla_oyun()
    {

        SceneManager.LoadScene("TowerDefens");
    }

    public void cik_oyundan()
    {
        Debug.Log("Oyundan Çýkýlýyor...");


#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Eðer gerçek oyunsa (.exe ise) uygulamayý kapat
            Application.Quit();
#endif
    }
}