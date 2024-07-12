using UnityEngine;

namespace Main
{
    public abstract class InputController : ScriptableObject
    {
        public abstract float RetrieveMoveInput();
        public abstract bool RetrieveJumpInput();
    }
}
