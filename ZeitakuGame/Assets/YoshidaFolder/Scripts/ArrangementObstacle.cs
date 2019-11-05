using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangementObstacle : MonoBehaviour
{
    [SerializeField]
    List<Transform> flags;
    int flagsCount = 0;
    int flagsCountMax = 0;
    [SerializeField]
    PositionFlags positionFlags;

    float time = 0;
    [SerializeField]
    GameObject poll;

    Transform now;
    Transform next;

    int instantiateCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        positionFlags = GameObject.Find("StageFlagg").GetComponent<PositionFlags>();
        flags = new List<Transform>();
        flags = positionFlags.GetList();
        flagsCountMax = flags.Count;
        flagsCount = 0;
        now = flags[0];
        next = flags[1];
        time = 0;
        instantiateCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += 0.005f;
        //transform.position = Vector3.Lerp(now.position, next.position, time);
        //transform.position = Vector3.Slerp(now.position, next.position, time);
        transform.position=GetPoint(flags[flagsCount].position, flags[flagsCount+1].position, flags[flagsCount+2].position, flags[flagsCount+3].position, time);
        if (time >= 1)
        {
            flagsCount += 3;
            now = flags[flagsCount];
            next = flags[flagsCount + 1];
            time = 0;
        }
        if (flagsCount >= flagsCountMax)
        {
            Debug.Log("ed");
        }

        if (instantiateCount > 10)
        {
            Instantiate(poll, new Vector3(transform.position.x + 10, transform.position.y, transform.position.z), new Quaternion());
            Instantiate(poll, new Vector3(transform.position.x - 10, transform.position.y, transform.position.z), new Quaternion());
            instantiateCount = 0;
        }
        else
        {
            instantiateCount++;
        }
    }

    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        var a = Vector3.Lerp(p0, p1, t); // 緑色の点1
        var b = Vector3.Lerp(p1, p2, t); // 緑色の点2
        var c = Vector3.Lerp(p2, p3, t); // 緑色の点3

        var d = Vector3.Lerp(a, b, t);   // 青色の点1
        var e = Vector3.Lerp(b, c, t);   // 青色の点2

        return Vector3.Lerp(d, e, t);    // 黒色の点
    }
}
