using System.Collections.Generic;
using UnityEngine;

public class AnomalyCullingGroup : MonoBehaviour//todo esto porque se supone que si que considera el occlusion https://riptutorial.com/unity3d/example/16053/culling-object-visibility
//no se por que solo esta considerando el frustrum, pasa del occlusion culling
{
    private CullingGroup _group;
    private BoundingSphere[] _spheres;
    [Header("All anomalies should be here")]
    [SerializeField] private Camera anomalyCam;
    [SerializeField] private List<AnomaliesData>  anomalies;
    

    private void Start()
    {
        _group = new CullingGroup();

        _spheres = new BoundingSphere[anomalies.Count];
        
        for (int i = 0; i < _spheres.Length; i++)
        {
            _spheres[i].position = anomalies[i].transform.position;
            _spheres[i].radius = 1f;
        }
        _group.SetBoundingSpheres(_spheres);
        _group.targetCamera = anomalyCam;
        
        _group.onStateChanged = CullingEvent;
    }

    private void OnDisable()
    {
        _group.Dispose();
        _group = null;
    }

    private void CullingEvent(CullingGroupEvent sphere)
    {
        anomalies[sphere.index].cullingIsVisible = sphere.isVisible;//for this to be correct use an end of frame (so it's properly rendered)
        Debug.Log(sphere.index + " " + sphere.isVisible);
    }
    
}
