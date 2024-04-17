using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        string copytest = "Alooooo presidentes";
        CopyToClipboard(copytest);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CopyToClipboard(string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }
}
