namespace SiteTask.Model.OriginalModel;

public class SecretData
{
    public SecretData(string login, string ip, string macAddress, string oc, string pc, string browser)
    {
        Login = login;
        Ip = ip;
        MacAddress = macAddress;
        Oc = oc;
        Pc = pc;
        Browser = browser;
    }

    public string Login { get; set; }
    public string Ip { get; set; }
    public string MacAddress { get; set; }
    public string Oc { get; set; }
    public string Pc { get; set; }
    public string Browser { get; set; }
}