using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIRoot.Instance.init("Prefabs","MainMenu","FunctionLayer",0);
    }
}
