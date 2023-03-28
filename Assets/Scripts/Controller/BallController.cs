using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BossSkill1;

public class BallController : MonoBehaviour
{
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        go = this.gameObject;
        /*Skill1Beats[] sb = new Skill1Beats[100];
        sb[0].interval=3;
        sb[0].type=1;
        sb[1].interval = 1;
        sb[1].type = 1;
        sb[2].interval = 3;
        sb[2].type = 1;
        sb[3].interval = 1;
        sb[3].type = 1;
        BossSkill1.Instance.StartCount(4,sb);*/
    }

    // Update is called once per frame
    void Update()
    {
        int now = Beats.Instance.count;
        go.transform.localScale = new Vector3(1+0.04f*now,1+0.04f*now,1+0.04f*now);
    }
}
