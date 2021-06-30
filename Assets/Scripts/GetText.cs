using System.IO;


namespace ConsoleApp3
{
    static class GetText
    {
        static public string GetTextAbout(string name, string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {
                    var line = sr.ReadLine();
                    var data = line.Split(';');
                    if (data[0] == name)
                    {
                        return data[1];
                    }

                }
                return "Нет информации...";
            }
        }
    }
}
