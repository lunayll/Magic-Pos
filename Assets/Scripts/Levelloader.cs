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
        // 获取触发事件的按钮的名称
        string sceneName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("Loading scene: " + sceneName);

        // 加载与按钮名称相同的场景
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
