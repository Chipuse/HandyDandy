using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    public Camera startCamera;
    public Camera Camera1;
    public Animator nextAnimator;

    private void Start()
    {
        startCamera.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        startCamera.gameObject.SetActive(false);
        Camera1.gameObject.SetActive(true);
        nextAnimator.SetTrigger("Start");
        //Debug.Log("hit");
    }
}
