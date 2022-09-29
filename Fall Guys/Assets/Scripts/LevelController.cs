using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] List<GameObject> plates = new List<GameObject>();
    [SerializeField] List<GameObject> rbWalls = new List<GameObject>();

    void Start()
    {
        for (var i = 0; i < plates.Count; i += plates.Count / 12)
        {
            var x = Random.Range(i, i + 4);
            OffPlate(x);
        }

        for (var i = 0; i < rbWalls.Count; i += rbWalls.Count / 3)
        {
            var x = Random.Range(i, i + 6);
            RigidbodyWall(x);
        }
    }

    public void RigidbodyWall(int count)
    {
        Rigidbody rb = rbWalls[count].gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
    }

    public void OffPlate(int count)
    {
        plates[count].GetComponent<Collider>().isTrigger = true;
    }
}
