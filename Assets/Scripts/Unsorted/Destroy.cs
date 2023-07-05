using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour


{
    public ParticleSystem dust;
// Start is called before the first frame update
void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!dust.IsAlive())
        {
            Object.Destroy(this.gameObject);
        }
    }
}
