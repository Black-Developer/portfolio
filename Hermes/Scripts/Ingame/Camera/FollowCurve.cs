using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FollowCurve : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;
    private float tParam;
    private Vector3 catPosition;
    public float speedModifier;
    public bool coroutineAllowed;


    public GameObject mainCanvas;
    void Start()
    {
        //transform.LookAt(new Vector3(90.6f, 80.4f, -192.2f));
        routeToGo = 0;
        tParam = 0f;
        //speedModifier = 0.4f;
        coroutineAllowed = false;
    }


    void Update()
    {
        if(coroutineAllowed)
            StartCoroutine(GoByRoute(routeToGo));
    }
    private IEnumerator GoByRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            catPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;
            //print(catPosition);
            transform.LookAt(catPosition);
            transform.position = catPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
            coroutineAllowed = false;
            if(SceneManager.GetActiveScene().name != "S_Castle")
                mainCanvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<StartScene>().ActiveTitlePannel();
            if (SceneManager.GetActiveScene().name == "S_Castle")
            {
                mainCanvas.transform.GetChild(6).gameObject.SetActive(true);
            }

        }
        else
        {
            coroutineAllowed = true;
        }
    }

    public void RegistRoute(Transform tr)
    {
        routes = new Transform[1];
        routes[0] = tr;
    }
}
