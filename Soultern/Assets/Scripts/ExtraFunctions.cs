using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraFunctions : MonoBehaviour
{
    public GameObject FindInactive(string name)
    {
        Transform[] Objects = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];

        for (int i = 0; i < Objects.Length; i++)
            if (Objects[i].name == name)
                return Objects[i].gameObject;

        return null;
    }
}
