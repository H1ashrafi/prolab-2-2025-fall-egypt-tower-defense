using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("Kule Ayarlarý")]
    [SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private int[] towerCosts;

    private int selectedTower = 0;

    private void Awake()
    {

        if (main != null)
        {
            Debug.LogError("DÝKKAT! SAHNEDE ÝKÝNCÝ BÝR BUILDMANAGER VAR! SUÇLU BU OBJE: " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        main = this;
        Debug.Log("Ana BuildManager Atandý: " + gameObject.name);
    }

    public GameObject GetSelectedTower()
    {
        if (towerPrefabs == null || selectedTower >= towerPrefabs.Length) return null;
        return towerPrefabs[selectedTower];
    }

    public int GetTowerCost()
    {
        if (towerCosts == null || selectedTower >= towerCosts.Length) return 0;
        return towerCosts[selectedTower];
    }


    public void SetSelectedTower(int C)
    {
        selectedTower = C;
        Debug.Log("Kule Seçildi: " + selectedTower);
    }
}