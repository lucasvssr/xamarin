using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TP5_Reseau.ViewModel
{
    public interface INetworkStatus
    {
        Task<bool> CanAccess();
        bool IsAccessing { get; set; }
    }
}
