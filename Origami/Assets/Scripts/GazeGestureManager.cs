using UnityEngine;
using UnityEngine.XR.WSA.Input; //Windows-specific APIs，可以得到手势输入的详细信息

public class GazeGestureManager : MonoBehaviour {
    public static GazeGestureManager Instance { get; private set; } //这句话没看懂什么意思，为何在开始定义一个类的句柄，而且是静态类。感觉可能以后应该有的其他脚本会调用这个句柄

    //展示正在注视的全息影像
    public GameObject FocusedObject { get; private set; } // Auto-implemented properties for trivial get and set

    GestureRecognizer recognizer; //手势识别对象

    void Awake () //在GazeGestureManager创建之后便执行
    {
        Instance = this;

        //创建一个GestureRecognizer来检测选择的手势
        recognizer = new GestureRecognizer ();
        recognizer.Tapped += (args) => {
            //若注视的对象存在，则向其所有ancestor发送消息
            if (FocusedObject != null) {
                FocusedObject.SendMessageUpwards ("OnSelect", SendMessageOptions.DontRequireReceiver);
            }
        };
        recognizer.StartCapturingGestures ();
    }

    // Update is called once per frame
    void Update () {
        //指出哪个全息影像被focused
        GameObject oldFocusedObject = FocusedObject;
        //根据使用者头部的方位和姿态，做一个光线投射(raycast)
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast (headPosition, gazeDirection, out hitInfo)) {
            //若光线投射到一个全息影像
            FocusedObject = hitInfo.collider.gameObject;
        } else {
            FocusedObject = null;
        }

        //若focused object在这一帧改变，则重新开始检测手势
        if (FocusedObject != oldFocusedObject) {
            recognizer.CancelGestures ();
            recognizer.StartCapturingGestures ();
        }
    }
}