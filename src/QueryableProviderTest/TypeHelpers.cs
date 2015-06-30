using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QueryableProviderTest
{
    internal static class TypeSystem
    {
        public static TypeCode GetTypeCode(this Type type)
        {
            if (type == null)
                return TypeCode.Empty;
            else if (type == typeof(bool))
                return TypeCode.Boolean;
            else if (type == typeof(char))
                return TypeCode.Char;
            else if (type == typeof(sbyte))
                return TypeCode.SByte;
            else if (type == typeof(byte))
                return TypeCode.Byte;
            else if (type == typeof(short))
                return TypeCode.Int16;
            else if (type == typeof(ushort))
                return TypeCode.UInt16;
            else if (type == typeof(int))
                return TypeCode.Int32;
            else if (type == typeof(uint))
                return TypeCode.UInt32;
            else if (type == typeof(long))
                return TypeCode.Int64;
            else if (type == typeof(ulong))
                return TypeCode.UInt64;
            else if (type == typeof(float))
                return TypeCode.Single;
            else if (type == typeof(double))
                return TypeCode.Double;
            else if (type == typeof(decimal))
                return TypeCode.Decimal;
            else if (type == typeof(System.DateTime))
                return TypeCode.DateTime;
            else if (type == typeof(string))
                return TypeCode.String;
            else if (type.GetTypeInfo().IsEnum)
                return GetTypeCode(Enum.GetUnderlyingType(type));
            else
                return TypeCode.Object;
        }

        internal static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            if (ienum == null) return seqType;
            return ienum.GetGenericArguments()[0];
        }
        private static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.GetTypeInfo().IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }
            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null) return ienum;
                }
            }

            var basetype = seqType.GetTypeInfo().BaseType;
            if (basetype != null && basetype != typeof(object))
            {
                return FindIEnumerable(basetype);
            }
            return null;
        }
    }
}
