using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    [SerializeField]
    private ShaderVariantCollection variantCollection;

    void Awake()
    {
        if(!variantCollection.isWarmedUp)
            variantCollection.WarmUp();
    }
}