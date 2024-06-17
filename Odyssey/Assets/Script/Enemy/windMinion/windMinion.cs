using System.Collections;
using UnityEngine;

public class windMinion : Enemy
{
    private bool isVisible = false; // Minion�� ���̴��� ���θ� ����
    private bool canUpdate = false; // Update �Լ��� ���� �������� ���θ� ����
    [SerializeField] State initState;

    private void Awake()
    {
        sound = GetComponent<EnemySoundController>();

        RigidbodyInit();
        stats = GetComponent<WindMinion_Stats>();
    }
    void Update()
    {
        if (canUpdate)
        {
            StateTick();
        }
        else
        {
            CheckVisibility(); // Minion�� ���̴��� Ȯ��
        }
    }
    protected override void StateTick()
    {
        base.StateTick();
    }
    void CheckVisibility()
    {
        if (IsVisible() && !isVisible)
        {
            StartCoroutine(StartUpdatingAfterDelay()); // 1�� �Ŀ� Update �Լ��� ����ǵ��� �ڷ�ƾ ����
        }
    }
    IEnumerator StartUpdatingAfterDelay()
    {
        yield return new WaitForSeconds(1); // 1�� ���
        ChangeState(initState); // Minion�� ó������ ������ ��
        canUpdate = true; // 1�� �Ŀ� Update �Լ� ���� ����
    }
    bool IsVisible()
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);

        // ����Ʈ ���� �ִ��� Ȯ��
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1 && viewportPoint.z > 0;
    }
    protected override void RigidbodyInit()
    {
        base.RigidbodyInit();
        rigidbody.useGravity = false;
    }
}