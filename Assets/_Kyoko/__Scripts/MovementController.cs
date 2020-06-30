using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public static float currentDir;
    private bool isLeftDown, isRightDown;

    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR 
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            isLeftDown = true;
            isRightDown = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            isLeftDown = false;
            isRightDown = true;
        }
        else
        {
            isLeftDown = false;
            isRightDown = false;
        }
#endif
        if (isLeftDown)
        {
            currentDir = -1;
        }
        else if (isRightDown)
        {
            currentDir = 1;
        }
        else
        {
            currentDir = 0;
        }
    }

    public void OnLeftDown()
    {
        isLeftDown = true;
    }
    public void OnLeftUp()
    {
        isLeftDown = false;
    }

    public void OnRightDown()
    {
        isRightDown = true;
    }
    public void OnRightUp()
    {
        isRightDown = false;
    }
}
