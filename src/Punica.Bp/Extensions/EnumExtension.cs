using System.ComponentModel;
using System.Reflection;

namespace Punica.Bp.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
            return attribute != null ? attribute.Description : value.ToString();
        }

        public static T? GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attribute = memInfo[0].GetCustomAttribute(typeof(T), false);
            return (T?)attribute;
        }
    }
}