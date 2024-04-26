using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public bool[] levelsUnlocked;  // �ؿ��Ƿ����������

    public Button[] levelButtons;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // ȷ���������ڼ����³���ʱ��������
            InitializeLevels();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        LoadLevels();

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = LevelManager.Instance.levelsUnlocked[i];
        }
    }

    // ��ʼ���ؿ�����״̬
    void InitializeLevels()
    {
        levelsUnlocked = new bool[5];  // ������5���ؿ�
        levelsUnlocked[0] = true;  // ��һ���ؿ�Ĭ�Ͻ���
        for (int i = 1; i < levelsUnlocked.Length; i++)
        {
            levelsUnlocked[i] = false;  // �����ؿ�Ĭ��δ����
        }
    }

    // �����ض��ؿ�
    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex < levelsUnlocked.Length)
        {
            levelsUnlocked[levelIndex] = true;
        }
        SaveLevels();
    }

    void SaveLevels()
    {
        for (int i = 0; i < levelsUnlocked.Length; i++)
        {
            PlayerPrefs.SetInt("LevelUnlocked" + i, levelsUnlocked[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    void LoadLevels()
    {
        for (int i = 0; i < levelsUnlocked.Length; i++)
        {
            levelsUnlocked[i] = PlayerPrefs.GetInt("LevelUnlocked" + i, 0) == 1;
        }
    }

    public void UpdateLevelSelectMenu()
    {
        for (int i = 0; i < levelsUnlocked.Length; i++)
        {
            levelButtons[i].interactable = levelsUnlocked[i];
        }
    }

}
