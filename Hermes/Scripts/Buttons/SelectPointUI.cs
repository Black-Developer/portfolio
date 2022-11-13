using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPointUI : MonoBehaviour
{
    public GameObject panel;

    public void Awake()
    {
        panel = this.gameObject;
    }

    public void Start()
    {
        panel.SetActive(true);
    }

    public void DisableText()
    {
        panel.SetActive(false);
    }
}
