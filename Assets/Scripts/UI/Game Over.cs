using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{

        public GameObject GameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("CheckPlayerExistence", 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CheckPlayerExistence()
    {
        GameObject playerObject = GameObject.FindWithTag("PlayerObject");
        if (playerObject == null)
        {
            GameOverUI.SetActive(true);
        }
    }
}
