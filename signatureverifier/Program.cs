using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

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
            const string n = "lCQcYCmXf1D7YicND7iyXy2dooEzag5_S-hdoMZ9sAbitANwVomwUKvxqzNDjn2C4GaNEU7ZS8JlOuCznR5J2C4wlOkNRnyduyILRUJH2RoU-CwqPtrjbOMqbhcQBT5jdEmvRyocOttXWkm6VCfgkNbNfr9FwDbppLqPPAez7v6UlA_JiXFC_2CLHfl6sUo6BmDNgvE23LYZuOuPutn0BN8mOPDHxpn4KfeZW0mvLXfXz4DOjj2yufPROZbqGVzkCZhmh0PrWEaMpQB76X5rWEwyfvOIda9vpyai0CJCxD5YoJlmE7eSYuWqJ2z7V3_08zGSqaDhNltMliRNpKPx8w";
            const string token = "eyJ0eXAiOiJKV1QiLCJraWQiOiJDanhYcUhFYlpvWEdYNkRHTE4xZzRaY1hCWTQ9IiwiYWxnIjoiUlMyNTYifQ.eyJzdWIiOiJURVNUMTAiLCJhdWRpdFRyYWNraW5nSWQiOiJkMjI4Mzc3Mi1hYmU5LTQyY2QtYjY4NC04MTlmZjQ2MTQzOTMiLCJpc3MiOiJodHRwczovL3d3dy50ZXN0LnZlZ3Zlc2VuLm5vOjQ0My9vcGVuYW0vb2F1dGgyL2VtcGxveWVlcyIsInRva2VuTmFtZSI6ImFjY2Vzc190b2tlbiIsInRva2VuX3R5cGUiOiJCZWFyZXIiLCJhdXRoR3JhbnRJZCI6IjE2YzQwZWVjLWVjZmMtNGQwNS04ODg5LTU4NjkwMjIyODY0ZiIsImF1ZCI6IkNvbUFyb3VuZCIsIm5iZiI6MTUwODIzODQwOCwic2NvcGUiOlsic3Z2cHJvZmlsZSIsIm9wZW5pZCJdLCJhdXRoX3RpbWUiOjE1MDgyMzg0MDgsInJlYWxtIjoiL0VtcGxveWVlcyIsImV4cCI6MTUwODI2NzIwOCwiaWF0IjoxNTA4MjM4NDA4LCJleHBpcmVzX2luIjoyODgwMDAwMCwianRpIjoiY2I0NzM4MGYtZTNhNC00NWQ3LThhZWQtZWQ4NmQ2MDQ4MmQ5In0.G4x-Rzj--9wonRKd5xKglxxw6WZcAFD-aHJL8YiAfqfYuZFjVsrxzXzM1LLb70slHPzGiW57roCFGuSANV2vSNfIA1G3yngEf77hbyGUwPxZ3H2fLfe4TEOG6KSTJ2aAR5y5sGxGJKPrhkPmZoXw2evUk1NAG0duISfQCKyTOBhcP5AeGSqH59CuZVxmUuxMY-UFH8JO6exImTSTPLF44u92NOT5-mlQyfGcEjby9Ns54Sjz0BD_Nh-6dUpb5owmPJQSM2eo1uJX6xoYWKaZoeL80IcuVD5tYEeBskj_1dK1UbDXoAFrLx2YykFnZV69NhpRgCS2M-ilEaWbF4DkVw";

            var parts = token.Split('.');
            var encodedData = Encoding.UTF8.GetBytes($"{parts[0]}.{parts[1]}");
            var encodedSignature = FromBase64Url(parts[2]);
            //var certBuffer = FromBase64Url("MIIDujCCAqKgAwIBAgIEU7/DwDANBgkqhkiG9w0BAQUFADCBnjELMAkGA1UEBhMCTk8xDTALBgNVBAcTBE9zbG8xGTAXBgNVBAoTEFN0YXRlbnMgVmVndmVzZW4xGzAZBgNVBAsTEk9yZ25yIDk3MTAzMjA4MU1WQTEMMAoGA1UECxMDSUtUMRcwFQYDVQQLEw5QbGF0dGZvcm1kcmlmdDEMMAoGA1UECxMDSUFNMRMwEQYDVQQDEwpTQU1MMiBUZXN0MB4XDTE0MDcxMTExMDAxNloXDTE5MDcxMDExMDAxNlowgZ4xCzAJBgNVBAYTAk5PMQ0wCwYDVQQHEwRPc2xvMRkwFwYDVQQKExBTdGF0ZW5zIFZlZ3Zlc2VuMRswGQYDVQQLExJPcmduciA5NzEwMzIwODFNVkExDDAKBgNVBAsTA0lLVDEXMBUGA1UECxMOUGxhdHRmb3JtZHJpZnQxDDAKBgNVBAsTA0lBTTETMBEGA1UEAxMKU0FNTDIgVGVzdDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAJQkHGApl39Q+2InDQ+4sl8tnaKBM2oOf0voXaDGfbAG4rQDcFaJsFCr8aszQ459guBmjRFO2UvCZTrgs50eSdguMJTpDUZ8nbsiC0VCR9kaFPgsKj7a42zjKm4XEAU+Y3RJr0cqHDrbV1pJulQn4JDWzX6/RcA26aS6jzwHs+7+lJQPyYlxQv9gix35erFKOgZgzYLxNty2Gbjrj7rZ9ATfJjjwx8aZ+Cn3mVtJry1318+Azo49srnz0TmW6hlc5AmYZodD61hGjKUAe+l+a1hMMn7ziHWvb6cmotAiQsQ+WKCZZhO3kmLlqids+1d/9PMxkqmg4TZbTJYkTaSj8fMCAwEAATANBgkqhkiG9w0BAQUFAAOCAQEACZFuKtuEdhNOhkYPLPJjMDbFDOvuRq6ILBJFYBZlITyoZYRPZntr48x9m/bw4wXHmTmLqY7wCA3U2pJBHOVvdi6aQHrNGycLtQxEw33xunKattGwiDOc+AL1f6hpJj3xxO2PR/MKziDucpQQ85jQ/ASzB1dOxTCk2T6FetFCTgBselPivsVCSBe05eT2wfsASNWVzmiCncdvkTXuHhVga+tIb/43p7NAAPSQmAhtTJ6YZuc+0Z6Hv31JdDKi+cMwF9jFBSx5PDOx0mlTqGa8TF0Dlxg4ZUz3s9+xULUUiFywsyvfXk/fnkbFWWlLJgyUBSz7hV1Im/Z01u4U4neECw==");
            //var certificate = new X509Certificate2(certBuffer);
            //var c = certificate.PublicKey.Key.ToXmlString(false);
            //var rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            //rsaCryptoServiceProvider.FromXmlString(c);
            //var parameters =  rsaCryptoServiceProvider.ExportParameters(false);
            var rsaCryptoServiceProvider2 = new RSACryptoServiceProvider();
            rsaCryptoServiceProvider2.ImportParameters(new RSAParameters
            {
                Exponent = FromBase64Url(e),
                Modulus = FromBase64Url(n)
            });
            //var exponment = ToBase64Url(parameters.Exponent);
            //var modulus = ToBase64Url(parameters.Modulus);
            //Console.WriteLine($"Exponent: {exponment}");
            //Console.WriteLine($"Modulus: {modulus}");


            //    var orig = DecodeBytes(n);
            //     var m = parameters.Modulus;




            // Code without IdentityModel.Tokens
            var valid = rsaCryptoServiceProvider2.VerifyData(encodedData, encodedSignature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

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

        private static string ToBase64Url(byte[] data)
        {
            var base64 = Convert.ToBase64String(data);
            var trim = base64.TrimEnd('=');
            return trim.Replace("/", "_").Replace("+", "-");
        }


        //private static byte[] DecodeBytes(string str)
        //{
        //    const char base64PadCharacter = '=';
        //    const string doubleBase64PadCharacter = "==";
        //    const char base64Character62 = '+';
        //    const char base64Character63 = '/';
        //    const char base64UrlCharacter62 = '-';
        //    const char base64UrlCharacter63 = '_';
        //    if (null == str)
        //    {
        //        throw new ArgumentNullException(nameof(str));
        //    }
        //    // 62nd char of encoding
        //    str = str.Replace(base64UrlCharacter62, base64Character62);

        //    // 63rd char of encoding
        //    str = str.Replace(base64UrlCharacter63, base64Character63);

        //    // check for padding
        //    switch (str.Length % 4)
        //    {
        //        case 0:
        //            // No pad chars in this case
        //            break;
        //        case 2:
        //            // Two pad chars
        //            str += doubleBase64PadCharacter;
        //            break;
        //        case 3:
        //            // One pad char
        //            str += base64PadCharacter;
        //            break;
        //        // ReSharper disable once RedundantEmptySwitchSection
        //        default:
        //            break;
        //    }
        //    return Convert.FromBase64String(str);
        //}
    }
}
