using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCommnands : MonoBehaviour {
    Vector3 originalPosition;

    //Use this for initialization
    void Start () {
        //在这个程序刚开始执行时记录Sphere原始位置
        originalPosition = this.transform.localPosition;
    }

    //OnSelect()由GazeGestureManager调用，当focus在此game object上时，则利用SendMessageUpwards发送OnSelect消息
    void OnSelect () {
        //如果sphere没有Rigidbody组件，则为其添加一个，从而可以启动物理仿真
        if (!this.GetComponent<Rigidbody> ()) {
            var rigidbody = this.gameObject.AddComponent<Rigidbody> ();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    //OnReset方法，当"Reset World"指令发出时
    void OnReset () {
        var rigidbody = this.GetComponent<Rigidbody> ();
        if (rigidbody != null) {
            rigidbody.isKinematic = true;
            Destroy (rigidbody);
        }
        //将Sphere恢复原始位置
        this.transform.localPosition = originalPosition;
    }

    //OnDrop方法，当"Drop Sphere"指令发出时
    void OnDrop () {
        //调用OnSelect()函数，和Gesture一样
        OnSelect ();
    }
}