using UnityEngine;

public class WindMinion_Stats : EntityStats
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

    public override void TakeDamage()
    {
        // Damage logic here
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
    }
}