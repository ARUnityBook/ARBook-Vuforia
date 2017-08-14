/*===============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.

Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Vuforia;

/// <summary>
/// Specialized tap handler class for video playback.
/// </summary>
public class VuMarkTapHandler : TapHandler
{
    #region PUBLIC_MEMBERS
    public GameObject VuMarkCard;
    #endregion //PUBLIC_MEMBERS


    #region PROTECTED_METHODS
    protected override void OnSingleTap()
    {
        base.OnSingleTap();

        // Check if we hit the vumark card panel
        if (CheckVuMarkCardHit())
        {
            // Reset the tap count, 
            // cause the tap event is fully consumed by closing the VuMark card
            mTapCount = 0;
        }
    }
    #endregion //PROTECTED_METHODS


    #region PRIVATE_METHODS
    private bool CheckVuMarkCardHit()
    {
        var hits = new List<RaycastResult>();
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        EventSystem.current.RaycastAll(pointer, hits);
        for (int k = 0; k < hits.Count; k++)
        {
            GameObject hitObj = hits[k].gameObject;
            if (VuMarkCard && (hitObj == VuMarkCard))
            {
                Animator cardAnimator = VuMarkCard.GetComponent<Animator>();
                if (cardAnimator)
                    cardAnimator.SetTrigger("HidePanel");

                return true;
            }
        }
        return false;
    }
    #endregion //PRIVATE_METHODS
}
