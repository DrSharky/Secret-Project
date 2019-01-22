using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    Light spotLight;
    [SerializeField]
    Light haloLight;

    int flickerMax = 8, flickerMin = 3;
    float timeMin = 0.08f, timeMax = 0.9f;
    float timeInterval = 5.0f, timeCount = 0.0f;

	void Start ()
    {

	}
	
	void Update ()
    {
		if(timeCount >= timeInterval) { }
            //StartCoroutine();
	}
}