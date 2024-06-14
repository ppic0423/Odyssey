using UnityEngine;

public class CameraMover : MonoBehaviour
{
    GameObject player;
    [SerializeField] float speed = 5f;
    [SerializeField] float yFollowSpeed = 2f;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    private void FixedUpdate()
    {
        // �̵� �ӵ��� ���������� �̵�
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // �÷��̾��� y ��ġ�� ���� ī�޶��� y ��ġ ����
        float newYPosition = Mathf.Lerp(transform.position.y, player.transform.position.y, yFollowSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }
}