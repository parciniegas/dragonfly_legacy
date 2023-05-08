using System;
using System.Data.SqlClient;

namespace Dragonfly.Core.ErrorMessages
{
    public class SqlErrorMessageProvider : IErrorMessageProvider
    {
        #region Private Fields
        private readonly string _connectionString;
        #endregion

        #region Constructors
        public SqlErrorMessageProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region public methods
        public IErrorMessage GetErrorMessage(string code, object parameters)
        {
            const string query = "select Id, Code, Message from ErrorMessages where Code = ?";
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    var errorMessage = cn.ExecuteEntity<ErrorMessage>(query, code);
                    errorMessage.Message.Inject(parameters);
                    return errorMessage;
                }
            }
            catch (Exception ex)
            {
                return new ErrorMessage { Id = 0, Code = "0xNA", Message = ex.Message };
            }
        }

        #endregion
    }
}
