using Flunt.Notifications;
using Gcm.Gestao.Campanha.Marketing.Infrastructure.Models.RestRequest;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gcm.Gestao.Campanha.Marketing.Infrastructure.Helpers
{
    /// <summary>
    /// Classe base para requisições rest
    /// </summary>
    public static class RestDeserializeBase
    {     
        #region Deserialize
        /// <summary>
        /// Realiza a deserialização da reposta de uma lista tratando o retorno em caso de erro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="resposta"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> DeserializeList<T, E>(HttpResponseMessage resposta) where T : Notifiable where E : GatewayErroBase
        {
            var resultado = new List<T>();
            var objetoErro = (T)Activator.CreateInstance(typeof(T));

            if (resposta.StatusCode == HttpStatusCode.NoContent || resposta.StatusCode == HttpStatusCode.NotFound)
            {
                return resultado;
            }
            else if (resposta.IsSuccessStatusCode)
            {
                return await resposta.Content.ReadAsAsync<IEnumerable<T>>();
            }
            else            
            {
                try
                {
                    var erroResult = await resposta.Content.ReadAsAsync<E>();
                    var notifications = erroResult.ToNotifications();
                    objetoErro.AddNotifications(notifications);
                }
                catch
                {
                    objetoErro.AddNotification(new Notification("Requisição", $"Status: {resposta.StatusCode}, \r\n {resposta.Content}"));
                }
                return resultado;
            }            
        }

        /// <summary>
        /// Realiza a deserialização da reposta tratando o retorno em caso de erro tratando erro na estrutura padrão de Notification
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="resposta"></param>
        /// <returns></returns>
        public static async Task<T> Deserialize<T, E>(HttpResponseMessage resposta) where T : Notifiable where E : GatewayErroBase
        {
            if (resposta.StatusCode == HttpStatusCode.NoContent || resposta.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else if (resposta.IsSuccessStatusCode)
            {
                return await SerializaRetornoComLog<T>(resposta);
            }
            else
            {
                var resultado = (T)Activator.CreateInstance(typeof(T));
                try
                {
                    var erro = await resposta.Content.ReadAsAsync<E>();
                    var notifications = erro.ToNotifications();
                    resultado.AddNotifications(notifications);
                }
                catch
                {
                    resultado.AddNotification(new Notification("Requisição", $"Status: {resposta.StatusCode}, \r\n {resposta.Content}"));
                }

                Console.WriteLine(JsonSerializer.Serialize(resultado));
                
                return resultado;
            }
        }
               
        private static async Task<T> SerializaRetornoComLog<T>(HttpResponseMessage resposta) where T : Notifiable
        {
            var resultado = await resposta.Content.ReadAsAsync<T>();

            Console.WriteLine(JsonSerializer.Serialize(resultado));

            return resultado;
        }
        #endregion
    }
}
