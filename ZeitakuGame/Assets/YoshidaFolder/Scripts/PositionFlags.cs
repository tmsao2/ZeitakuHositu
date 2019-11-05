using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFlags : MonoBehaviour
{
    [SerializeField]
    List<Transform> flags;
    // Start is called before the first frame update
    void Awake()
    {
        flags = new List<Transform>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            flags.Add(transform.GetChild(i).transform);
        }
    }

    public List<Transform> GetList()
    {
        return flags;
    }
}
