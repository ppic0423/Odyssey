using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] StageGimmick stageGimmick;
    private void Start()
    {
        StageInit();
    }
    void Update()
    {
        if(stageGimmick != null)
        {
            stageGimmick.Excute();
        }
    }

    void StageInit()
    {
        // �׸��� ����
        Light[] lights = FindObjectsOfType<Light>();

        foreach (Light light in lights)
        {
            light.shadows = LightShadows.None;
        }
    }
}
