using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCSVReader : MonoBehaviour {
    private const string PLANET_CSV_PATH = "PlanetLoadInfo/Planets";

    //private int lastNumOfCSVChars;
    private static TextAsset planetCSV;

    public static string[] GetPlanetNames_EditorOnly() {
#if UNITY_EDITOR
        if (planetCSV == null)
            planetCSV = Resources.Load(PLANET_CSV_PATH) as TextAsset;

        //update list of planet names
        string[] lines = planetCSV.text.Split('\n');
        string[] planetNames = new string[lines.Length - 1];

        for (int i = 1; i < lines.Length; i++) {
            string[] info = lines[i].Split(',');
            planetNames[i - 1] = info[0];
        }
        return planetNames;
#else
        return null;
#endif
    }

    public static PlanetData[] GetAllPlanets() {
        if (planetCSV == null)
            planetCSV = Resources.Load(PLANET_CSV_PATH) as TextAsset;

        string[] lines = planetCSV.text.Split('\n');
        PlanetData[] pData = new PlanetData[lines.Length-1];

        //read csv, skipping the first line in the text file
        for (int i = 1; i < lines.Length; i++) {
            string[] info = lines[i].Split(',');
            PlanetData p = new PlanetData(info[0], float.Parse(info[1]), float.Parse(info[2]), float.Parse(info[3]), float.Parse(info[4]));
            pData[i-1] = p;
        }

        return pData;
    }

}
