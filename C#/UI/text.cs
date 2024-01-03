using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class text : MonoBehaviour
{
    
    private Transform cameraTransform;
    public float scaleFactor = 0.005f; // 거리에 따른 크기 조정을 위한 스케일 인자
    public float minFontSize = 0.1f; // 최소 폰트 크기
    public TMP_Text tMP;
    baseplanet baseplanet;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        baseplanet = transform.parent.GetComponent<baseplanet>();
    }

    void Update()
    {
        tMP.text = baseplanet.@base.name;
        // 카메라와 텍스트 객체 간의 거리 계산
        float distance = Vector3.Distance(transform.position, cameraTransform.position);

        // 거리에 따라 텍스트 폰트 크기 조절
        float adjustedSize = Mathf.Max(minFontSize, distance * scaleFactor);
        tMP.fontSize = adjustedSize;

        // 거리에 따라 오브젝트의 localScale 조절
        float scale = adjustedSize / 10; // 예를 들어 기준 크기를 10으로 설정하고 조절합니다
        transform.localScale = new Vector3(scale, scale, 1);
    }
}

