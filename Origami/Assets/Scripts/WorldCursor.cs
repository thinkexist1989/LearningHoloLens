using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCursor : MonoBehaviour {
    private MeshRenderer meshRenderer;

    //Start()在第一帧调用Update()前调用，用来初始化
    void Start () {
        //获取此脚本对应的物体上的网格渲染器（mesh renderer），此处的Object应该是Cursor
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer> ();
    }

    // 每一帧都要调用Update()
    void Update () {
        //根据使用者头部的方位和姿态，做一个光线投射(raycast)
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast (headPosition, gazeDirection, out hitInfo)) {
            //如果raycast落到了一个hologram上，则显示Cursor的mesh
            meshRenderer.enabled = true;
            //将Cursor移动到raycast的落点
            this.transform.position = hitInfo.point;
            //旋转Cursor使其紧贴hologram的表面
            this.transform.rotation = Quaternion.FromToRotation (Vector3.up, hitInfo.normal);
        } else {
            //如果raycast未落到hologram上，则不显示Cursor的mesh
            meshRenderer.enabled = false;
        }
    }
}