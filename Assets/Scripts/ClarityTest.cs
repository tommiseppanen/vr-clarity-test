using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClarityTest : MonoBehaviour
{
    [SerializeField]
    private GameObject ePrefab;
    [SerializeField]
    private GameObject ratingValuePrefab;
    [SerializeField]
    private GameObject cameraRig;

    private const float standardVision = 5.0f/60.0f;
    private const float distance = 6.0f;
    private const float startHeight = 2.0f;
    private const float horizontalOffset = -0.3f;

    // Start is called before the first frame update
    void Start()
    {
        var standardLetterSize = 2.0f * distance * Mathf.Tan(Mathf.Deg2Rad*(standardVision / 2.0f));
        Debug.Log(standardLetterSize);
        var spaceSize = standardLetterSize * 12.5f;
        
        for (int row = 1; row < 11; row++)
        {
            var visionRating = 0.1f * row;
            var rowLetterSize = standardLetterSize * (1.0f / visionRating);
            var rowOffset = startHeight - row * 1.25f * spaceSize;
            for (int letter = 0; letter < 6; letter++)
            {
                var newLetter = Instantiate(ePrefab, new Vector3(horizontalOffset+letter * spaceSize, rowOffset, 0), Quaternion.Euler(0, 0, Random.Range(0, 4) * 90));        
                newLetter.transform.localScale = Vector3.one * rowLetterSize;
            }
            var rating = Instantiate(ratingValuePrefab, new Vector3(horizontalOffset+7 * spaceSize, rowOffset, 0), Quaternion.identity);
            rating.transform.localScale = Vector3.one * standardLetterSize * 5.0f;
            var textMesh = rating.GetComponent(typeof(TextMesh)) as TextMesh;
            textMesh.text = visionRating.ToString("0.0#");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            UnityEngine.XR.XRSettings.eyeTextureResolutionScale += 0.25f;
            Debug.Log(UnityEngine.XR.XRSettings.eyeTextureResolutionScale);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UnityEngine.XR.XRSettings.eyeTextureResolutionScale -= 0.25f;
            Debug.Log(UnityEngine.XR.XRSettings.eyeTextureResolutionScale);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveCameraRig(0, 0, distance / 3.0f);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            MoveCameraRig(0, 0, -distance / 3.0f);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.XR.InputTracking.Recenter();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCameraRig(0, -0.1f);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCameraRig(0, 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveCameraRig(-0.1f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveCameraRig(0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            MoveCameraRig(0,0, 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.End))
        {
            MoveCameraRig(0,0, -0.1f);
        }
    }

    private void MoveCameraRig(float x, float y = 0.0f, float z = 0.0f)
    {
        cameraRig.transform.position = new Vector3(cameraRig.transform.position.x + x, cameraRig.transform.position.y + y, cameraRig.transform.position.z + z);
    }
}
