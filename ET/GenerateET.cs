using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        string sqlPath = Path.Combine(currentDir, "Database_DaiNam.sql");
        
        if (!File.Exists(sqlPath)) {
            string parentDir = Directory.GetParent(currentDir.TrimEnd(Path.DirectorySeparatorChar)).FullName;
            sqlPath = Path.Combine(parentDir, "Database_DaiNam.sql");
            if (File.Exists(sqlPath)) currentDir = parentDir;
        }

        if (!File.Exists(sqlPath)) {
            Console.WriteLine("Khong tim thay file SQL tai: " + sqlPath);
            return;
        }

        string sqlText = File.ReadAllText(sqlPath);
        string outDir = Path.Combine(currentDir, "ET");
        if (!Directory.Exists(outDir)) Directory.CreateDirectory(outDir);

        var tableMatches = Regex.Matches(sqlText, @"CREATE TABLE\s+([A-Za-z0-9_]+)\s*\((.*?)\)\s*;", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        
        foreach (Match match in tableMatches)
        {
            string tableName = match.Groups[1].Value.Trim();
            string columnsBlock = match.Groups[2].Value;
            
            string classContent = "using System;\n\nnamespace ET\n{\n";
            classContent += "    public partial class ET_" + tableName + "\n    {\n";

            var lines = columnsBlock.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                string trimLine = line.Trim();
                if (string.IsNullOrEmpty(trimLine) || trimLine.StartsWith("--")) continue;
                
                var parts = trimLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue;

                string colName = parts[0].Replace("[", "").Replace("]", "").Replace(",", "");
                if (new[] { "CONSTRAINT", "PRIMARY", "FOREIGN", "CHECK", "UNIQUE" }.Contains(colName.ToUpper())) continue;

                string colTypeRaw = parts[1].ToUpper().Replace(",", "");
                string colType = colTypeRaw.Split('(')[0];
                
                string csharpType = GetCSharpType(colType);
                
                bool isNotNull = trimLine.ToUpper().Contains("NOT NULL") || trimLine.ToUpper().Contains("PRIMARY KEY");
                bool isNullable = !isNotNull;

                if (isNullable && csharpType != "string" && csharpType != "byte[]")
                {
                    csharpType += "?";
                }

                classContent += string.Format("        public {0} {1} {{ get; set; }}\n", csharpType, colName);
            }

            classContent += "    }\n}\n";
            File.WriteAllText(Path.Combine(outDir, "ET_" + tableName + ".cs"), classContent);
        }
    }

    static string GetCSharpType(string sqlType)
    {
        switch (sqlType)
        {
            case "INT": return "int";
            case "BIGINT": return "long";
            case "SMALLINT": return "short";
            case "TINYINT": return "byte";
            case "DECIMAL":
            case "NUMERIC":
            case "MONEY":
            case "SMALLMONEY": return "decimal";
            case "FLOAT": return "double";
            case "REAL": return "float";
            case "DATETIME":
            case "DATETIME2":
            case "DATE":
            case "SMALLDATETIME": return "DateTime";
            case "TIME": return "TimeSpan";
            case "BIT": return "bool";
            case "UNIQUEIDENTIFIER": return "Guid";
            case "ROWVERSION":
            case "TIMESTAMP": return "byte[]";
            default: return "string";
        }
    }
}
