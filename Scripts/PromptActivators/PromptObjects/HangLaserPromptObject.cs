using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangLaserPromptObject : BasePromptObject
{
    public override string GetPromptText() {
        return "Dzia�o laserowe mo�esz zawiesi� w powietrzu wciskaj�c "
            + GameInput.Instance.GetBindingText(GameInput.Binding.HangWeapon)
            + " i nakierowa� je przytrzymuj�c "
            + GameInput.Instance.GetBindingText(GameInput.Binding.DirectHangedWeapon)
            + "."
            ;
    }
}
