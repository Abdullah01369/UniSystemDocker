using UniSystem.Core.Models.ModelsForAIJob;

namespace UniSystem.Core.Services
{
    public interface IAIAnalysisService
    {
        Task<List<TableSchema>> GetDatabaseSchemaAsync();
        Task<List<ForeignKeyRelation>> GetForeignKeyRelationsAsync();
        Task LoadSchemaAndTables();
        string CleanSqlAnswer(string answer);

    }
}




