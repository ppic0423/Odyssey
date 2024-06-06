using UnityEngine;

public class FireMinion_Wait : State
{
    [Header("�ν� ����(������)")]
    [SerializeField] float radius = 5f;
    public override State Execute()
    {
        if (CheckPlayerInSphere())
        {
            return nextState;
        }
        else
        {
            return this;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    bool CheckPlayerInSphere()
    {
        // ���� �����ϰ� �߽��� ����
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // �÷��̾ �� �ȿ� �ִ��� Ȯ��
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer == (int)Define.LayerMask.PLAYER)
            {
                return true;
            }
        }
        return false;
    }
}
