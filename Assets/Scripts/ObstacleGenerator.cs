using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class ObstacleGenerator : MonoBehaviour
{
    struct ObjectSetting
    {
        public int id;
        public float position;

        public ObjectSetting(int id_, float position_)
        {
            id = id_;
            position = position_;
        }
    }
    public GameObject[] objectPrefabs;
    public PathCreator path;
    // Start is called before the first frame update
    void Start()
    {
        ObjectSetting[] objectSettings = new ObjectSetting[]
        {
            new ObjectSetting(3, 15),
            new ObjectSetting(4, 20),
            new ObjectSetting(5, 23),
            new ObjectSetting(6, 27),
            new ObjectSetting(0, 32),
            new ObjectSetting(0, 38),
        };

        for (int i = 0; i < objectSettings.Length; i++)
        {
            Vector3 pos = path.path.GetPointAtDistance(objectSettings[i].position * 15);
            pos.y = 10.5f;
            Quaternion rot = path.path.GetRotationAtDistance(objectSettings[i].position * 15);
            GameObject gameObject = Object.Instantiate<GameObject>(objectPrefabs[objectSettings[i].id], pos, rot);
            gameObject.transform.Rotate(0, 0, 90, Space.Self);
            gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
