using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPromptObject : BasePromptObject
{
    public override string GetPromptText() {
        return "Mo�esz podnosi� obiekty wciskaj�c "
            + GameInput.Instance.GetBindingText(GameInput.Binding.Interactions)
            + " i upuszcza� je za pomoc� "
            + GameInput.Instance.GetBindingText(GameInput.Binding.Drop)
            + ". Pami�taj, �e odpowiadasz za wszelkie szkody materialne poniesione przez sprz�t laboratoryjny.";
    }
}
