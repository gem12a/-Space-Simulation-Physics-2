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
        //ī�޶� ȸ��

    }

    //���� �̵�
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
       
        // ��ǥ ���� ���� ���
        this.TargetPos = TargetPos;
        Vector3 targetDirection = TargetPos.transform.position - transform.position;
        targetDirection.y = 0; // ���������� Y �� ȸ�� ���� (���� ȸ���� ���)

        // ��ǥ ������ ȸ������ ����
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // �ε巯�� ȸ���� ���� Quaternion.Slerp ���
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);

        // �̵� �ڵ�
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
    

    //�յ�, �¿� �̵�
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
