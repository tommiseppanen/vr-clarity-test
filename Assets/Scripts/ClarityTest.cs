using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClarityTest : MonoBehaviour
{
    [SerializeField]
    private GameObject ePrefab;
    [SerializeField]
    private GameObject ratingValuePrefab;

    private const float standardVision = 5.0f/60.0f;
    private const float distance = 6.0f;
    private float standardLetterSize = 1.0f;
    private const float startHeight = 1.9f;
    private const float horizontalOffset = -0.5f;

    // Start is called before the first frame update
    void Start()
    {
        standardLetterSize = 2.0f * distance * Mathf.Tan(Mathf.Deg2Rad*(standardVision / 2.0f));
        var spaceSize = standardLetterSize * 12.5f;
        
        for (int row = 1; row < 11; row++)
        {
            var visionRating = 0.1f * row;
            var rowLetterSize = standardLetterSize * (1.0f / visionRating);
            for (int letter = 0; letter < 6; letter++)
            {
                var newLetter = Instantiate(ePrefab, new Vector3(horizontalOffset+letter * spaceSize, startHeight - row * 1.25f *spaceSize, 0), Quaternion.Euler(0, 0, Random.Range(0, 4) * 90));        
                newLetter.transform.localScale = Vector3.one * rowLetterSize;
            }
            var rating = Instantiate(ratingValuePrefab, new Vector3(horizontalOffset+7 * spaceSize, startHeight - row * 1.25f * spaceSize, 0), Quaternion.identity);
            rating.transform.localScale = Vector3.one * standardLetterSize * 5.0f;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Camera.main.transform = new Vector3(0, 0, distance);
        }

        
    }
}
