namespace DapperMVCLearning.Data.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "SqlServerConnection");
        Task SaveData<T>(string spName, T parameters, string connectionId = "SqlServerConnection");
    }
}