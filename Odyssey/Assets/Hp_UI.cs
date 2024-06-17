using UnityEngine;
using UnityEngine.UI;

public class Hp_UI : MonoBehaviour
{
    [SerializeField] float hpLayout_width;

    [Header("==========")]
    [SerializeField] RectTransform Hp_BackGround;
    [SerializeField] Image[] hpImages;
    [SerializeField] Sprite[] hpCircle_Images3;
    [SerializeField] Sprite[] hpCircle_Images5;
    [SerializeField] Sprite hp_Unfilled;
    [SerializeField] Image hpCircle;

    Sprite[] currentSprites;
    int currentIndex = 0;

    void Start()
    {
        // hp 5ĭ�� ���
        if(GameScene.Instance.canAddHp)
        {
            Hp_BackGround.sizeDelta = new Vector2(hpLayout_width, Hp_BackGround.sizeDelta.y);
            hpImages[3].gameObject.SetActive(true);
            hpImages[4].gameObject.SetActive(true);
            currentSprites = hpCircle_Images5;
            hpCircle.sprite = currentSprites[currentIndex];
        }
        else
        {
            hpImages[3].gameObject.SetActive(false);
            hpImages[4].gameObject.SetActive(false);
            currentSprites = hpCircle_Images3;
            hpCircle.sprite = currentSprites[currentIndex];
        }
    }
    public void SetHp_UI(int damage)
    {
        int damageCount = damage;
        // �迭�� ������ �ε������� �����ؼ� �������� �˻�
        for (int i = currentSprites.Length - 2; i >= 0; i--)
        {
            // �ش� ������Ʈ�� Ȱ��ȭ�Ǿ� �ִٸ�
            if (hpImages[i].sprite != hp_Unfilled)
            {
                // ������Ʈ ��Ȱ��ȭ
                hpImages[i].sprite = hp_Unfilled;

                // damage�� �ϳ� ����
                damageCount--;

                // ��Ȱ��ȭ�� ������Ʈ ���� 0�� �Ǹ� �Լ� ����
                if (damageCount == 0)
                {
                    break;
                }
            }
        }

        currentIndex = (currentIndex + damage) % currentSprites.Length;
        hpCircle.sprite = currentSprites[currentIndex];
    }
}
