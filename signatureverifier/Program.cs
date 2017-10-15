using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace signatureverifier
{
    static class Program
    {
        static void Main()
        {
            // ComAround
            const string e = "AQAB";
            const string n = "3-f4dQItaX04RceIkb62Xy8Xti17FyeYOdUEZYuoMBOOjPN1mWwaZhKQzsjN-YpkNJ4FckhkwOVZg0jHXwoPt4pz98Z95MoXLRp4VtnF0mL5fjrvqqN4x_SGRID84aEjK8yOSxhlweHEAq7WvgxVVGVrrviAXILGWe5HxwtXherJmwbdC-NxIrvDftlVGRU0Mg0fsQ_CcMLgJk97zU-hlZyRrUo1VvNNCM2pUMUCctgilh3cGEagg7mjge3K6BWAIkR8dm1j9smk_WFPgeg9R_yo3ufAlyHfgOGrwEnjJ27LsDvSqWVxhyHKxpS9xg8mSE6L1sUgsrnU21IudNwfhw";
            const string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjZncnh0VXdnZ3FEUFFKbm1tRDRtNURpU2N6dyIsImtpZCI6IjZncnh0VXdnZ3FEUFFKbm1tRDRtNURpU2N6dyJ9.eyJpc3MiOiJodHRwczovL3Rlc3Rsb2dpbi5jb21hcm91bmQuY29tLyIsImF1ZCI6Imh0dHBzOi8vdGVzdGxvZ2luLmNvbWFyb3VuZC5jb20vcmVzb3VyY2VzIiwiZXhwIjoxNTA4MDI3NTk1LCJuYmYiOjE1MDgwMjM5OTUsImNsaWVudF9pZCI6Inplcm9mcm9udGVuZCIsInNjb3BlIjoiemVyb2FwaSIsInN1YiI6IjYxOTAiLCJhdXRoX3RpbWUiOjE1MDgwMjM5OTEsImlkcCI6Imlkc3J2IiwibmFtZSI6Ik1hcnRpbiBXw6VnZXIiLCJhbGxvd2VkaXAiOiIqIiwiY3VzdG9tZXJpZCI6IjEzOSIsImVtYWlsIjoibWFydGluLndAY29tYXJvdW5kLnNlIiwidXNlcnVnYW0iOiIxNjQxNyIsImxpY2Vuc2UiOiJCYXNpYyIsInBvcnRhbGlkcyI6WyIxOTciLCIyMDEiLCI5NzgiXSwicm9sZSI6Ik1hbmFnZXIiLCJ1c2VyZ3JvdXBpZHMiOlsiMTQyNSIsIjE0NDUiLCIzOTA3Il0sImFtciI6WyJQYXNzd29yZCJdfQ.NLVZqNnw6lcy6MPiSawWxR-jMoGMXsd4MlY3rzwr1Bh5O_YBGy_Z6KowSRdlCyP4CPlNOQI8o-REXi6BsFouT1gWCodwSakdVSF-arBTL2mXNf8N-TWIKV1TfwtAzamotrnkMVqzddSy8jsbCL2hOMa9ytPx6sFPvBm623wnh4N8WlAwqsSxlzA4EhPWWygDl6t4bpIWdz8_Sv16G_YTlw4UdQsuIc2E6SB7nV0a5rFzdntmSy7hRArj2jaDZp2i19dr0k_Eq3l_HUOxpIkBw4rbhpJFlUOq7fiXpoYkZUxK2hOK3iTOLDsGJfG7U0FIadU_sQwBH1axfrtgVcve8w";

            // Vv
            //const string e = "AQAB";
            //const string n = "AJQkHGApl39Q-2InDQ-4sl8tnaKBM2oOf0voXaDGfbAG4rQDcFaJsFCr8aszQ459guBmjRFO2UvCZTrgs50eSdguMJTpDUZ8nbsiC0VCR9kaFPgsKj7a42zjKm4XEAU-Y3RJr0cqHDrbV1pJulQn4JDWzX6_RcA26aS6jzwHs-7-lJQPyYlxQv9gix35erFKOgZgzYLxNty2Gbjrj7rZ9ATfJjjwx8aZ-Cn3mVtJry1318-Azo49srnz0TmW6hlc5AmYZodD61hGjKUAe-l-a1hMMn7ziHWvb6cmotAiQsQ-WKCZZhO3kmLlqids-1d_9PMxkqmg4TZbTJYkTaSj8fM";
            //const string token = "eyJ0eXAiOiJKV1QiLCJraWQiOiJDanhYcUhFYlpvWEdYNkRHTE4xZzRaY1hCWTQ9IiwiYWxnIjoiUlMyNTYifQ.eyJzdWIiOiJURVNUMTAiLCJhdWRpdFRyYWNraW5nSWQiOiIzZDJkNTVlZS1hNmEzLTRmNDgtYjM3MC0yZDkwNmM2MzA4NGQiLCJpc3MiOiJodHRwczovL3d3dy50ZXN0LnZlZ3Zlc2VuLm5vOjQ0My9vcGVuYW0vb2F1dGgyL2VtcGxveWVlcyIsInRva2VuTmFtZSI6ImFjY2Vzc190b2tlbiIsInRva2VuX3R5cGUiOiJCZWFyZXIiLCJhdXRoR3JhbnRJZCI6IjFkN2M0OWQ0LWM4OTItNDYxZS1iMGQ2LWIzZDNjMDk4ZDZiZSIsImF1ZCI6IkNvbUFyb3VuZCIsIm5iZiI6MTUwODAyMjM0Niwic2NvcGUiOlsic3Z2cHJvZmlsZSIsIm9wZW5pZCJdLCJhdXRoX3RpbWUiOjE1MDgwMjIzNDYsInJlYWxtIjoiL0VtcGxveWVlcyIsImV4cCI6MTUwODA1MTE0NiwiaWF0IjoxNTA4MDIyMzQ2LCJleHBpcmVzX2luIjoyODgwMDAwMCwianRpIjoiNzE3Yzk0YTYtNDdmOS00MzlhLWEwNzQtMjI4MjYyMTBiMzc5In0.kY2T0pAWopjsOmajjscXeI8HgrrQ8-e6LZprn9_LgSPRfYq5crUMxJ70rLawEMnpIkv7KEvNO3yfJNYTbif3miztbY5ipEN9CHE-2UBZqTv_A5i2-lvq52UDo4D10BL45j3ULW4VpqI_hu_JWCRxJrE5lt4AoLRywWJvdIHuaQ49krzANb2pzvZT0mwVwB0XfiwmwHCZ8uHAhVaxx9JtdiDyhfpLBstqjUpp1tfpJRpzjoqCcvKeUM7FmtnUnG6QMTTerixqBSveKEjL2naKnF7h7m4p6WjsA5POYK3Dpra12SPQ-iflr_8MbcIyVxMtj_yhCdUWAglHOPoxb0RG7Q";


            var parts = token.Split('.');
            var encodedBytes = Encoding.UTF8.GetBytes($"{parts[0]}.{parts[1]}");
            var signatureBytes = Base64UrlEncoder.DecodeBytes(parts[2]);
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(new RSAParameters
            {
                Exponent = Base64UrlEncoder.DecodeBytes(e),
                Modulus = Base64UrlEncoder.DecodeBytes(n),
            });
            var key = new RsaSecurityKey(rsa);
            var valid = key.Rsa.VerifyData(encodedBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            // Code for v 1.0.4 of IdentityModel.Tokens
            // var deformatter = key.GetSignatureDeformatter(SecurityAlgorithms.RsaSha256Signature);
            // var hash = key.GetHashAlgorithmForSignature(SecurityAlgorithms.RsaSha256Signature);
            // deformatter.SetHashAlgorithm(hash.GetType().ToString());
            // var valid = deformatter.VerifySignature(hash.ComputeHash(encodedBytes), signatureBytes);

            Console.WriteLine(valid ? "Signature is valid" : "Signature is not valid");

        }
    }
}
