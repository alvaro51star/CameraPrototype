using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class EV_Luz : MonoBehaviour
{
    public GameObject lightFlickering;
    public MMFeedbacks luzSonidoFeedback;
    private bool isFlickering = false;
    private bool canFlick = false;
    private float timeDelay;
    public float nRandomTime1, nRandomTime2;

    private void Update()
    {
        if (canFlick == true)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(nRandomTime1, nRandomTime2);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(nRandomTime1, nRandomTime2);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canFlick = true;
            luzSonidoFeedback?.PlayFeedbacks();
        }
    }
    public void Deactivated()
    {
        lightFlickering.SetActive(false);
        Debug.Log("desactiva");
    }
}
