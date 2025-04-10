using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangLaserPromptObject : BasePromptObject
{
    public override string GetPromptText() {
        return "Dzia³o laserowe mo¿esz zawiesiæ w powietrzu wciskaj¹c "
            + GameInput.Instance.GetBindingText(GameInput.Binding.HangWeapon)
            + " i nakierowaæ je przytrzymuj¹c "
            + GameInput.Instance.GetBindingText(GameInput.Binding.DirectHangedWeapon)
            + "."
            ;
    }
}
