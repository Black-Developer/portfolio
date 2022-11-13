using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrail : MonoBehaviour
{
    public TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    public void TrailOn()
    {
        trail.enabled = true;
    }
    public void TrailOff()
    {
        trail.enabled = false;
    }
}
