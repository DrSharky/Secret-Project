using UnityEngine;

public class ItemParticleRotation : MonoBehaviour
{
    Transform parent;

    private void Awake()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(-parent.transform.rotation.x, 0, -parent.transform.rotation.z);
    }
}
