using nClam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Extension.Configuration;

namespace DapperMVCLearning.VirusScanner
{
    public class ClamFileScanner : IFileScanner
    {
        //private readonly IConfiguration _configuration;

        //public ClamFileScanner(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        private readonly string _clamAVServerUrl;
        private readonly int _clamAVServerPort;

        //public FileScannerService(IConfiguration configuration)
        public ClamFileScanner(string clamAVServerUrl, int clamAVServerPort)
        {
            //_configuration = configuration;
            _clamAVServerUrl = clamAVServerUrl;
            _clamAVServerPort = clamAVServerPort;
        }

        public async Task<string> ScanFileAsync(byte[] fileBytes)
        {
            try
            {
                var clam = new ClamClient(_clamAVServerUrl, _clamAVServerPort);
                var scanResult = await clam.SendAndScanFileAsync(fileBytes);

                return scanResult.Result switch
                {
                    ClamScanResults.Clean => "Clean",
                    ClamScanResults.VirusDetected => "Virus Detected",
                    ClamScanResults.Error => "Error in File",
                    ClamScanResults.Unknown => "Unknown File",
                    _ => "No case available"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
