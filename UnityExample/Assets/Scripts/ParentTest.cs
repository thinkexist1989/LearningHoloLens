using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(string.Format("获取当前游戏对象{0}的父对象{1}的Transform组件：", gameObject, transform.parent.name));
        print(string.Format("transform.parent.position = {0}", transform.parent.position));
        print(string.Format("transform.parent.localRotation = {0}", transform.parent.localRotation));
        print(string.Format("transform.parent.localScale = {0}", transform.parent.localScale));

        GameObject parentGameObject = transform.Find("SubCube").gameObject; //从当前对象向下查找一个叫SubCube的对象
        print(string.Format("这个游戏对象的{0}的根对象是：{1}", parentGameObject.name, parentGameObject.transform.root.name));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
