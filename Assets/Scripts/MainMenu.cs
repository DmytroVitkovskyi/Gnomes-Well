using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ��������� ������� ����.
public class MainMenu : MonoBehaviour
{
    // ��� �����, ���������� ���� ����.
    public string sceneToLoad;
    // ��������� ����������������� ����������,
    // ���������� ����� "Loading...".
    public RectTransform loadingOverlay;
    // ��������� �������� ����� � ������� ������. ������������
    // ��� ����������, ����� ��������� ����������� �����.
    AsyncOperation sceneLoadingOperation;
    // ��� ������� �������� �������� ����.
    void Start()
    {
        // ������ �������� 'loading'
        loadingOverlay.gameObject.SetActive(false);
        // ������ �������� ����� � ������� ������...
        sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        // ...�� �� ������������� � ����� �����,
        // ���� �� �� ����� ������.
        sceneLoadingOperation.allowSceneActivation = false;
    }

    // ���������� � ����� �� ������� ������ New Game.
    public void LoadScene()
    {
        // ������� �������� 'Loading' �������
        loadingOverlay.gameObject.SetActive(true);
        // �������� �������� �������� �����, ��� ���������
        // ����������� ����� �� ��������� ��������.
        sceneLoadingOperation.allowSceneActivation = true;
    }
}
