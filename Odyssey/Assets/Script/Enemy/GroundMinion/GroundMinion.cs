using UnityEngine;

public class GroundMinion : Enemy
{
    private void Awake()
    {
        this.RigidbodyInit();
        stats = GetComponent<GroundMinion_stats>();
    }

    private void Update()
    {
        StateTick();
    }

    protected override void RigidbodyInit()
    {
        rigidbody = GetComponent<Rigidbody>();

        // ������Ʈ�� ������ �߰�
        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody>();
        }

        // ������ٵ� ������Ʈ �ʱ�ȭ
        rigidbody.constraints = RigidbodyConstraints.FreezePositionZ |
                         RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezeRotationZ |
                         RigidbodyConstraints.FreezeRotationX;
        rigidbody.isKinematic = true;
        rigidbody.useGravity = true;
    }
}
