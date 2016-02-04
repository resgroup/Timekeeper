using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RES_Timekeeper.Base
{
    public interface IDatabaseObject
    {
        void Save();

        bool IsDirty {get;}
        bool IsNew {get;}
        bool IsDeleted {get;}
    }
}
