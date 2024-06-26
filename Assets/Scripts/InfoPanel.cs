using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour {
    public static InfoPanel Instance { get; private set; }
    public bool IsVisible { get { return gameObject.activeSelf; } }
    public string CurrPlanet { get; private set; }

    [Header("Component Refs")]
    [SerializeField] TMP_Text planetName;
    [SerializeField] Image image;
    [SerializeField] TMP_Text info;
    [Space(5f)]
    [Header("Default Settings")]
    [SerializeField] string defaultLanguage;

    private Dictionary<string, PlanetUIData> uiDataInCurrLanguage = new Dictionary<string, PlanetUIData>();
    private string currLanguage;
    private class PlanetUIData {
        public string name;
        public string image;
        public string info;

        public PlanetUIData(string pName, string pImage, string pInfo) {
            name = pName;
            image = pImage;
            info = pInfo;
        }
    }

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of InfoPanel on {Instance.gameObject.name} was destroyed and replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;

        ToggleVisibility(false);
        UploadLanguage(defaultLanguage);
    }

    #region JSON Reading & Language Switching

    [System.Serializable] private class PlanetJSONItem {
        public string ID;
        public string name;
        public string image;
        public string info;
    }
    [System.Serializable] private class PlanetJSONItemList {
        public PlanetJSONItem[] Planets;
    }

    //to be called whenever the current used language is switched
    private void UploadLanguage(string language) {
        TextAsset json = LanguageLibrary.Instance.GetJSON(language);
        if (json == null) {
            Debug.LogError($"Language {language} was not identified in LanguageLibrary. Are you misspelling an entry?");
            return;
        }

        uiDataInCurrLanguage.Clear();

        PlanetJSONItemList readJSON = JsonUtility.FromJson<PlanetJSONItemList>(json.text);
        foreach (PlanetJSONItem planet in readJSON.Planets) {
            PlanetUIData planetUIInfo = new PlanetUIData(planet.name, planet.image, planet.info);
            uiDataInCurrLanguage.Add(planet.ID, planetUIInfo);
        }
    }
    #endregion

    #region UI Visibility & Updates
    public void ToggleVisibility(bool visible) { gameObject.SetActive(visible); }
    public void ToggleVisibility() { gameObject.SetActive(!IsVisible); }

    public void UpdateUI(PlanetData planetToDisplay) {
        PlanetUIData planetUI = uiDataInCurrLanguage[planetToDisplay.name];
        planetName.text = planetUI.name;
        image.sprite = Resources.Load<Sprite>(planetUI.image);
        info.text = planetUI.info;

        CurrPlanet = planetToDisplay.name;
    }
    #endregion


}
