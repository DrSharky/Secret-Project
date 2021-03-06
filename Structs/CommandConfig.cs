﻿using UnityEngine;

[System.Serializable]
public struct CommandConfig
{
    //Header anchor and position.
    public Vector2 headerAnchor;
    public Vector2 headerPos;

    [HideInInspector]
    public string headerString;

    //Inputfield position.
    public Vector2 inputPos;

    //Caret position.
    public Vector2 caretPos;

    //Vector to set header Width.
    //Needs to change to accommodate continue text. default is 250.
    public Vector2 headerVector;

}