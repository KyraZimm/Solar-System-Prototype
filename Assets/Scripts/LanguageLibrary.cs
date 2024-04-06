using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LanguageLibrary : MonoBehaviour
{
    private static LanguageLibrary instance;
    public static LanguageLibrary Instance {
        get {
            if (instance == null)
                instance = Resources.Load("PlanetLoadInfo/LanguageLibrary").GetComponent<LanguageLibrary>();
            return instance;
        }
    }

    [System.Serializable] private class LanguageFiles {
        public string language; //ideally, this would not be a manually input string, but an option from an array that designers could input/pull from in a JSON. But again, for the scope of the project, this is more time efficient
        public TextAsset uiJson;
    }

    [SerializeField] private List<LanguageFiles> languageFiles;

    public TextAsset GetJSON(string language) {
        foreach (LanguageFiles file in languageFiles)
            if (file.language == language)
                return file.uiJson;

        return null;
    }
}
