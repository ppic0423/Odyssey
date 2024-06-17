using UnityEngine;

public class WindMinion_Stats : EnemyStats
{
    [Header("�浹 ��")]
    [SerializeField] float bounceForce = 10f;

    [Header("�̵� �ð�")]
    [SerializeField] float bounceTime = 1f;

    [Header("==========")]
    [SerializeField] WindMinion_Wait waitState;

    float bounceTimeDelta = 0f;
    bool isBouncing = false;

    private void FixedUpdate()
    {
        if (isBouncing)
        {
            bounceTimeDelta += Time.deltaTime; // ��� �ð� ������Ʈ

            if (bounceTimeDelta >= bounceTime)
            {
                // ���� �ð��� ������ ƨ���� ���߰� �ӵ��� 0���� ����
                enemy.rigidbody.velocity = Vector3.zero;
                bounceTimeDelta = 0.0f; // ��� �ð� �ʱ�ȭ
                enemy.ChangeState(waitState);
                isBouncing = false;
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        // Damage logic here
    }
    public override void Dead()
    {
        enemy.sound.PlayDeadSound();
        base.Dead();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && !isBouncing)
        {
            // ���� �ӵ��� �ݴ� �������� ���� ���� �� ũ�� ����
            Vector3 currentVelocity = enemy.rigidbody.velocity;
            Vector3 reverseVelocity = -currentVelocity.normalized * bounceForce;

            // �ݴ� �������� �ӵ� ����
            enemy.rigidbody.velocity = reverseVelocity;

            // ƨ�� ���� Ȱ��ȭ
            isBouncing = true;
        }

        if (collision.gameObject.layer == (int)Define.LayerMask.PLAYER)
        {
            collision.gameObject.GetComponent<Player>().stats.TakeDamage(damage);
        }
    }
}