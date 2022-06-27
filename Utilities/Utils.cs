using UnityEngine;

public static class Utils
{
    public static T FindCompInChildren<T>(this GameObject gameObj) where T : Component
    {
        T comp = gameObj.transform.GetComponent<T>();
        if (comp == null)
        {
            comp = gameObj.transform.GetComponentInChildren<T>();
        }
        if (comp != null)
        {
            return comp;
        }
        else
        {
            for(int i = 0; i < gameObj.transform.childCount; i++)
            {
                comp = gameObj.transform.GetChild(i).gameObject.FindCompInChildren<T>();

                if (comp != null)
                {
                    return comp;
                }
            }
        }
        return null;
    }
}
