using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;


public class VisibilityController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] hiddenObjects;  // 要显示的物体数组
    public float visibilityDuration = 10.0f;  // 可见持续时间
    public float cooldownDuration = 15.0f; // 冷却持续时间
    public TextMeshProUGUI cooldownText; // 冷却时间文本UI
    public TextMeshProUGUI visibilityText;
    private bool isCooldown = false; // 冷却状态标志

    private float visibilityTimeLeft;
    private float cooldownTimeLeft;


    public PostProcessVolume postProcessVolume;  // 后处理音量组件
    private ColorGrading colorGrading;
    private ChromaticAberration chromaticAberration;
    void Start()
    {
        SetVisibility(false);
        // 获取色调调整组件
        postProcessVolume.profile.TryGetSettings(out colorGrading);
        colorGrading.active = false;
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        chromaticAberration.active = false;
        // 默认禁用色调调整
   

    }

    // Update is called once per frame
    void Update()
    {
        // 监听F键的按下
        if (Input.GetKeyDown(KeyCode.F) && !isCooldown)
        {
            isCooldown = true; // 开启冷却
            // 显示所有物体
            SetVisibility(true);
            // 开始计时，时间结束后隐藏物体
            colorGrading.active = true;  
            chromaticAberration.active = true;
            // 启用色调调整，显示紫色滤镜
            //冷却UI初始化
            visibilityTimeLeft = visibilityDuration;
            cooldownTimeLeft = cooldownDuration;
            visibilityText.gameObject.SetActive(true);
            cooldownText.gameObject.SetActive(true);
            StartCoroutine(HideAfterDelay(visibilityDuration));
            StartCoroutine(Cooldown());
        }
        // 更新剩余的可视持续时间
        if (visibilityTimeLeft > 0)
        {
            visibilityTimeLeft -= Time.deltaTime;
            visibilityText.text = "Visibility: " + Mathf.Max(visibilityTimeLeft, 0).ToString("F2") + "s";
        }

        // 更新冷却时间
        if (cooldownTimeLeft > 0)
        {
            cooldownTimeLeft -= Time.deltaTime;
            cooldownText.text = "Cooldown: " + Mathf.Max(cooldownTimeLeft, 0).ToString("F2") + "s";
        }
    }
    void SetVisibility(bool isVisible)
    {
        foreach (var obj in hiddenObjects)
        {
            obj.SetActive(isVisible);  // 设置物体的激活状态
        }
    }

    IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetVisibility(false);  // 时间结束后隐藏物体
        visibilityText.gameObject.SetActive(false);//重置可视时间
        colorGrading.active = false;
        chromaticAberration.active = false;// 禁用色调调整，移除紫色滤镜
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
        cooldownText.gameObject.SetActive(false);
    }

}
