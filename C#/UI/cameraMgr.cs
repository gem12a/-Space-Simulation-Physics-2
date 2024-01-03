using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMgr : MonoBehaviour
{
    // 카메라 참조
    public static cameraMgr Instance;
    private void Awake()
    {
        Instance = this;

    }
    public Camera mainCamera;
    public Camera secondaryCamera;

    void Start()
    {
        // 시작 시 주 카메라 활성화, 보조 카메라 비활성화
        mainCamera.enabled = true;
        secondaryCamera.enabled = false;
        mainCamera.GetComponent<AudioListener>().enabled = mainCamera.enabled;
        secondaryCamera.GetComponent<AudioListener>().enabled = secondaryCamera.enabled;
    }

    public void SwitchCamera()
    {
        // 카메라 활성화 상태 전환
        mainCamera.enabled = !mainCamera.enabled;
        secondaryCamera.enabled = !secondaryCamera.enabled;

        // 오디오 리스너도 함께 전환
        mainCamera.GetComponent<AudioListener>().enabled = mainCamera.enabled;
        secondaryCamera.GetComponent<AudioListener>().enabled = secondaryCamera.enabled;
    }

}
