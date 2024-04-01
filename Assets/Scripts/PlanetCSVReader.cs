using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCSVReader : MonoBehaviour {
    private const string PLANET_CSV_PATH = "PlanetLoadInfo/Planets";
    //[HideInInspector][SerializeField] public static string[] AllPlanetNames { get; private set; }

    //private static int lastNumOfCSVChars;
    private static TextAsset planetCSV;


/*#if UNITY_EDITOR
    private void OnValidate() {
        if (planetCSV == null)
            planetCSV = Resources.Load(PLANET_CSV_PATH) as TextAsset;

        //if contents of CSV have not changed, do not update
        if (lastNumOfCSVChars == planetCSV.text.Length)
            return;

        //update list of planet names
        string[] lines = planetCSV.text.Split('\n');
        string[] planetNames = new string[lines.Length - 1];

        for (int i = 1; i < lines.Length; i++) {
            string[] info = lines[i].Split(',');
            planetNames[i - 1] = info[0];
        }
        AllPlanetNames =  planetNames;
    }
#endif*/

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

}
