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

    private const float StandardVision = 5.0f/60.0f;
    private const float Distance = 6.0f;
    private const float StartHeight = 2.0f;
    private const float HorizontalOffset = -0.3f;

    private const int AmountOfLetters = 6;
    private const int AmountOfRows = 10;

    private const float RatingTextMultiplier = 5.0f;
    private const string RatingFormat = "0.0#";

    private const float LineHeight = 1.25f;
    private const float LetterSpacing = 12.5f;

    private const float FirstRowRating = 0.1f;

    // Start is called before the first frame update
    void Start() 
    {
        CreateLetters();
    }

    void CreateLetters()
    {
        //Official formula
        var standardLetterSize = 2.0f * Distance * Mathf.Tan(Mathf.Deg2Rad * (StandardVision / 2.0f));
        var spaceSize = standardLetterSize * LetterSpacing;
        for (int row = 1; row <= AmountOfRows; row++)
        {
            CreateRow(row, standardLetterSize, spaceSize);
        }
    }

    void CreateRow(int row, float standardLetterSize, float spaceSize)
    {
        var visionRating = FirstRowRating * row;
        var rowLetterSize = standardLetterSize * (1.0f / visionRating);
        var rowOffset = StartHeight - row * LineHeight * spaceSize;
        for (int letter = 0; letter < AmountOfLetters; letter++)
        {
            var newLetter = Instantiate(ePrefab, new Vector3(HorizontalOffset + letter * spaceSize, rowOffset, 0), 
                Quaternion.Euler(0, 0, Random.Range(0, 4) * 90));
            newLetter.transform.localScale = Vector3.one * rowLetterSize;
        }
        CreateRatingText(visionRating, standardLetterSize, spaceSize, rowOffset);
    }

    void CreateRatingText(float visionRating, float standardLetterSize, float spaceSize, float rowOffset)
    {
        var rating = Instantiate(ratingValuePrefab, 
            new Vector3(HorizontalOffset + (AmountOfLetters+1) * spaceSize, rowOffset, 0), Quaternion.identity);
        rating.transform.localScale = Vector3.one * standardLetterSize * RatingTextMultiplier;
        var textMesh = rating.GetComponent(typeof(TextMesh)) as TextMesh;
        textMesh.text = visionRating.ToString(RatingFormat);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MoveCameraRig(0, 0, Distance / 3.0f);

        if (Input.GetKeyDown(KeyCode.LeftAlt))
            MoveCameraRig(0, 0, -Distance / 3.0f);

        if (Input.GetKeyDown(KeyCode.Return))
            UnityEngine.XR.InputTracking.Recenter();

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveCameraRig(0, -0.1f);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            MoveCameraRig(0, 0.1f);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveCameraRig(-0.1f);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveCameraRig(0.1f);

        if (Input.GetKeyDown(KeyCode.Home))
            MoveCameraRig(0, 0, 0.1f);

        if (Input.GetKeyDown(KeyCode.End))
            MoveCameraRig(0, 0, -0.1f);

        if (Input.GetKeyDown(KeyCode.X))
            AdjustPixelDensity(0.25f);

        if (Input.GetKeyDown(KeyCode.Z))
            AdjustPixelDensity(-0.25f);
    }

    private void MoveCameraRig(float x, float y = 0.0f, float z = 0.0f)
    {
        cameraRig.transform.position = new Vector3(cameraRig.transform.position.x + x, 
            cameraRig.transform.position.y + y, cameraRig.transform.position.z + z);
    }

    private void AdjustPixelDensity(float densityDelta)
    {
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale += densityDelta;
        Debug.Log(UnityEngine.XR.XRSettings.eyeTextureResolutionScale);
    }
}
