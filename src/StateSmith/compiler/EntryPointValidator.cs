﻿namespace StateSmith.Compiling;

public class EntryPointValidator
{
    public static void Validate(EntryPoint state)
    {
        PseudoStateValidator.ValidateParentAndNoChildren(state);
        PseudoStateValidator.ValdiateEnteringBehaviors(state);
    }
}
