using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject previousButton;

    [SerializeField] float pageSpeed = 1f;
    [SerializeField] List<Transform> pgs;
    int index = -1;
    bool rotate = false;

    public void Start()
    {
        InitialState();
    }

    public void InitialState()
    {
        for (int i = 0; i < pgs.Count; i++)
            pgs[i].transform.localRotation = Quaternion.Euler(0,0,0);
        pgs[0].SetAsLastSibling();
        previousButton.SetActive(false);
    }

    public void NextPage()
    {
        if (rotate == true)
            return;
        index++;
        float angle = -180;
        NextButtonAction();
        pgs[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }

    public void NextButtonAction()
    {
        if (previousButton.activeInHierarchy == false)
            previousButton.SetActive(true);
        if (index == pgs.Count - 1)
            nextButton.SetActive(false);
    }

    public void PreviousPage()
    {
        if (rotate == true)
            return;
        float angle = 0;
        PreviousButtonAction();
        pgs[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, false));
    }

    public void PreviousButtonAction()
    {
        if (nextButton.activeInHierarchy == false)
            nextButton.SetActive(true);
        if (index - 1 == -1)
            previousButton.SetActive(false);
    }

    IEnumerator Rotate(float angle, bool next)
    {
        float value = 0f;
        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.time * pageSpeed;
            pgs[index].rotation = Quaternion.Slerp(pgs[index].localRotation, targetRotation, value);
            float angle1 = Quaternion.Angle(pgs[index].localRotation, targetRotation);
            if(angle1 < 0.1f)
            {
                if (next == false)
                    index--;
                rotate = false;
                break;
            }
            yield return null;
        }
    }
}
