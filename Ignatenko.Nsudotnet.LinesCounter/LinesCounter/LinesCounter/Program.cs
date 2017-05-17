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
                            int roiStartIdx = 0;      // Roi - region of interest
                            int roiEndIdx = line.Length;

                            int singleLineCommentStartIdx = line.IndexOf("//", roiStartIdx, roiEndIdx - roiStartIdx);
                            if (singleLineCommentStartIdx != -1)
                            {
                                roiEndIdx = singleLineCommentStartIdx;
                            }

                            while ((roiEndIdx - roiStartIdx) != 0)
                            {
                                if (Char.IsWhiteSpace(line[roiStartIdx]))
                                {
                                    ++roiStartIdx;
                                    continue;
                                }

                                if (!readingMultipleLineComment)
                                {
                                    int multipleLineCommentStartIdx = line.IndexOf("/*", roiStartIdx, roiEndIdx - roiStartIdx);
                                    if (multipleLineCommentStartIdx != 0)
                                    {
                                        usefulLine = true;
                                    }

                                    if (multipleLineCommentStartIdx != -1)
                                    {
                                        readingMultipleLineComment = true;
                                        roiStartIdx = multipleLineCommentStartIdx + 2;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    int multipleLineCommentEndIdx = line.IndexOf("*/", roiStartIdx, roiEndIdx - roiStartIdx);
                                    if (multipleLineCommentEndIdx != -1)
                                    {
                                        readingMultipleLineComment = false;
                                        roiStartIdx = multipleLineCommentEndIdx + 2;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
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
