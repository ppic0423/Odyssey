using UnityEngine;

public abstract class State : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] protected State nextState;

    [SerializeField] protected Enemy enemy;

    // ���°� ���۵� �� �� �� ����Ǵ� �Լ�
    public virtual void Enter() { }

    // ���°� �� �����Ӹ��� ����Ǵ� �Լ�
    public abstract State Execute();

    // ���°� ����� �� �� �� ����Ǵ� �Լ�
    public virtual void Exit() { }
}
