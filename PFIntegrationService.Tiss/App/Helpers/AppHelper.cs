using PFIntegrationService.Tiss.Models.V3_05_00;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PFIntegrationService.Tiss.App.Helpers
{
    public static class AppHelper
    {
        /// <summary>
        /// Retorna o valor do Enum com base no seu XmlEnumAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetXmlEnumValue<T>(object name)
        {
            var type = CheckEnum<T>();

            var val = (from f in type.GetFields()
                       let attribute = f.GetCustomAttributes(typeof(System.Xml.Serialization.XmlEnumAttribute), true).FirstOrDefault() as System.Xml.Serialization.XmlEnumAttribute
                       where attribute != null
                          && attribute.Name == name.ToString()
                       select (T)f.GetValue(null));


            //Fazer checagem por Atributo e descricao

            //var objectEnum = (T)Enum.Parse(typeof(T), name.ToString(), true);

            if (val.Count() == 0)
            {

                throw new ArgumentException(string.Format("O valor {0} não é válido para o Tipo {1}", name, typeof(T).FullName), "name");
            }


            return val.First();
        }


        public static T GetXmlEnumValue<T>(object name, string complemento)
        {
            var type = CheckEnum<T>();

            var val = (from f in type.GetFields()
                       let attribute = f.GetCustomAttributes(typeof(System.Xml.Serialization.XmlEnumAttribute), true).FirstOrDefault() as System.Xml.Serialization.XmlEnumAttribute
                       where attribute != null
                          && attribute.Name == name.ToString()
                       select (T)f.GetValue(null));

            if (val.Count() == 0)
            {

                var msg = string.Format("O valor : {0} não é válido para o Tipo {1}", name, typeof(T).FullName);

                msg += "\n" + complemento;

                throw new ArgumentException(msg, "name");

            }


            return val.First();
        }

        /// <summary>
        /// Retorno o valor contindo no XmlAttribute de um Enum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetXmlEnumAttributeValueFromEnum<TEnum>(TEnum value) where TEnum : struct, IConvertible
        {
            var enumType = typeof(TEnum);

            if (!enumType.IsEnum) return null;

            var member = enumType.GetMember(value.ToString()).FirstOrDefault();
            if (member == null) return null;

            var attribute = member.GetCustomAttributes(false).OfType<XmlEnumAttribute>().FirstOrDefault();
            if (attribute == null) return null;
            return attribute.Name;
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = CheckEnum<T>();

            var val = (from f in type.GetFields()
                       let attribute = f.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true).FirstOrDefault() as System.ComponentModel.DescriptionAttribute
                       where attribute != null
                          && attribute.Description == description
                       select (T)f.GetValue(null));

            if (val.Count() == 0)
                throw new ArgumentException(string.Format("{0} não é válido para a descrição {1}", description, typeof(T).FullName), "description");

            return val.First();
        }

        private static Type CheckEnum<T>()
        {
            var type = typeof(T);
            if (type.IsEnum == false)
                throw new InvalidOperationException(string.Format("{0} não é um Enum", typeof(T)));
            return type;
        }

        /// <summary>
        /// retorna o xml de uma classe em uma string
        /// </summary>
        /// <param name="obj">instancia da classe com o XML</param>
        /// <returns></returns>
        public static string GetTissXMLString(Object obj)
        {
            MemoryStream memoryStream = new MemoryStream();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());

            ns.Add("ans", "http://www.ans.gov.br/padroes/tiss/schemas");

            XmlTextWriter xmlTextWriter;

            xmlTextWriter = new XmlTextWriter(memoryStream, new System.Text.UTF8Encoding(false));

            xs.Serialize(xmlTextWriter, obj, ns);

            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

            Byte[] characters = memoryStream.ToArray();

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            return encoding.GetString(characters);
        }

        public static string GetPathSchemaTiss(dm_versao versaoTiss)
        {
            var caminhoSchemaTiss = string.Empty;

            if (versaoTiss == dm_versao.Item30500)
            {
                caminhoSchemaTiss = AppDomain.CurrentDomain.BaseDirectory + AppConst.PATH_SCHEMA_V30500; 
            }

            return caminhoSchemaTiss;

        }

        /// <summary>
        /// Converte um String para objeto XmlDocument
        /// </summary>
        /// <param name="strXML">String em formato Xml</param>
        /// <returns>Objeto XmlDocument</returns>
        public static XmlDocument StringToXmlDocument(String strXML)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(new StringReader(strXML));

            return XmlDoc;
        }

        /// <summary>
        /// Converte o conteúdo de um objeto XmlDocument para String
        /// </summary>
        /// <param name="XmlDoc">Objeto XmlDocument</param>
        /// <returns>String com o conteúdo Xml</returns>
        public static String XmlDocumentToString(XmlDocument XmlDoc)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter xtw = new XmlTextWriter(sw);
            XmlDoc.WriteTo(xtw);

            return sw.ToString();
        }

        public static string GeraHashMD5(string texto)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(texto);
            byte[] hash = md5.ComputeHash(inputBytes);

            var sb = new System.Text.StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
