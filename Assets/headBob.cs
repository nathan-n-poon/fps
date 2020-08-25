using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour
{
    public Camera m_Camera;
    float deltaY;
    float totalTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        deltaY = GameObject.FindObjectOfType<characterMove>().getWalkingSpeed().magnitude * Mathf.Sin(totalTime*5) / 100;
        transform.localPosition = new Vector3(0, 0.5f + deltaY, 0);
        if(totalTime > float.MaxValue - 1000)
        {
            totalTime = 0;
        }
        Debug.Log(transform.localPosition.y - 0.5);
    }
}
