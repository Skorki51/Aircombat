using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLimit : MonoBehaviour
{
    public int frameLimit = 60;
    void Start()
    {
        Application.targetFrameRate = frameLimit;
    }
}
