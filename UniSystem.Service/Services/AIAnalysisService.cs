using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using UniSystem.Core.Models.ModelsForAIJob;
using UniSystem.Core.Services;

namespace UniSystem.Service.Services
{
    public class AIAnalysisService : IAIAnalysisService
    {
        private readonly string _connnectionstring = "";

        private readonly IMemoryCache _cache;
        public AIAnalysisService(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            _connnectionstring = configuration.GetConnectionString("SqlServer");
        }

        public async Task<List<TableSchema>> GetDatabaseSchemaAsync()
        {
            var tables = new List<TableSchema>();

            using (SqlConnection connection = new SqlConnection(_connnectionstring))
            {
                await connection.OpenAsync();
                string query = @"
                SELECT 
                    TABLE_NAME AS TableName, 
                    COLUMN_NAME AS ColumnName, 
                    DATA_TYPE AS DataType
                FROM INFORMATION_SCHEMA.COLUMNS";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string tableName = reader["TableName"].ToString();
                        string columnName = reader["ColumnName"].ToString();
                        string dataType = reader["DataType"].ToString();

                        var table = tables.Find(t => t.TableName == tableName);
                        if (table == null)
                        {
                            table = new TableSchema { TableName = tableName, Columns = new List<ColumnSchema>() };
                            tables.Add(table);
                        }

                        table.Columns.Add(new ColumnSchema { ColumnName = columnName, DataType = dataType });
                    }
                }
            }

            return tables;
        }


        public async Task<List<ForeignKeyRelation>> GetForeignKeyRelationsAsync()
        {
            var relations = new List<ForeignKeyRelation>();

            using (SqlConnection connection = new SqlConnection(_connnectionstring))
            {
                await connection.OpenAsync();
                string query = @"
                SELECT 
                    fk.name AS ForeignKeyName,
                    tp.name AS ParentTable,
                    cp.name AS ParentColumn,
                    tr.name AS ReferencedTable,
                    cr.name AS ReferencedColumn
                FROM sys.foreign_keys AS fk
                INNER JOIN sys.foreign_key_columns AS fkc ON fk.object_id = fkc.constraint_object_id
                INNER JOIN sys.tables AS tp ON fkc.parent_object_id = tp.object_id
                INNER JOIN sys.columns AS cp ON fkc.parent_object_id = cp.object_id AND fkc.parent_column_id = cp.column_id
                INNER JOIN sys.tables AS tr ON fkc.referenced_object_id = tr.object_id
                INNER JOIN sys.columns AS cr ON fkc.referenced_object_id = cr.object_id AND fkc.referenced_column_id = cr.column_id
                ORDER BY ParentTable;";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        relations.Add(new ForeignKeyRelation
                        {
                            ForeignKeyName = reader["ForeignKeyName"].ToString(),
                            ParentTable = reader["ParentTable"].ToString(),
                            ParentColumn = reader["ParentColumn"].ToString(),
                            ReferencedTable = reader["ReferencedTable"].ToString(),
                            ReferencedColumn = reader["ReferencedColumn"].ToString()
                        });
                    }
                }
            }

            return relations;
        }

        public string CleanSqlAnswer(string answer)
        {
            if (string.IsNullOrEmpty(answer))
                return string.Empty;


            var startIndex = answer.IndexOf("```sql") + 7;
            var endIndex = answer.LastIndexOf("```");

            if (startIndex > -1 && endIndex > startIndex)
            {

                return answer.Substring(startIndex, endIndex - startIndex).Trim();
            }


            return answer;
        }


        // cacheleme işlemini yabtik
        public async Task LoadSchemaAndTables()
        {
            if (!_cache.TryGetValue("DatabaseSchema", out _) && !_cache.TryGetValue("DatabaseRelationsInfo", out _))
            {
                var schemaInfo = await GetDatabaseSchemaAsync();
                var relationsInfo = await GetForeignKeyRelationsAsync();

                _cache.Set("DatabaseSchema", schemaInfo);
                _cache.Set("DatabaseRelationsInfo", relationsInfo);
            }
        }

    }


}



