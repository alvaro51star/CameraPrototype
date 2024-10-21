using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnomalyCullingGroup : MonoBehaviour//todo esto porque se supone que si que considera el occlusion https://riptutorial.com/unity3d/example/16053/culling-object-visibility
//no se por que solo esta considerando el frustrum, pasa del occlusion culling
{
    [Header("All anomalies should be here")]
    [SerializeField] private Camera m_cam_anomalyCam;
    [SerializeField] private List<AnomaliesData>  m_anomaliesDataL;
    private CullingGroup m_cullingGroup;
    private BoundingSphere[] m_boundingSpheresA;

    private void Start()
    {
        m_cullingGroup = new CullingGroup();

        m_boundingSpheresA = new BoundingSphere[m_anomaliesDataL.Count];
        
        for (int i = 0; i < m_boundingSpheresA.Length; i++)
        {
            m_boundingSpheresA[i].position = m_anomaliesDataL[i].transform.position;
            m_boundingSpheresA[i].radius = 1f;
        }
        m_cullingGroup.SetBoundingSpheres(m_boundingSpheresA);
        m_cullingGroup.targetCamera = m_cam_anomalyCam;
        
        m_cullingGroup.onStateChanged = CullingEvent;
    }

    private void OnDisable()
    {
        m_cullingGroup.Dispose();
        m_cullingGroup = null;
    }

    private void CullingEvent(CullingGroupEvent sphere)
    {
        m_anomaliesDataL[sphere.index].isCullingVisible = sphere.isVisible;//for this to be correct use an end of frame (so it's properly rendered)
        Debug.Log(sphere.index + " " + sphere.isVisible);
    }
    
}
