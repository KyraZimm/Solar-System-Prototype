//These are notes I usually jot down during coding sessions, so I can add updates to documentation later:

//USAGE NOTES
-  All designer-friendly resources are being quaratined to Resources > PlanetLoadInfo. Designers can be directed here if they want to modify planet values, textures, etc
-  The PlanetTextures asset in the PlanetLoadInfo folder updates automatically to match the Planets.csv asset in that folder. Direct designers to update the CSV first if they want to add new planets
-  To add/edit UI text in different languages, designers should be directed to "UIInfo_JSON" in the PlanetLoadInfo folder. Folder contains a JSON template called "_JSON_TEMPLATE" that designers can copy-paste when adding new languages.
		-  There should be 1 JSON file per supported language. The "ID" property for each planet MUST be the name of the planet in English, and match the planet name in the "Planets" CSV file.
		-  (In a larger project, I would structure this so that designers don't have to manually input the ID field, and potentially misspell/forget fields. But for a short project with 3 people, this should be fine.)

//ART ASSETS
Planet textures taken off of this free site:
- https://planetpixelemporium.com/sun.html

Credits for UI images of planets: Nasa Image Gallery
-  Mercury: https://images.nasa.gov/details/PIA11245
-  Venus: https://images.nasa.gov/details/PIA00104
-  Earth: https://images.nasa.gov/details/PIA18033
-  Mars: https://images.nasa.gov/details/PIA02697
-  Jupiter: https://images.nasa.gov/details/PIA00343
-  Saturn: https://images.nasa.gov/details/PIA02225
-  Uranus: https://images.nasa.gov/details/PIA18182
-  Neptune: https://images.nasa.gov/details/PIA00046