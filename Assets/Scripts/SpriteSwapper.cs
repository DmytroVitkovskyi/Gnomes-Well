using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���� ������ �� ������. ��������, ��� ������������ ���������
// �� ��������� '��������� ����' � ��������� '��������� ���'.
public class SpriteSwapper : MonoBehaviour
{
    // ������, ������� ��������� ����������.
    public Sprite spriteToUse;
    // ������������ �������, ������� ������ ��������������
    // ��� ����������� ������ �������.
    public SpriteRenderer spriteRenderer;
    // �������� ������. ������������ � ������ ResetSprite.
    private Sprite originalSprite;

    // ������ ������.
    public void SwapSprite()
    {
        // ���� ��������� ������ ���������� �� ��������...
        if (spriteToUse != spriteRenderer.sprite)
        {
            // ��������� ���������� � originalSprite
            originalSprite = spriteRenderer.sprite;
            // �������� ����� ������ �������������.
            spriteRenderer.sprite = spriteToUse;
        }
    }
    // ���������� ������� ������.
    public void ResetSprite()
    {
        // ���� ������� ������ ��� ��������...
        if (originalSprite != null)
        {
            // ...�������� ��� �������������.
            spriteRenderer.sprite = originalSprite;
        }
    }
}
