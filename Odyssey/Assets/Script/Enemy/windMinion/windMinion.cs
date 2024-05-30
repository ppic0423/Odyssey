using System.Collections;
using UnityEngine;

public class windMinion : Enemy
{
    private bool isVisible = false; // Minion�� ���̴��� ���θ� ����
    private bool canUpdate = false; // Update �Լ��� ���� �������� ���θ� ����

    private void Awake()
    {
        this.RigidbodyInit();
        stats = GetComponent<WindMinion_Stats>();
    }
    void Update()
    {
        if (canUpdate)
        {
            if (currentState != null)
            {
                State nextState = currentState.Execute();
                if (nextState != currentState)
                {
                    ChangeState(nextState);
                }
            }
        }
        else
        {
            CheckVisibility(); // Minion�� ���̴��� Ȯ��
        }
    }

    void CheckVisibility()
    {
        if (IsVisible() && !isVisible)
        {
            isVisible = true; // Minion�� ó������ ������ ��
            StartCoroutine(StartUpdatingAfterDelay()); // 1�� �Ŀ� Update �Լ��� ����ǵ��� �ڷ�ƾ ����
        }
    }

    IEnumerator StartUpdatingAfterDelay()
    {
        yield return new WaitForSeconds(1); // 1�� ���
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