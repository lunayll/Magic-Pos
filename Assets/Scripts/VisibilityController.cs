using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;


public class VisibilityController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] hiddenObjects;  // Ҫ��ʾ����������
    public float visibilityDuration = 10.0f;  // �ɼ�����ʱ��
    public float cooldownDuration = 15.0f; // ��ȴ����ʱ��
    public TextMeshProUGUI cooldownText; // ��ȴʱ���ı�UI
    public TextMeshProUGUI visibilityText;
    private bool isCooldown = false; // ��ȴ״̬��־

    private float visibilityTimeLeft;
    private float cooldownTimeLeft;


    public PostProcessVolume postProcessVolume;  // �����������
    private ColorGrading colorGrading;
    private ChromaticAberration chromaticAberration;
    void Start()
    {
        SetVisibility(false);
        // ��ȡɫ���������
        postProcessVolume.profile.TryGetSettings(out colorGrading);
        colorGrading.active = false;
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        chromaticAberration.active = false;
        // Ĭ�Ͻ���ɫ������
   

    }

    // Update is called once per frame
    void Update()
    {
        // ����F���İ���
        if (Input.GetKeyDown(KeyCode.F) && !isCooldown)
        {
            isCooldown = true; // ������ȴ
            // ��ʾ��������
            SetVisibility(true);
            // ��ʼ��ʱ��ʱ���������������
            colorGrading.active = true;  
            chromaticAberration.active = true;
            // ����ɫ����������ʾ��ɫ�˾�
            //��ȴUI��ʼ��
            visibilityTimeLeft = visibilityDuration;
            cooldownTimeLeft = cooldownDuration;
            visibilityText.gameObject.SetActive(true);
            cooldownText.gameObject.SetActive(true);
            StartCoroutine(HideAfterDelay(visibilityDuration));
            StartCoroutine(Cooldown());
        }
        // ����ʣ��Ŀ��ӳ���ʱ��
        if (visibilityTimeLeft > 0)
        {
            visibilityTimeLeft -= Time.deltaTime;
            visibilityText.text = "Visibility: " + Mathf.Max(visibilityTimeLeft, 0).ToString("F2") + "s";
        }

        // ������ȴʱ��
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
            obj.SetActive(isVisible);  // ��������ļ���״̬
        }
    }

    IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetVisibility(false);  // ʱ���������������
        visibilityText.gameObject.SetActive(false);//���ÿ���ʱ��
        colorGrading.active = false;
        chromaticAberration.active = false;// ����ɫ���������Ƴ���ɫ�˾�
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
        cooldownText.gameObject.SetActive(false);
    }

}
