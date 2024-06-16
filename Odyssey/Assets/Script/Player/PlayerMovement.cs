using Unity.Burst.CompilerServices;
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
    GameObject groundObject;

    [Header("����")]
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    [SerializeField] float raycastStartPoint = 0;
    [SerializeField] float raycastDistance = 1.1f;

    [Header("����")]
    [SerializeField] float attackCooldown;
    float attackCooldownDelta = 0;
    [SerializeField] float attackDuration;
    float attackDurationDelta = 0;
    [SerializeField] int dashDamage = 0;
    [SerializeField] GameObject horizontalHitBox;
    [SerializeField] GameObject downHitBox;

    [Header("�뽬")]
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 1f;
    bool isDashing = false;
    float dashTime = 0;
    float dashCooldownDelta = 0;
    [SerializeField] int damageDuringDash = 10;

    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        attackCooldownDelta = attackCooldown;
    }

    private void Update()
    {
        #region �ٴ� ����
        if (isGrounded)
            Debug.Log("asdf");
        RaycastHit hit;
        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + raycastStartPoint, transform.position.z);
        Debug.DrawRay(startPoint, Vector3.down * raycastDistance, Color.red);
        isGrounded = Physics.Raycast(startPoint, Vector3.down * raycastDistance, out hit, raycastDistance);
        if (isGrounded && player.rigidbody.velocity.y <= 0)
        {
            groundObject = hit.collider.gameObject;
            isJump = false;
            isDoubleJump = false;
        }
        #endregion

        #region ��Ÿ��
        attackCooldownDelta += Time.deltaTime;
        dashCooldownDelta += Time.deltaTime;

        attackDurationDelta += Time.deltaTime;
        if (attackDurationDelta > attackDuration)
        {
            horizontalHitBox.gameObject.SetActive(false);
            downHitBox.gameObject.SetActive(false);
        }
        #endregion

        if (!isDashing) // �뽬 ���� �ƴ� ���� �ٸ� ���� ����
        {
            Attack(); // ����
            JumpAndFall(); // ���� �� ����
        }

        Dash(); // �뽬
    }

    private void FixedUpdate()
    {
        if (!isDashing) // �뽬 ���� �ƴ� ���� �̵� ����
        {
            StopMove(); // �̵� ����
            LeftMove(); // ���� �̵�
            RightMove(); // ���� �̵�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDashing && other.gameObject.layer == (int)Define.LayerMask.ENEMY)
        {
            EnemyStats enemy = other.gameObject.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageDuringDash);
            }

            if (other.GetComponent<GroundMinion>() != null)
            {
                isDashing = false;
            }
        }
        else if (isDashing && other.gameObject.layer == (int)Define.LayerMask.GROUND)
        {
            if(other.gameObject != groundObject)
            {
                isDashing = false;
                player.rigidbody.useGravity = true;
                player.collider.isTrigger = false;
            }
        }
    }

    void StopMove()
    {
        if ((InputManager.Instance.IsLeftMove && InputManager.Instance.IsRightMove) || (!InputManager.Instance.IsLeftMove && !InputManager.Instance.IsRightMove))
        {
            if(!isJump)
            {
                player.anim.PlayAnimation("idle");
            }
            player.rigidbody.velocity = new Vector3(0, player.rigidbody.velocity.y, player.rigidbody.velocity.z);
        }
    }
    void RightMove()
    {
        if (InputManager.Instance.IsRightMove)
        {
            if(!isJump)
            {
                player.anim.PlayAnimation("run");
            }
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            player.rigidbody.velocity = new Vector3(moveSpeed, player.rigidbody.velocity.y, player.rigidbody.velocity.z);
        }
    }
    void LeftMove()
    {
        if (InputManager.Instance.IsLeftMove)
        {
            if (!isJump)
            {
                player.anim.PlayAnimation("run");
            }
            player.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            player.rigidbody.velocity = new Vector3(-moveSpeed, player.rigidbody.velocity.y, player.rigidbody.velocity.z);
        }
    }
    void JumpAndFall()
    {
        if (InputManager.Instance.IsJump)
        {
            if (isGrounded)
            {
                player.anim.PlayAnimation("jump");
                player.rigidbody.velocity += Vector3.up * jumpForce;
                isJump = true;
            }
            else if (isJump && !isDoubleJump && GameScene.Instance.canDoubleJump)
            {
                player.anim.PlayAnimation("jump");
                player.rigidbody.velocity += Vector3.up * jumpForce;
                isDoubleJump = true;
            }
        }

        if (player.rigidbody.velocity.y < 0)
        {
            player.rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            if(!isGrounded)
            {
                player.anim.PlayAnimation("fall");
            }
        }
        else if (player.rigidbody.velocity.y > 0 && !InputManager.Instance.IsJump)
        {
            player.rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            if(!isGrounded)
            {
                player.anim.PlayAnimation("fall");
            }
        }
    }
    void Attack()
    {
        if (InputManager.Instance.IsAttack)
        {
            if (attackCooldownDelta < attackCooldown)
                return;

            if (InputManager.Instance.IsDownMove && !isGrounded)
            {
                downHitBox.gameObject.SetActive(true);
            }
            else
            {
                horizontalHitBox.gameObject.SetActive(true);
            }

            attackCooldownDelta = 0;
            attackDurationDelta = 0;
        }
    }
    void Dash()
    {
        if (InputManager.Instance.IsDash && dashCooldownDelta >= dashCooldown && GameScene.Instance.canDash)
        {
            isDashing = true;
            dashTime = 0;
            dashCooldownDelta = 0;
            player.collider.isTrigger = true;
            player.rigidbody.useGravity = false;
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;

            if (dashTime < dashDuration)
            {
                Vector3 dashDirection = player.transform.forward;
                player.rigidbody.velocity = dashDirection * dashSpeed;
            }
            else
            {
                isDashing = false;
                player.rigidbody.useGravity = true;
                player.collider.isTrigger = false;
            }
        }
    }
}