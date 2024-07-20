using System.ComponentModel;
using System.Reflection;

namespace JobListAPI.Extensions;

public static class EnumToDescription
{
    public static string ToDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute == null ? value.ToString() : attribute.Description;
    }
}
