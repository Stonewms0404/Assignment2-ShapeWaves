using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(_StateController controller) 
    {
        //Call a VoiceLine "God I hope no one comes by and its a quiet night."
    }

    public void UpdateState(_StateController controller) //Acts like the Update function in MonoBehavior
    {
        //Searching for a player.
        //Sees player calls ChangeState(ChaseState)
    }

    public void OnExit(_StateController controller)
    {
        //Switching to the Chase state and yells "Hey get back here!"
        //Switching to the Injured state and yells "Oh no I have been hurt!:
    }

    public void InjuredState(_StateController controller)
    {
        controller.ChangeState(controller.injuredState);
    }
}
