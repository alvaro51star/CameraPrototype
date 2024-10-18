using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    [SerializeField] private GameObject m_GO_nextButton, m_GO_previousButton;
    [SerializeField] private float m_pageSpeed = 1f;
    [SerializeField] private List<Transform> m_tfL_pgs;
    private int m_index = -1;
    private bool m_isRotate = false;

    public void Start()
    {
        InitialState();
    }

    //Custom 
    IEnumerator Rotate(float angle, bool next)
    {
        float value = 0f;
        while (true)
        {
            m_isRotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.time * m_pageSpeed;
            m_tfL_pgs[m_index].rotation = Quaternion.Slerp(m_tfL_pgs[m_index].localRotation, targetRotation, value);
            float angle1 = Quaternion.Angle(m_tfL_pgs[m_index].localRotation, targetRotation);
            if(angle1 < 0.1f)
            {
                if (next == false)
                    m_index--;
                m_isRotate = false;
                break;
            }
            yield return null;
        }
    }
    
    public void InitialState()
    {
        for (int i = 0; i < m_tfL_pgs.Count; i++)
            m_tfL_pgs[i].transform.localRotation = Quaternion.Euler(0,0,0);
        m_tfL_pgs[0].SetAsLastSibling();
        m_GO_previousButton.SetActive(false);
    }

    public void NextPage()
    {
        if (m_isRotate == true)
            return;
        m_index++;
        float angle = -180;
        NextButtonAction();
        m_tfL_pgs[m_index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }

    public void NextButtonAction()
    {
        if (m_GO_previousButton.activeInHierarchy == false)
            m_GO_previousButton.SetActive(true);
        if (m_index == m_tfL_pgs.Count - 1)
            m_GO_nextButton.SetActive(false);
    }

    public void PreviousPage()
    {
        if (m_isRotate == true)
            return;
        float angle = 0;
        PreviousButtonAction();
        m_tfL_pgs[m_index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, false));
    }

    public void PreviousButtonAction()
    {
        if (m_GO_nextButton.activeInHierarchy == false)
            m_GO_nextButton.SetActive(true);
        if (m_index - 1 == -1)
            m_GO_previousButton.SetActive(false);
    }
}
