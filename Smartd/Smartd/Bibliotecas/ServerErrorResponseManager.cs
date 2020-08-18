using Smartd.Constances;
using Smartd.Controllers;
using Smartd.Models.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smartd.Bibliotecas
{
   public class ServerErrorResponseManager
    {
        public async Task<bool> ManageResponseAsync(ResponseCore response, Page page, bool showUnknownError = true,
                                            bool showServerTranslatedError = true, bool handleUnauthorizedError = true)
        {
            bool sucesso = true;
            if (response == null || response.HTTPStatusCode == null)
            {
                await page.Alerta("Ocorreu um erro. Por favor tente novamente.", "Atenção!");
                sucesso = false;
            }
            else if (!response.HTTPStatusCode.Equals(HttpStatusCode.OK))
            {
                if (response.HTTPStatusCode.Equals(MHttpStatusCode.NO_INTERNET_CONNECTION))
                {
                    await page.Alerta("Por favor, confira a sua conexão com a internet.", "Atenção!");
                    sucesso = false;
                }
                else if (response.HTTPStatusCode.Equals(MHttpStatusCode.SERVER_OFFLINE))
                {
                    await page.Alerta($"Servidor está indisponível.\nPor favor tente novamente.", "Atenção!");
                    sucesso = false;
                }
                else if (response.HTTPStatusCode.Equals(MHttpStatusCode.TIMEOUT))
                {
                    await page.Alerta($"Por favor, tente novamente.", "Tempo expirado!");
                    sucesso = false;
                }
                else if (handleUnauthorizedError && response.HTTPStatusCode.Equals(HttpStatusCode.Unauthorized))
                {
                    //MotoristaController motoristaController = new MotoristaController();

                    //if (!await motoristaController.AtualizarToken())
                    //{
                        //await page.Alerta("Por favor, realize o login novamente.", "Atenção!");

                        sucesso = false;
                        //Configuracao.SessionActive = false;
                        Application.Current.MainPage = new NavigationPage(new Views.Login.LoginPage(true));
                    //}
                }
                else if (showServerTranslatedError && !string.IsNullOrEmpty(response.Error?.Message))
                {
                    await page.Alerta(response.Error?.Message, "Atenção!");
                    sucesso = false;
                }
                else if (showUnknownError)
                {
                    await page.Alerta("Ocorreu um erro. Por favor tente novamente.", "Atenção!");
                    sucesso = false;
                }
            }

            return await Task.FromResult(sucesso);
        }
    }
}
