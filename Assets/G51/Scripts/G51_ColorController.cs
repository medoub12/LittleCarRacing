using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class G51_ColorController : MonoBehaviour {
    public bool refresh;
    public Color edge;
    Color _edge;
    public Color fill;
    Color _fill;
    public Color decor;
    Color _decor;
	
	// Update is called once per frame
	void Update ()
    {
		if(_edge != edge || refresh)
        {
            var allRock = FindObjectsOfTypeAll(typeof(G51_RockColorizer));
            foreach (G51_RockColorizer rock in allRock)
            {
                rock.SetColorOutline(edge);
            }
            _edge = edge;
        }
        if(_fill!= fill || refresh)
        {
            var allRock = FindObjectsOfTypeAll(typeof(G51_RockColorizer));
            foreach (G51_RockColorizer rock in allRock)
            {
                rock.SetColorFill(fill);
            }
            _fill = fill;
        }
        if (_decor != decor || refresh)
        {
            var allRock = FindObjectsOfTypeAll(typeof(G51_RockColorizer));
            foreach (G51_RockColorizer rock in allRock)
            {
                rock.SetColorDecor(decor);
            }
            _decor = decor;
        }
        if (refresh) refresh = false;
    }
}
