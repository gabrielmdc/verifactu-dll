using Sha256HelperDll;
using System.Globalization;
using System.Reflection.Metadata;

namespace tests;

public class HashTest()
{
    [Fact(DisplayName = "Primer registro de facturacion - alta")]
    public void Case1()
    {
        const string msg = "IDEmisorFactura=89890001K&NumSerieFactura=12345678/G33&FechaExpedicionFactura=01-01-2024&TipoFactura=F1&CuotaTotal=12.35&ImporteTotal=123.45&Huella=&FechaHoraHusoGenRegistro=2024-01-01T19:20:30+01:00";
        const string targetHash = "3C464DAF61ACB827C65FDA19F352A4E3BDC2C640E9E9FC4CC058073F38F12F60";
        var hash = Sha256Helper.GetMessageHash(msg);

        Assert.Equal(targetHash, hash);
    }

    [Fact(DisplayName = "Segundo registro de facturacion - alta con registro de facturación anterior")]
    public void Case2()
    {
        const string msg = "IDEmisorFactura=89890001K&NumSerieFactura=12345679/G34&FechaExpedicionFactura=01-01-2024&TipoFactura=F1&CuotaTotal=12.35&ImporteTotal=123.45&Huella=3C464DAF61ACB827C65FDA19F352A4E3BDC2C640E9E9FC4CC058073F38F12F60&FechaHoraHusoGenRegistro=2024-01-01T19:20:35+01:00";
        const string targetHash = "F7B94CFD8924EDFF273501B01EE5153E4CE8F259766F88CF6ACB8935802A2B97";
        var hash = Sha256Helper.GetMessageHash(msg);

        Assert.Equal(targetHash, hash);
    }

    [Fact(DisplayName = "Tercer registro de facturacion - anulacion")]
    public void Case3()
    {
        const string msg = "IDEmisorFacturaAnulada=89890001K&NumSerieFacturaAnulada=12345679/G34&FechaExpedicionFacturaAnulada=01-01-2024&Huella=F7B94CFD8924EDFF273501B01EE5153E4CE8F259766F88CF6ACB8935802A2B97&FechaHoraHusoGenRegistro=2024-01-01T19:20:40+01:00";
        const string targetHash = "177547C0D57AC74748561D054A9CEC14B4C4EA23D1BEFD6F2E69E3A388F90C68";
        var hash = Sha256Helper.GetMessageHash(msg);

        Assert.Equal(targetHash, hash);
    }
}
