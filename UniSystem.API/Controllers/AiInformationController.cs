
using Dapper;
using GroqSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SharedLayer.Dtos;
using UniSystem.Core.Models.ModelsForAIJob;
using UniSystem.Core.Services;

namespace UniSystem.API.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class AiInformationController : CustomBaseController
    {
        private readonly IAIAnalysisService _aIAnalysisService;
        private readonly IMemoryCache _cache;
        private readonly IGroqClient _groqClient;
        private readonly IConfiguration _configuration;
        public AiInformationController(IAIAnalysisService aIAnalysisService, IMemoryCache memoryCache, IGroqClient groqClient, IConfiguration configuration)
        {
            _cache = memoryCache;
            _aIAnalysisService = aIAnalysisService;
            _groqClient = groqClient;
            _configuration = configuration;
        }



        [HttpPost("askgroq")]
        public async Task<IActionResult> GenerateSQL(string prompt)
        {
            string question = prompt;
            if (!_cache.TryGetValue("DatabaseSchema", out List<TableSchema> schema) ||
                !_cache.TryGetValue("DatabaseRelationsInfo", out List<ForeignKeyRelation> relations))
            {
                await _aIAnalysisService.LoadSchemaAndTables();
            }


            schema = _cache.Get<List<TableSchema>>("DatabaseSchema");
            relations = _cache.Get<List<ForeignKeyRelation>>("DatabaseRelationsInfo");

            string schemaJsonCompact = JsonConvert.SerializeObject(schema, Formatting.None);
            string relationsJsonCompact = JsonConvert.SerializeObject(relations, Formatting.None);


            string aiPrompt = $"Generate ONLY an SQL query based on the database schema and relationships provided below. Do not include any explanation or text, only the SQL query. " +
                $"If the question is irrelevant or cannot be answered with a valid SQL query, respond with 'I cannot generate a valid SQL query for this question.' " +
                $"My database name is: unisystem3, use this information if needed:\n\n{schemaJsonCompact}\n\n{relationsJsonCompact}\n\nQuestion: {question}";



            string aiResponse = await _groqClient.CreateChatCompletionAsync(new GroqSharp.Models.Message { Content = aiPrompt });

            var val = _aIAnalysisService.CleanSqlAnswer(aiResponse);

            IEnumerable<dynamic> sqlresponse;
            string connectionString = _configuration.GetConnectionString("SqlServer");
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = val;
                sqlresponse = connection.Query<dynamic>(sql).ToList();


            }

            var filteredResponse = sqlresponse.Select(item =>
            {

                var dict = item as IDictionary<string, object>;
                if (dict != null)
                {

                    dict.Remove("PasswordHash");
                    dict.Remove("PhotoBase64Text");
                    return dict;
                }

                var json = JsonConvert.SerializeObject(item);
                var parsedDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);


                if (parsedDict.ContainsKey("PhotoBase64Text") || parsedDict.ContainsKey("PasswordHash"))
                {
                    parsedDict.Remove("PhotoBase64Text");
                }

                return parsedDict;
            }).ToList();

            string sqlResponseJson = JsonConvert.SerializeObject(filteredResponse, Formatting.Indented);

            string aiPromptForAnalysis = $"Kullanıcım veritanımla ilgli bir soru soruyor, Sana kullanıcnın sorduğu soruyu ve bu soruya gelen cevabı vereceğim,cevap benim veritabanımdan gelen sonuçtan oluşuyor, cevabı soruya göre analiz et ve yaz, hiçbir veriyi atlama tamamını yaz, güzel bir yazı görünümünde bana yaz. Soru metnim bu : {prompt}, bu ise sorunun cevabı : {sqlResponseJson}";

            //     string aiPromptForAnalysis = $"Kullanıcının sorusuna göre SQL sorgusunun cevabını analiz et. Veriyi aşağıdaki gibi değerlendir: " +
            //$"\"{prompt}\" sorusuna gelen cevabı incelerken, verinin neyi ifade ettiğini, önemli metrikleri (toplam kayıt sayısı, ortalamalar, en yüksek ve en düşük değerler gibi), " +
            //"ve varsa dikkat edilmesi gereken herhangi bir hususu belirt. Ayrıca, verideki hassas bilgileri (şifreler, TC kimlik numaraları vs.) analiz et, ama bunların içeriğine girmeden sadece uyarı ver. " +
            //$"Verinin anlaşılırlığını bozmadan, gelen cevabı sade ve açık bir şekilde özetle. SQL Sorgusu Cevabı:\n{sqlResponseJson}\n\n" +
            //"Bu analizi net ve odaklanmış şekilde yaz, gereksiz şeylere yer verme.";



            string aiResponseforreturn = await _groqClient.CreateChatCompletionAsync(new GroqSharp.Models.Message { Content = aiPromptForAnalysis });
            var response = Response<string>.Success(aiResponseforreturn, 200);

            return ActionResultInstance(response);


        }



    }
}
