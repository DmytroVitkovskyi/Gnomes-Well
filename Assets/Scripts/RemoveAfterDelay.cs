using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� ������ � �������� ���������.
public class RemoveAfterDelay : MonoBehaviour
{
    // �������� � �������� ����� ���������.
    public float delay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        // ��������� ����������� 'Remove'.
        StartCoroutine("Remove");
    }

    IEnumerator Remove()
    {
        // ����� 'delay' ������ � ����� ���������� ������
        // gameObject, �������������� � ������� this.
        yield return new WaitForSeconds(delay);

        // var body = gameObject.transform.Find("Body");
        //if (body != null)
        //{
        //     print("Body fined!");
        //}
        Destroy(gameObject);

        // ������ ������������ ����� Destroy(this) - �� ��������� ���
        // ������ �������� RemoveAfterDelay.
    }
}
