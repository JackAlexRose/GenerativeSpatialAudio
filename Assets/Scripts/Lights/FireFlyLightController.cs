using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyLightController : MonoBehaviour
{
    private Light glow;
    private float glowIntensity;
    private bool up = true;

    // Start is called before the first frame update
    void Start()
    {
        glow = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Light>();
        glowIntensity = Random.Range(0.1f, 0.3f);

        if(glowIntensity > 0.2f){
            up = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(up){
            glowIntensity += 0.1f * Time.deltaTime;

            if(glowIntensity >= 0.3f){
                up = false;
            }
        }
        else{
            glowIntensity -= 0.1f * Time.deltaTime;

            if(glowIntensity <= 0.1f){
                up = true;
            }
        }

        glow.intensity = glowIntensity;

    }
}
