using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CamScript : MonoBehaviour
{
    public UnityEvent onExit;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 4)
            onExit.Invoke();

    }
}
