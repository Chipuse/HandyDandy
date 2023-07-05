using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class switchOne : MonoBehaviour
{
    public Camera active;
    public Camera nActive;
    public Animator nextAnimator;
    public AudioSource cartS;

    private void Start()
    {
        active.gameObject.SetActive(false);
        
    }

    void switchCamera()
    {
        //Debug.Log("switch");
        active.gameObject.SetActive(false);
        nActive.gameObject.SetActive(true);
        nextAnimator.SetTrigger("Start");
    }

    void cartBump()
    {
        cartS.Play(0);
        //Debug.LogError("started the sound");
    }

    void switchScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Management.Instance.CompleteLevel();
    }

}
