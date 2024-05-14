using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingManager : MonoBehaviour
{
    public static TestingManager Instance;
    private float time = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            time += Time.deltaTime;
        }
    }

    public void AddTime(GameFinalState finalState)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string timeString = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);

        if (finalState == GameFinalState.Win)
        {
            timeString.Concat(" ---> Win");
        }
        else
        {
            timeString.Concat(" ---> Lost");
        }

        GameManager.Instance.AddTimeToList(timeString);
    }
}

public enum GameFinalState
{
    Lost,
    Win
}
