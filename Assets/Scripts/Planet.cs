using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    PlanetData data;
    private Rigidbody rb;

    const float TIME_SCALE = .0001f;
    private static GameObject planetPrefab {
        /*get {
            if (m_value == null)
                m_value = Resources.Load("PlanetPrefab") as GameObject;
            return m_value;
        }*/
        get { return Resources.Load("PlanetPrefab") as GameObject; }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        //planet orbit
        float angleOfOrbitPerFrame = 360 / (data.orbit * Time.fixedDeltaTime);
        float currAngleOfOrbit = angleOfOrbitPerFrame * Time.time * TIME_SCALE;

        float xDisp = Mathf.Cos(currAngleOfOrbit) * data.distFromSun;
        float zDisp = Mathf.Sin(currAngleOfOrbit) * data.distFromSun;
        rb.MovePosition(new Vector3(xDisp, 0, zDisp));

        //planet rotation
        float angleOfRotPerFrame = 360 / (data.rot * Time.fixedDeltaTime);
        float currAngleOfRot = angleOfRotPerFrame * Time.time * TIME_SCALE;
        rb.MoveRotation(Quaternion.AngleAxis(currAngleOfRot, Vector3.up));
    }

    private void OnMouseOver() { UIValues.Instance.UpdateUI(data); }

    public static Planet MakeNewPlanet(PlanetData newPlanetData) {
        Vector3 startingPos = new Vector3(newPlanetData.distFromSun, 0, 0);
        Planet newPlanet = Instantiate(planetPrefab, startingPos, Quaternion.identity).GetComponent<Planet>();
        newPlanet.gameObject.name = newPlanetData.name;
        newPlanet.data = newPlanetData;
        newPlanet.transform.localScale = new Vector3(newPlanetData.scale, newPlanetData.scale, newPlanetData.scale);
        return newPlanet;
    }

}
