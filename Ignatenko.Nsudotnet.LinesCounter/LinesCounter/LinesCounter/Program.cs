using System;
using System.IO;

namespace Ignatenko.Nsudotnet.LinesCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Usage:\n LinesCounter.exe *.<file_extension>");
                Console.ReadLine();
            }
            else
            {
                string extension = args[1];
                string directory = Directory.GetCurrentDirectory();
                string[] fileNames = Directory.GetFiles(directory, extension, SearchOption.AllDirectories);

                int lineCount = 0;
                foreach (var fileName in fileNames)
                {
                    using (var streamReader = new StreamReader(fileName))
                    {
                        string line;
                        bool readingMultipleLineComment = false;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            bool usefulLine = false;
                            int singleLineCommentStartIdx = line.IndexOf("//");
                            if (singleLineCommentStartIdx != -1)
                            {
                                line = line.Substring(0, singleLineCommentStartIdx);
                            }
                            line = line.Trim();

                            while (!String.IsNullOrEmpty(line))
                            {
                                if (!readingMultipleLineComment)
                                {
                                    int multipleLineCommentStartIdx = line.IndexOf("/*");
                                    if (multipleLineCommentStartIdx != 0)
                                    {
                                        usefulLine = true;
                                    }

                                    if (multipleLineCommentStartIdx != -1)
                                    {
                                        readingMultipleLineComment = true;
                                        line = line.Substring(multipleLineCommentStartIdx + 2);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    int multipleLineCommentEndIdx = line.IndexOf("*/");
                                    if (multipleLineCommentEndIdx != -1)
                                    {
                                        readingMultipleLineComment = false;
                                        line = line.Substring(multipleLineCommentEndIdx + 2);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                line = line.Trim();
                            }

                            if (usefulLine)
                            {
                                ++lineCount;
                            }
                        }
                    }
                }
                Console.WriteLine("Count of lines: {0}", lineCount);
                Console.ReadLine();
            }
        }
    }
}
