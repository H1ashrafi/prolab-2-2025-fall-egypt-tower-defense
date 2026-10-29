using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Düþman Prefablarý")]
    public GameObject at_arabasi;   // zýrhsýz
    public GameObject zirhli_glad;  // Zýrhlý
    public GameObject ucan_bocek; //hava

    public Transform spawnPoint;

    public static Spawner ornk;

    public int Suanki_Dalga = 0;

    public bool dalgalarBitti = false;

    private void Awake()
    {
        ornk = this;
    }

    void Start()
    {
        StartCoroutine(DalgaBaslat());
    }

    IEnumerator DalgaBaslat()
    {

        Suanki_Dalga = 0;


        if (Txt_yazýcý.ornk != null)
        {
            Txt_yazýcý.ornk.LogEkle("\nDalga 1 Baþladý. (Tip: Atlý=2/Gladyator=1/Bocek=2, Sayý: 5)");
        }

        Debug.Log("Dalga 1 baslamasina son 7 saniye...");
        yield return new WaitForSeconds(7f);

        Debug.Log("Dalga 1 Baþlýyor...");

       
        if (Txt_yazýcý.ornk != null)
        {
            Txt_yazýcý.ornk.LogEkle("Dalga 1'in ilk birliði: 'AtArabasý' haritaya giriyor.");
        }

        Instantiate(at_arabasi, spawnPoint.position, Quaternion.identity);
        Txt_yazýcý.ornk.LogEkle("AtArabasý haritaya girdi(50 Can).");
        yield return new WaitForSeconds(1.2f);

        


        Instantiate(at_arabasi, spawnPoint.position, Quaternion.identity);
        Txt_yazýcý.ornk.LogEkle("AtArabasý haritaya girdi(50 Can).");
        yield return new WaitForSeconds(6f);

        


        Instantiate(zirhli_glad, spawnPoint.position, Quaternion.identity);
        Txt_yazýcý.ornk.LogEkle("Gladyatör haritaya girdi(75 Can).");
        yield return new WaitForSeconds(12f);

        



        Instantiate(ucan_bocek, spawnPoint.position, Quaternion.identity);
          Txt_yazýcý.ornk.LogEkle("Uçan böcek haritaya girdi(50 Can).");
        yield return new WaitForSeconds(8f);



        Instantiate(ucan_bocek, spawnPoint.position, Quaternion.identity);
         Txt_yazýcý.ornk.LogEkle("Uçan böcek haritaya girdi(50 Can)."); 
        yield return new WaitForSeconds(8f);



        Debug.Log("Dalga 1 bitti. 5 saniye dinlenme...");
        yield return new WaitForSeconds(5f);

        Suanki_Dalga = 1;
       
        Txt_yazýcý.ornk.LogEkle("\nDalga 2 Baþladý. (Tip: Atlý=2/Gladyator=3/Bocek=1, Sayý: 6)");


        Debug.Log("Dalga 2 Baþladý.");

     
        Txt_yazýcý.ornk.LogEkle("Dalga 2'nin ilk birliði: 'Gladyatör' haritaya giriyor.");

        Instantiate(zirhli_glad, spawnPoint.position, Quaternion.identity);
        Txt_yazýcý.ornk.LogEkle("Gladyatör haritaya girdi(75 Can).");
        yield return new WaitForSeconds(10f);

        Instantiate(at_arabasi, spawnPoint.position, Quaternion.identity);
        Txt_yazýcý.ornk.LogEkle("AtArabasý haritaya girdi(50 Can).");
        yield return new WaitForSeconds(6f);


        Instantiate(ucan_bocek, spawnPoint.position, Quaternion.identity);
       Txt_yazýcý.ornk.LogEkle("Uçan böcek haritaya girdi(50 Can).");
        yield return new WaitForSeconds(8f);


        Instantiate(at_arabasi, spawnPoint.position, Quaternion.identity);
         Txt_yazýcý.ornk.LogEkle("AtArabasý haritaya girdi(50 Can).");
        yield return new WaitForSeconds(5f);


        Instantiate(zirhli_glad, spawnPoint.position, Quaternion.identity);
        Txt_yazýcý.ornk.LogEkle("Gladyatör haritaya girdi(75 Can).");
        yield return new WaitForSeconds(8f);


        Instantiate(zirhli_glad, spawnPoint.position, Quaternion.identity);
        Txt_yazýcý.ornk.LogEkle("Gladyatör haritaya girdi(75 Can).");
        yield return new WaitForSeconds(10f);

        Instantiate(ucan_bocek, spawnPoint.position, Quaternion.identity);
        Txt_yazýcý.ornk.LogEkle("Uçan böcek haritaya girdi(50 Can).");


        Debug.Log("Sonunda buda sonuncu düþmandý...");

        dalgalarBitti = true;
    }
}