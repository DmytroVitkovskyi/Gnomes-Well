using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// �������� ���� UnityEvent, ������� ������������ ���
// ��������� ������� this � �������� ���������.
public class Resettable : MonoBehaviour
{
    // � ��������� ���������� ��� ������� � �������, ������� ������
    // ���������� � ������ ������ ����.
    public UnityEvent onReset;
    // ���������� ����������� ���� GameManager � ������ ������ ����.
    public void Reset()
    {
        // �������� �������, ������� ������� ���
        // ������������ ������.
        onReset.Invoke();
    }
}
