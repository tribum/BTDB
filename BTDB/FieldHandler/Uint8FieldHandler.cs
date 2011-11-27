using System;
using BTDB.IL;
using BTDB.StreamLayer;

namespace BTDB.FieldHandler
{
    public class Uint8FieldHandler : SimpleFieldHandlerBase, IFieldHandler
    {
        public Uint8FieldHandler()
            : base(
                EmitHelpers.GetMethodInfo(() => ((AbstractBufferedReader)null).ReadUInt8()),
                EmitHelpers.GetMethodInfo(() => ((AbstractBufferedReader)null).SkipUInt8()),
                EmitHelpers.GetMethodInfo(() => ((AbstractBufferedWriter)null).WriteUInt8(0)))
        {
        }

        public override string Name
        {
            get { return "UInt8"; }
        }

        public static bool IsCompatibleWith(Type type)
        {
            return type == typeof(byte);
        }

        bool IFieldHandler.IsCompatibleWith(Type type, FieldHandlerOptions options)
        {
            return IsCompatibleWith(type);
        }
    }
}