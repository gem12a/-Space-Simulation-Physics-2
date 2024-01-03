using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class text : MonoBehaviour
{
    
    private Transform cameraTransform;
    public float scaleFactor = 0.005f; // �Ÿ��� ���� ũ�� ������ ���� ������ ����
    public float minFontSize = 0.1f; // �ּ� ��Ʈ ũ��
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
        // ī�޶�� �ؽ�Ʈ ��ü ���� �Ÿ� ���
        float distance = Vector3.Distance(transform.position, cameraTransform.position);

        // �Ÿ��� ���� �ؽ�Ʈ ��Ʈ ũ�� ����
        float adjustedSize = Mathf.Max(minFontSize, distance * scaleFactor);
        tMP.fontSize = adjustedSize;

        // �Ÿ��� ���� ������Ʈ�� localScale ����
        float scale = adjustedSize / 10; // ���� ��� ���� ũ�⸦ 10���� �����ϰ� �����մϴ�
        transform.localScale = new Vector3(scale, scale, 1);
    }
}

