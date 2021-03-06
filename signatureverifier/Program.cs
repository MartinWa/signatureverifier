﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace signatureverifier
{
    internal static class Program
    {
        private static void Main()
        {
            const string e = "AQAB";

            // ComAround
            //const string n = "3-f4dQItaX04RceIkb62Xy8Xti17FyeYOdUEZYuoMBOOjPN1mWwaZhKQzsjN-YpkNJ4FckhkwOVZg0jHXwoPt4pz98Z95MoXLRp4VtnF0mL5fjrvqqN4x_SGRID84aEjK8yOSxhlweHEAq7WvgxVVGVrrviAXILGWe5HxwtXherJmwbdC-NxIrvDftlVGRU0Mg0fsQ_CcMLgJk97zU-hlZyRrUo1VvNNCM2pUMUCctgilh3cGEagg7mjge3K6BWAIkR8dm1j9smk_WFPgeg9R_yo3ufAlyHfgOGrwEnjJ27LsDvSqWVxhyHKxpS9xg8mSE6L1sUgsrnU21IudNwfhw";
            //const string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjZncnh0VXdnZ3FEUFFKbm1tRDRtNURpU2N6dyIsImtpZCI6IjZncnh0VXdnZ3FEUFFKbm1tRDRtNURpU2N6dyJ9.eyJpc3MiOiJodHRwczovL3Rlc3Rsb2dpbi5jb21hcm91bmQuY29tLyIsImF1ZCI6Imh0dHBzOi8vdGVzdGxvZ2luLmNvbWFyb3VuZC5jb20vcmVzb3VyY2VzIiwiZXhwIjoxNTA4MjQxODIxLCJuYmYiOjE1MDgyMzgyMjEsImNsaWVudF9pZCI6Inplcm9mcm9udGVuZCIsInNjb3BlIjoiemVyb2FwaSIsInN1YiI6IjYxOTAiLCJhdXRoX3RpbWUiOjE1MDgyMzgyMjAsImlkcCI6Imlkc3J2IiwibmFtZSI6Ik1hcnRpbiBXw6VnZXIiLCJhbGxvd2VkaXAiOiIqIiwiY3VzdG9tZXJpZCI6IjEzOSIsImVtYWlsIjoibWFydGluLndAY29tYXJvdW5kLnNlIiwidXNlcnVnYW0iOiIxNjQxNyIsImxpY2Vuc2UiOiJCYXNpYyIsInBvcnRhbGlkcyI6WyIxOTciLCIyMDEiLCI5NzgiXSwicm9sZSI6Ik1hbmFnZXIiLCJ1c2VyZ3JvdXBpZHMiOlsiMTQyNSIsIjE0NDUiLCIzOTA3Il0sImFtciI6WyJQYXNzd29yZCJdfQ.Eonf9Gwc-DcW6SEW6oXwb1taVrgQdN6JCdgbtQ_GN5IEkwE3QHaO6FBvV_qOP_cu82AK_r3o6ZEzGcQdXamQZY63bF4Bd4uV7fTOvx4jAWNlqg9UgM0ZoR4VPeIptSTxX_2SFlfRzHN2zAG3a3XrOGApWSrrXUzwjOgufidNUyCFH0nOZndf2pGFweIAUspoWQVPGW1qdpKzMUS9H0abfdFvSOrvpv2G2elkV3IbdSECKMZ8msvs4OSuq9EwhsnnLADNjAgs3OOVK4vRvQnahg1ifEKrN4-k-Sl-PwzEUVKRBrmRhBVVpI-YtgDVR_V8kRoJUK7ChIU5PsvxLwAPyg";

            // jwt.io example
            //const string n = "3ZWrUY0Y6IKN1qI4BhxR2C7oHVFgGPYkd38uGq1jQNSqEvJFcN93CYm16/G78FAFKWqwsJb3Wx+nbxDn6LtP4AhULB1H0K0g7/jLklDAHvI8yhOKlvoyvsUFPWtNxlJyh5JJXvkNKV/4Oo12e69f8QCuQ6NpEPl+cSvXIqUYBCs=";
            //const string token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.EkN-DOsnsuRjRO6BxXemmJDm3HbxrbRzXglbN2S4sOkopdU4IsDxTI8jO19W_A4K8ZPJijNLis4EZsHeY559a4DFOd50_OqgHGuERTqYZyuhtF39yxJPAjUESwxk2J5k_4zM3O-vtd1Ghyo4IbqKKSy6J9mTniYJPenn5-HIirE";

            // Vv
            const string n = "AKfCLcoplNcx1L13DoWRRrKyct94_W14520ayQxFr6l0PQL773KgVOfGOPwgFnopvS2p9kU72CYTFSByI0DU8TXbDlytGHpxOORTI5kG3aJpm4Xo5914SX9Et49FxLjok4OWe0woG3--X4dZAMJW6B_bFY3GEyOI7QbxSg1IKQCPTYcgd1Zu3Mnse1K3hw6gIas5tHaOzVvjS7i5Iy5kiUmvyVLwsYlaYY2wsum91iL3Sy2edkzeOap-WoOxcDxRvud5rSy43ZV1fpH1WsviHmI53u1ws-5WcE--u6zZLaIExTmwbEdKY_kKfsXXVLV-gfl7KIJ8PwZ1lm5enKnf0X8";
            const string token = "eyJ0eXAiOiJKV1QiLCJraWQiOiJDanhYcUhFYlpvWEdYNkRHTE4xZzRaY1hCWTQ9IiwiYWxnIjoiUlMyNTYifQ.eyJzdWIiOiJURVNUMTAiLCJhdWRpdFRyYWNraW5nSWQiOiJkMjI4Mzc3Mi1hYmU5LTQyY2QtYjY4NC04MTlmZjQ2MTQzOTMiLCJpc3MiOiJodHRwczovL3d3dy50ZXN0LnZlZ3Zlc2VuLm5vOjQ0My9vcGVuYW0vb2F1dGgyL2VtcGxveWVlcyIsInRva2VuTmFtZSI6ImFjY2Vzc190b2tlbiIsInRva2VuX3R5cGUiOiJCZWFyZXIiLCJhdXRoR3JhbnRJZCI6IjE2YzQwZWVjLWVjZmMtNGQwNS04ODg5LTU4NjkwMjIyODY0ZiIsImF1ZCI6IkNvbUFyb3VuZCIsIm5iZiI6MTUwODIzODQwOCwic2NvcGUiOlsic3Z2cHJvZmlsZSIsIm9wZW5pZCJdLCJhdXRoX3RpbWUiOjE1MDgyMzg0MDgsInJlYWxtIjoiL0VtcGxveWVlcyIsImV4cCI6MTUwODI2NzIwOCwiaWF0IjoxNTA4MjM4NDA4LCJleHBpcmVzX2luIjoyODgwMDAwMCwianRpIjoiY2I0NzM4MGYtZTNhNC00NWQ3LThhZWQtZWQ4NmQ2MDQ4MmQ5In0.G4x-Rzj--9wonRKd5xKglxxw6WZcAFD-aHJL8YiAfqfYuZFjVsrxzXzM1LLb70slHPzGiW57roCFGuSANV2vSNfIA1G3yngEf77hbyGUwPxZ3H2fLfe4TEOG6KSTJ2aAR5y5sGxGJKPrhkPmZoXw2evUk1NAG0duISfQCKyTOBhcP5AeGSqH59CuZVxmUuxMY-UFH8JO6exImTSTPLF44u92NOT5-mlQyfGcEjby9Ns54Sjz0BD_Nh-6dUpb5owmPJQSM2eo1uJX6xoYWKaZoeL80IcuVD5tYEeBskj_1dK1UbDXoAFrLx2YykFnZV69NhpRgCS2M-ilEaWbF4DkVw";

            var parts = token.Split('.');
            var encodedData = Encoding.UTF8.GetBytes($"{parts[0]}.{parts[1]}");
            var encodedSignature = FromBase64Url(parts[2]);
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            rsaCryptoServiceProvider.ImportParameters(new RSAParameters
            {
                Exponent = FromBase64Url(e),
                Modulus = FromBase64Url(n)
            });

            // Code without IdentityModel.Tokens
            var valid = rsaCryptoServiceProvider.VerifyData(encodedData, encodedSignature, HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            // Code for v 4.0.4 of IdentityModel.Tokens
            //var rsaSecurityKey = new RsaSecurityKey(rsaCryptoServiceProvider);
            //var deformatter = rsaSecurityKey.GetSignatureDeformatter(SecurityAlgorithms.RsaSha256Signature);
            //var hasher = rsaSecurityKey.GetHashAlgorithmForSignature(SecurityAlgorithms.RsaSha256Signature);
            //deformatter.SetHashAlgorithm(hasher.GetType().ToString());
            //var hash = hasher.ComputeHash(encodedData);
            //var valid = deformatter.VerifySignature(hash, encodedSignature);

            // Code for v 5.14 of IdentityModel.Tokens
            //var rsa = rsaSecurityKey.Rsa;
            //var valid = rsa.VerifyData(encodedBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            Console.WriteLine(valid ? "Signature is valid" : "Signature is not valid, press enter");
            Console.ReadLine();
        }


        private static byte[] FromBase64Url(string base64Url)
        {
            var padded = base64Url.Length % 4 == 0 ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            var base64 = padded.Replace("_", "/").Replace("-", "+");
            return Convert.FromBase64String(base64);
        }

    }
}
