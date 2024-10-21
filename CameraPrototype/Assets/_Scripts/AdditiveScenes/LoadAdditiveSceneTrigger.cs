using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditiveSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);       
    }
}
