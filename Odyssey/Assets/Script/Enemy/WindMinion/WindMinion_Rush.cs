using UnityEngine;

public class WindMinion_Rush : State
{
    [Header("�̵� �ӵ�")]
    [SerializeField] float moveSpeed = 5f;

    Vector3 targetPos;

    public override void Enter()
    {
        targetPos = (FindObjectOfType<Player>().transform.position - transform.position).normalized;
        enemy.rigidbody.velocity = targetPos * moveSpeed;
    }
    public override State Execute()
    {
        return this;
    }
}
