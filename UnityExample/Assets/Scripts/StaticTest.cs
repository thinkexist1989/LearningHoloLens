using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject parantObject = GameObject.Find("ParentGameObject");
        print("At Beginning, Parent Object's static property is: " + parantObject.isStatic);
        parantObject.isStatic = !parantObject.isStatic;
        print("Now, Parent Object's static property is: " + parantObject.isStatic);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
