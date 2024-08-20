using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MoreMountains.Feedbacks;
using FMODUnity;

public class EV_Luz : MonoBehaviour
{
    [SerializeField] private EventReference AmbienceSFX;
    public GameObject lightFlickering;
    //public MMFeedbacks luzSonidoFeedback;
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
            AudioManager.Instance.PlayOneShot(AmbienceSFX /*, this.transform.position*/);
            //luzSonidoFeedback?.PlayFeedbacks();
            StartCoroutine(TurnOff());
        }
    }
    public void Deactivated()
    {
        lightFlickering.SetActive(false);
    }

    private IEnumerator TurnOff()
    {
        yield return new WaitForSecondsRealtime(2f);
        Deactivated();
    }
}
