using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    public GameObject pauseGameList;
    public GameObject pauseScreen;

    private float timeProgress;
    public Text timerText;
    public void Update()
    {
        timeProgress += Time.deltaTime;
    }
    public void StartGame()
    {
        pauseGameList.SetActive(true);
        SetTime();
    }
    public void OnClickPauseGameBTN()
    {
        PauseGame();
        PauseUI(true);
    }
    public void OnClickContinueBTN()
    {
        ContinueGame();
        PauseUI(false);
    }

    private void SetTime()
    {
        timeProgress = 0.0f;
        timerText.text = "";
    }
    private string makeTimeHMS(float timer)
    {
        string text = "";
        int h, m, s;
        h = (int)timer / 3600;
        m = ((int)timer % 3600) / 60;
        s = (int)timer % 60;

        if (h != 0) text += h + "h  ";
        if (m != 0) text += m + "m  ";
        if (s != 0) text += s + "s";
        return text;
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
    }
    private void PauseUI(bool active)
    {
        if(active)
        {
            timerText.text = "게임 진행 시간\n" + makeTimeHMS(timeProgress);
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(false);
        }
    }
}
