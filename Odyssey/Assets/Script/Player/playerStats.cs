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
    }
    protected override void Dead()
    {
        base.Dead(); // �⺻ ���� ó�� ���� ����
    }
}