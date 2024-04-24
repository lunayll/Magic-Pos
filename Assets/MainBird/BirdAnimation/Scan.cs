using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Animator>().SetBool("Scan", true);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            GetComponent<Animator>().SetBool("Scan", false);
        }
    }
}
