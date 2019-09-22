using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest : MonoBehaviour
{
    Quaternion rotation = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        //输出当前场景中this指向的游戏对象Transform组件中的Position, Rotation, Scale的值
        print(string.Format("当前游戏对象{0}的postion={1}", gameObject, transform.position)); //全局坐标
        print(string.Format("当前游戏对象{0}的localPosition={1}", gameObject, transform.localPosition)); //相对parent object的坐标

        print(string.Format("当前游戏对象{0}的eulerAngles={1}", gameObject, transform.eulerAngles)); //全局欧拉角
        print(string.Format("当前游戏对象{0}的localEulerAngles={1}", gameObject, transform.localEulerAngles)); //相对parent object的欧拉角

        print(string.Format("当前游戏对象{0}的localScale={1}", gameObject, transform.localScale)); //相对parent object的scale，全局的叫lossyScale

        rotation.eulerAngles = new Vector3(0, 30, 0); //将this object绕y轴旋转30°
        print(string.Format("当前游戏对象{0}的rotation.eulerAngles可以被赋值为{1}", gameObject, rotation.eulerAngles));
        print(string.Format("当前游戏对象{0}的localRotation={1}", gameObject, transform.localRotation));

    }

    // Update is called once per frame
    void Update()
    {

    }
}
