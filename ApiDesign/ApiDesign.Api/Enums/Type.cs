using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesign.Api.Enums
{
    public enum Type
    {
        [Description("Get")]
        GET,
        [Description("Post")]
        POST,
        [Description("Put")]
        PUT,
        [Description("Patch")]
        PATCH,
        [Description("Delete")]
        DELETE
    }
}
