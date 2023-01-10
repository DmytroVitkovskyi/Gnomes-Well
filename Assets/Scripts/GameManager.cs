using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // ��������������, ��� ������ ��������� ������.
    public GameObject startingPoint;
    // ������ �������, ���������� � ����������� �������.
    public Rope rope;
    // ��������, ����������� �������, ������� ������ ��������� �� ��������
    public CameraFollow cameraFollow;
    // '�������' ������ (� ����������������� ���� ��������)
    Gnome currentGnome;
    // ������-������ ��� �������� ������ �������
    public GameObject gnomePrefab;
    // ��������� ����������������� ���������� � ��������
    // '������������� � '����������'
    public RectTransform mainMenu;

    // ��������� ����������������� ���������� � ��������
    // '�����', '����' � '����'
    public RectTransform gameplayMenu;
    // ��������� ����������������� ���������� � �������
    // '�� ��������!'
    public RectTransform gameOverMenu;
    // �������� true � ���� �������� ������� ������������ ����� �����������
    // (�� ���������� ���������� �������).
    // ���������� 'get; set;' ���������� ���� � ��������, ���
    // ���������� ��� ����������� � ������ ������� � ����������
    // ��� Unity Events
    public bool gnomeInvincible { get; set; }
    // �������� ����� ��������� ������ ������� ����� ������
    public float delayAfterDeath = 8.0f;
    // ����, ������������� � ������ ������ �������
    public AudioClip gnomeDiedSound;
    // ����, ������������� � ������ ������ � ����
    public AudioClip gameOverSound;
    void Start()
    {
        // � ������ ������� ���� ������� Reset, �����
        // ����������� �������.
        Reset();
    }
    // ���������� ���� � �������� ���������.
    public void Reset()
    {
        if (Camera.main.transform.Find("Gnome") != null)
        {
            Invoke("Reset", 1.0f);
            return;
        }
        // ��������� ����, �������� ��������� ����
        if (gameOverMenu)
            gameOverMenu.gameObject.SetActive(false);
        if (mainMenu)
            mainMenu.gameObject.SetActive(false);
        if (gameplayMenu)
            gameplayMenu.gameObject.SetActive(true);
        // ����� ��� ���������� Resettable � �������� �� � �������� ���������
        var resetObjects = FindObjectsOfType<Resettable>();
        foreach (Resettable r in resetObjects)
        {
            r.Reset();
        }
        // ������� ������ �������
        CreateNewGnome();
        // �������� ����� � ����
        Time.timeScale = 1.0f;
    }

    void CreateNewGnome()
    {
        // ������� �������� �������, ���� �������
        RemoveGnome();
        // ������� ����� ������ Gnome � ��������� ��� �������
        GameObject newGnome = (GameObject)Instantiate(gnomePrefab,
        startingPoint.transform.position, Quaternion.identity);

        currentGnome = newGnome.GetComponent<Gnome>();
        // �������� �������
        rope.gameObject.SetActive(true);
        // ��������� ����� ������� � ���������
        // �������� ���� � ������� Gnome (��������, � ��� ����)
        rope.connectedObject = currentGnome.ropeBody;
        // ���������� ����� ������� � ��������� ��������
        rope.ResetLength();
        // �������� ������� cameraFollow, ��� �� ������
        // ������ ������� �� ����� �������� Gnome
        cameraFollow.target = currentGnome.cameraFollowTarget;
    }

    void RemoveGnome()
    {
        // ������ �� ������, ���� ������ ��������
        if (gnomeInvincible)
            return;
        // ������ �������
        rope.gameObject.SetActive(false);
        // ��������� ������ ��������� �� ��������
        cameraFollow.target = null;
        // ���� ������� ������ ����������, ��������� ��� �� ����
        if (currentGnome != null)
        {
            // ���� ������ ������ �� ���������� ���������
            currentGnome.holdingTreasure = false;
            // �������� ������ ��� ����������� �� ����
            // (����� ���������� ��������� �������� � ������������� � ���)
            currentGnome.gameObject.tag = "Untagged";
            // ����� ��� ������� � ����� "Player" � ������� ���� ���
            foreach (Transform child in currentGnome.transform)
            {
                child.gameObject.tag = "Untagged";
            }
            // ���������� ������� ���������� �������� �������
            currentGnome = null;
        }
    }
    // ������� �������.
    void KillGnome(Gnome.DamageType damageType)
    {
        if (currentGnome == null || currentGnome.IsDead)
        {
            return;
        }
        // ���� ����� �������� �����, ��������� ���� "������ �������"
        var audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.PlayOneShot(this.gnomeDiedSound);
        }
        // �������� ������ �������� �������
        currentGnome.ShowDamageEffect(damageType);
        // ���� ������ ������, �������� ����
        // � ��������� ������� �� ����.
        if (gnomeInvincible == false)
        {
            // �������� �������, ��� �� �����
            currentGnome.DestroyGnome(damageType);
            // ������� �������
            RemoveGnome();
            // �������� ����
            StartCoroutine(ResetAfterDelay());
        }
    }
    // ���������� � ������ ������ �������.
    IEnumerator ResetAfterDelay()
    {
        // ����� delayAfterDeath ������, ����� ������� Reset
        yield return new WaitForSeconds(delayAfterDeath);
        Reset();
    }
    // ����������, ����� ������ �������� �������
    // � ������
    public void TrapTouched()
    {
        KillGnome(Gnome.DamageType.Slicing);
    }
    // ����������, ����� ������ �������� �������� �������
    public void FireTrapTouched()
    {
        KillGnome(Gnome.DamageType.Burning);
    }
    // ����������, ����� ������ �������� ���������.
    public void TreasureCollected()
    {
        if (currentGnome == null || currentGnome.IsDead)
        {
            return;
        }
        // �������� �������� �������, ��� �� ���� ���������.
        currentGnome.holdingTreasure = true;
    }

    // ����������, ����� ������ �������� ������.
    public void ExitReached()
    {
        // ��������� ����, ���� ���� ������ � �� ������ ���������!
        if (currentGnome != null && currentGnome.holdingTreasure == true)
        {
            // ���� ����� �������� �����, ��������� ����
            // "���� ���������"
            var audio = GetComponent<AudioSource>();
            if (audio)
            {
                audio.PlayOneShot(this.gameOverSound);
            }
            // ������������� ����
            Time.timeScale = 0.0f;
            // ��������� ���� ���������� ���� � �������� �����
            // "���� ���������"!
            if (gameOverMenu)
            {
                gameOverMenu.gameObject.SetActive(true);
            }
            if (gameplayMenu)
            {
                gameplayMenu.gameObject.SetActive(false);
            }
        }
    }
    // ���������� � ����� �� ������� ������ Menu � Resume Game.
    public void SetPaused(bool paused)
    {
        // ���� ���� �� �����, ���������� ����� � �������� ����
        // (� ��������� ��������� ����)
        if (paused)
        {
            Time.timeScale = 0.0f;
            mainMenu.gameObject.SetActive(true);
            gameplayMenu.gameObject.SetActive(false);
        }
        else
        {
            // ���� ���� �� �� �����, ����������� ��� ������� �
            // ��������� ���� (� �������� ��������� ����)
            Time.timeScale = 1.0f;
            mainMenu.gameObject.SetActive(false);
            gameplayMenu.gameObject.SetActive(true);
        }
    }
    // ���������� � ����� �� ������� ������ Restart.
    public void RestartGame()
    {
        // ���������� ������� ������� (����� ���� ������)
        Destroy(currentGnome.gameObject);
        currentGnome = null;
        // �������� ���� � �������� ���������, ����� ������� ������ �������.
        Reset();
    }
}
