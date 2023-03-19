using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;

Dictionary<string, string[]> WrongWords = new Dictionary<string, string[]>
{
    {"привет", new [] {"првиет"}},
    {"hello", new [] {"helo"}},
    {"нет", new [] {"нэт"}},
    {"да", new [] {"до"}},
    {"спасибо", new [] {"спосибо"}},
    {"хорошо", new [] {"хорошо", "хароша", "хорохо", "харохо"}},
    {"сегодня", new [] {"седня", "сегодне", "седне"}},
    {"завтра", new [] {"зафтра", "завтро", "зафтро", "зафра", "завра"}},
    {"вчера", new [] {"вчра", "вчора", "вчоро", "вчераа", "вчраа"}},
    {"работа", new [] {"рабоta", "рабта", "рабода", "работо", "работя"}},
    {"компьютер", new [] {"компютер", "компьютр", "компутер", "компутр"}},
    {"интернет", new [] {"интернэт", "инетрнет", "интерент", "интернета", "интернету"}},
    {"телефон", new [] {"телефон", "телевон", "телефан", "телефона", "телефону"}},
    {"автомобиль", new [] {"автомобил", "автомобильчик", "автомобилечик"}},
    {"книга", new [] {"книжка", "кнега", "кнжка", "кнжга", "кнегу"}},
    {"музыка", new [] {"музыку", "музычка", "музычку", "музика", "музику"}},
    {"видеоигра", new [] {"видео-игра", "видео игра", "видеоигр", "видеоигры"}},
    {"фильм", new [] {"филм", "фильма", "филма", "фильму"}},
    {"солнце", new [] {"солце", "слонце", "слце", "солнцу", "солцу"}}
};

void FixMistakesInFiles(string DirectoryPath)
{
    string[] Files = Directory.GetFiles(DirectoryPath, "*.txt");

    foreach (string File in Files)
    {
        string[] Lines = System.IO.File.ReadAllLines(File);

        for (int LineIndex = 0; LineIndex < Lines.Length; LineIndex++)
        {
            foreach (KeyValuePair<string, string[]> Entry in WrongWords)
            {
                foreach (string WrongWord in Entry.Value)
                {
                    Lines[LineIndex] = Regex.Replace(Lines[LineIndex], @"\b" + WrongWord + @"\b", Entry.Key);
                }
            }
        }

        System.IO.File.WriteAllLines(File, Lines);
    }
}


void ReplacePhoneNumbers(string DirectoryPath)
{
    string[] Files = Directory.GetFiles(DirectoryPath, "*.txt");

    foreach (string File in Files)
    {
        string[] Lines = System.IO.File.ReadAllLines(File);

        for (int LineIndex = 0; LineIndex < Lines.Length; LineIndex++)
        {
            Lines[LineIndex] = Regex.Replace(Lines[LineIndex], @"\((\d{3})\)\s*(\d{3})-(\d{2})-(\d{2})", "+380 $1 $2 $3 $4");
        }

        System.IO.File.WriteAllLines(File, Lines);
    }
}

void FixFiles(string DirectoryPath)
{
    FixMistakesInFiles(DirectoryPath);
    ReplacePhoneNumbers(DirectoryPath);
}

string DirectoryPath = Path.Combine("/", "Users", "pr1nc3", "Projects", "fifth_lab", "fifth_lab");

FixFiles(DirectoryPath);
