using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterEffect : MonoBehaviour
{
    private bool defaultFog = false;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private float defaultFogEndDistance;
    private Material defaultSkybox;
    private bool underwaterTrigger = false;
    private bool effectApplied = false;
    private float waterLevel = 0;
    private Camera playerCam;

    public Color waterColor;
    public float waterDensity;

    void Start()
    {
        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        defaultFogEndDistance = RenderSettings.fogEndDistance;
        defaultSkybox = RenderSettings.skybox;

        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null) {
            playerCam = playerObject.transform.Find("Camera").GetComponent<Camera>();
        }

    }

    private void Update()
    {
        if (!effectApplied && underwaterTrigger && (playerCam.transform.position.y < waterLevel))
        {
            effectApplied = true;

            RenderSettings.fog = true;
            RenderSettings.fogColor = waterColor;
            RenderSettings.fogDensity = waterDensity;
            RenderSettings.fogEndDistance = 10;
            RenderSettings.skybox = null;
        }
        else if (effectApplied && (playerCam.transform.position.y >= waterLevel)) {
            effectApplied = false;

            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
            RenderSettings.fogEndDistance = defaultFogEndDistance;
            RenderSettings.skybox = defaultSkybox;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") {
            underwaterTrigger = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            underwaterTrigger = false;
            effectApplied = false;
        }
    }

}
