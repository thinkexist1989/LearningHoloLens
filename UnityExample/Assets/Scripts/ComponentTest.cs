using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentTest : MonoBehaviour
{
    Rigidbody rb;
    Rigidbody[] rbs;
    MeshRenderer mr;
    MeshRenderer[] mrs;
    Transform t;
    Transform[] ts;

    // Start is called before the first frame update
    void Start()
    {
        //1. GetComponent<T>() 获取当前游戏对象的制定组件，没有就返回null。不查找子物体组件
        rb = GetComponent<Rigidbody>();
        print(string.Format("使用GetComponent<T>()获取游戏对象{0}的RigidBody这个组件的InstanceID为{1}", rb.name, rb.GetInstanceID()));
        //2. GetComponents<T>() 获取当前游戏对象的所有指定不查找子物体的组件
        rbs = GetComponents<Rigidbody>();
        foreach (var rb in rbs)
        {
            print(string.Format("使用GetComponents<T>()获取游戏对象{0}的RigidBody这个组件的InstanceID为{1}", rb.name, rb.GetInstanceID()));
        }

        //3. GetComponentInChildren<T>() 先查找父物体有没有制定组件，没有的话再查找子物体，最后返回第一个指定组件（由上往下查找指定组件）
        mr = GetComponentInChildren<MeshRenderer>(); //因为Shpere本身就具有MeshRenderer组件，即使子物体也包含这个组件，也只返回本身这个物体的Sphere
        print(string.Format("使用GetComponentInChildren<T>()获取游戏对象{0}的MeshRenderer这个组件的InstanceID为{1}", mr.name, mr.GetInstanceID()));
        //4. GetComponentsInChildren<T>() 获取当前游戏对象及子对象的所有指定组件
        mrs = GetComponentsInChildren<MeshRenderer>();
        foreach (var mr in mrs)
        {
            print(string.Format("使用GetComponentsInChildren<T>()获取游戏对象{0}的MeshRenderer这个组件的InstanceID为{1}", mr.name, mr.GetInstanceID()));
        }

        //5. GetComponentInParent<T>() 由下往上查找指定组件
        t = GetComponentInParent<Transform>();
        print(string.Format("使用GetComponentInChildren<T>()获取游戏对象{0}的Transform这个组件的InstanceID为{1}", t.name, t.GetInstanceID()));
        //6. GetComponentInParent<T>() 获取当前游戏对象及父对象的所有指定组件
        ts = GetComponentsInParent<Transform>();
        foreach (var t in ts)
        {
            print(string.Format("使用GetComponentsInParent<T>()获取游戏对象{0}的Transform这个组件的InstanceID为{1}", t.name, t.GetInstanceID()));
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}
