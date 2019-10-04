using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<Transform, List<Transform>> AllPlatsAndBlks;
    [HideInInspector]
    public List<Transform> plats;

    public float amp = 0.05f;

    bool update = false;

    void Awake()
    {
        AllPlatsAndBlks = new Dictionary<Transform, List<Transform>>();
        plats = new List<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform plat in transform)
        {
            plats.Add(plat);
            List<Transform> tempBlocks = new List<Transform>();
            foreach (Transform block in plat)
            {
                tempBlocks.Add(block);
            }
            AllPlatsAndBlks.Add(plat, tempBlocks);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelOne()
    {
        foreach(var plat in plats)
        {
            foreach(var block in AllPlatsAndBlks[plat])
            {
                Vector2 tempPos = block.position;
                tempPos = new Vector2(block.position.x, Mathf.Sin(block.position.x * amp));
                block.position = tempPos;
            }
        }
        UpdateFloor();
    }

    public void LevelTwo()
    {
        foreach (var plat in plats)
        {
            foreach (var block in AllPlatsAndBlks[plat])
            {
                Vector2 tempPos = block.position;
                tempPos = new Vector2(block.position.x, -2.120759f);
                block.position = tempPos;
            }
        }
    }

    public void UpdateFloor()
    {
        if (!update)
        {
            for (int i = 0; i < AllPlatsAndBlks.Count; i++)
            {
                Transform tempPlat = transform.GetChild(i);
                for (int j = 0; j < AllPlatsAndBlks[tempPlat].Count; j++)
                {
                    transform.GetChild(i).GetChild(j).position = AllPlatsAndBlks[tempPlat][j].position;
                }
            }
            update = true;
        }
    }

    public void UpdateReset()
    {
        update = false;
    }

    
}
