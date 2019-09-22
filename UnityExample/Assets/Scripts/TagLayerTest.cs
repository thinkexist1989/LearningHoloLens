using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagLayerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject sphere = GameObject.Find("Sphere"); //Find GameObject "Sphere"

        //Layer
        print("The GameObject \'Cube\' is on Layer " + this.gameObject.layer); //Due to the script is attached to Cube, so this is point to Cube
        print("The GameObject \'Sphere\' is on Layer " + sphere.layer);

        //Tag
        print("The tag of GameObject \'Cube\' is " + this.gameObject.tag); //Due to the script is attached to Cube, so this is point to Cube
        print("The tag of GameObject \'Sphere\' is " + this.gameObject.tag); //Due to the script is attached to Cube, so this is point to Cube

    }

    // Update is called once per frame
    void Update()
    {

    }
}
