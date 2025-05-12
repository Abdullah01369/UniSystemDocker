namespace WorkerService.StaticDoc.DocumentDb
{
    public class DocumentValues
    {
        public DocumentValues() { }

        public string unilogopng { get; set; } = @"C:\Users\PC\source\repos\UniSystem\WorkerService\Files\unilogo.png";
        public string ataturkpng { get; set; } = @"C:\Users\PC\source\repos\UniSystem\WorkerService\Files\atamlogo.png";
        public string css { get; set; } = @"
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Öğrenci Belgesi</title>
    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f2f2f2;
            padding: 30px;
            display: flex;
            justify-content: center;
        }
        .container {
            width: 800px;
            background-color: #ffffff;
            padding: 30px;
            border: 2px solid #333;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
            position: relative;
        }
        .header {
            text-align: center;
            margin-bottom: 10px;
            color: #333;
        }
        .header img {
            max-width: 80px;
            margin-bottom: 10px;
        }
        .header .university-info {
            font-size: 0.9em;
            color: #444;
            font-weight: bold;
            line-height: 1.4;
        }
        .title {
            text-align: center;
            font-size: 1.7em;
            font-weight: bold;
            color: #222;
            margin: 20px 0;
            text-decoration: underline;
        }
        .content {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            font-size: 1em;
            line-height: 1.6;
            margin-bottom: 20px;
        }
        .info {
            width: 48%;
        }
        .info-item {
            margin-bottom: 10px;
            font-size: 1em;
        }
        .info-item span {
            font-weight: bold;
            width: 160px;
            display: inline-block;
        }
        .qr {
            width: 48%;
            text-align: center;
            margin-top: 20px;
        }
        .qr img {
            width: 150px;
            height: 150px;
            display: block;
            margin: 0 auto;
            border: 1px solid #333;
            padding: 10px;
        }
        .note {
            font-size: 0.9em;
            margin-top: 20px;
            padding: 15px;
            background-color: #f9f9f9;
            border-left: 4px solid #800000;
            color: #666;
            font-style: italic;
        }
        .note a {
            color: #800000;
            text-decoration: none;
        }
        .note a:hover {
            text-decoration: underline;
        }
        .signature {
            text-align: right;
            margin-top: 30px;
        }
        .signature img {
            width: 120px;
            height: auto;
            display: block;
        }
        .footer {
            text-align: center;
            font-size: 0.85em;
            color: #555;
            margin-top: 30px;
            font-style: italic;
        }
    </style>
</head>
 
    

";

    }
}
