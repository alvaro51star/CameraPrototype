using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Feel_Sonido : MonoBehaviour
{
    public MMFeedbacks ruidoFeedback;
    public GameObject gORuidoFeedback;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ruidoFeedback?.PlayFeedbacks();
        }
    }

    public void Deactivate()
    {
        gORuidoFeedback.SetActive(false);
    }
}
