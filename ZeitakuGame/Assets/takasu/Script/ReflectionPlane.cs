﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionPlane : MonoBehaviour
{
    private Transform trfMainCamera, trfRefCamera;
    private RenderTexture refTexture;
    private GameObject objRefCamera;
    private Camera mainCamera, refCamera;
    private Material matRefPalne;

    // Use this for initialization
    void Start()
    {
        refTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);

        mainCamera = Camera.main;
        trfMainCamera = Camera.main.transform;

        objRefCamera = new GameObject();
        objRefCamera.name = "Reflection Camera";
        refCamera = objRefCamera.AddComponent<Camera>();
        refCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("Mirror"));

        trfRefCamera = objRefCamera.transform;
        refCamera.targetTexture = refTexture;

        matRefPalne = gameObject.GetComponent<Renderer>().sharedMaterial;
        matRefPalne.SetTexture("_ReflectionTex", refTexture);
    }

    private void SetReflectionCamera()
    {
        Vector3 normal = transform.up;
        Vector3 pos = transform.position;
        Matrix4x4 mainCamMatrix = mainCamera.worldToCameraMatrix;

        float d = -Vector3.Dot(normal, pos);
        Matrix4x4 refMatrix = CalcReflectionMatrix(new Vector4(normal.x, normal.y, normal.z, d));
        refCamera.worldToCameraMatrix = mainCamera.worldToCameraMatrix * refMatrix;
        refCamera.projectionMatrix = mainCamera.projectionMatrix;
    }

    private Matrix4x4 CalcReflectionMatrix(Vector4 n)
    {
        Matrix4x4 refMatrix = new Matrix4x4();

        refMatrix.m00 = 1f - 2f * n.x * n.x;
        refMatrix.m01 = -2f * n.x * n.y;
        refMatrix.m02 = -2f * n.x * n.z;
        refMatrix.m03 = -2f * n.x * n.w;

        refMatrix.m10 = -2f * n.x * n.y;
        refMatrix.m11 = 1f - 2f * n.y * n.y;
        refMatrix.m12 = -2f * n.y * n.z;
        refMatrix.m13 = -2f * n.y * n.w;

        refMatrix.m20 = -2f * n.x * n.z;
        refMatrix.m21 = -2f * n.y * n.z;
        refMatrix.m22 = 1f - 2f * n.z * n.z;
        refMatrix.m23 = -2f * n.z * n.w;

        refMatrix.m30 = 0F;
        refMatrix.m31 = 0F;
        refMatrix.m32 = 0F;
        refMatrix.m33 = 1F;

        return refMatrix;
    }

    void OnWillRenderObject()
    {
        SetReflectionCamera();
        GL.invertCulling = true;
        refCamera.Render();
        GL.invertCulling = false;
        matRefPalne.SetTexture("_ReflectionTex", refTexture);
    }
}
