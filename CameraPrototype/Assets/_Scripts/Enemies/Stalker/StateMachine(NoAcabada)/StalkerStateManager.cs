using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerStateManager : MonoBehaviour
{

    public Transform player;

    StalkerBaseState currentState;
    public StalkerChaseState ChaseState = new StalkerChaseState();
    public StalkerSearchState SearchState = new StalkerSearchState();
    public StalkerStalkState StalkState = new StalkerStalkState();
    public StalkerStunnedState StunnedState = new StalkerStunnedState();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentState = StalkState;
        

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(StalkerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    

}
