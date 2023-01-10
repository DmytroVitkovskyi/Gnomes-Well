using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ����� ��������� ������ �������� ��������� �� ������������
// ����� ������. ������������ � ������� GameManager � InputManager.

// ����� ��������������� ��, ����������� ���, ��������:
// public class MyManager : Singleton<MyManager> { }

// ����� ����� �������� ����������� ���������� � �������������
// ������ ���������� ������, ��������, ���:
// MyManager.instance.DoSomething();


public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    // ������������ ��������� ������ 
    private static T _instance;

    // ����� �������. � ������ ������ �������� �������� _instance.
    // ���� ��������� ������ �� ������, ������� ��������� �� ������.
    public static T instance
    {
        get
        {
            // ���� �������� _instance ��� �� ��������� ...
            if (_instance == null)
            {
                // ���������� ����� ������.
                _instance = FindObjectOfType<T>();

                // ������� �������� � ������ �������.
                if (_instance == null)
                {
                    Debug.LogError("Can't find " + typeof(T) + "!");
                }
            }
            // ������� ��������� ��� �������������!
            return _instance;
        }
    }
    
}
