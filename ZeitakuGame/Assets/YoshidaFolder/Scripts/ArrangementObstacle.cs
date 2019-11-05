using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class ArrangementObstacle : MonoBehaviour
{
    Vector3 movement;
    //描画する座標のフラグを所持しているクラス
    [SerializeField]
    PositionFlags positionFlags;
    //PositionFlagsクラスから受け取る座標のリスト
    [SerializeField]
    List<Transform> flags;
    //リストのアクセスに使用する要素数
    int flagsCount = 0;
    //再生速度 
    float time = 0;

    //ステージの端になるポール
    [SerializeField]
    GameObject poll;

    //poll生成のインターバル
    int possInstantiateCount = 0;

    //障害物生成のインターバル
    int obstacleInstantiateCount = 0;

    //生成する障害物のセットとそれぞれの生成確率
    [System.Serializable]
    struct Obstacles
    {
        //障害物を格納する障害物配列　
        public GameObject obstacleArray;
        public float perf;
    }
    public GameObject ob;

    [SerializeField]
    Obstacles[] obstacles=null;

    float obstaclesRandSum = 0;

    // Start is called before the first frame update
    void Start()
    {
        positionFlags = GameObject.Find("StageFlagg").GetComponent<PositionFlags>();
        flags = new List<Transform>();
        flags = positionFlags.GetList();
        flagsCount = 0;
        time = 0;
        possInstantiateCount = 0;
        movement=new Vector3(0,0,0);
        for(int num=0;num<obstacles.Length;num++)
        {
            obstaclesRandSum += obstacles[num].perf;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flagsCount + 3 >= flags.Count)
        {
            return;
        }
        BezierCurveMove();
        SidePollInstantiate();
        ObstacleInstantiate();
        transform.position = movement;
    }

    //計算された軌道に反り移動する
    void BezierCurveMove()
    {
        time += 0.008f;
        movement = GetPoint(flags[flagsCount].position, flags[flagsCount + 1].position, flags[flagsCount + 2].position, flags[flagsCount + 3].position, time);
        //再生が終了したら次のフラグへ
        if (time >= 1)
        {
            flagsCount += 3;
            time = 0;
        }

    }

    void SidePollInstantiate()
    {
        //ポールの生成
        if (possInstantiateCount > 8)
        {
            Instantiate(poll, new Vector3(transform.position.x + 10, transform.position.y, transform.position.z), new Quaternion());
            Instantiate(poll, new Vector3(transform.position.x - 10, transform.position.y, transform.position.z), new Quaternion());
            possInstantiateCount = 0;
        }
        else
        {
            possInstantiateCount++;
        }
    }

    //ベジェ曲線の計算
    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        var a = Vector3.Lerp(p0, p1, t); // 緑色の点1
        var b = Vector3.Lerp(p1, p2, t); // 緑色の点2
        var c = Vector3.Lerp(p2, p3, t); // 緑色の点3

        var d = Vector3.Lerp(a, b, t);   // 青色の点1
        var e = Vector3.Lerp(b, c, t);   // 青色の点2

        return Vector3.Lerp(d, e, t);    // 黒色の点
    }

    void ObstacleInstantiate()
    {
        if (obstacleInstantiateCount > 50)
        {
            Vector3 pos = transform.position;
            //Quaternion qua = transform.rotation - movement.rotation;
            GameObject a = GetRandamObstacle();

            float dx = pos.x - movement.x;
            float dy = pos.z - movement.z;
            float rad = Mathf.Atan2(dy, dx);
            rad *= Mathf.Rad2Deg;
            Quaternion qua=new Quaternion();
            qua=Quaternion.Euler(new Vector3(0,rad+90,0));
            a = Instantiate(a, transform.position, qua);
            obstacleInstantiateCount = 0;
        }
        else
        {
            obstacleInstantiateCount++;
        }
    }

    GameObject GetRandamObstacle()
    {
        GameObject obj;
        float rand = Random.Range(0,obstaclesRandSum);
        for (int num = 0; num < obstacles.Length; num++)
        {
            float bef = 0;
            for(int b=0;b<num;b++)
            {
                bef += obstacles[b].perf;
            }

            if(rand<=obstacles[num].perf+bef)
            {
                return obstacles[num].obstacleArray;
            }
        }
            return obstacles[0].obstacleArray;
    }
}
