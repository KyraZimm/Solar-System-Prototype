using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetData {
    public string name;
    public float orbit;
    public float rot;
    public float scale;
    public float distFromSun;

    public PlanetData(string pName, float pOrbit, float pRot, float pScale, float pDist) {
        name = pName;
        orbit = pOrbit;
        rot = pRot;
        scale = pScale;
        distFromSun = pDist;
    }
}
