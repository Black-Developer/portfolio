using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    public void OnClick_HideOut_Button()
    {
        SceneManager.LoadScene("S_Hideout");
    }
}
