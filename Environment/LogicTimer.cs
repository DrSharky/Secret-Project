using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicTimer : MonoBehaviour
{
    public bool startsDisabled = false;
    public bool useRandomTime = true;
    public int minimumRandomInterval = 1;
    public int maximumRandomInterval = 5;
    public List<LogicObject> logics;
    private int interval;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountTimer()
    {
        yield return new WaitForSeconds(interval);
        Switch();
    }

    void Switch()
    {
        interval = UnityEngine.Random.Range(minimumRandomInterval, maximumRandomInterval);
        for(int i = 0; i < logics.Count; i++)
        {
            logics[i].Switch();
        }
        StartCoroutine(CountTimer());
    }
}
