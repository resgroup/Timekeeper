using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RES_Timekeeper.Data;

namespace RES_Timekeeper.Unit_Tests
{
    public class DatabaseTests
    {
        public void CreateDatabase()
        {
            Database db = new Database("Test.db");
        }
    }
}
