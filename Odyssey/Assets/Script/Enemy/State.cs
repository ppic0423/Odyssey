using UnityEngine;

public abstract class State : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] protected State nextState;

    protected Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // ���°� ���۵� �� �� �� ����Ǵ� �Լ�
    public virtual void Enter() { }

    // ���°� �� �����Ӹ��� ����Ǵ� �Լ�
    public abstract State Execute();

    // ���°� ����� �� �� �� ����Ǵ� �Լ�
    public virtual void Exit() { }
}
