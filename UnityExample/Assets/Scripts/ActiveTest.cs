using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTest : MonoBehaviour
{
    public GameObject m_ParentObject;
    public GameObject m_ChildObject;
    // Start is called before the first frame update
    void Start()
    {
        print("Parent Object's activeInHierarchy:" + m_ParentObject.activeInHierarchy);
        print("Parent object's activeSelf:" + m_ParentObject.activeSelf);
        print("Children Object's activeInHierarchy:" + m_ChildObject.activeInHierarchy);
        print("Children Object's activeSelf:" + m_ChildObject.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
