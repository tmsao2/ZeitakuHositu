using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSky : MonoBehaviour
{
    [SerializeField]
    float speed;

    private Material skyMaterial;
    void Start()
    {
        skyMaterial = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        skyMaterial.SetFloat("_Rotation", Mathf.Repeat(skyMaterial.GetFloat("_Rotation") + speed * Time.deltaTime, 360f));
    }
}
