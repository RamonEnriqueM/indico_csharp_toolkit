using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Indico;
using Newtonsoft.Json.Linq;
using System.Text;
using iText.Kernel.Pdf;

namespace classifier
{

    public class AutoClassifier
    {
        public static void get_path_files()
        {
            string[] files = Directory.GetFiles(@"//Users/rammjam/Desktop/Prueba", "*.*");
            List<string> tfiles = new List<string>();
            foreach (var file in files)
            {
                if (AutoClassifier.CheckExtension(file))
                {
                    tfiles.Add(file);
                }
            }
            var size = tfiles.Count;
            DocExtraction.doextration(tfiles);
        }

        public static bool CheckExtension(string path)
        {
            string filename = path;
            string ext;
            List<string> accepted_types = new List<string>() { ".pdf", ".tiff", ".tif", ".doc", ".docx" };

            ext = Path.GetExtension(filename);
            if (accepted_types.Contains(ext))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    public class DocExtraction
    {
        public static async Task doextration(List<string> tfiles)
        {
            if (tfiles.Count == 0)
            {
                Console.WriteLine("Please provide the filepath of a PDF to OCR");
                Environment.Exit(0);
            }

            var config = new IndicoConfig(
                host: "app.indico.io"
            );
            var client = new IndicoClient(config);

            var extractConfig = new JObject()
        {
            { "preset_config", "standard" }
        };
            var ocrQuery = client.DocumentExtraction(extractConfig);
            var job = await ocrQuery.Exec(tfiles[0]);

            var result = await job.Result();
            string url = (string)result.GetValue("url");
            var blob = await client.RetrieveBlob(url).Exec();
            Console.WriteLine(blob.AsJSONObject());
        }
    }
    public class To_CSV
    {
        public static void Dt(string result, string url, string blob)
        {
            string path = @"//Users/rammjam/Desktop/Prueba";
            string space = ",";
            StringBuilder output = new StringBuilder();

            String cont = result + "," + url + "," + blob;
            List<string> inside = new List<string>();
            inside.Add(cont);

            for(int i = 0; i< inside.Count; i++)
            {
                output.AppendLine(string.Join(space, inside[1]));
                File.AppendAllText(path, output.ToString());
            }
        }
    }
    public class AutoClassFirstPageClassifier
    {
        public static void set_page_clases()
        {
            string[] files = Directory.GetFiles(@"//Users/rammjam/Desktop/Prueba", "*.*");
            List<string> tfiles = new List<string>();
            foreach (var file in files)
            {
                if (AutoClassifier.CheckExtension(file))
                {
                    tfiles.Add(file);
                }
            }
            var size = tfiles.Count;
            List<string> fplist = new List<string>();
            foreach (var ffile in tfiles)
            {
                var pdfDocument = new PdfDocument(new PdfReader(ffile));
                int npages = pdfDocument.GetNumberOfPages();
                for (int ind = 1; 1 <= pdfDocument.GetNumberOfPages(); ++ind)
                {
                    if (npages == 1)
                    {
                        fplist.Add("Fist Page");
                    }
                    else
                    {
                        fplist.Add("No fist Page");
                    }
                }
            }
            DocExtraction.doextration(tfiles);
        }

        public static bool CheckExtension(string path)
        {
            string filename = path;
            string ext;
            List<string> accepted_types = new List<string>() { ".pdf", ".tiff", ".tif", ".doc", ".docx" };

            ext = Path.GetExtension(filename);
            if (accepted_types.Contains(ext))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    public class FistDocExtraction
    {
        public static async Task doextration(List<string> tfiles)
        {
            if (tfiles.Count == 0)
            {
                Console.WriteLine("Please provide the filepath of a PDF to OCR");
                Environment.Exit(0);
            }

            var config = new IndicoConfig(
                host: "app.indico.io"
            );
            var client = new IndicoClient(config);

            var extractConfig = new JObject()
        {
            { "preset_config", "standard" }
        };
            var ocrQuery = client.DocumentExtraction(extractConfig);
            var job = await ocrQuery.Exec(tfiles[0]);

            var result = await job.Result();
            string url = (string)result.GetValue("url");
            var blob = await client.RetrieveBlob(url).Exec();
            Console.WriteLine(blob.AsJSONObject());
        }
    }
    public class To_CSV_First
    {
        public static void Dt(string result, string url, string blob)
        {
            string path = @"//Users/rammjam/Desktop/Prueba";
            string space = ",";
            StringBuilder output = new StringBuilder();

            String cont = result + "," + url + "," + blob;
            List<string> inside = new List<string>();
            inside.Add(cont);

            for (int i = 0; i < inside.Count; i++)
            {
                output.AppendLine(string.Join(space, inside[1]));
                File.AppendAllText(path, output.ToString());
            }
        }
    }
}