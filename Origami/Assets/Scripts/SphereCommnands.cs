using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCommnands : MonoBehaviour {
    //OnSelect()由GazeGestureManager调用，当focus在此game object上时，则利用SendMessageUpwards发送OnSelect消息
    void OnSelect () {
        //如果sphere没有Rigidbody组件，则为其添加一个，从而可以启动物理仿真
        if (!this.GetComponent<Rigidbody> ()) {
            var rigidbody = this.gameObject.AddComponent<Rigidbody> ();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }
}