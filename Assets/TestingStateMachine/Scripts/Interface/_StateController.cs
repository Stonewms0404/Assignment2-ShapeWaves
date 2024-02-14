using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _StateController : MonoBehaviour
{
    IState currentState;

    public ChaseState chaseState = new ChaseState();
    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();
    public InjuredState injuredState = new InjuredState();

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEnter(this);
    }
}
public interface IState
{
    public void OnEnter(_StateController controller);
    public void UpdateState(_StateController controller);
    public void InjuredState(_StateController controller);
    public void OnExit(_StateController controller);
}
