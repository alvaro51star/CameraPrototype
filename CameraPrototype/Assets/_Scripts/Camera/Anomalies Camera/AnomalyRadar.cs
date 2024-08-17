using UnityEngine;

public class AnomalyRadar : MonoBehaviour
{
    [SerializeField] private GameObject anomalyRadarUI;
    private Collider _collider;

    private void OnEnable()
    {
        EventManager.OnUsingCamera += OnUsingCamera;
        EventManager.OnNotUsingCamera += OnNotUsingCamera;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= OnUsingCamera;
        EventManager.OnNotUsingCamera -= OnNotUsingCamera;

    }

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<AnomaliesData>() && other.gameObject.GetComponent<AnomaliesData>().enabled)
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.instance.anomalyDetected /*, this.transform.position */);
            anomalyRadarUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<AnomaliesData>() && other.gameObject.GetComponent<AnomaliesData>().enabled)
        {
            anomalyRadarUI.SetActive(false);
        }
    }

    private void OnUsingCamera()
    {
        _collider.enabled = true;
    }
    private void OnNotUsingCamera()
    {
        anomalyRadarUI.SetActive(false);
        _collider.enabled = false;
    }
}
