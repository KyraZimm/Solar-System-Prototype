using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlanetData {
    public string name;
    public float orbit;
    public float rot;
    public float scale;
    public float distFromSun;

   /* public PlanetData(string pName, float pOrbit, float pRot) {
        name = pName;
        orbit = pOrbit;
        rot = pRot;
    }*/

    //really not how I'd have liked to do this, but I got pressed for time close to the end
    public PlanetData(string csvData) {
        string[] info = csvData.Split(',');

        name = info[0];
        orbit = float.Parse(info[1]);
        rot = float.Parse(info[2]);
        scale = float.Parse(info[3]);
        distFromSun = float.Parse(info[4]);
    }
}
