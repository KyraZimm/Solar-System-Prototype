using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Planet : MonoBehaviour {

    PlanetData data;

    //component refs
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private GameObject highlight;

    //physics values
    private static float TIME_SCALE = .001f;
    private const float MIN_ALLOWED_TIME_SCALE = 0f;
    private const float MAX_ALLOWED_TIME_SCALE = 0.01f;
    private float angleOfOrbitLastFixedUpdate = 0;
    private float rotAngleLastFixedUpdate = 0;

    //resources for planet loading
    private const string PLANET_RESOURCES_PATH = "PlanetLoadInfo";
    private static GameObject _planetPrefab;
    private static GameObject planetPrefab {
        get {
            if (_planetPrefab == null)
                _planetPrefab = Resources.Load("PlanetPrefab") as GameObject;
            return _planetPrefab;
        }
    }

    private static TextureMapper _planetTextures;
    private static TextureMapper planetTextures {
        get {
            if (_planetTextures == null)
                _planetTextures = Resources.Load(PLANET_RESOURCES_PATH + "/PlanetTextures").GetComponent<TextureMapper>();
            return _planetTextures;
        }
    }


    #region Physics Updates
    private void FixedUpdate() {
        //planet orbit
        float angleOfOrbitPerFixedUpdate = (360 / data.orbit * Time.fixedDeltaTime) * TIME_SCALE;
        //float currAngleOfOrbit = angleOfOrbitPerFrame * Time.time * TIME_SCALE;
        float currAngleOfOrbit = angleOfOrbitLastFixedUpdate + angleOfOrbitPerFixedUpdate;

        float xDisp = Mathf.Cos(currAngleOfOrbit) * data.distFromSun;
        float zDisp = Mathf.Sin(currAngleOfOrbit) * data.distFromSun;
        rb.MovePosition(new Vector3(xDisp, 0, zDisp));

        //planet rotation
        float angleOfRotPerFrame = (360 / data.rot * Time.fixedDeltaTime) * TIME_SCALE;
        //float currAngleOfRot = angleOfRotPerFrame * Time.time * TIME_SCALE;
        float currAngleOfRot = rotAngleLastFixedUpdate + angleOfRotPerFrame;
        rb.MoveRotation(Quaternion.AngleAxis(currAngleOfRot, Vector3.up));

        //store angles for next fixed update
        angleOfOrbitLastFixedUpdate = currAngleOfOrbit;
        rotAngleLastFixedUpdate = currAngleOfRot;
    }

    public static void ChangeTimeScale(float timeScale) {
        if (timeScale > MAX_ALLOWED_TIME_SCALE) {
            Debug.LogWarning($"Tried to change time scale to {timeScale}, which above the accepted ceiling of {MAX_ALLOWED_TIME_SCALE}.");
            timeScale = MAX_ALLOWED_TIME_SCALE;
        }
        else if (timeScale < MIN_ALLOWED_TIME_SCALE) {
            Debug.LogWarning($"Tried to change time scale to {timeScale}, which below the accepted floor of {MIN_ALLOWED_TIME_SCALE}.");
            timeScale = MIN_ALLOWED_TIME_SCALE;
        }
        TIME_SCALE = timeScale;
    }

    #endregion

    #region UI Interaction
    private void Awake() {
        ShowHighlight(false);
    }

    private void OnMouseOver() { ShowHighlight(true); }
    private void OnMouseExit() { ShowHighlight(false); }
    private void OnMouseDown() {
        if (InfoPanel.Instance.CurrPlanet == data.name)
            InfoPanel.Instance.ToggleVisibility();
        else {
            InfoPanel.Instance.UpdateUI(data);
            InfoPanel.Instance.ToggleVisibility(true);
        }
    }

    private void ShowHighlight(bool highlightOn) { highlight.SetActive(highlightOn); }
    #endregion

    #region GameObject Instantiation & Initialization

    public static Planet MakeNewPlanet(PlanetData newPlanetData) {
        //instantiate new planet
        Vector3 startingPos = new Vector3(newPlanetData.distFromSun, 0, 0);
        Planet newPlanet = Instantiate(planetPrefab, startingPos, Quaternion.identity).GetComponent<Planet>();

        //adjust planet properties to match spawn data
        newPlanet.gameObject.name = newPlanetData.name;
        newPlanet.data = newPlanetData;
        newPlanet.transform.localScale = new Vector3(newPlanetData.scale, newPlanetData.scale, newPlanetData.scale);

        //if a texture has been chosen for planet, assign texture
        Texture pTex = planetTextures.GetPlanetTexture(newPlanetData.name);
        if (pTex == null)
            return newPlanet;

        MaterialPropertyBlock matBlock = new MaterialPropertyBlock(); //using a material property block instead of different materials to reduce draw calls
        matBlock.SetTexture("_MainTex", pTex);
        newPlanet.GetComponent<MeshRenderer>().SetPropertyBlock(matBlock);

        return newPlanet;
    }

    #endregion

}
