using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class BallSO : ScriptableObject
{
    public BallSettings Value;
}
[Serializable]
public class BallSettings
{
    public Color color;
    public float speed;
    public float scale;

    public static byte[] Serialize(object obj)
    {
        BallSettings settings = (BallSettings)obj;
        return new byte[] { (byte)settings.color.r, (byte)settings.color.g, (byte)settings.color.b, (byte)settings.speed, (byte)settings.scale };
    }
    public static BallSettings Deserialize(byte[] obj)
    {
        return new BallSettings()
        {
            color = new Color(obj[0], obj[1], obj[2]),
            speed = obj[3],
            scale = obj[4]
        };
    }
}
