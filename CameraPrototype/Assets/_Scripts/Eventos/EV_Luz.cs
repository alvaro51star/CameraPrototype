using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class EV_Luz : MonoBehaviour
{
    public GameObject lightFlickering;
    public MMFeedbacks luzSonidoFeedback;
    public bool isFlickering = false;
    public float timeDelay;

    private void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.25f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.1f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            luzSonidoFeedback?.PlayFeedbacks();
        }
    }

    public void Deactivated()
    {
        lightFlickering.SetActive(false);
    }
}
