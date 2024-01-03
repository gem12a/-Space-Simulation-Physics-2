using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMgr : MonoBehaviour
{
    // ī�޶� ����
    public static cameraMgr Instance;
    private void Awake()
    {
        Instance = this;

    }
    public Camera mainCamera;
    public Camera secondaryCamera;

    void Start()
    {
        // ���� �� �� ī�޶� Ȱ��ȭ, ���� ī�޶� ��Ȱ��ȭ
        mainCamera.enabled = true;
        secondaryCamera.enabled = false;
        mainCamera.GetComponent<AudioListener>().enabled = mainCamera.enabled;
        secondaryCamera.GetComponent<AudioListener>().enabled = secondaryCamera.enabled;
    }

    public void SwitchCamera()
    {
        // ī�޶� Ȱ��ȭ ���� ��ȯ
        mainCamera.enabled = !mainCamera.enabled;
        secondaryCamera.enabled = !secondaryCamera.enabled;

        // ����� �����ʵ� �Բ� ��ȯ
        mainCamera.GetComponent<AudioListener>().enabled = mainCamera.enabled;
        secondaryCamera.GetComponent<AudioListener>().enabled = secondaryCamera.enabled;
    }

}
