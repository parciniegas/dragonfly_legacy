using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Dragonfly.Core
{
    public static class DbCommandExtensions
    {
        #region Private Methods
        private static DbType GetParameterType(Type type)
        {
            if (type == typeof(bool))
                return DbType.Boolean;

            if (type == typeof(byte))
                return DbType.Byte;

            if (type == typeof(DateTime))
                return DbType.DateTime;

            if (type == typeof(DateTimeOffset))
                return DbType.DateTimeOffset;

            if (type == typeof(decimal))
                return DbType.Decimal;

            if (type == typeof(double))
                return DbType.Double;

            if (type == typeof(float))
                return DbType.Single;

            if (type == typeof(Guid))
                return DbType.Guid;

            if (type == typeof(short))
                return DbType.Int16;

            if (type == typeof(int))
                return DbType.Int32;

            if (type == typeof(long))
                return DbType.Int64;

            return DbType.String;
        }

        private static void AddParametersFrom(this DbCommand @this, object parameters)
        {
            if (parameters.IsNull())
                return;

            parameters.GetProperties().ForEach(p =>
            {
                var parameter = @this.CreateParameter();
                parameter.ParameterName = p.Name;
                parameter.Value = parameters.GetPropertyValue(p.Name); 
                parameter.DbType = GetParameterType(parameter.Value.GetType());

                @this.Parameters.Add(parameter);
            });
        }
        #endregion

        #region ExecuteEntities
        public static T ExecuteEntity<T>(this DbCommand @this, object parameters) where T : new()
        {
            @this.AddParametersFrom(parameters);
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                using (IDataReader reader = @this.ExecuteReader())
                {
                    reader.Read();
                    return reader.ToEntity<T>();
                }
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static T ExecuteEntity<T>(this DbCommand @this) where T : new()
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                using (IDataReader reader = @this.ExecuteReader())
                {
                    reader.Read();
                    return reader.ToEntity<T>();
                }
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static IEnumerable<T> ExecuteEntities<T>(this DbCommand @this, object parameters) where T : new()
        {
            @this.AddParametersFrom(parameters);
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                using (IDataReader reader = @this.ExecuteReader())
                {
                    return reader.ToEntities<T>();
                }
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static IEnumerable<T> ExecuteEntities<T>(this DbCommand @this) where T : new()
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                using (IDataReader reader = @this.ExecuteReader())
                {
                    return reader.ToEntities<T>();
                }
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }
        #endregion

        #region ExecuteDynamics
        public static dynamic ExecuteExpandoObject(this DbCommand @this)
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                using (IDataReader reader = @this.ExecuteReader())
                {
                    reader.Read();
                    return reader.ToExpandoObject();
                }
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static dynamic ExecuteExpandoObject(this DbCommand @this, object parameters)
        {
            @this.AddParametersFrom(parameters);
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                using (IDataReader reader = @this.ExecuteReader())
                {
                    reader.Read();
                    return reader.ToExpandoObject();
                }
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static IEnumerable<dynamic> ExecuteDynamics(this DbCommand @this)
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                using (IDataReader reader = @this.ExecuteReader())
                {
                    return reader.ToExpandoObjects();
                }
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static IEnumerable<dynamic> ExecuteDynamics(this DbCommand @this, object parameters)
        {
            @this.AddParametersFrom(parameters);
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                using (IDataReader reader = @this.ExecuteReader())
                {
                    return reader.ToExpandoObjects();
                }
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }
        #endregion

        #region ExecuteScalar
        public static T ExecuteScalarAs<T>(this DbCommand @this)
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                return (T)@this.ExecuteScalar();
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static object ExecuteScalar(this DbCommand @this, object parameters)
        {
            @this.AddParametersFrom(parameters);
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                return @this.ExecuteScalar();
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static T ExecuteScalarAsOrDefault<T>(this DbCommand @this)
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                return (T)@this.ExecuteScalar();
            }
            catch (Exception)
            {
                return default(T);
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static T ExecuteScalarAsOrDefault<T>(this DbCommand @this, T defaultValue)
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                return (T)@this.ExecuteScalar();
            }
            catch (Exception)
            {
                return defaultValue;
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static T ExecuteScalarAsOrDefault<T>(this DbCommand @this, Func<DbCommand, T> defaultValueFactory)
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                return (T)@this.ExecuteScalar();
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static T ExecuteScalarTo<T>(this DbCommand @this)
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                return @this.ExecuteScalar().To<T>();
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static T ExecuteScalarToOrDefault<T>(this DbCommand @this)
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                return @this.ExecuteScalar().To<T>();
            }
            catch (Exception)
            {
                return default(T);
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static T ExecuteScalarToOrDefault<T>(this DbCommand @this, T defaultValue)
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                return @this.ExecuteScalar().To<T>();
            }
            catch (Exception)
            {
                return defaultValue;
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }

        public static T ExecuteScalarToOrDefault<T>(this DbCommand @this, Func<DbCommand, T> defaultValueFactory)
        {
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                return @this.ExecuteScalar().To<T>();
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }
        #endregion

        #region ExecuteNonQuery
        public static void ExecuteNonQuery(this DbCommand @this, object parameters)
        {
            @this.AddParametersFrom(parameters);
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                @this.ExecuteNonQuery();
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }
        #endregion

        #region ExecuteReader
        public static DbDataReader ExecuteReader(this DbCommand @this, object parameters)
        {
            @this.AddParametersFrom(parameters);
            var opened = @this.Connection.State == ConnectionState.Open;
            try
            {
                if (!opened)
                    @this.Connection.Open();

                return @this.ExecuteReader();
            }
            finally
            {
                if (!opened)
                    @this.Connection.Close();
            }
        }
        #endregion
    }
}
