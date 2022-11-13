using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHide : MonoBehaviour
{
    public Transform target;
    public GameObject hideWall;
    public float distanceCal;
    public Camera camera;

    Ray castRay;
    RaycastHit hit;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    private void Update()
    {
        if (target.gameObject.activeSelf == true)
        {
            castRay = new Ray(camera.transform.position, target.position - camera.transform.position);

            distanceCal = Vector3.Distance(target.position, camera.transform.position);
            if (Physics.Raycast(castRay, out hit, distanceCal))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Wall"))
                    {
                        if (hideWall != null)
                        {
                            if (hideWall != hit.collider.gameObject)
                            {
                                Visible(true);
                                hideWall = hit.collider.gameObject;
                                Visible(false);
                            }
                        }
                        else
                        {
                            hideWall = hit.collider.gameObject;
                            Visible(false);
                        }
                    }
                    else
                    {
                        if (hideWall != null)
                        {
                            Visible(true);
                            hideWall = null;
                        }
                    }
                }
            }
        }
    }
    public void Visible(bool _isVisible)
    {
        if(hideWall.transform.parent != null && hideWall.transform.parent.CompareTag("Wall"))
        {
            for(int i = 0; i < hideWall.transform.parent.childCount; i++)
            {
                MeshRenderer wallMesh = hideWall.transform.parent.GetChild(i).GetComponent<MeshRenderer>() ?? null;

                if(wallMesh != null)
                {
                    wallMesh.enabled = _isVisible;
                }

            }
        }
        else if(hideWall.transform.childCount < 0)
        {
            for(int i = 0; i < hideWall.transform.childCount; i++)
            {
                MeshRenderer wallMesh = hideWall.transform.GetChild(i).GetComponent<MeshRenderer>() ?? null;
                if(wallMesh != null)
                {
                    wallMesh.enabled = _isVisible;
                }
            }
        }
        else
        {
            MeshRenderer wallMesh = hideWall.GetComponent<MeshRenderer>() ?? null;
            if(wallMesh != null)
            {
                wallMesh.enabled = _isVisible;
            }
        }
    }

}
