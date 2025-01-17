using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class GoogleLogin_domi : MonoBehaviour
{
    public string GooglePlayToken;
    public string GooglePlayError;

    public async Task Authenticate()
    {
        PlayGamesPlatform.Activate();
        await UnityServices.InitializeAsync();
        
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google was successful.");
                PlayGamesPlatform.Instance.RequestServerSideAccess(false, async code =>
                {
                    LoginAlertWindow.ShowUI("Google Login", "Login with Google was successful.");
                    GUIUtility.systemCopyBuffer = code;
                    Debug.Log($"Auth code is {code}");
                    GooglePlayToken = code;

                    LoginAlertWindow.ShowUI("Google Login 2", Social.localUser.id+ " / "+ Social.localUser.userName);

                    await AuthenticateWithUnity();
                });
            }
            else
            {
                GooglePlayError = "Failed to retrieve GPG auth code";
                LoginAlertWindow.ShowUI("Google Login", "Failed to retrieve GPG auth code");
                Debug.LogError("Login Unsuccessful");
            }
        });
    }

    private async Task AuthenticateWithUnity()
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGoogleAsync(GooglePlayToken);
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
            throw;
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            throw;
        }

    }

    private void Start() {
        _ = Authenticate();
    }
}
