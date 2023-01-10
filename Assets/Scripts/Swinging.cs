using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���������� ��������� ����� ��� ���������� ������� ����
// � �������. ������������ ��� ���������� ������� � �������.
public class Swinging : MonoBehaviour
{
    // ��������� ������� ������ ���� ����������?
    // ������ ����� = ������ ����������
    public float swingSensitivity = 100.0f;
    
    // ������ Update ������������ FixedUpdate, �����
    // ��������� ������ � ���������� �������
    private void FixedUpdate()
    {
        // ���� ������� ���� ����������� (���), �������
        // ���� ���������
        if (GetComponent<Rigidbody2D>() == null)
        {
            Destroy(this);
            return;
        }

        // �������� �������� ������� �� InputManager
        float swing = InputManager.instance.sidewaysMotion;

        // ��������� ����������� ����
        Vector2 force = new Vector2(swing * swingSensitivity, 0);

        // ��������� ����
        GetComponent<Rigidbody2D>().AddForce(force);
    }
}
