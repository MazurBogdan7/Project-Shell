using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    GeneratorMatricsWorld newGen = new GeneratorMatricsWorld(5, 5);
    // Start is called before the first frame update
    void Awake()
    {
        newGen.ResultGeneration();
        //Debug.Log();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
