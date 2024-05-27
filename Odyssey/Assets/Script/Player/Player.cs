using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMovement playermovement;

    private void Awake()
    {
        RigidbodyInit();
    }

    private void Start()
    {
        if(GetComponent<PlayerMovement>() == null)
        {
            playermovement = gameObject.AddComponent<PlayerMovement>();
        }
    }

    void RigidbodyInit()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

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
