using UnityEngine;

public class Enemy : Entity
{
    [Header("���� ����")]
    [SerializeField] protected State currentState;
    [Header("����")]
    [SerializeField] protected EntityStats stats;

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter();
        }
    }
}
