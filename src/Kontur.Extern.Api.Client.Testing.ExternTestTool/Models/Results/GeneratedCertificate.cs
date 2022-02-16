// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.Models.Results
{
    /// <summary>
    /// Тестовый сертификат
    /// </summary>
    /// <param name="PublicKeyCertificate">Сертификат открытого ключа</param>
    /// <param name="PrivateKey">Закрытый ключ</param>
    /// <param name="Pfx">Pfx (Pkcs12) сертификат с закрытым ключом, пароль по умолчанию - \&quot;1\&quot;, если был сгенерирован pfx формат сертификата с закрытым ключом</param>
    public record GeneratedCertificate(byte[] PublicKeyCertificate, string PrivateKey, byte[] Pfx);
}