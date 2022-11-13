using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrapInteraction : MonoBehaviour
{
    public Image trapImage1;
    public AudioSource trap1AudioSource;
    public AudioClip trap1Audio;
    Coroutine ActiveTrap = null;

    private void Start()
    {
        trap1AudioSource = GetComponent<AudioSource>();
    }
    public void PlayerTraped()
    {
        ActiveTrap = StartCoroutine(Traped());
    }
    IEnumerator Traped()
    {
        trapImage1.gameObject.SetActive(true);
        trap1AudioSource.PlayOneShot(trap1Audio);
        yield return new WaitForSeconds(1.0f);

        trapImage1.gameObject.SetActive(false);
    }
}
