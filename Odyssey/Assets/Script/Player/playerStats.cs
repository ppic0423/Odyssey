using UnityEngine;

public class playerStats : EntityStats
{
    [Header("���� �ð�")]
    [SerializeField] float immuneTime; // ���� �ð� ����
    float immuneTimeDelta; // ���� ���� �ð��� �����ϴ� ����

    Player player;

    private void Awake()
    {
        player = gameObject.GetComponent<Player>(); // Player ������Ʈ ��������
    }
    private void Start()
    {
        if (GameScene.Instance.canAddHp)
        {
            hp = 5;
        }
        else
        {
            hp = 3;
        }
    }
    private void Update()
    {
        // �� �����Ӹ��� ���� ���� �ð��� ���ҽ�Ŵ
        if (immuneTimeDelta > 0)
        {
            immuneTimeDelta -= Time.deltaTime;
        }
    }

    public override void TakeDamage(int damage)
    {
        if (immuneTimeDelta > 0)
        {
            return;  // �÷��̾ ���� ���� �����̸� �޼��� ����
        }

        base.TakeDamage(damage); // �⺻ ������ ó�� ���� ����
        FindObjectOfType<Hp_UI>().SetHp_UI(damage);
        immuneTimeDelta = immuneTime;  // ���� �ð� �ʱ�ȭ
        player.sound.PlayHitSound();
    }
    public override void Dead()
    {
        base.Dead(); // �⺻ ���� ó�� ���� ����
        GetComponent<SkinnedMeshRenderer>().enabled = false;
        player.sound.PlayDeadSound();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}