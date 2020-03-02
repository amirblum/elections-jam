using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    RPGTalk _rpgt;
    NavMeshAgent _nva;

    GameObject Level;

    Vector3 TargetPoint;

    const int TEXTFILELINECOUNT = 20;

    // Start is called before the first frame update
    void Start()
    {
        _nva = GetComponent<NavMeshAgent>();
        _rpgt = GetComponentInChildren<RPGTalk>();
        Level = GameObject.FindGameObjectWithTag("Level");
        TargetPoint = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            RepositionTargetPoint();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        NewRandomTalk();
    }

    void NewRandomTalk()
    {
        int lineStart = Random.Range(1, TEXTFILELINECOUNT);
        int lineBreak = lineStart + 3;
        _rpgt.NewTalk(lineStart.ToString(), lineBreak.ToString());
    }

    void RepositionTargetPoint()
    {
        Mesh _lm = Level.GetComponent<MeshFilter>().mesh;
        print(_lm.bounds.min.x + " " + _lm.bounds.max.x);
        Vector3 _tmp = new Vector3(Random.Range(_lm.bounds.min.x, _lm.bounds.max.x), 0, Random.Range(_lm.bounds.min.z, _lm.bounds.max.z));
        TargetPoint = _tmp;
        print(TargetPoint);
        _nva.SetDestination(TargetPoint);
    }

}
