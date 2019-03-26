using FluentFTP;
using System.Net;

namespace ARMI.Services
{
    public class FtpService: IFtpService
    {
        public FtpClient FtpClient { get; set; }

        private void Connect(Models.Client client)
        {
            switch (client.ClientHostType)
            {
                case Enums.ClientHostType.SFtp:
                case Enums.ClientHostType.Ftp:
                    // create an FTP client
                    // create an FTP client
                    FtpClient = new FtpClient(client.Host)
                    {
                        Port = client.Port
                    };
                    // if you don't specify login credentials, we use the "anonymous" user account
                    if (client.UserName != string.Empty)
                        FtpClient.Credentials =  new NetworkCredential(client.UserName, client.Password);
                    // begin connecting to the server
                    FtpClient.Connect();
                    break;
                case Enums.ClientHostType.Samba:
                    break;
            }            
        }
        public void ImportEmulators(Models.Client client)
        {



        }
    }   
}
