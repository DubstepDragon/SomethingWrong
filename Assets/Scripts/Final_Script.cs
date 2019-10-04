using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final_Script : MonoBehaviour
{
    private float total_time = 0.0f;
    private float alpha = 0.0f;
    public GlitchEffect Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        total_time += Time.deltaTime;
        alpha += Time.deltaTime / 4;
        Debug.Log(alpha);
        if (alpha < 1.0f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, alpha);
        }

        if(alpha > 1.0f && alpha < 2.0f)
        {
            Camera.reg = false;
            Camera.intensity = 1.0f;
            Camera.colorIntensity = 1.0f;
            Camera.flipIntensity = 1.0f;
        }
        else
        {
            Camera.reg = true;
            Camera.intensity = 0.0f;
            Camera.colorIntensity = 0.0f;
            Camera.flipIntensity = 0.0f;
        }
    }
}
