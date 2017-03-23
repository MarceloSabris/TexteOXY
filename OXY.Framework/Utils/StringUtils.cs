using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OXY.Net.Framework.Utils
{
    public static class StringUtils
    {
        public static string RemoverAcentos(this string texto)
        {
            string retorno = "";
            if (string.IsNullOrEmpty(texto))
                return retorno;
            else
            {
                //Substituo acentuação por caracteres normais
                byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(texto);
                retorno = System.Text.Encoding.UTF8.GetString(bytes);

                //Removo caracteres especiais
                retorno = Regex.Replace(retorno, "[^a-zA-Z0-9 ]", string.Empty);
            }
            return retorno;
        }

        public static string RemoverAcentosEspacosECaracteresEspeciais(this string texto)
        {
            string textoTratado = texto.Normalize(NormalizationForm.FormD);

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < textoTratado.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(textoTratado[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(textoTratado[i]);
                }
            }

            textoTratado = Regex.Replace(builder.ToString(), @"[^0-9a-zA-Z]+", "");
            textoTratado = textoTratado.ToUpper();

            return textoTratado;
        }

        public static string SomenteNumeros(String str) 
        {
            return string.Join(null, System.Text.RegularExpressions.Regex.Split(str, "[^\\d]"));
        }

        public static string RetornarStringPreenchida(String str1, String str2)
        {
            if (String.IsNullOrEmpty(str1))
                return str2;

            return str1;
        }

        public static string RemoverFinal(String str, char caractere)
        {
            if (String.IsNullOrEmpty(str))
                return String.Empty;

            if (str.Length <= 0)
                return String.Empty;

            if (str.Substring(str.Length - 1, 1) == Convert.ToString(caractere))
            {
                return RemoverFinal(str.Substring(0, str.Length - 1), caractere);
            }
            else
            {
                return str;
            }
        }

        public static string InsertQuote(String str)
        {
            return "\"" + str.Trim() + "\"";
        }

        public static string DecodeBase64(this string base64)
        {
            byte[] data = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(data);
        }

        public static string EncodeBase64(this string texto)
        {
            byte[] data = Encoding.UTF8.GetBytes(texto);
            return Convert.ToBase64String(data);
        }

        public static string RemoverCaracteresInvalidos(String vStr)
        {
            if (String.IsNullOrEmpty(vStr))
                return string.Empty;
            vStr = vStr.Replace(@"#13#10", "<BR>");
            vStr = vStr.Replace(@"#13", "<BR>");
            vStr = vStr.Replace(@"#10", "<BR>");
            vStr = vStr.Replace(@"#0", "");
            vStr = vStr.Replace(@"#1","");
            vStr = vStr.Replace(@"#2","");
            vStr = vStr.Replace(@"#3","");
            vStr = vStr.Replace(@"#4","");
            vStr = vStr.Replace(@"#5","");
            vStr = vStr.Replace(@"#6","");
            vStr = vStr.Replace(@"#7","");
            vStr = vStr.Replace(@"#8","");
            vStr = vStr.Replace(@"#9","");
            vStr = vStr.Replace(@"#11","");
            vStr = vStr.Replace(@"#12","");
            vStr = vStr.Replace(@"#14","");
            vStr = vStr.Replace(@"#15","");
            vStr = vStr.Replace(@"#16","");
            vStr = vStr.Replace(@"#17","");
            vStr = vStr.Replace(@"#18","");
            vStr = vStr.Replace(@"#19","");
            vStr = vStr.Replace(@"#20","");
            vStr = vStr.Replace(@"#21","");
            vStr = vStr.Replace(@"#22","");
            vStr = vStr.Replace(@"#23","");
            vStr = vStr.Replace(@"#24","");
            vStr = vStr.Replace(@"#25","");
            vStr = vStr.Replace(@"#26","");
            vStr = vStr.Replace(@"#27","");
            vStr = vStr.Replace(@"#28","");
            vStr = vStr.Replace(@"#29","");
            vStr = vStr.Replace(@"#30","");
            vStr = vStr.Replace(@"#31","");            
            vStr = vStr.Replace(@"&", "&amp;");
            vStr = vStr.Replace(@"¡", "&#161;");
            vStr = vStr.Replace(@"¢", "&#162;");
            vStr = vStr.Replace(@"¥", "&#163;");
            vStr = vStr.Replace(@"¦", "&#166;");
            vStr = vStr.Replace(@"§", "&#167;");
            vStr = vStr.Replace(@"¨", "&#168;");
            vStr = vStr.Replace(@"©", "&#169;");
            vStr = vStr.Replace(@"ª", "&#170;");
            vStr = vStr.Replace(@"«", "&#171;");
            vStr = vStr.Replace(@"­", "&#173;");
            vStr = vStr.Replace(@"®", "&#174;");
            vStr = vStr.Replace(@"¯", "&#175;");
            vStr = vStr.Replace(@"°", "&#176;");
            vStr = vStr.Replace(@"±", "&#177;");
            vStr = vStr.Replace(@"²", "&#178;");
            vStr = vStr.Replace(@"³", "&#179;");
            vStr = vStr.Replace(@"´", "&#180;");
            vStr = vStr.Replace(@"µ", "&#181;");
            vStr = vStr.Replace(@"·", "&#182;");
            vStr = vStr.Replace(@"¸", "&#184;");
            vStr = vStr.Replace(@"¹", "&#185;");
            vStr = vStr.Replace(@"º", "&#186;");
            vStr = vStr.Replace(@"»", "&#187;");
            vStr = vStr.Replace(@"¼", "&#188;");
            vStr = vStr.Replace(@"½", "&#189;");
            vStr = vStr.Replace(@"¾", "&#190;");
            vStr = vStr.Replace(@"¿", "&#191;");
            vStr = vStr.Replace(@"×", "&#215;");
            vStr = vStr.Replace(@"Þ", "&#222;");
            vStr = vStr.Replace(@"÷", "&#247;");
            vStr = vStr.Replace(@"<", "&lt;");
            vStr = vStr.Replace(@">", "&gt;");
            vStr = vStr.Replace(StringUtils.InsertQuote(""), @"&quot;");
            vStr = vStr.Replace(@"\0", "");


            if (Convert.ToChar(vStr.Substring(vStr.Length - 1, 1)) == Convert.ToChar('\0'))
                vStr = vStr.Substring(0, vStr.Length - 1);

            byte[] utf8Bytes = Encoding.UTF8.GetBytes(vStr);
            return Encoding.UTF8.GetString(utf8Bytes);            
        }

        public static string SubstringMaxLength(this string texto, int startIndex, int length)
        {
            if (length + startIndex > texto.Length)
                length = texto.Length - startIndex;
            return texto.Substring(startIndex, length);
        }

        public static string EmptyIfNull(object str)
        {
            if (str == null)
            {
                return string.Empty;
            }
            return str.ToString();
        }

        public static bool EqualsStr(object str1, object str2)
        {
            return EmptyIfNull(str1).Trim().ToUpper().Equals(EmptyIfNull(str2).Trim().ToUpper());
        }
    }
}