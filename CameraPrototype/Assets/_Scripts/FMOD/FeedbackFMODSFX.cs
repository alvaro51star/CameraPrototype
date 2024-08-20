using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FeedbackFMODSFX : MonoBehaviour
{
    [SerializeField] private EventReference AmbienceSFX;
    public GameObject gORuidoFeedback;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.PlayOneShot(AmbienceSFX /*, this.transform.position*/);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        gORuidoFeedback.SetActive(false);
        Debug.Log("desactivado");
    }
}
