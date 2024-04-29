using UnityEngine;

public class AddPlayerConfigManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerConfigManagerPrefab;  // Hivatkozás a PlayerConfigurationManager prefabra

    public void AddPlayerConfigM()
    {
        // Ellenõrizzük, hogy a PlayerConfigurationManager.Instance már létezik-e
        if (PlayerConfigurationManager.Instance == null)
        {
            // Ha nincs, akkor létrehozzuk a prefabból
            GameObject newPlayerConfigManager = Instantiate(playerConfigManagerPrefab);

            // Gyõzõdj meg róla, hogy ez a gyökérobjektum, és nem egy alárendelt objektum
            newPlayerConfigManager.transform.SetParent(null);

            // DontDestroyOnLoad biztosítása a jelenetváltások túléléséhez
            DontDestroyOnLoad(newPlayerConfigManager);

            Debug.Log("PlayerConfigurationManager instantiated and added to the scene.");
        }
        else
        {
            Debug.Log("PlayerConfigurationManager already exists.");
        }
    }
}
