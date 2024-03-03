using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperMVCLearning.VirusScanner
{
    public interface IFileScanner
    {
        Task<string> ScanFileAsync(byte[] fileBytes);
    }
}
