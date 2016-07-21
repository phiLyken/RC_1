using UnityEngine;

public class IntBonus : MonoBehaviour
{
   

    /// <summary>
    /// returns an effect based on the users int
    /// </summary>
    /// <param name="instigator"></param>
    /// <returns></returns>
    public virtual UnitEffect GetEffectForInstigator(int _int)
    {
        return null;
    }


    public virtual string IntBonusText()
    {
        return "does something based on int";
    }
}

