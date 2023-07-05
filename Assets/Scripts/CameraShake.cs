using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform transform;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.3f;
    private float dampingSpeed = 2.0f;
    Vector3 initalPosition;

    public ParticleSystem stoneP;


    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent<Transform>();
        }
    }

    private void OnEnable()
    {
        initalPosition = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        stoneP.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initalPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initalPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 5.0f;
        stoneP.Play();
    }
}
