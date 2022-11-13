using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    public GameObject[] players;
    public bool isGameStart;
    public bool isSomeone;
    public GameObject gameOver;
    // Update is called once per frame
    private void Start()
    {
        isGameStart = false;
    }
    void Update()
    {
        if (isGameStart == true)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            isSomeone = false;
            foreach (GameObject obj in players)
            {
                if (obj)
                {
                    isSomeone = true;
                }
            }
            if (isSomeone == false)
            {
                StartCoroutine("GameOver");
            }
        }
    }
    public void IsStart(bool start)
    {
        isGameStart = start;
    }
    IEnumerator GameOver()
    {
        isGameStart = false;
        gameOver.SetActive(true);
        // 1초 뒤에 시작화면으로 이동
        yield return new WaitForSeconds(1.0f);
        // 이부분에서 시작 씬으로 호출
        //부탁해요 박문수
    }
}
