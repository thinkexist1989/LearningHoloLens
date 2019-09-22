using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("游戏对象" + this.gameObject + "在" + this.gameObject.scene.name + "场景下");
        print("游戏对象" + this.gameObject + "所在场景在Scenes in build中的序号为：" + this.gameObject.scene.buildIndex);
        print("游戏对象" + this.gameObject + "所在场景保存的路径为：" + this.gameObject.scene.path);
        print("游戏对象" + this.gameObject + "共包含父物体" + this.gameObject.scene.rootCount + "个");

        print("游戏对象" + this.gameObject + "工作在场景部分操作修改后不保存返回true。此时值为：" + this.gameObject.scene.isDirty);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
