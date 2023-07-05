using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadMenu : MonoBehaviour
{
   void loadScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            SceneManager.LoadScene(1);
    }
}
