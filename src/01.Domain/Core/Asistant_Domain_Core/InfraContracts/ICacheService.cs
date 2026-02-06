using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_Domain_Core.InfraContracts
{
    public interface ICacheService
    {
        void SetSliding<T>(string key, T data, int expiretionTime);
        void Set<T>(string key, T data, int expiretionTime);
        T Get<T>(string key);

        void Remove(string key);
    }
}
