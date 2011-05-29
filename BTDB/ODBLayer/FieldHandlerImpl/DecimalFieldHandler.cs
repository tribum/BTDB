﻿using System;
using BTDB.IL;
using BTDB.KVDBLayer.ReaderWriters;

namespace BTDB.ODBLayer.FieldHandlerImpl
{
    public class DecimalFieldHandler : SimpleFieldHandlerBase
    {
        public DecimalFieldHandler(): base(
            EmitHelpers.GetMethodInfo(() => ((AbstractBufferedReader)null).ReadDecimal()),
            EmitHelpers.GetMethodInfo(() => ((AbstractBufferedReader)null).SkipDecimal()),
            EmitHelpers.GetMethodInfo(() => ((AbstractBufferedWriter)null).WriteDecimal(0)))
        {
        }

        public override string Name
        {
            get { return "Decimal"; }
        }
    }
}