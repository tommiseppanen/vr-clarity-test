using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClarityTest : MonoBehaviour
{
    [SerializeField]
    private GameObject ePrefab;
    [SerializeField]
    private GameObject ratingValuePrefab;

    private const float letterMultiplier = 0.023f;

    // Start is called before the first frame update
    void Start()
    {
        for (int row = 1; row < 11; row++)
        {
            var visionRating = 0.1f * row;
            for (int letter = 0; letter < 6; letter++)
            {
                var newLetter = Instantiate(ePrefab, new Vector3(letter * 0.03f, 1.7f - row * 0.05f, 0), Quaternion.Euler(0, 0, Random.Range(0, 4) * 90));        
                newLetter.transform.localScale = Vector3.one * letterMultiplier * 0.1f / visionRating;//(row * 0.01f + 0.02f);
            }
            var rating = Instantiate(ratingValuePrefab, new Vector3(7 * 0.03f, 1.7f - row * 0.05f, 0), Quaternion.identity);
            rating.transform.localScale = Vector3.one * letterMultiplier;
            var textMesh = rating.GetComponent(typeof(TextMesh)) as TextMesh;
            textMesh.text = visionRating.ToString("0.0#");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Plus))
        {
            UnityEngine.XR.XRSettings.eyeTextureResolutionScale += 0.25f;
            Debug.Log(UnityEngine.XR.XRSettings.eyeTextureResolutionScale);
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            UnityEngine.XR.XRSettings.eyeTextureResolutionScale -= 0.25f;
            Debug.Log(UnityEngine.XR.XRSettings.eyeTextureResolutionScale);
        }
    }
}
