/*============================================================================== 
 Copyright (c) 2016 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using System.Collections.Generic;
using Vuforia;

/// <summary>
/// This comparer can be used to order detected words according to their 
/// position from left to right and top to bottom. 
/// The comparison is based on the oriented 2D bounding boxes of the words.
/// The comparison should be used after new words have been detected
/// because then the words are oriented in the same way as the device.
/// </summary>
public class ObbComparison : Comparer<WordResult>
{
    /// <summary>
    /// Returns 1 if word x is located after word y, i.e. it's x or y position is greater.
    /// Otherwise returns -1.
    /// </summary>
    public override int Compare(WordResult x, WordResult y)
    {
        var box1 = x.Obb;
        var box2 = y.Obb;

        var mid = (box1.Center + box2.Center)*0.5f;
        var min2 = box2.Center - box2.HalfExtents;
        var max2 = box2.Center + box2.HalfExtents;


        // we check first if both words are on the same line
        // both words are said to be on the same line if the
        // mid point (on Y axis) of the first point
        // is between the values of the second point
        if (mid.y < max2.y && mid.y > min2.y)
        {
            return CompareX(box1, box2);
        }
        return CompareY(box1, box2);

    }

    private static int CompareX(OrientedBoundingBox box1, OrientedBoundingBox box2)
    {
        return box1.Center.x > box2.Center.x ? 1 : -1;
    }

    private static int CompareY(OrientedBoundingBox box1, OrientedBoundingBox box2)
    {
        return box1.Center.y > box2.Center.y ? 1 : -1;
    }
}

