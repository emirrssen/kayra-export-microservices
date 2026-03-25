namespace KayraExport.Microservices.Services.Auth.Application.Helpers
{
    public class EncryptHelper
    {
        /// <summary>
        /// Verilen orijinal şifreleme anahtarını (secret key), HMAC-SHA256 ve benzeri kriptografik algoritmaların 
        /// gerektirdiği minimum uzunluk (varsayılan: 32 karakter / 256 bit) standartlarına uygun hale getirir.
        /// Eğer anahtar 32 karakterden kısaysa, sonuna '0' (sıfır) ekleyerek (padding) uzunluğunu 32'ye tamamlar.
        /// Eğer anahtar 32 karakterden uzunsa, ilk 32 karakterini alarak keser (truncation).
        /// </summary>
        /// <param name="originalKey">Sistemde tanımlı veya Environment'tan gelen ham şifreleme anahtarı.</param>
        /// <returns>Kriptografik işlemler için güvenli, tam 32 karakter uzunluğunda formatlanmış anahtar.</returns>
        public static string FormatEncryptionKey(string originalKey)
        {
            if (originalKey.Length < 32)
                return originalKey.PadRight(32, '0');

            return originalKey.Substring(0, 32);
        }
    }
}
