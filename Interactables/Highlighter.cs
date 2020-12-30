using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [HideInInspector]
    public bool highlighting = false;
    [HideInInspector]
    public bool inRange = false;

    private Material[] mats;
    private Color baseColor;
    private float ceiling = 0.1f, floor = 0f;
    private bool frozenSet = false;

	// Use this for initialization
	void Start()
	{
        baseColor = Color.white;
        mats = GetComponent<Renderer>().materials;
	}
	
	// Update is called once per frame
	void Update()
	{
        if (frozenSet)
            return;

        if (inRange)
        {
            if(highlighting)
            {
                    float emission = Mathf.PingPong(Time.time * 0.25f, ceiling - floor);
                    Color emissionColor = baseColor * Mathf.LinearToGammaSpace(emission);
                    SetColor(emissionColor);
            }
        }
	}

    public void PCFreezeToggle(bool freeze) { frozenSet = !freeze; SetColor(Color.black); }

    public void SetColor(Color color)
    {
        for(int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_EmissionColor", color);
        }
    }
}