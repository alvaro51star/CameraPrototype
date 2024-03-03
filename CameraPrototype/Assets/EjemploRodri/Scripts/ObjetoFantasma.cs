using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoFantasma : MonoBehaviour
{
    /*****Al testearlo cierra la pestaña de SCENE porque sino se cree que lo está renderizando,
     aun que sea con la cámara de edición***/


    public List<Transform> spawnPositions = new List<Transform>();
    [SerializeField] Renderer objectMesh;

    bool isVisible;

    void Update()
    {
        if (objectMesh.GetComponent<Renderer>().isVisible)
        {
            print("Visible");
            isVisible = true;
        }
        else
        {
            print("Invisible");
            if (isVisible) {
                TeleportToRandomPlace();
            }
            //Not visible code here
            isVisible = false;
        }
    }

    void TeleportToRandomPlace() {
        int random = Random.Range(0, spawnPositions.Count);
        print("Teleport a " + spawnPositions[random].position);
        transform.position = spawnPositions[random].position;
        StartCoroutine(ComprobarAframePasado());
    }

    IEnumerator ComprobarAframePasado() {
        yield return new WaitForEndOfFrame();
        if (objectMesh.GetComponent<Renderer>().isVisible)
        {
            print("Otra vez");
            TeleportToRandomPlace();

        }
    }

}
