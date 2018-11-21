using System;

namespace Yezz.Database.Interfaces
{
    public interface IQueryAdapter : IRegularQueryAdapter, IDisposable
    {
        long InsertQuery();
        void RunQuery();
    }
}