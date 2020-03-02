using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    RPGTalk _rpgt;
    NavMeshAgent _nva;

    GameObject Player;

    GameObject Level;

    Vector3 TargetPoint;

    const int TEXTFILELINECOUNT = 34;
    bool _isTalking = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        _nva = GetComponent<NavMeshAgent>();
        _rpgt = GetComponentInChildren<RPGTalk>();
        _rpgt.callback.AddListener(StopTalking);
        Level = GameObject.FindGameObjectWithTag("Level");
        TargetPoint = new Vector3();
        StartCoroutine(MoveEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            RepositionTargetPoint();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            NewRandomTalk();
        }
    }

    public void NewRandomTalk()
    {
        int lineStart = Random.Range(1, TEXTFILELINECOUNT);
        int lineBreak = lineStart + 1;
        _rpgt.NewTalk(lineStart.ToString(), lineBreak.ToString());
    }

    void RepositionTargetPoint()
    {
        Mesh _lm = Level.GetComponent<MeshFilter>().mesh;
        print(_lm.bounds.min.x + " " + _lm.bounds.max.x);
        Vector3 _tmp = new Vector3(Random.Range(_lm.bounds.min.x, _lm.bounds.max.x), 0, Random.Range(_lm.bounds.min.z, _lm.bounds.max.z));
        TargetPoint = _tmp;
        _nva.SetDestination(TargetPoint);
    }

    public void StartTalking()
    {
        transform.LookAt(Player.transform);
        _isTalking = true;
        _nva.isStopped = true;
        NewRandomTalk();
    }
    public void StopTalking()
    {
        _isTalking = false;
        _nva.isStopped = false;
        Player.GetComponent<PlayerController>().CanMove = true;
    }

    IEnumerator MoveEnemy()
    {
        while (true)
        {
            if (!_isTalking) RepositionTargetPoint();
            yield return new WaitForSeconds(10);
        }
    }
}
