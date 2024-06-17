using UnityEngine;

public class Stage1 : MonoBehaviour, StageGimmick
{
    [Header("��Ÿ��")]
    [SerializeField] float coolDown = 9f;
    float coolDownDelta = 0f;

    [Header("��� �ð�")]
    [SerializeField] float warningTime = 1f;
    [SerializeField] float scalingSpeed;
    [Header("��� ������Ʈ")]
    [SerializeField] GameObject warningObject;
    [SerializeField] float maxScale;

    [Header("��� �ð�")]
    [SerializeField] float waitingTime = 1f;

    [Header("���� �ð�")]
    [SerializeField] float holdingTime = 2f;
    [SerializeField] GameObject fireWall;

    float phaseTimeDelta = 0;

    Define.GimmickPhase_Stage1 phase = Define.GimmickPhase_Stage1.WARNING;
    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
        warningObject.SetActive(false);
    }

    void Update()
    {
        coolDownDelta += Time.deltaTime;

        if(coolDownDelta >= coolDown)
        {
            Excute();
        }
    }

    public void Excute()
    {
        phaseTimeDelta += Time.deltaTime;
        Debug.Log(phaseTimeDelta);

        // ��� ������
        if (phase == Define.GimmickPhase_Stage1.WARNING)
        {
            if(phaseTimeDelta < warningTime)
            {
                // ��� ������Ʈ Ȱ��ȭ
                warningObject.SetActive(true);
                // ��� ������Ʈ �÷��̾� �߰�
                warningObject.transform.position = player.transform.position;
                // ��� ������Ʈ ũ�� ����
                float newScaleX = Mathf.Lerp(warningObject.transform.localScale.x, maxScale, scalingSpeed * Time.deltaTime);
                float newScaleZ = Mathf.Lerp(warningObject.transform.localScale.z, maxScale, scalingSpeed * Time.deltaTime);
                warningObject.transform.localScale = new Vector3(newScaleX, 50, newScaleZ);
            }
            else if(phaseTimeDelta >= warningTime) 
            {
                // ������ �ʱ�ȭ
                phaseTimeDelta = 0;
                phase = Define.GimmickPhase_Stage1.WAITING;
            }
        }
        // ��� ������
        else if(phase == Define.GimmickPhase_Stage1.WAITING)
        {
            if(phaseTimeDelta < waitingTime)
            {

            }
            else if(phaseTimeDelta >= waitingTime)
            {
                // ������ �ʱ�ȭ
                phaseTimeDelta = 0;
                warningObject.SetActive(false);
                phase = Define.GimmickPhase_Stage1.LAUNCH;
            }
        }
        // �ߵ� ������
        else if(phase == Define.GimmickPhase_Stage1.LAUNCH)
        {
            // �ʱ�ȭ
            phase = Define.GimmickPhase_Stage1.WARNING;
            fireWall.SetActive(true);
            warningObject.transform.localScale = Vector3.zero;
            phaseTimeDelta = 0;
            coolDownDelta = 0;
        }
    }
}
