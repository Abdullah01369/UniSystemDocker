using DinkToPdf;
using DinkToPdf.Contracts;
using System.Runtime.InteropServices;
using WorkerService.ModelDto;
using WorkerService.StaticDoc.DocumentDb;

namespace WorkerService.Services
{
    public class PdfCreatorService
    {
        private readonly IConverter _converter;

        private readonly UserService _userService;

        public PdfCreatorService()
        {
        }

        public PdfCreatorService(UserService userService)
        {
            _userService = userService;
            try
            {
                // Platforma uygun DLL dosyasını belirle
                string dllFileName;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    dllFileName = "libwkhtmltox.dll";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    dllFileName = "libwkhtmltox.so";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    dllFileName = "libwkhtmltox.dylib";
                }
                else
                {
                    throw new PlatformNotSupportedException("Bu işletim sistemi desteklenmiyor.");
                }

                var dllPath = Path.Combine(AppContext.BaseDirectory, "lib", dllFileName);

                if (!File.Exists(dllPath))
                    throw new FileNotFoundException($"DLL bulunamadı: {dllPath}");

                var context = new CustomAssemblyLoadContext();
                context.LoadUnmanagedLibrary(dllPath);

                _converter = new SynchronizedConverter(new PdfTools());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HATA] PDF kütüphanesi yüklenemedi: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        public async Task ConvertToPdf(string outputPath, string No)
        {
            var model = await _userService.GetUserInfoForStudentDoc(No);
            DocumentValues documentValues = new DocumentValues();


            string unilogopng = documentValues.unilogopng;
            string ataturkpng = documentValues.ataturkpng;

            string base64Stringunilogo = Convert.ToBase64String(File.ReadAllBytes(unilogopng));
            string base64Stringataturk = Convert.ToBase64String(File.ReadAllBytes(ataturkpng));

            string css = documentValues.css;

            string _htmlContent = $@"

 <!DOCTYPE html>
<html lang=""tr"">

<head>
    <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"" rel=""stylesheet"">
</head>

{css}
<body>
    <div class=""container"">
        <!-- Header Bölümü -->
        <div class=""header"">
           


  <div style=""text-align: center; white-space: nowrap;"">
            <!-- Sol Resim -->
            <div style=""display: inline-block; vertical-align: middle; margin-right: 10px;"">
                <img src=""data:image/png;base64,{base64Stringataturk}"" alt=""Atatürk Logosu"" style=""width: 50px; height: auto;"">
            </div>
            <!-- Orta Resim -->
            <div style=""display: inline-block; vertical-align: middle; margin-right: 10px;"">
                <img src=""data:image/png;base64,{base64Stringunilogo}"" alt=""Üniversite Logosu"" style=""width: 60; height: auto;"">
            </div>
            <!-- Sağ Resim -->
            <div style=""display: inline-block; vertical-align: middle;"">
                <img src=""data:image/png;base64,{model.Photo}"" alt=""Öğrenci Görseli"" style=""width: 50px; height: auto;"">
            </div>
        </div>


 

            <div class=""text-center mt-3 university-info"">
                <p>T.C.</p>
                <p>ÜNİVERSİTE</p>
                <p>ÖĞRENCİ İŞLERİ DAİRE BAŞKANLIĞI</p>
                <p>1919 TÜRKİYE</p>
            </div>
        </div>

        <!-- Belge Başlığı -->
        <div class=""text-center my-4 title"">
            <h3>ÖĞRENCİ BELGESİ</h3>
        </div>

        <!-- İçerik Bölümü -->
        <div class=""content row"">
            <!-- Sol Bilgi Alanı -->
            <div class=""col-md-6"">
                <div class=""info-item""><strong>Adı Soyadı:</strong> {model.Name} {model.Surname}</div>
                <div class=""info-item""><strong>Baba Adı:</strong> {model.FathersName}</div>
                <div class=""info-item""><strong>Anne Adı:</strong> {model.MothersName}</div>
                <div class=""info-item""><strong>Doğum Yeri / Tarihi:</strong> {model.BornPlace} / {model.BornDate}</div>
                <div class=""info-item""><strong>T.C. Kimlik No:</strong> {model.TC}</div>
                <div class=""info-item""><strong>Öğrenci No:</strong> {model.No}</div>
                <div class=""info-item""><strong>Kayıt Tarihi:</strong> {model.RecordDate}</div>
                <div class=""info-item""><strong>Düzenleme Tarihi:</strong> {model.TakingDate}</div>
            </div>
            <!-- Sağ Bilgi Alanı -->
            <div class=""col-md-6"">
                <div class=""info-item""><strong>Sınıf:</strong> 4</div>
                <div class=""info-item""><strong>Fakülte:</strong> {model.Faculty}</div>
                <div class=""info-item""><strong>Bölüm:</strong> {model.Department}</div>
                <div class=""info-item""><strong>Öğretim Yılı:</strong> düzenlenecek</div>
                <div class=""info-item""><strong>Dönem:</strong> düzenlenecek</div>
                <div class=""info-item""><strong>Kayıt Şekli:</strong> ÖSYS</div>
                <div class=""info-item""><strong>Sene:</strong> düzenlenecek</div>
            </div>
        </div>

        <!-- QR Kod Alanı -->
        <div class=""text-center my-4"">
            <img src=""qr-placeholder.png"" alt=""Karekod"" style=""max-width: 150px;"">
            <p>Doğrulama Kodu: 4945D7EA</p>
            <p>Pin Kodu: 7643</p>
        </div>

        <!-- Not Bölümü -->
        <div class=""note my-4"">
            <p>Bu öğrenci belgesinde ıslak imza ve mühür yerine görseller kullanılmıştır.</p>
            <p>Öğrenci belgesinin doğruluğunu kontrol edebilmek için: <a href=""https://bd.erciyes.edu.tr"" target=""_blank"">https://bd.erciyes.edu.tr</a> veya mobil cihazınıza yükleyeceğiniz karekod uygulamasını kullanarak yan taraftaki karekodu okutabilirsiniz.</p>
            <p><strong>NOT:</strong> Bu belge askerlik tecil işlemlerinde kullanılmaz.</p>
        </div>

        <!-- İmza Alanı -->
        <div class=""text-center signature my-4"">
            <p>Öğrenci İşleri Daire Başkanlığı</p>
            <img src=""signature-placeholder.png"" alt=""İmza"" style=""max-width: 150px;"">
        </div>

        <!-- Belge Sonu Tarih Bilgisi -->
        <div class=""text-end footer mt-4"">
            <p>{DateTime.Now.ToShortDateString()}</p>
        </div>
    </div>
</body>

</html>


                ";

            try
            {

                if (string.IsNullOrWhiteSpace(outputPath) || Path.GetInvalidPathChars().Any(outputPath.Contains))
                {
                    throw new ArgumentException("Geçersiz dosya yolu: " + outputPath);
                }


                string directoryPath = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }


                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = new GlobalSettings
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Out = outputPath
                    },
                    Objects = { new ObjectSettings { HtmlContent = _htmlContent } }
                };

                _converter.Convert(doc);

                DocumentSavingDto documentSavingDto = new DocumentSavingDto
                {
                    StudentNo = No,
                    DocumentType = "0",
                    FilePath = outputPath

                };
                await _userService.SaveDocInfo(documentSavingDto);
                Console.WriteLine($"PDF başarıyla oluşturuldu: {outputPath}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"PDF oluşturma hatası: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }



        }
    }

    public class CustomAssemblyLoadContext : System.Runtime.Loader.AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            if (!File.Exists(absolutePath))
            {
                throw new FileNotFoundException($"DLL bulunamadı: {absolutePath}");
            }

            try
            {
                return LoadUnmanagedDllFromPath(absolutePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DLL yükleme hatası: {ex.Message}");
                throw;
            }
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            return IntPtr.Zero;
        }
    }
}







