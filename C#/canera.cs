using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canera : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;
    bool blapproach = false;
    Rigidbody TargetPos;
    void Update()
    {
        if (!blapproach)
        {
            UpDown();
            LeftRight();
        }
        else
        {
            Approach(TargetPos);
        }
        if (Input.anyKeyDown)
        {
            blapproach = false;
        }
        //카메라 회전

    }

    //상하 이동
    public void Approach(Rigidbody TargetPos)
    {
        if (TargetPos == null)
        {
            blapproach = false;
            return;
        }
        if ((TargetPos.transform.position - transform.position).magnitude > 10)
        {
            blapproach = true;
        }
        else
        {
            blapproach = false;
        }
       
        // 목표 방향 벡터 계산
        this.TargetPos = TargetPos;
        Vector3 targetDirection = TargetPos.transform.position - transform.position;
        targetDirection.y = 0; // 선택적으로 Y 축 회전 제한 (수평 회전만 고려)

        // 목표 방향의 회전각을 구함
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // 부드러운 회전을 위해 Quaternion.Slerp 사용
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);

        // 이동 코드
        transform.position = Vector3.Lerp(transform.position,  TargetPos.transform.position, Time.deltaTime * 10f);
    }
    void UpDown()
    {
        
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += new Vector3(0, -1, 0);
        }
    }
    

    //앞뒤, 좌우 이동
    void LeftRight()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetButton("Horizontal"))
            {
                transform.position += transform.right * Input.GetAxis("Horizontal");
            }
            if (Input.GetButton("Vertical"))
            {
                transform.position += transform.forward * Input.GetAxis("Vertical") ;
            }
        }
        else
        {
            if (Input.GetButton("Horizontal"))
            {
                transform.position += transform.right * Input.GetAxis("Horizontal") * 0.1f;
            }
            if (Input.GetButton("Vertical"))
            {
                transform.position += transform.forward * Input.GetAxis("Vertical") * 0.1f;
            }
        }
        
    }
}
