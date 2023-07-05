
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour {
    public int level = 2;

	public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }

    public void SetLevel(int setTo)
    {
        level = setTo + 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
