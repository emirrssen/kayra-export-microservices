namespace KayraExport.Microservices.Services.Auth.Application.Helpers
{
    public class DateTimeHelper
    {
        private static readonly TimeZoneInfo TurkeyTimeZone =
            #if WINDOWS
                TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            #else
                TimeZoneInfo.FindSystemTimeZoneById("Europe/Istanbul");
        #endif

        /// <summary>
        /// Geçerli Evrensel Eşgüdümlü Zamanı (UTC) alır ve işletim sistemine uygun TimeZone ID'sini 
        /// kullanarak güncel Türkiye yerel saatine dönüştürür. Özellikle Docker/Linux 
        /// ortamlarındaki saat kaymalarını ve TimeZoneNotFound hatalarını önlemek için tasarlanmıştır.
        /// </summary>
        /// <returns>Türkiye saat dilimine (UTC+3) göre ayarlanmış güncel <see cref="DateTime"/> nesnesi.</returns>
        public static DateTime GetNowByTurkiyeTimeZone()
            => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TurkeyTimeZone);
    }
}
