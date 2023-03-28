using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour
{

    Dictionary<string,GameObject> maps = new Dictionary<string, GameObject>();

    private static UIRoot _instance;
    public static UIRoot Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject FunctionLayer,TipsLayer;
    // Start is called before the first frame update
    void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    

    public void init(string preaddress,string name,string layer,int id)
    {
        node tmp = new node();
        tmp.ops = 3;
        tmp.preaddress = preaddress;
        tmp.name = name;
        tmp.nickname = name+id.ToString();
        tmp.layer = layer;
        print(name);
        q.Enqueue(tmp);
    }

    public void uninit(string name,int id)
    {
        //Destroy(maps[name]);
        //maps.Remove(name);
        node tmp = new node();
        tmp.ops = 1;
        tmp.name = name;
        tmp.nickname = name+id.ToString();
        print(name);
        q.Enqueue(tmp);
        
    }

    public void TurnTo(int id)
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(id);
        node tmp = new node();
        tmp.ops = 2;
        tmp.id = id;
        q.Enqueue(tmp);
    }

    public void MoveSon(string layer,string name,int id,string son,Vector3 v)
    {
        node tmp = new node();
        tmp.ops = 4;
        tmp.name = name;
        tmp.nickname = name+id.ToString();
        tmp.son = son;
        tmp.v = v;
        tmp.layer = layer;
        print(name);
        q.Enqueue(tmp);
    }

    public void StopRingCount(string name,int id)
    {
        node tmp = new node();
        tmp.ops = 5;
        tmp.name = name;
        tmp.nickname = name+id.ToString();
        q.Enqueue(tmp);
    }

    struct node
    {
        public int ops;
        public string name;
        public string nickname;
        public int id;
        public string preaddress;
        public string layer;
        public string son;
        public Vector3 v;
    };

    Queue<node>q = new Queue<node>();
    private void Update() {
        while(q.Count>0)
        {
            node tmp = q.Dequeue();
            if(tmp.ops == 1)
            {
                print(tmp.name);
                Destroy(maps[tmp.nickname]);
                maps.Remove(tmp.nickname);
            }
            else if(tmp.ops == 2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(tmp.id);
            }
            else if(tmp.ops == 3)
            {
                if(tmp.layer == "FunctionLayer")
                {
                    GameObject menu = (GameObject)Resources.Load(tmp.preaddress+"/"+tmp.name);
                    menu = Instantiate(menu);
                    menu.transform.SetParent(UIRoot.Instance.FunctionLayer.transform);
                    menu.transform.name = tmp.nickname;
                    maps.Add(tmp.nickname,menu);
                    print(tmp.name);
                }
                else if(tmp.layer == "TipsLayer")
                {
                    GameObject menu = (GameObject)Resources.Load(tmp.preaddress+"/"+tmp.name);
                    menu = Instantiate(menu);
                    menu.transform.SetParent(UIRoot.Instance.TipsLayer.transform);
                    menu.transform.name = tmp.nickname;
                    maps.Add(tmp.nickname,menu);
                }
                else if(tmp.layer == "")
                {
                    GameObject menu = (GameObject)Resources.Load(tmp.preaddress+"/"+tmp.name);
                    menu = Instantiate(menu);
                    menu.transform.name = tmp.nickname;
                    maps.Add(tmp.nickname,menu);
                }
            }
            else if(tmp.ops == 4)
            {
                GameObject go = maps[tmp.nickname];
                Transform son = go.transform.Find(tmp.son);
                son.position=tmp.v;
            }
            else if(tmp.ops == 5)
            {
                if(maps.ContainsKey(tmp.nickname))
                {
                    RingController RC=maps[tmp.nickname].GetComponent<RingController>();
                    RC.stopcount();
                }
            }
        }
    }
}
