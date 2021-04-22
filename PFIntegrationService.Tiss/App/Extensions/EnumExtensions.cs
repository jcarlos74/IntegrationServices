using System;
using System.Linq;
using System.Xml.Serialization;

namespace PFIntegrationService.Tiss.App.Extensions
{
    public static class EnumExtensions
    {

        /// <summary>
        /// Retorno o valor contindo no XmlAttribute de um Enum
        /// </summary>
        /// <returns></returns>
        public static string XmlValueFromEnum(this Enum value)
        {
            var enumType = typeof(Enum);

            if (!enumType.IsEnum) return null;

            var member = enumType.GetMember(value.ToString()).FirstOrDefault();
            
            if (member == null) return null;

            var attribute = member.GetCustomAttributes(false).OfType<XmlEnumAttribute>().FirstOrDefault();
            
            if (attribute == null) return null;

            return attribute.Name;
        }
    }
}
