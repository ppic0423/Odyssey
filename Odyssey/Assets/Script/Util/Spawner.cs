using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("�߻� ��")]
    [SerializeField] float launchForce = 0;

    [Header("��ȯ ������Ʈ")]
    [SerializeField] GameObject waterMinion;

    [Header("���� �ð�")]
    [SerializeField] float spawnTime = 2f;

    [Header("�߻� �ð�")]
    [SerializeField] float waitingTime = 1f;

    BoxCollider boxCollider;
    Bounds bounds;
    GameObject go;

    private float timer = 0f;
    private bool hasSpawned = false;

    private void Awake()
    {
        BoxColliderInit();
    }

    private void Update()
    {
        if(waterMinion != null)
        {
            timer += Time.deltaTime;

            if (!hasSpawned && timer >= spawnTime)
            {
                go = Instantiate(waterMinion, GetRandomPositionInBounds(), Quaternion.identity);
                hasSpawned = true;
                timer = 0f; // Ÿ�̸Ӹ� �ʱ�ȭ�Ͽ� ���� �ܰ�� �Ѿ
            }
            else if (hasSpawned && timer >= waitingTime)
            {
                go.GetComponent<WaterMinion>().ChangeState(go.GetComponent<WaterMinion>().launchState);
                go.GetComponent<Rigidbody>().AddForce(Vector3.right * launchForce, ForceMode.Impulse);
                hasSpawned = false;
                timer = 0f; // Ÿ�̸Ӹ� �ʱ�ȭ�Ͽ� ���� �ܰ�� �Ѿ
            }
        }
    }

    Vector3 GetRandomPositionInBounds()
    {
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(transform.position.x, y, transform.position.z);
    }

    void BoxColliderInit()
    {
        boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        boxCollider.excludeLayers = ~0;
        bounds = boxCollider.bounds; // BoxCollider�� ��� ������ ������
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.layer == (int)Define.LayerMask.PLAYER)
            {
                collision.gameObject.GetComponent<playerStats>().TakeDamage(999);
            }
        }
    }
}