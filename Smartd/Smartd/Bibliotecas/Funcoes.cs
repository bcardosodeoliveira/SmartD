using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.Bibliotecas
{
    public static class Funcoes
    {
        public static async Task Alerta(this Page Tela, string Mensagem, string Titulo = "Alerta", string Botao = "OK")
        {
            await Tela.DisplayAlert(Titulo, Mensagem, Botao);
        }
        public static async Task<bool> Confirmacao(this Page Tela, string Mensagem, string Titulo = "Alerta", string Botao1 = "SIM", string Botao2 = "NÃO")
        {
            return await Tela.DisplayAlert(Titulo, Mensagem, Botao1, Botao2);
        }

        public static async Task<string> PopUp(this Page Tela, string Titulo, string[] Opcoes, string Botao = "Cancelar", string BotaoDelete = null)
        {
            return await Tela.DisplayActionSheet(Titulo, Botao, BotaoDelete, Opcoes);
        }

        public static void Toast(string pstrMensagem, ToastAction pToastAction = null)
        {
            ToastConfig toastConfig = new ToastConfig(pstrMensagem)
            {
                Duration = TimeSpan.FromSeconds(6),
                Position = ToastPosition.Bottom,
                Action = pToastAction
            };
            UserDialogs.Instance.Toast(toastConfig);
        }

        public static async Task Navegar(this Page TelaAtual, Page ProximaTela)
        {
            await TelaAtual.Navigation.PushAsync(ProximaTela);
        }

        public static async Task AbrirModal(this Page TelaAtual, Page ProximaTela)
        {
            await TelaAtual.Navigation.PushModalAsync(ProximaTela);
        }

        public static async Task Voltar(this Page TelaAtual)
        {
            await TelaAtual.Navigation.PopAsync();
        }

        public static async Task FecharModal(this Page TelaAtual)
        {
            await TelaAtual.Navigation.PopModalAsync();
        }

        public static bool IsNullOrEmpty(this string Valor)
        {
            if (string.IsNullOrEmpty(Valor) || string.IsNullOrWhiteSpace(Valor))
                return true;

            return false;
        }

        public static bool IsNullOrEmpty(this StringBuilder Valor)
        {
            if (string.IsNullOrEmpty(Valor.ToString()) || string.IsNullOrWhiteSpace(Valor.ToString()))
                return true;

            return false;
        }

        public static bool IsNullOrEmpty(this string Valor, int Lenght)
        {
            if (string.IsNullOrEmpty(Valor) || string.IsNullOrWhiteSpace(Valor))
                return true;
            else if (Valor.Length < Lenght)
                return true;

            return false;
        }

        public static string ToNull(this string Valor)
        {
            if (IsNullOrEmpty(Valor))
                return null;

            return Valor;
        }
        public static string ToNull(this string Valor, string ValorDefault)
        {
            if (IsNullOrEmpty(Valor))
                return ValorDefault;

            return Valor;
        }

        public static bool IsEmail(this string pstrEmail)
        {
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            return Regex.IsMatch(pstrEmail, strModelo);
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static int ToInt(this string value)
        {
            int.TryParse(value, out int result);

            return result;
        }

        public static int ToInt(this object value)
        {
            int.TryParse(value.ToString(), out int result);

            return result;
        }

        public static decimal ToDecimal(this string value)
        {
            decimal.TryParse(value, out decimal result);

            return result;
        }

        public static int ToInt(this string value, out bool isSuccess)
        {
            isSuccess = int.TryParse(value, out int result);

            return result;
        }

        public static int ToInt(this double value)
        {
            return (int)value;
        }

        public static double ToDouble(this string value)
        {
            return double.Parse(value, new CultureInfo("pt-BR"));
        }

        public static bool ToBool(this string value)
        {
            bool.TryParse(value, out bool result);

            return result;
        }

        public static bool ToBool(this string value, string valueDafault)
        {
            return value.Equals(valueDafault);
        }

        public static TimeSpan ToTime(this double value)
        {
            return TimeSpan.FromSeconds(value);
        }

        public static int IsZero(this int value, int valueDefault)
        {
            int retorno = value;
            if (value.Equals(0))
                retorno = valueDefault;

            return retorno;
        }

        public async static Task<byte[]> ToBaseByte(this Stream file)
        {
            byte[] bytes = new byte[file.Length];
            await file.ReadAsync(bytes, 0, (int)file.Length);

            return bytes;
        }

        public async static Task<string> ToBase64(this string value)
        {
            return await Task.FromResult(Convert.ToBase64String(Encoding.UTF8.GetBytes(value)));
        }

        public static string GetAttribute(this object item, string attribute)
        {
            string strRetorno = null;
            if (item != null)
                strRetorno = item.GetType()?.GetProperty(attribute)?.GetValue(item)?.ToString();

            return strRetorno;
        }

        public static T GetAttribute<T>(this object item, string attribute) where T : new()
        {
            T retorno = new T();
            if (item != null)
                retorno = (T)item.GetType()?.GetProperty(attribute)?.GetValue(item);

            return retorno;
        }

        public async static Task<ObservableCollection<T>> ToListAsync<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> list = new ObservableCollection<T>();
            foreach (T item in source)
                list.Add(item);

            return await Task.FromResult(list);
        }

        public static string ValorParaCodigoBenassi(this string value)
        {

            string result = value.Replace("1", "C")
                                 .Replace("2", "A")
                                 .Replace("3", "M")
                                 .Replace("4", "P")
                                 .Replace("5", "O")
                                 .Replace("6", "N")
                                 .Replace("7", "E")
                                 .Replace("8", "S")
                                 .Replace("9", "I")
                                 .Replace("0", "L");

            return result;
        }
        public static bool IsCpf(this string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static JwtSecurityToken decodeHS256(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }

        public async static Task<byte[]> ToBase64(this Stream file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                return ms.ToArray();
            }
            //byte[] buffer = new byte[file.Length];
            //MemoryStream ms;
            //using (ms = new MemoryStream())
            //{
            //    int read;
            //    int read2 = 0;
            //    while ((read = await file.ReadAsync(buffer, read2, buffer.Length)) > 0)
            //    {
            //        ms.Write(buffer, read2, read);
            //        read2 = read;
            //    }
            //}

            //byte[] bytes = new byte[file.Length];
            //int receivedDataCount = 0;

            //do
            //{
            //    receivedDataCount = await file.ReadAsync(bytes, receivedDataCount, bytes.Length - receivedDataCount);
            //}
            //while (receivedDataCount > 0);


            //return ms.ToArray();
            //return bytes;
        }

        public static ObservableCollection<Uf> ListaUf(string textDefault = "Selecione", string valueDefault = "")
        {
            ObservableCollection<Uf> lista = new ObservableCollection<Uf>();

            if (valueDefault != null)
                lista.Add(new Uf(valueDefault, textDefault));

            lista = new ObservableCollection<Uf>()
            {
                new Uf("AC", "Acre"),
                new Uf("AL", "Alagoas"),
                new Uf("AP", "Amapá"),
                new Uf("AM", "Amazonas"),
                new Uf("BA", "Bahia"),
                new Uf("CE", "Ceará"),
                new Uf("DF", "Distrito Federal"),
                new Uf("ES", "Espírito Santo"),
                new Uf("GO", "Goiás"),
                new Uf("MA", "Maranhão"),
                new Uf("MT", "Mato Grosso"),
                new Uf("MS", "Mato Grosso do Sul"),
                new Uf("MG", "Minas Gerais"),
                new Uf("PA", "Pará"),
                new Uf("PB", "Paraíba"),
                new Uf("PR", "Paraná"),
                new Uf("PE", "Pernambuco"),
                new Uf("PI", "Piauí"),
                new Uf("RJ", "Rio de Janeiro"),
                new Uf("RN", "Rio Grande do Norte"),
                new Uf("RS", "Rio Grande do Sul"),
                new Uf("RO", "Rondônia"),
                new Uf("RR", "Roraima"),
                new Uf("SC", "Santa Catarina"),
                new Uf("SP", "São Paulo"),
                new Uf("SE", "Sergipe"),
                new Uf("TO", "Tocantins")
            };

            return lista;
        }

        public class Uf
        {
            public string UF { get; set; }
            public string Estado { get; set; }

            public Uf(string uf, string estado)
            {
                UF = uf;
                Estado = estado;
            }
        }
    }
}
