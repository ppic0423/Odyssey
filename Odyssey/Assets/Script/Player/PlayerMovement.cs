using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("�̵�")]
    [SerializeField] float moveSpeed = 5f;

    [Header("����")]
    [SerializeField] float jumpForce = 10f;
    bool isGrounded = true;
    bool isJump = false;
    bool isDoubleJump = false;

    [Header("����")]
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    [SerializeField] float raycastDistance = 1.1f;

    [Header("����")]
    [SerializeField] float attackCooldown;
    float attackCooldownDelta = 0;
    [SerializeField] float attackDuration;
    float attackDurationDelta = 0;
    [SerializeField] GameObject horizontalHitBox;
    [SerializeField] GameObject downHitBox;

    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        attackCooldownDelta = attackCooldown;
    }
    private void Update()
    {
        #region �ٴ� ����
        // �ٴ� ����
        Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.red);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance);
        if (isGrounded && player.rigidbody.velocity.y <= 0)
        {
            isJump = false;
            isDoubleJump = false;
        }
        #endregion

        #region ��Ÿ��
        // ���� ��Ÿ��
        attackCooldownDelta += Time.deltaTime;
        // ���� ��Ȱ��ȭ
        attackDurationDelta += Time.deltaTime;
        if (attackDurationDelta > attackDuration)
        {
            horizontalHitBox.gameObject.SetActive(false);
            downHitBox.gameObject.SetActive(false);
        }
        #endregion

        Attack(); // ����
        JumpAndFall(); // ���� �� ����
    }
    private void FixedUpdate()
    {
        StopMove(); // �̵� ����
        LeftMove(); // ���� �̵�
        RightMove(); // ���� �̵�
    }
    void StopMove()
    {
        if ((InputManager.Instance.IsLeftMove && InputManager.Instance.IsRightMove) || (!InputManager.Instance.IsLeftMove && !InputManager.Instance.IsRightMove))
        {
            player.rigidbody.velocity = new Vector3(0, player.rigidbody.velocity.y, player.rigidbody.velocity.z);
        }
    }
    void RightMove()
    {
        if (InputManager.Instance.IsRightMove)
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            player.rigidbody.velocity = new Vector3(moveSpeed, player.rigidbody.velocity.y, player.rigidbody.velocity.z);
        }
    }
    void LeftMove()
    {
        if (InputManager.Instance.IsLeftMove)
        {
            player.transform.rotation = Quaternion.Euler(Vector3.zero);
            player.rigidbody.velocity = new Vector3(-moveSpeed, player.rigidbody.velocity.y, player.rigidbody.velocity.z);
        }
    }
    void JumpAndFall()
    {
        // ����
        if (InputManager.Instance.IsJump)
        {
            if (isGrounded)
            {
                player.rigidbody.velocity += Vector3.up * jumpForce;
                isJump = true;
            }
            else if (isJump && !isDoubleJump)
            {
                player.rigidbody.velocity += Vector3.up * jumpForce;
                isDoubleJump = true;
            }
        }

        // ����
        if (player.rigidbody.velocity.y < 0)
        {
            player.rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (player.rigidbody.velocity.y > 0 && !InputManager.Instance.IsJump)
        {
            player.rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void Attack()
    {
        if (InputManager.Instance.IsAttack)
        {
            // ��Ÿ���̸� return
            if (attackCooldownDelta < attackCooldown)
                return;

            // �����̰� �Ʒ�Ű�� ������ ������ �ϴ� ����
            if (InputManager.Instance.IsDownMove && !isGrounded)
            {
                downHitBox.gameObject.SetActive(true);
            }
            // Ⱦ����
            else
            {
                horizontalHitBox.gameObject.SetActive(true);
            }

            attackCooldownDelta = 0;
            attackDurationDelta = 0;
        }
    }

}