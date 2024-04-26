using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public bool[] levelsUnlocked;  // 关卡是否解锁的数组

    public Button[] levelButtons;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 确保管理器在加载新场景时不被销毁
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

    // 初始化关卡解锁状态
    void InitializeLevels()
    {
        levelsUnlocked = new bool[5];  // 假设有5个关卡
        levelsUnlocked[0] = true;  // 第一个关卡默认解锁
        for (int i = 1; i < levelsUnlocked.Length; i++)
        {
            levelsUnlocked[i] = false;  // 其他关卡默认未解锁
        }
    }

    // 解锁特定关卡
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
