using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class EV_EnemigoTeVe : MonoBehaviour
{
    public MMFeedbacks enemigoTeVeFeedback;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            enemigoTeVeFeedback?.PlayFeedbacks();
        }
    }
}
