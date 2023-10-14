using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Elastic;

namespace Template.Application
{
    public interface IElasticService
    {
        Task<bool> Write(EsBulkRequest request);
        Task<bool> WriteBulk(List<object> request);
        Task<T> Ready<T>(string index, string id);
        Task<EsSearchResponce> Ready<T>(EsSearchRequest request);
    }
}
