using UnityEngine;

public class FireMinion_Follow : State
{
    [Header("�̵� �ӵ�")]
    [SerializeField] float followSpeed;
    [Header("���� ����")]
    [SerializeField] float explosionRadius;
    [Header("���� �ð�")]
    [SerializeField] float explosionTime;
    float explosionTimeDelta = 0f;
    [Header("���� ������")]
    [SerializeField] int explosionDamage;
    [Header("���� ���� �ð�")]
    [SerializeField] float interval = 1.0f;
    [Header("���� ����")]
    [SerializeField] Color changeColor;
    [SerializeField] GameObject bombEffect;

    Player player = null;
    Renderer[] objectRenderer;
    Color[] originalColor;
    bool isColorChange = false;
    float timer = 0;

    public override void Enter()
    {
        // �÷��̾� ����
        player = FindObjectOfType<Player>();
        // ��ü�� Renderer�� ������ �ʱ� ������ ����
        objectRenderer = enemy.GetComponentsInChildren<Renderer>();
        originalColor = new Color[objectRenderer.Length]; // �ʱ�ȭ �߰�
        for (int i = 0; i < objectRenderer.Length; i++)
        {
            originalColor[i] = objectRenderer[i].material.color;
        }
    }

    public override State Execute()
    {
        FollowPlayer(); // �÷��̾� �߰�

        // ���� �ȿ� �÷��̾ ���� ���
        if (CheckPlayerInSphere())
        {
            ChangeColor();
            explosionTimeDelta += Time.deltaTime;
        }
        else
        {
            OriginColor(true);
            explosionTimeDelta = 0;
        }

        // ����
        if (explosionTimeDelta > explosionTime)
        {
            Explose();
        }

        return this;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    bool CheckPlayerInSphere()
    {
        // ���� �����ϰ� �߽��� ����
        Collider[] colliders = Physics.OverlapSphere(enemy.transform.position, explosionRadius);

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

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, newPosition, followSpeed * Time.deltaTime);
            if (player.transform.position.x > transform.position.x)
            {
                enemy.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                enemy.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
    }

    void Explose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            // Entity ������Ʈ�� ������ �ִ��� Ȯ��
            EntityStats stats = hitCollider.GetComponent<EntityStats>();
            if (stats != null)
            {
                // TakeDamage �Լ� ����
                stats.TakeDamage(explosionDamage);
            }
        }
        SoundManager.Instance.PlayFireMinionExplosion();
        Instantiate(bombEffect, enemy.transform.position, enemy.transform.rotation);
        enemy.stats.Dead();
    }

    void ChangeColor()
    {
        timer += Time.deltaTime; // ������ �ð� ����

        if (timer >= interval)
        {
            // interval �ð���ŭ �������� ���� ����
            if (isColorChange)
            {
                OriginColor(true);
            }
            else
            {
                OriginColor(false);
            }

            isColorChange = !isColorChange; // ���� ���� ���� ����
            timer = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }

    void OriginColor(bool origin)
    {
        if (origin)
        {
            for (int i = 0; i < objectRenderer.Length; i++)
            {
                objectRenderer[i].material.color = originalColor[i];
            }
        }
        else
        {
            for (int i = 0; i < objectRenderer.Length; i++)
            {
                objectRenderer[i].material.color = changeColor; // ������ ���� ���� ���
            }
        }
    }
}