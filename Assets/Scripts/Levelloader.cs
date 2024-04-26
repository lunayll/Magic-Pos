using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levelloader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;
    public void LoadSceneBasedOnButtonName()
    {
        // ��ȡ�����¼��İ�ť������
        string sceneName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("Loading scene: " + sceneName);

        // �����밴ť������ͬ�ĳ���
        StartCoroutine(SceneTransition(sceneName));
    }

    IEnumerator SceneTransition(string sceneName)
    {
        //paly animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //Load scene
        SceneManager.LoadScene(sceneName);
    }
}
