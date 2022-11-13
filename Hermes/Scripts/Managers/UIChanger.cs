using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIChanger : MonoBehaviour
{
    #region defaultChange
    public void OnClickRaidBTN()
    {
        SceneManager.LoadScene("S_Raid");
    }
    public void SelectRaidMap(string RaidMapName)
    {
        SceneManager.LoadScene(RaidMapName);
    }
    public void OnClickTitleBTN()
    {
        SceneManager.LoadScene("S_Title");
    }
    public void OnClickResultBTN()
    {
        SceneManager.LoadScene("S_Result");
    }
    public void OnClickInnBTN()
    {
        SceneManager.LoadScene("S_Inn");
    }
    public void OnClickGuildBTN()
    {
        SceneManager.LoadScene("S_Guild");
    }
    public void OnClickHeroDrawBTN()
    {
        SceneManager.LoadScene("S_HeroDraw");
    }
    public void OnClickStartRaidBTN()
    {
        SceneManager.LoadScene("S_ForestMap");
    }
    #endregion
}