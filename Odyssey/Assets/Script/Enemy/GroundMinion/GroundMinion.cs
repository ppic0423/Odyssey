using UnityEngine;

public class GroundMinion : Enemy
{
    [Header("��Ʈ �ڽ� ��ġ")]
    [SerializeField] Vector3 boxSize = new Vector3(1, 1, 1);
    [SerializeField] Vector3 damagePos;

    private void Awake()
    {
        sound = GetComponent<EnemySoundController>();

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
