namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.Models
{
    /// <summary>
    /// Тестовый сертификат
    /// </summary>
    /// <param name="PublicKey">Публичная часть сертификата.</param>
    /// <param name="PrivateKey">Закрытый ключ.</param>
    /// <param name="Pfx">Pfx (Pkcs12) сертификат с закрытым ключом, пароль по умолчанию - \&quot;1\&quot;, если был сгенерирован pfx формат сертификата с закрытым ключом.</param>
    public record GeneratedCertificate(byte[] PublicKey, string PrivateKey, byte[] Pfx);
}