using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����������� ������, ���������� �� �������������,
// � ���������� � ������� ��������.
public class InputManager : Singleton<InputManager>
{
    // �������� ��������. -1.0 = ����������� �����,
    // +1.0 = ����������� ������
    private float _sidewaysMotion = 0.0f;

    // ��� �������� �������� ������ ��� ������, �������
    // ������ �������� �� ������ �������� ���.
    public float sidewaysMotion { get => _sidewaysMotion; }

    // �������� ���������� ����������� � ������ �����
    void Update()
    {
        Vector3 accel = Input.acceleration;

        _sidewaysMotion = accel.x;
    }
}
