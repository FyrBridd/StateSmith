using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StateSmith.Output.UserConfig;

public interface IRenderConfigC
{
    /// <summary>
    /// Whatever this property returns will be placed at the top of the rendered .h file.
    /// </summary>
    string HFileTop => StringUtils.DeIndentTrim(@"
            // Autogenerated with StateSmith
        ");

    string HFileIncludes => "";
    string CFileIncludes => "";

    /// <summary>
    /// Whatever this property returns will be placed at the top of the rendered .c file.
    /// </summary>
    string CFileTop => StringUtils.DeIndentTrim(@"
            // Autogenerated with StateSmith
        ");

    string VariableDeclarations => "";

    /// <summary>
    /// https://github.com/StateSmith/StateSmith/issues/91
    /// </summary>
    string AutoExpandedVars => "";

    /// <summary>
    /// Not used yet. A comma seperated list of allowed event names. TODO case sensitive?
    /// </summary>
    string EventCommaList => "";
}

public class DummyRenderConfigC : IRenderConfigC
{
    
}
