using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Transform[] childs = null;
    // Start is called before the first frame update
    void Start()
    {
        childs = new Transform[transform.childCount];
        for(int i = 0; i <transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i< childs.Length;i++)
        {
            if (childs[i] != null)
            childs[i].rotation = Camera.main.transform.rotation;
        }
    }
}
