using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI_Action_Bar_Skip : MonoBehaviour, IToolTip {



    public  object GetItem()
    {
        return new GenericToolTipTarget("Ends turn.\n\nThe next turn of the unit will be sooner.");
    }
}
