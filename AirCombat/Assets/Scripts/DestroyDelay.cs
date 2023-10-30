using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField] float delayTime = 1.0f;
    void Start()
    {
        Invoke(nameof(Destroy), delayTime);
    }
    private void Destroy()
    {
        Destroy(this.gameObject);
    }

}
