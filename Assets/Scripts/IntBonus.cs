using UnityEngine;

public class IntBonus : MonoBehaviour
{
    public UnitEffect_Container Effect;

    /// <summary>
    /// returns an effect based on the users int
    /// </summary>
    /// <param name="instigator"></param>
    /// <returns></returns>
    public virtual UnitEffect GetEffectForInstigator(int _int)
    {
        return null;
    }
}

