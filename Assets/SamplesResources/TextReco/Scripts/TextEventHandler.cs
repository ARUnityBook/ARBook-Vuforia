/*===============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.
Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
/// A custom event handler for TextReco-events
/// </summary>
public class TextEventHandler : MonoBehaviour, ITextRecoEventHandler, IVideoBackgroundEventHandler
{
    #region PRIVATE_MEMBERS
    // Size of text search area in percentage of screen
    private float mLoupeWidth = 0.9f;
    private float mLoupeHeight = 0.15f;
    
    // Line width of viusalized boxes around detected words
    private float mBBoxLineWidth = 3.0f;
    // Padding between detected words and visualized boxes
    private float mBBoxPadding = 0.0f;
    // Color of visualized boxes around detected words
    private Color mBBoxColor = new Color(1.0f, 0.447f, 0.0f, 1.0f);

    private Rect mDetectionAndTrackingRect;
    private Texture2D mBoundingBoxTexture;
    private Material mBoundingBoxMaterial;

    private bool mIsInitialized;
    private bool mVideoBackgroundChanged;

    private readonly List<WordResult> mSortedWords = new List<WordResult>();
    private Text[] mDisplayedWords;

    [SerializeField] 
    private Material boundingBoxMaterial = null;
    #endregion //PRIVATE_MEMBERS


    #region PUBLIC_MEMBERS
    public Canvas textRecoCanvas;
    #endregion //PUBLIC_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    public void Start()
    {
        // create the texture for bounding boxes
        mBoundingBoxTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        mBoundingBoxTexture.SetPixel(0, 0, mBBoxColor);
        mBoundingBoxTexture.Apply(false);

        mBoundingBoxMaterial = new Material(boundingBoxMaterial);
        mBoundingBoxMaterial.SetTexture("_MainTex", mBoundingBoxTexture);

        // register to TextReco events
        var trBehaviour = GetComponent<TextRecoBehaviour>();
        if (trBehaviour)
        {
            trBehaviour.RegisterTextRecoEventHandler(this);
        }

        // register for the OnVideoBackgroundConfigChanged event at the VuforiaBehaviour
        VuforiaARController.Instance.RegisterVideoBgEventHandler(this);

        mDisplayedWords = textRecoCanvas ? textRecoCanvas.GetComponentsInChildren<Text>(true) : new Text[0];
    }

    void OnRenderObject()
    {
        DrawWordBoundingBoxes();
    }

    void Update()
    {   
        if (mIsInitialized)
        {
            // Once the text tracker has initialized and every time the video background changed,
            // set the region of interest
            if (mVideoBackgroundChanged)
            {
                TextTracker textTracker = TrackerManager.Instance.GetTracker<TextTracker>();
                if (textTracker != null)
                {
                    CalculateLoupeRegion();
                    textTracker.SetRegionOfInterest(mDetectionAndTrackingRect, mDetectionAndTrackingRect);
                }
                mVideoBackgroundChanged = false;
            }
            
            // Clear the content of the displayed words
            foreach (var dw in mDisplayedWords)
            {
                dw.text = "";
            }

            // Update the list of words displayed
            int wordIndex = 0;
            foreach (var word in mSortedWords)
            {
                if (word.Word != null && wordIndex < mDisplayedWords.Length)
                {
                    mDisplayedWords[wordIndex].text = word.Word.StringValue;
                }
                wordIndex++;
            }
        }
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region ITextRecoEventHandler_IMPLEMENTATION
    /// <summary>
    /// Called when text reco has finished initializing
    /// </summary>
    public void OnInitialized()
    {
        CalculateLoupeRegion();
        mIsInitialized = true;
    }

    /// <summary>
    /// This method is called whenever a new word has been detected
    /// </summary>
    /// <param name="wordResult">New trackable with current pose</param>
    public void OnWordDetected(WordResult wordResult)
    {
        var word = wordResult.Word;
        if (ContainsWord(word))
            Debug.LogError("Word was already detected before!");


        Debug.Log("Text: New word: " + wordResult.Word.StringValue + "(" + wordResult.Word.ID + ")");
        AddWord(wordResult);
    }

    /// <summary>
    /// This method is called whenever a tracked word has been lost and is not tracked anymore
    /// </summary>
    public void OnWordLost(Word word)
    {
        if (!ContainsWord(word))
            Debug.LogError("Non-existing word was lost!");

        Debug.Log("Text: Lost word: " + word.StringValue + "(" + word.ID + ")");

        RemoveWord(word);
    }
    #endregion //PUBLIC_METHODS

    
    #region IVideoBackgroundEventHandler_IMPLEMENTATION
    // set a flag that the video background has changed. This means the region of interest has to be set again.
    public void OnVideoBackgroundConfigChanged()
    {
        mVideoBackgroundChanged = true;
    }
    #endregion // IVideoBackgroundEventHandler_IMPLEMENTATION


    #region PRIVATE_METHODS
    /// <summary>
    /// Draw a 3d bounding box around each currently tracked word
    /// </summary>
    private void DrawWordBoundingBoxes()
    {
        // render a quad around each currently tracked word
        foreach (var word in mSortedWords)
        {
            var pos = word.Position;
            var orientation = word.Orientation;
            var size = word.Word.Size;
            var pose = Matrix4x4.TRS(pos, orientation, new Vector3(size.x, 1, size.y));

            var cornersObject = new[]
                {
                    new Vector3(-0.5f, 0.0f, -0.5f), new Vector3(0.5f, 0.0f, -0.5f),
                    new Vector3(0.5f, 0.0f, 0.5f), new Vector3(-0.5f, 0.0f, 0.5f)
                };
            var corners = new Vector2[cornersObject.Length];
            for (int i = 0; i < cornersObject.Length; i++)
                corners[i] = Camera.current.WorldToScreenPoint(pose.MultiplyPoint(cornersObject[i]));

            DrawBoundingBox(corners);
        }
    }

    private void DrawBoundingBox(Vector2[] corners)
    {
        var normals = new Vector2[4];
        for (var i = 0; i < 4; i++)
        {
            var p0 = corners[i];
            var p1 = corners[(i + 1)%4];
            normals[i] = (p1 - p0).normalized;
            normals[i] = new Vector2(normals[i].y, -normals[i].x);
        }

        //add padding to inner corners
        corners = ExtendCorners(corners, normals, mBBoxPadding);
        //computer outer corners
        var outerCorners = ExtendCorners(corners, normals, mBBoxLineWidth);

        //create vertices in screen space
        var vertices = new Vector3[8];
        float depth = 1.02f * Camera.current.nearClipPlane;
        for (var i = 0; i < 4; i++)
        {
            vertices[i] = new Vector3(corners[i].x, corners[i].y, depth);
            vertices[i + 4] = new Vector3(outerCorners[i].x, outerCorners[i].y, depth);
        }

        //transform vertices into world space
        for (int i = 0; i < 8; i++)
            vertices[i] = Camera.current.ScreenToWorldPoint(vertices[i]);

        var mesh = new Mesh()
            {
                vertices = vertices,
                uv = new Vector2[8],
                triangles = new[]
                    {
                        0, 5, 4, 1, 5, 0,
                        1, 6, 5, 2, 6, 1,
                        2, 7, 6, 3, 7, 2,
                        3, 4, 7, 0, 4, 3
                    },
            };

        mBoundingBoxMaterial.SetPass(0);
        Graphics.DrawMeshNow(mesh, Matrix4x4.identity);
        Destroy(mesh);
    }

    private static Vector2[] ExtendCorners(Vector2[] corners, Vector2[] normals, float extension)
    {
        //compute positions along the outer side of the boundary
        var linePoints = new Vector2[corners.Length * 2];
        for (var i = 0; i < corners.Length; i++)
        {
            var p0 = corners[i];
            var p1 = corners[(i + 1) % 4];

            var po0 = p0 + normals[i] * extension;
            var po1 = p1 + normals[i] * extension;
            linePoints[i * 2] = po0;
            linePoints[i * 2 + 1] = po1;
        }

        //compute corners of outer side of bounding box lines
        var outerCorners = new Vector2[corners.Length];
        for (var i = 0; i < corners.Length; i++)
        {
            var i2 = i * 2;
            outerCorners[(i + 1) % 4] = IntersectLines(linePoints[i2], linePoints[i2 + 1], linePoints[(i2 + 2) % 8],
                                             linePoints[(i2 + 3) % 8]);
        }
        return outerCorners;
    }

    /// <summary>
    /// Intersect the line p1-p2 with the line p3-p4
    /// </summary>
    private static Vector2 IntersectLines(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        var denom = (p1.x - p2.x) * (p3.y - p4.y) - (p1.y - p2.y) * (p3.x - p4.x);
        var x = ((p1.x * p2.y - p1.y * p2.x) * (p3.x - p4.x) - (p1.x - p2.x) * (p3.x * p4.y - p3.y * p4.x)) / denom;
        var y = ((p1.x * p2.y - p1.y * p2.x) * (p3.y - p4.y) - (p1.y - p2.y) * (p3.x * p4.y - p3.y * p4.x)) / denom;
        return new Vector2(x, y);
    }

    private void AddWord(WordResult wordResult)
    {
        //add new word into sorted list
        var cmp = new ObbComparison();
        int i = 0;
        while (i < mSortedWords.Count && cmp.Compare(mSortedWords[i], wordResult) < 0)
        {
            i++;
        }

        if (i < mSortedWords.Count)
        {
            mSortedWords.Insert(i, wordResult);
        }
        else
        {
            mSortedWords.Add(wordResult);
        }
    }

    private void RemoveWord(Word word)
    {
        for (int i = 0; i < mSortedWords.Count; i++)
        {
            if (mSortedWords[i].Word.ID == word.ID)
            {
                mSortedWords.RemoveAt(i);
                break;
            }
        }
    }

    private bool ContainsWord(Word word)
    {
        foreach (var w in mSortedWords)
            if (w.Word.ID == word.ID)
                return true;
        return false;
    }

    private void CalculateLoupeRegion()
    {
        // define area for text search
        var loupeWidth = mLoupeWidth * Screen.width;
        var loupeHeight = mLoupeHeight * Screen.height;
        var leftOffset = (Screen.width - loupeWidth) * 0.5f;
        var topOffset = leftOffset;
        mDetectionAndTrackingRect = new Rect(leftOffset, topOffset, loupeWidth, loupeHeight);
    }
    #endregion //PRIVATE_METHODS
}

