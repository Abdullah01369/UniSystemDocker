namespace UniSystem.Core.Models.ModelsForAIJob
{
    public class TableSchema
    {
        public string TableName { get; set; }
        public List<ColumnSchema> Columns { get; set; }
    }
}
