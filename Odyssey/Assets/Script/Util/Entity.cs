using UnityEngine;

public class Entity : MonoBehaviour
{
    [HideInInspector] public new Rigidbody rigidbody;

    protected virtual void ColliderInit()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), GetComponentsInChildren<Collider>()[1]);
    }
    protected virtual void RigidbodyInit()
    {
        rigidbody = GetComponent<Rigidbody>();

        // ������Ʈ�� ������ �߰�
        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody>();
        }

        // ������ٵ� ������Ʈ �ʱ�ȭ
        rigidbody.constraints = RigidbodyConstraints.FreezePositionZ |
                         RigidbodyConstraints.FreezeRotationZ |
                         RigidbodyConstraints.FreezeRotationX;
    }
}
