using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Object", menuName = "Asset/New Audio Object")]
public class AudioNode : ScriptableObject
{
    public AudioClip clip;
    public string subtitle;

    
}
