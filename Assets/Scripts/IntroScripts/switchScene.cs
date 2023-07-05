using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour
{

    public ParticleSystem crashC;
    public ParticleSystem crashM;
    public AudioSource crashA;
    public AudioSource crashS;
    public GameObject border;
    public SpriteRenderer brokenCrystal;

    public Camera active;
    public Camera nActive;
    public Animator nextAnimator;
    private void Start()
    {
         crashC.Stop();
         crashM.Stop();
    }

    void crash()
    {
        crashC.Play();
        crashM.Play();
        crashA.Play();
        border.SetActive(true);
        brokenCrystal.enabled = false;
    }

    void crashSound()
    {
        crashS.Play();
        brokenCrystal.enabled = true;
    }

    void switchMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    void switchCamera()
    {
        Debug.Log("switch");
        active.gameObject.SetActive(false);
        nActive.gameObject.SetActive(true);
        nextAnimator.SetTrigger("Start");
        
    }

}
