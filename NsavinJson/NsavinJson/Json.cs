﻿using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace NsavinJson
{
    public class Json
    {
        
        public static string GetObject(object obj)
        {
            return Serialize(obj);
        }

        private static string Serialize(object resive)
        {
            var typeResive = resive.GetType();

            if (!typeResive.IsSerializable)
            {
                throw new SerializationException("Incorrect type");
            }


            if (typeResive.IsPrimitive)
            {
                return resive.ToString();
            }
            if (typeof(string) == typeResive)
            {
                return "\"" + resive + "\"";
            }
            if (typeResive.IsArray)
            {
                return SerializeArray((IEnumerable)resive);
            }
            var stringBuilder = new StringBuilder();
            var fieldInfos = typeResive.GetFields();

            for (var index = 0; index < fieldInfos.Length; index++)
            {
                var fieldInfo = fieldInfos[index];
                if (fieldInfo.GetCustomAttribute<NonSerializedAttribute>() != null)
                {
                    continue;
                }

                if (!StringBuilderIsEmpty(stringBuilder))
                {
                    stringBuilder.Append(", ");
                }
                if (StringBuilderIsEmpty(stringBuilder))
                {
                    stringBuilder.Append("{");
                }
                stringBuilder.AppendFormat("\"{0}\" : {1}", fieldInfo.Name,
                    Serialize(fieldInfo.GetValue(resive)));
            }

            stringBuilder.Append("}");
            
            return  stringBuilder.ToString();
        }

        private static string SerializeArray(IEnumerable arrayEnumerable)
        {
            var stringBuilder = new StringBuilder();
            foreach (var obj in arrayEnumerable)
            {
                if (!StringBuilderIsEmpty(stringBuilder))
                {
                    stringBuilder.Append(", ");
                }
                if (StringBuilderIsEmpty(stringBuilder))
                {
                    stringBuilder.Append("[");
                }
                stringBuilder.Append(Serialize(obj));
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        private static bool StringBuilderIsEmpty(StringBuilder stringBuilder)
        {
            return 0 == stringBuilder.Length;
        }
    }
}
