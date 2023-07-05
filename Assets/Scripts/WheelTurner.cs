using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTurner : MonoBehaviour
{
    public Transform parent;
    private Transform oldParent;
    public Transform wheel;
    private Transform oldwheel;
    private float p = 100;

    // Start is called before the first frame update
    void Start()
    {
        oldParent = parent;
        oldwheel = wheel;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, p*-parent.position.x);
        wheel.rotation = rotation;
    }
}
