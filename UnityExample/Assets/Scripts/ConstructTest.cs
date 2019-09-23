using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //运行时在场景中新建一个Game Object，默认名称为New Game Object
        GameObject gameObject = new GameObject();
        //将新的object重命名为MyNewObject
        gameObject.name = "MyNewObject";
        GameObject gameObject2 = new GameObject("Beautiful Object", new System.Type[2] { typeof(BoxCollider), typeof(MeshRenderer) });
        gameObject2.AddComponent<Light>(); //利用AddComponent方法给游戏对象添加组件
    }

    // Update is called once per frame
    void Update()
    {

    }
}
