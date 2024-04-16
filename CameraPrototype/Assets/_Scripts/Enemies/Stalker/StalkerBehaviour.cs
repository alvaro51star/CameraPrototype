using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class StalkerBehaviour : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private Renderer objectMesh;
    [SerializeField] private GameObject player;

    [Header("States")]
    State states;
    public StalkState stalkState;
    public StunnedState stunnedState;
    public ChaseState chaseState;
    public PlayerCatchState playerCatchState;


    public bool isVisible = false;

    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private Collider collision;
    [SerializeField] private Collider triggerCollision;

    #region Time Variables
    [Space]
    [Header("Time Variables")]
    [SerializeField] private float maxTimeLooked = 2f;
    [SerializeField] private float currentTimeLooked = 0;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        //Set ups de los estados
        stalkState.SetUp(gameObject, objectMesh);
        stunnedState.SetUp(navMesh);
        chaseState.SetUp(navMesh, player);
        playerCatchState.SetUp(navMesh, player, uiManager);


        states = stalkState;
        states.Enter();

        transform.LookAt(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (objectMesh.isVisible)
        {
            isVisible = true;
        }
        else
        {
            isVisible = false;
        }

        if (states.isComplete)
        {
            SelectNextState();
        }

        states.Do();
    }

    private void SelectNextState()
    {
        states.Exit();

        if (currentTimeLooked >= maxTimeLooked)
        {
            states = chaseState;
        }
        else
        {
            states = stalkState;
        }

        states.Enter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && currentTimeLooked >= maxTimeLooked)
        {
            Debug.Log("Player detectado");
            states = playerCatchState;
            states.Enter();
        }
    }

    

    #region StalkBehaviour

    public void AddVision(float deltaTime)
    {
        Debug.Log($"Time looked = {currentTimeLooked}");
        currentTimeLooked += deltaTime;

        if (currentTimeLooked >= maxTimeLooked)
        {
            states = chaseState;
            states.Enter();
        }

        EventManager.OnTimeAdded?.Invoke(currentTimeLooked, maxTimeLooked);
    }
    #endregion


    public void ActivateCollision()
    {
        collision.isTrigger = false;
        triggerCollision.enabled = true;
    }

    public void StunEnemy()
    {
        //isStunned = true;
        states = stunnedState;
        states.Enter();
    }

}
